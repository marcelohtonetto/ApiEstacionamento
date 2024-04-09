using ApiEstacionamento.Models;

namespace ApiEstacionamento.Repositorios.Interfaces;

public interface IPrecoHoraRepositorio
{
    Task<PrecoHoraModel> GravarPrecoHora(PrecoHoraModel precohoramodel);

    Task<List<PrecoHoraModel>> BuscaHistoricosPrecos();

    Task<List<PrecoHoraModel>> BuscaPrecoAtual();

}
