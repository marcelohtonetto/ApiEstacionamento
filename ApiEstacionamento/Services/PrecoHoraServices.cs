using ApiEstacionamento.Models;
using ApiEstacionamento.Repositorios;
using ApiEstacionamento.Repositorios.Interfaces;
using ApiEstacionamento.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApiEstacionamento.Services;

public class PrecoHoraServices : IPrecoHoraServices
{

    private readonly IPrecoHoraRepositorio _precoHoraRepositorio;

    public PrecoHoraServices(IPrecoHoraRepositorio precoHoraRepositorio)
    {
        _precoHoraRepositorio = precoHoraRepositorio;
    }

    public Task<List<PrecoHoraModel>> BuscaHistoricosPrecos()
    {
        return _precoHoraRepositorio.BuscaHistoricosPrecos();
    }

    public async Task<PrecoHoraModel> BuscaPrecoAtual()
    {
        var precoAtual = await _precoHoraRepositorio.BuscaPrecoAtual();

        if (precoAtual == null)
        {
            throw new Exception("Não existe preço cadastrado");
        }

        return precoAtual.FirstOrDefault();
    }

    public Task<PrecoHoraModel> GravarPrecoHora(PrecoHoraModel precohoramodel)
    {
       return _precoHoraRepositorio.GravarPrecoHora(precohoramodel);
    }

}
