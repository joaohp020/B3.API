using B3Smart.API.Models;

namespace B3Smart.API.Interfaces
{
    public interface ICdbService
    {
        CdbResponseModel CalcularInvestimento(decimal valorInicial, int prazoMeses);
    }
}
