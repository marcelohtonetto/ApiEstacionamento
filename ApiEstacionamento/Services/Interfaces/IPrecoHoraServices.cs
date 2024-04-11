using ApiEstacionamento.Models;

namespace ApiEstacionamento.Services.Interfaces;

public interface IPrecoHoraServices
{
    Task<PrecoHoraModel> GravarPrecoHora(PrecoHoraModel precohoramodel);

    Task<List<PrecoHoraModel>> BuscaHistoricosPrecos();

    Task<PrecoHoraModel> BuscaPrecoAtual();
}
