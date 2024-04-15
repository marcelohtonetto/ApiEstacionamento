using ApiEstacionamento.Data;
using ApiEstacionamento.Models;
using ApiEstacionamento.Repositorios.Interfaces;
using ApiEstacionamento.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiEstacionamento.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrecoHoraController : ControllerBase
    {
        private readonly IPrecoHoraServices _PrecoHoraServices;

        public PrecoHoraController(IPrecoHoraServices precoHoraRepositorio)
        {
            _PrecoHoraServices = precoHoraRepositorio;
        }

        [HttpGet("BuscaHistoricosDePrecos")]
        public async Task<ActionResult<List<PrecoHoraModel>>> BuscaHistoricosDePrecos()
        {
           var historicoPrecos = await _PrecoHoraServices.BuscaHistoricosPrecos();

           return Ok(historicoPrecos);
        }

        [HttpGet("BuscaPrecoAtual")]
        public async Task<ActionResult<decimal>> BuscaPrecoAtual() 
        { 
            var precoAtual = await _PrecoHoraServices.BuscaPrecoAtual();

            return  Ok(precoAtual);
        }

        [HttpPost]
        public async Task<ActionResult<PrecoHoraModel>> NovoPrecoHora([FromBody]PrecoHoraModel precohora)
        {
            PrecoHoraModel PrecoHora = await _PrecoHoraServices.GravarPrecoHora(precohora);

            return  Ok(PrecoHora);
        }

    }
}
