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
            return await _dbContext.VeiculosEntrada.Where(x => x.DataEntrada == dataentrada).ToListAsync();
        }

        public async Task<List<VeiculoModel>> BuscarPorDataSaida(DateTime datasaida)
        {
            return await _dbContext.VeiculosEntrada.Where(x => x.DataSaida == datasaida).ToListAsync();
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

        public async Task<VeiculoModel> GravarEntrada(VeiculoModel veiculo)
        {
            _dbContext.VeiculosEntrada.Add(veiculo);
            await _dbContext.SaveChangesAsync();
            return veiculo;
        }

        public async Task<VeiculoModel> GravarSaida(string placa)
        {

            var veiculoSaida = await BuscarPorPlaca(placa);

            if (veiculoSaida == null)
            {
                throw new Exception($"Placa {veiculoSaida.PlacaVeiculo} não encontrada");
            }

            if (veiculoSaida.DataSaida is not null)
            {
                throw new Exception($"O veículo com a placa {veiculoSaida.PlacaVeiculo} não tem sáida.");
            }

            veiculoSaida.DataSaida = DateTime.Now;
            veiculoSaida.ValorPago = await RetornaValorASerPago(veiculoSaida);
            veiculoSaida.PagamentoEfetuado = 1;

           
            _dbContext.VeiculosEntrada.Update(veiculoSaida);
            await _dbContext.SaveChangesAsync();
            return veiculoSaida;
        }

        private async Task<decimal> RetornaValorASerPago(VeiculoModel veiculo)
        {
            DateTime horaSaida = DateTime.Now;

            TimeSpan diferencaTempo = horaSaida - veiculo.DataEntrada;   

            var valorASerPago = Math.Round(RetornaValorHora()) * Convert.ToDecimal(diferencaTempo.TotalHours);

            return valorASerPago;
        }

        private decimal RetornaValorHora()
        {
            PrecoHoraModel precoAtual = _dbContext.PrecoHora.OrderByDescending(x => x.Id).Take(1).FirstOrDefault();

            if (precoAtual is null)
            {
                throw new Exception($"Preco da hora não cadastrado");
            }

            return precoAtual.Preco;
        }

    }
}
