using ApiEstacionamento.Models;

namespace ApiEstacionamento.Repositorios.Interfaces;

public interface IVeiculoRepositorio
{
    Task<List<VeiculoModel>> BuscarPorDataEntrada(DateTime dataentrada);

    Task<List<VeiculoModel>> BuscarPorDataSaida(DateTime datasaida);

    Task<List<VeiculoModel>> BuscarTodasEntradasPorPlaca(string placa);

    Task<VeiculoModel> BuscarPorPlaca(string placa);

    Task<List<VeiculoModel>> BuscarPorModelo(string modelo);

    Task<VeiculoModel> GravarEntrada(VeiculoModel veiculo);

    Task<VeiculoModel> GravarSaida(string placa);

}
