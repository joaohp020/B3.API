using Microsoft.AspNetCore.Mvc;

namespace B3Smart.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CdbController : ControllerBase
    {
        [HttpPost("cacular")]
        public ActionResult<ResultadoCdbDto> Calcular([FromBody] CalculoCdbDto input)
        {
            decimal valorFinalBruto = CalcularValorFinal(input.ValorInicial, input.PrazoMeses);
            decimal valorFinalLiquido = AplicarImposto(valorFinalBruto, input.PrazoMeses);

            return Ok(new ResultadoCdbDto
            {
                ValorBruto = valorFinalBruto,
                ValorLiquido = valorFinalLiquido
            });
        }

        [HttpGet("calcularValorFinal")]
        public decimal CalcularValorFinal(decimal valorInicial, int prazoMeses)
        {
            decimal cdi = 0.009m;  // CDI 0,9% 
            decimal tb = 1.08m;    // TB 108%
            decimal valorFinal = valorInicial;

            for (int i = 0; i < prazoMeses; i++)
            {
                valorFinal *= (1 + (cdi * tb));
            }

            return valorFinal;
        }

        [HttpPost("aplicarImposto")]
        public decimal AplicarImposto(decimal valorFinal, int prazoMeses)
        {
            decimal taxaImposto;

            if (prazoMeses <= 6)
                taxaImposto = 0.225m;
            else if (prazoMeses <= 12)
                taxaImposto = 0.20m;
            else if (prazoMeses <= 24)
                taxaImposto = 0.175m;
            else
                taxaImposto = 0.15m;

            decimal lucro = valorFinal - valorFinal / (1 + taxaImposto);
            return valorFinal - lucro;
        }
    }

    public class CalculoCdbDto
    {
        public decimal ValorInicial { get; set; }
        public int PrazoMeses { get; set; }
    }

    public class ResultadoCdbDto
    {
        public decimal ValorBruto { get; set; }
        public decimal ValorLiquido { get; set; }
    }
}
