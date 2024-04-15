using System.ComponentModel.DataAnnotations;

namespace ApiEstacionamento.Models.FilterModel
{
    public class VeiculoFilterModel
    {
        private string _placa;
        private string _modelo;

        [StringLength(7, MinimumLength = 7, ErrorMessage = "A placa teve ter o tamanho de 7 caracteres")]
        public string? PlacaVeiculo { get => _placa; set => _placa = value.ToUpper(); }

        public string? Modelo { get => _modelo; set => _modelo = value.ToUpper(); }

        public DateTime? DataEntrada { get; set; }

        public DateTime? DataSaida { get; set; }
    }
}
