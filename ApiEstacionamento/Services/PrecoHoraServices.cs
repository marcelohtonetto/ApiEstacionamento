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

    public async Task<List<PrecoHoraModel>> BuscaHistoricosPrecos()
    {
        var listaHistoricoPrecos = await _precoHoraRepositorio.BuscaHistoricosPrecos();
        if (listaHistoricoPrecos is null)
        {
            throw new Exception($"Sem registros de preço da hora.");
        }

        return listaHistoricoPrecos;
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

    public async Task<PrecoHoraModel> GravarPrecoHora(PrecoHoraModel precohoramodel)
    {
        if (precohoramodel.Preco == 0)
        {
            throw new Exception("O preçõ não pode ser 0");
        }
        if (precohoramodel.DataPrecoCadastrado == DateTime.MinValue)
        {
            throw new Exception("Data de cadastro do valor tem que ser válida.");
        }

        return await _precoHoraRepositorio.GravarPrecoHora(precohoramodel);
    }

}
