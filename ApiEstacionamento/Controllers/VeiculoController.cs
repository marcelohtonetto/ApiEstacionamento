using ApiEstacionamento.Data;
using ApiEstacionamento.Models;
using ApiEstacionamento.Models.FilterModel;
using ApiEstacionamento.Repositorios.Interfaces;
using ApiEstacionamento.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Linq.Expressions;
using System.Numerics;

namespace ApiEstacionamento.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VeiculoController : ControllerBase
    {
        private readonly IVeiculoServices _VeiculoServices;

        public VeiculoController(IVeiculoServices veiculoservices)
        {
            _VeiculoServices = veiculoservices;
        }

        /// <summary>
        /// Busca lista de Veiculo baseados nos campos preenchidos
        /// </summary>
        /// <param name="veiculo"></param>
        /// <returns></returns>
        [HttpPost("BuscarListaDeVeiculos")]
        public async Task<ActionResult<List<VeiculoModel>>> BuscarListaDeVeiculos([FromBody] VeiculoFilterModel veiculo)
        {
            return Ok(await _VeiculoServices.BuscarVeiculo(veiculo));
        }

        [HttpGet("BuscarPorPlaca/{placa}")]
        public async Task<ActionResult<VeiculoModel>> BuscarPorPlaca(string placa)
        {
            return Ok(await _VeiculoServices.BuscarPorPlaca(placa));
        }

        [HttpPost("GravarEntrada")]
        public async Task<ActionResult<List<VeiculoModel>>> GravarEntrada([FromBody] VeiculoModel veiculoentrada)
        {
            return Ok(await _VeiculoServices.GravarEntrada(veiculoentrada));
        }

        [HttpPut("GravarSaida/{placa}")]
        public async Task<ActionResult<List<VeiculoModel>>> GravarSaida(string placa)
        {
            return Ok(await _VeiculoServices.GravarSaida(placa));
        }
    }
}
