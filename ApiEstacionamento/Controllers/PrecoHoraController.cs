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
           return Ok(await _PrecoHoraServices.BuscaHistoricosPrecos());
        }

        [HttpGet("BuscaPrecoAtual")]
        public async Task<ActionResult<decimal>> BuscaPrecoAtual() 
        { 
            return  Ok(await _PrecoHoraServices.BuscaPrecoAtual());
        }

        [HttpPost]
        public async Task<ActionResult<PrecoHoraModel>> NovoPrecoHora([FromBody]PrecoHoraModel precohora)
        {
            return  Ok(await _PrecoHoraServices.GravarPrecoHora(precohora));
        }

    }
}
