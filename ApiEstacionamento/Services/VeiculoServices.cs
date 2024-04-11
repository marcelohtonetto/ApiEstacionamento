
using ApiEstacionamento.Models;
using ApiEstacionamento.Repositorios;
using ApiEstacionamento.Repositorios.Interfaces;
using ApiEstacionamento.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

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

    public Task<List<VeiculoModel>> BuscarPorDataEntrada(DateTime dataentrada)
    {
        return _VeiculoRepositorio.BuscarPorDataEntrada(dataentrada);
    }

    public Task<List<VeiculoModel>> BuscarPorDataSaida(DateTime datasaida)
    {
        return _VeiculoRepositorio.BuscarPorDataSaida(datasaida);
    }

    public Task<List<VeiculoModel>> BuscarPorModelo(string modelo)
    {
        return _VeiculoRepositorio.BuscarPorModelo(modelo);
    }

    public Task<VeiculoModel> BuscarPorPlaca(string placa)
    {
        return _VeiculoRepositorio.BuscarPorPlaca(placa);
    }

    public Task<List<VeiculoModel>> BuscarTodasEntradasPorPlaca(string placa)
    {
        return _VeiculoRepositorio.BuscarTodasEntradasPorPlaca(placa);
    }

    public Task<VeiculoModel> GravarEntrada(VeiculoModel veiculo)
    {
        veiculo.DataEntrada = DateTime.Now;
        return _VeiculoRepositorio.GravarEntrada(veiculo);
    }

    public async Task<VeiculoModel> GravarSaida(string placa)
    {
        var veiculoSaida = await _VeiculoRepositorio.BuscarPorPlaca(placa);

        if (veiculoSaida == null)
        {
            throw new Exception($"Placa {placa} não encontrada");
        }

        if (veiculoSaida.DataSaida is not null)
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
