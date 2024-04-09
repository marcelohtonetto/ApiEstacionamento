﻿using ApiEstacionamento.Data;
using ApiEstacionamento.Models;
using ApiEstacionamento.Repositorios.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;

namespace ApiEstacionamento.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VeiculoController : ControllerBase
    {
        private readonly IVeiculoRepositorio _VeiculoRepositorio;

        public VeiculoController(IVeiculoRepositorio veiculorepositorio)
        {
            _VeiculoRepositorio = veiculorepositorio;
        }
        /// <summary>
        /// Retorna lista de veículos que entraram nessa data
        /// </summary>
        /// <param name="dataentrada"></param>
        /// <returns></returns>
        [HttpGet("BuscarPorDataEntrada/{dataentrada}")]
        public async Task<ActionResult<List<VeiculoModel>>> BuscarPorDataEntrada(DateTime dataentrada)
        {
            var listaPorEntrada = await _VeiculoRepositorio.BuscarPorDataEntrada(dataentrada);

            if (listaPorEntrada is null)
            {
               return NotFound();
            }

            return Ok(listaPorEntrada);
        }

        [HttpGet("BuscarPorDataSaida/{datasaida}")]
        public async Task<ActionResult<List<VeiculoModel>>> BuscarPorDataSaida(DateTime datasaida)
        {
            var listaPorSaida = await _VeiculoRepositorio.BuscarPorDataEntrada(datasaida);

            if (listaPorSaida is null)
            {
                return NotFound();
            }

            return Ok(listaPorSaida);
        }

        [HttpGet("BuscarTodasEntradasPorPlaca/{placa}")]
        public async Task<ActionResult<List<VeiculoModel>>> BuscarTodasEntradasPorPlaca(string placa)
        {
            var todasEntradasPorPlacas = await _VeiculoRepositorio.BuscarTodasEntradasPorPlaca(placa);

            if (todasEntradasPorPlacas is null)
            {
                return NotFound();
            }

            return Ok(todasEntradasPorPlacas);
        }

        [HttpGet("BuscarPorPlaca/{placa}")]
        public async Task<ActionResult<List<VeiculoModel>>> BuscarPorPlaca(string placa)
        {
            var listaPorPlaca = await _VeiculoRepositorio.BuscarPorPlaca(placa);

            if (listaPorPlaca is null)
            {
                return NotFound();
            }

            return Ok(listaPorPlaca);
        }
        [HttpGet("BuscarPorModelo/{modelo}")]
        public async Task<ActionResult<List<VeiculoModel>>> BuscarPorModelo(string modelo)
        {
            var listaPorModelo = await _VeiculoRepositorio.BuscarPorPlaca(modelo);

            if (listaPorModelo is null)
            {
                return NotFound();
            }

            return Ok(listaPorModelo);
        }
        [HttpPost("GravarEntrada")]
        public async Task<ActionResult<List<VeiculoModel>>> GravarEntrada([FromBody] VeiculoModel veiculoentrada)
        {
            var entradaVeiculo =  await _VeiculoRepositorio.GravarEntrada(veiculoentrada);

            return Ok(entradaVeiculo);
        }
        [HttpPut("GravarSaida/{placa}")]
        public async Task<ActionResult<List<VeiculoModel>>> GravarSaida(string placa)
        {
            var saidaVeiculo = await _VeiculoRepositorio.GravarSaida(placa);


            return Ok(saidaVeiculo);
        }
    }
}