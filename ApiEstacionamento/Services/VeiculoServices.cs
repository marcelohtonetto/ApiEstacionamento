
using ApiEstacionamento.Models;
using ApiEstacionamento.Models.FilterModel;
using ApiEstacionamento.Repositorios;
using ApiEstacionamento.Repositorios.Interfaces;
using ApiEstacionamento.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace ApiEstacionamento.Services;

public class VeiculoServices : IVeiculoServices
{
    private readonly IVeiculoRepositorio _VeiculoRepositorio;
    private readonly IPrecoHoraServices _PrecohoraServices;
    public VeiculoServices(IVeiculoRepositorio veiculoRepositorio, IPrecoHoraServices precoHoraServices )
    {
        _VeiculoRepositorio = veiculoRepositorio;
        _PrecohoraServices = precoHoraServices;
    }

    public async Task<List<VeiculoModel>> BuscarPorDataEntrada(DateTime dataentrada)
    {
        var listaDeVeiculosPorEntrada = await _VeiculoRepositorio.BuscarPorDataEntrada(dataentrada);

        if (listaDeVeiculosPorEntrada is null)
        {
            throw new Exception($"Sem registros de veículos.");
        }

        return listaDeVeiculosPorEntrada;
    }

    public async Task<List<VeiculoModel>> BuscarPorDataSaida(DateTime datasaida)
    {
        var listaDeVeiculosPorSaida = await _VeiculoRepositorio.BuscarPorDataSaida(datasaida);

        if (listaDeVeiculosPorSaida is null)
        {
            throw new Exception($"Sem registros de veículos.");
        }
        return listaDeVeiculosPorSaida;
    }

    public async Task<List<VeiculoModel>> BuscarPorModelo(string modelo)
    {
        var listaDeVeiculosModelo = await _VeiculoRepositorio.BuscarPorModelo(modelo);

        if (listaDeVeiculosModelo is null)
        {
            throw new Exception($"Sem registros de veículos.");
        }
        return listaDeVeiculosModelo;
    }

    public async Task<VeiculoModel> BuscarPorPlaca(string placa)
    {
        var veiculosPlaca = await _VeiculoRepositorio.BuscarPorPlaca(placa);

        if (veiculosPlaca is null)
        {
            throw new Exception($"Placa {placa} não encontrada");
        }
        return veiculosPlaca;
    }

    public async Task<List<VeiculoModel>> BuscarTodasEntradasPorPlaca(string placa)
    {
        var listaEntradaDeVeiculosPlaca = await _VeiculoRepositorio.BuscarTodasEntradasPorPlaca(placa);

        if (listaEntradaDeVeiculosPlaca is null)
        {
            throw new Exception($"Sem registros de veículos.");
        }
        return listaEntradaDeVeiculosPlaca;
    }

    public async Task<List<VeiculoModel>> BuscarVeiculo(VeiculoFilterModel veiculo)
    {
        if (veiculo is null)
        {
            throw new Exception($"Dados do veículo incorretos.");
        }

        var ListaDeVeiculos = await _VeiculoRepositorio.BuscarVeiculo(veiculo);

        if (ListaDeVeiculos is null)
        {
            throw new Exception($"Sem registros de veículos.");
        }
        return ListaDeVeiculos;
    }

    public async Task<VeiculoModel> GravarEntrada(VeiculoModel veiculo)
    {
        if (string.IsNullOrWhiteSpace(veiculo.PlacaVeiculo))
        {
            throw new Exception($"A placa não pode ser vazia.");
        }
        if (string.IsNullOrWhiteSpace(veiculo.Modelo))
        {
            throw new Exception($"O modelo não pode ser vazio.");
        }

        veiculo.DataEntrada = DateTime.Now;
        return await _VeiculoRepositorio.GravarEntrada(veiculo);
    }

    public async Task<VeiculoModel> GravarSaida(string placa)
    {
        var veiculoSaida = await _VeiculoRepositorio.BuscarPorPlaca(placa);

        if (veiculoSaida.PagamentoEfetuado == 1)
        {
            throw new Exception($"O veículo com a placa {placa} não tem sáida.");
        }

        veiculoSaida.DataSaida = DateTime.Now;
        veiculoSaida.ValorPago = await RetornaValorASerPago(veiculoSaida);
        veiculoSaida.PagamentoEfetuado = 1;

        return await _VeiculoRepositorio.GravarSaida(veiculoSaida);
    }
    private async Task<decimal> RetornaValorASerPago(VeiculoModel veiculo)
    {
        DateTime horaSaida = DateTime.Now;

        TimeSpan diferencaTempo = horaSaida - veiculo.DataEntrada;
        var preco = await _PrecohoraServices.BuscaPrecoAtual();

        var valorASerPago = Math.Round(preco.Preco) * Convert.ToDecimal(diferencaTempo.TotalHours);

        return valorASerPago;
    }
}
