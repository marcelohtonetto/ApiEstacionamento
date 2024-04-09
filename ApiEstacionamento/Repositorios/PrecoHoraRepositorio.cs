﻿using ApiEstacionamento.Data;
using ApiEstacionamento.Models;
using ApiEstacionamento.Repositorios.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace ApiEstacionamento.Repositorios;

public class PrecoHoraRepositorio : IPrecoHoraRepositorio
{
    private readonly EstacionamentoDBContext _DbContext;

    public PrecoHoraRepositorio(EstacionamentoDBContext estacionamentodbcontext)
    {
        _DbContext = estacionamentodbcontext;     
    }
    public Task<List<PrecoHoraModel>> BuscaHistoricosPrecos()
    {  
        return  _DbContext.PrecoHora.OrderByDescending(x => x.DataPrecoCadastrado).ToListAsync();     
    }

    public async Task<List<PrecoHoraModel>> BuscaPrecoAtual()
    {
        var precoAtual = await _DbContext.PrecoHora.OrderByDescending(x => x.Id).Take(1).ToListAsync();

        if (precoAtual == null) 
        {
            throw new Exception("Não existe preço cadsatrado");
        }

        return  precoAtual;
    }

    public async Task<PrecoHoraModel> GravarPrecoHora(PrecoHoraModel precohoramodel)
    {
        await _DbContext.PrecoHora.AddAsync(precohoramodel);
        await _DbContext.SaveChangesAsync();
        return precohoramodel;
    }
}
