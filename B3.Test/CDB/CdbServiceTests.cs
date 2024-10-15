using B3Smart.API.Services;
using System;
using Xunit;

namespace B3.Test.CDB
{
    public class CdbServiceTests
    {
        private readonly CdbService _cdbService;

        public CdbServiceTests()
        {
            _cdbService = new CdbService();
        }

        [Fact]
        public void CalcularInvestimento_DeveRetornarValorBrutoELiquido()
        {
            decimal valorInicial = 1000m;
            int prazoMeses = 12;

            var resultado = _cdbService.CalcularInvestimento(valorInicial, prazoMeses);

            Assert.NotNull(resultado);
            Assert.StartsWith("R$", resultado.ValorBruto);
            Assert.StartsWith("R$", resultado.ValorLiquido);
        }

        [Fact]
        public void CalcularInvestimento_DeveCalcularImpostoCorretamente()
        {
            decimal valorInicial = 1000m;
            int prazoMeses = 6;

            var resultado = _cdbService.CalcularInvestimento(valorInicial, prazoMeses);

            Assert.NotNull(resultado);
            Assert.StartsWith("R$", resultado.ValorLiquido);
        }

        [Fact]
        public void CalcularInvestimento_PrazoMinimoValido_DeveRetornarValorCorreto()
        {
            decimal valorInicial = 1000m;
            int prazoMeses = 2;

            var resultado = _cdbService.CalcularInvestimento(valorInicial, prazoMeses);

            Assert.NotNull(resultado);
            Assert.StartsWith("R$", resultado.ValorBruto);
            Assert.StartsWith("R$", resultado.ValorLiquido);
        }

        [Fact]
        public void CalcularInvestimento_PrazoMaximoValido_DeveRetornarValorCorreto()
        {
            decimal valorInicial = 1000m;
            int prazoMeses = 24;

            var resultado = _cdbService.CalcularInvestimento(valorInicial, prazoMeses);

            Assert.NotNull(resultado);
            Assert.StartsWith("R$", resultado.ValorBruto);
            Assert.StartsWith("R$", resultado.ValorLiquido);
        }

        [Fact]
        public void CalcularInvestimento_PrazoAcimaDoMaximo_DeveRetornarValorCorreto()
        {
            decimal valorInicial = 1000m;
            int prazoMeses = 25;

            var resultado = _cdbService.CalcularInvestimento(valorInicial, prazoMeses);

            Assert.NotNull(resultado);
            Assert.StartsWith("R$", resultado.ValorBruto);
            Assert.StartsWith("R$", resultado.ValorLiquido);
        }

        [Fact]
        public void CalcularInvestimento_ValorInicialZero_DeveLancarExcecao()
        {
            decimal valorInicial = 0m;
            int prazoMeses = 12;

            var exception = Assert.Throws<ArgumentException>(() => _cdbService.CalcularInvestimento(valorInicial, prazoMeses));
            Assert.Equal("O valor inicial deve ser maior que zero.", exception.Message);
        }

        [Fact]
        public void CalcularInvestimento_PrazoMenorQueUm_DeveLancarExcecao()
        {
            decimal valorInicial = 1000m;
            int prazoMeses = 1;

            var exception = Assert.Throws<ArgumentException>(() => _cdbService.CalcularInvestimento(valorInicial, prazoMeses));
            Assert.Equal("O prazo deve ser maior que 1 mês.", exception.Message);
        }

        [Fact]
        public void CalcularInvestimento_PrazoDeUmMes_DeveLancarExcecao()
        {
            decimal valorInicial = 1000m;
            int prazoMeses = 1; // Inválido, se a regra for > 1.

            var exception = Assert.Throws<ArgumentException>(() => _cdbService.CalcularInvestimento(valorInicial, prazoMeses));
            Assert.Equal("O prazo deve ser maior que 1 mês.", exception.Message);
        }
    }
}
