using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace ProcesadorDePagos.Api.Controllers
{
    [ApiController]
    [Route("api/processor")]
    public class ProcessorController : ControllerBase
    {
        [HttpPost]
        public IActionResult PaymentRequestProcessor([FromBody] ProcessorDto request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var amountParsed = int.TryParse(request.Monto, out int amount);

            if (amountParsed)
                return Ok(new ProcessorResponseDto() { Aprobado = true, Monto = amount });

            decimal amountDenied;
            var amountDeniedParsed = Decimal.TryParse(request.Monto, NumberStyles.Any, CultureInfo.InvariantCulture, out amountDenied);

            if (amountDeniedParsed)
                return Ok(new ProcessorResponseDto() { Aprobado = false, MontoDenegado = amountDenied });

            return BadRequest();
        }
    }

    public class ProcessorDto
    {
        public string NombreCliente { get; set; }
        public string Monto { get; set; }
    }

    public class ProcessorResponseDto
    {
        public bool Aprobado { get; set; }

        public int? Monto { get; set; }

        public decimal? MontoDenegado { get; set; }
    }
}
