﻿using ApiEstacionamento.Data;
using ApiEstacionamento.Models;
using ApiEstacionamento.Repositorios.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiEstacionamento.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrecoHoraController : ControllerBase
    {
        private readonly IPrecoHoraRepositorio _PrecoHoraRepositorio;

        public PrecoHoraController(IPrecoHoraRepositorio precohorarepositorio)
        {
            _PrecoHoraRepositorio = precohorarepositorio;
        }

        [HttpGet("BuscaHistoricosDePrecos")]
        public async Task<ActionResult<List<PrecoHoraModel>>> BuscaHistoricosDePrecos()
        {
           var historicoPrecos = await _PrecoHoraRepositorio.BuscaHistoricosPrecos();

            if (historicoPrecos is null)
            {
              return  NotFound();
            }

           return Ok(historicoPrecos);
        }
        [HttpGet("BuscaPrecoAtual")]
        public async Task<ActionResult<PrecoHoraModel>> BuscaPrecoAtual() 
        { 
            var precoAtual = await _PrecoHoraRepositorio.BuscaPrecoAtual();

            if (precoAtual is null)
            {
                return NotFound();
            }

            return  Ok(precoAtual);
        }

        [HttpPost]
        public async Task<ActionResult<PrecoHoraModel>> NovoPrecoHora([FromBody]PrecoHoraModel precohora)
        {
            PrecoHoraModel PrecoHora = await _PrecoHoraRepositorio.GravarPrecoHora(precohora);

            return  Ok(PrecoHora);
        }

    }
}
