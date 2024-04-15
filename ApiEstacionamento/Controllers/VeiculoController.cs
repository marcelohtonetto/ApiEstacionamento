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
            if (veiculo is null)
            {
                return BadRequest();
            }

            var listaDeVeiculos = await _VeiculoServices.BuscarVeiculo(veiculo);

            return Ok(listaDeVeiculos);
        }

        [HttpGet("BuscarPorPlaca/{placa}")]
        public async Task<ActionResult<VeiculoModel>> BuscarPorPlaca(string placa)
        {
            var veiculo = await _VeiculoServices.BuscarPorPlaca(placa);

            return Ok(veiculo);
        }

        [HttpPost("GravarEntrada")]
        public async Task<ActionResult<List<VeiculoModel>>> GravarEntrada([FromBody] VeiculoModel veiculoentrada)
        {
            var entradaVeiculo = await _VeiculoServices.GravarEntrada(veiculoentrada);

            return Ok(entradaVeiculo);
        }

        [HttpPut("GravarSaida/{placa}")]
        public async Task<ActionResult<List<VeiculoModel>>> GravarSaida(string placa)
        {
            var saidaVeiculo = await _VeiculoServices.GravarSaida(placa);

            return Ok(saidaVeiculo);
        }
    }
}
