using AutorizacionDePagos.Api.Application.ApprovedAuthorizations;
using AutorizacionDePagos.Api.Application.Authorization;
using EasyNetQ;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace AutorizacionDePagos.Api.Controllers
{
    [ApiController]
    [Route("api/authorization")]
    public class AuthorizationController : ControllerBase
    {
        private readonly IMediator mediator;

        public AuthorizationController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAuthorizationCommand command)
        {
            try
            {
                var result = await mediator.Send(command);
                return Ok(result);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Se rompio xd");
            }
            
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAuthorizations()
        {
            var query = new GetAllAuthorizationsQuery();
            var result = await mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("approved-authorizations")]
        public async Task<IActionResult> GetApprovedAuthorizations()
        {
            var query = new GetApprovedAuthorizationsQuery();
            var result = await mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("reverse-authorizations")]
        public async Task<IActionResult> GetReverseAuthorizations()
        {
            var query = new GetReverseAuthorizationsQuery();
            var result = await mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuthorizationById(Guid id)
        {
            var query = new GetAuthorizationByIdQuery() { Id = id };
            var result = await mediator.Send(query);
            return Ok(result);
        }

    }
}
