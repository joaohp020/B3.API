using B3Smart.API.Interfaces;
using B3Smart.API.Models;
using System.Globalization;

namespace B3Smart.API.Services
{
    public class CdbService : ICdbService
    {
        private const decimal CDI = 0.009m;
        private const decimal TB = 1.08m;

        public CdbResponseModel CalcularInvestimento(decimal valorInicial, int prazoMeses)
        {
            if (valorInicial <= 0)
            {
                throw new ArgumentException("O valor inicial deve ser maior que zero.");
            }

            if (prazoMeses <= 1)
            {
                throw new ArgumentException("O prazo deve ser maior que 1 mês.");
            }

            decimal valorBruto = valorInicial;

            for (int i = 0; i < prazoMeses; i++)
            {
                valorBruto *= (1 + (CDI * TB));
            }

            decimal lucro = valorBruto - valorInicial;
            decimal imposto = CalcularImposto(prazoMeses, lucro);
            decimal valorLiquido = valorBruto - imposto;

            return new CdbResponseModel
            {
                ValorBruto = valorBruto.ToString("C", new CultureInfo("pt-BR")),
                ValorLiquido = valorLiquido.ToString("C", new CultureInfo("pt-BR"))
            };
        }

        private decimal CalcularImposto(int prazoMeses, decimal lucro)
        {
            decimal taxaImposto = prazoMeses switch
            {
                <= 6 => 0.225m,
                <= 12 => 0.20m,
                <= 24 => 0.175m,
                _ => 0.15m
            };

            return lucro * taxaImposto;
        }
    }
}
