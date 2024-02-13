using AutorizacionDePagos.Api.Application.Authorization;
using AutorizacionDePagos.Api.Application.Services.ApprovedAuthorization;
using AutorizacionDePagos.Api.Application.Services.ProcessorPayment;
using AutorizacionDePagos.Api.Domain;
using AutorizacionDePagos.Api.Interfaces;
using MediatR;
using System.Net;

namespace AutorizacionDePagos.Api.Application.Services.Authorization
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IRepository<Autorizacion> autorizacionRepository;
        private readonly IRepository<Cliente> clienteRepository;
        private readonly IApprovedAuthorizationService approvedAuthorizationService;
        private readonly ITipoAutorizacionRepository tipoAutorizacionRepository;
        private readonly IEstadoRepository estadoRepository;
        private readonly IProcessorPaymentService processorPayment;

        public AuthorizationService(
            IUnitOfWork unitOfWork,
            IRepository<Autorizacion> autorizacionRepository,
            IRepository<Cliente> clienteRepository,
            IApprovedAuthorizationService approvedAuthorizationService,
            ITipoAutorizacionRepository tipoAutorizacionRepository,
            IEstadoRepository estadoRepository,
            IProcessorPaymentService processorPayment
            )
        {
            this.unitOfWork = unitOfWork;
            this.autorizacionRepository = autorizacionRepository;
            this.clienteRepository = clienteRepository;
            this.approvedAuthorizationService = approvedAuthorizationService;
            this.tipoAutorizacionRepository = tipoAutorizacionRepository;
            this.estadoRepository = estadoRepository;
            this.processorPayment = processorPayment;
        }

        public async Task<Autorizacion> Create(CreateAuthorizationRequest request)
        {
            try
            {

                var customer = await clienteRepository.GetByIdAsync(request.ClienteId);

                if (customer is null)
                    throw new ArgumentNullException("Cliente no encontrado");

                var paymentProcessed = await processorPayment.PaymentProcessor(new ProcessorRequestDto() { NombreCliente = customer.Nombre, Monto = request.Monto });

                if (paymentProcessed is null)
                    throw new ArgumentNullException("ProcessorResponseDto, no se pudo procesar el Monto");

                var authorization = new Autorizacion()
                {
                    CodigoOperacion = request.CodigoOperacion,
                    NombreCliente = customer.Nombre
                };

                var authorizationType = await tipoAutorizacionRepository.GetAuthorizationTypeByName(request.TipoAutorizacion);
                authorization.TipoAutorizacionId = authorizationType.Id;

                if (!paymentProcessed.Aprobado)
                {
                    await DeniedAuthorizationWorkFlow(authorization, customer, paymentProcessed);
                }
                else if (customer.TipoCliente == TipoClienteEnum.PRIMERO)
                {
                    await ApprovedAuthorizationWorkFlow(authorization, customer, paymentProcessed);
                }
                else if (customer.TipoCliente == TipoClienteEnum.SEGUNDO)
                {
                    await SecondCustomerTypeWorkFlow(authorization, customer, paymentProcessed);
                }

                await autorizacionRepository.AddAsync(authorization);
                await unitOfWork.CommitAsync();

                return authorization;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private async Task DeniedAuthorizationWorkFlow(Autorizacion authorization, Cliente customer, ProcessorResponseDto processorResponse)
        {
            var state = await estadoRepository.GetStateByName(EstadoEnum.DENEGADO);
            authorization.EstadoAutorizacionId = state.Id;
            authorization.MontoDenegado = processorResponse.MontoDenegado;
        }

        private async Task ApprovedAuthorizationWorkFlow(Autorizacion authorization, Cliente customer, ProcessorResponseDto processorResponse)
        {
            var state = await estadoRepository.GetStateByName(EstadoEnum.APROBADO);
            authorization.EstadoAutorizacionId = state.Id;
            authorization.Monto = processorResponse.Monto;

            await approvedAuthorizationService.ApprovedAuthorizationPublish(authorization);
        }

        private async Task SecondCustomerTypeWorkFlow(Autorizacion authorization, Cliente customer, ProcessorResponseDto processorResponse)
        {
            var state = await estadoRepository.GetStateByName(EstadoEnum.CONFIRMAR);
            authorization.EstadoAutorizacionId = state.Id;
            authorization.Monto = processorResponse.Monto;
        }


    }
}
