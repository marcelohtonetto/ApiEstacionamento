using ApiEstacionamento.Data;
using ApiEstacionamento.Models;
using ApiEstacionamento.Models.FilterModel;
using ApiEstacionamento.Repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApiEstacionamento.Repositorios
{
    public class VeiculoRepositorio : IVeiculoRepositorio
    {
        private readonly EstacionamentoDBContext _dbContext;
        public VeiculoRepositorio(EstacionamentoDBContext estacionamentoDBContext)
        {
            _dbContext = estacionamentoDBContext;
        }

        public async Task<List<VeiculoModel>> BuscarPorDataEntrada(DateTime dataentrada)
        {
            return await _dbContext.VeiculosEntrada.Where(x => x.DataEntrada.Date == dataentrada.Date).ToListAsync();
        }

        public async Task<List<VeiculoModel>> BuscarPorDataSaida(DateTime datasaida)
        {
            return await _dbContext.VeiculosEntrada.Where(x => x.DataSaida.Date == datasaida.Date).ToListAsync();
        }

        public async Task<List<VeiculoModel>> BuscarPorModelo(string modelo)
        {
            return await _dbContext.VeiculosEntrada.Where(x => x.Modelo == modelo).ToListAsync();
        }

        public async Task<List<VeiculoModel>> BuscarTodasEntradasPorPlaca(string placa)
        {
            return await _dbContext.VeiculosEntrada.Where(x => x.PlacaVeiculo == placa).ToListAsync();
        }
        public async Task<VeiculoModel> BuscarPorPlaca(string placa)
        {
            return await _dbContext.VeiculosEntrada.Where(x => x.PlacaVeiculo == placa).OrderByDescending(x => x.DataEntrada).FirstOrDefaultAsync();
        }
        public async Task<List<VeiculoModel>> BuscarVeiculo(VeiculoFilterModel veiculo)
        {
            return await _dbContext.VeiculosEntrada
                         .Where(x =>
                            (string.IsNullOrWhiteSpace(veiculo.PlacaVeiculo) || x.PlacaVeiculo == veiculo.PlacaVeiculo) &&
                            (string.IsNullOrWhiteSpace(veiculo.Modelo) || x.Modelo == veiculo.Modelo) &&
                            (!veiculo.DataEntrada.HasValue || x.DataEntrada.Date == veiculo.DataEntrada.Value.Date) &&
                            (!veiculo.DataSaida.HasValue || x.DataSaida.Date == veiculo.DataSaida.Value.Date)
                         ).ToListAsync();
        }
        public async Task<VeiculoModel> GravarEntrada(VeiculoModel veiculoentrada)
        {
            _dbContext.VeiculosEntrada.Add(veiculoentrada);
            await _dbContext.SaveChangesAsync();
            return veiculoentrada;
        }

        public async Task<VeiculoModel> GravarSaida(VeiculoModel veiculosaida)
        {
            _dbContext.VeiculosEntrada.Update(veiculosaida);
            await _dbContext.SaveChangesAsync();
            return veiculosaida;
        }
    }
}
