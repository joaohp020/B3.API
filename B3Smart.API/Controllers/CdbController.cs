using B3Smart.API.Interfaces;
using B3Smart.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace B3Smart.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CdbController : ControllerBase
    {
        private readonly ICdbService _cdbService;

        public CdbController(ICdbService cdbService)
        {
            _cdbService = cdbService;
        }

        [HttpPost("calcular")]
        public IActionResult CalcularCdb([FromBody] CdbRequestModel request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var resultado = _cdbService.CalcularInvestimento(request.ValorInicial, request.PrazoMeses);
            return Ok(resultado);
        }
    }
}
