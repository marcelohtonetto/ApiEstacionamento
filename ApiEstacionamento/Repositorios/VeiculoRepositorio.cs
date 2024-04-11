using ApiEstacionamento.Data;
using ApiEstacionamento.Models;
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
            return await _dbContext.VeiculosEntrada.Where(x => x.DataSaida == datasaida.Date).ToListAsync();
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
            return await _dbContext.VeiculosEntrada.FirstOrDefaultAsync(x => x.PlacaVeiculo == placa);
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
