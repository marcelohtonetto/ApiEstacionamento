using System.ComponentModel.DataAnnotations;

namespace ApiEstacionamento.Models;

public class VeiculoModel
{
    private string _placa;
    private string _modelo;

    [Key]
    [Required]
    public int Id { get; set; }

    [Required(ErrorMessage = "O campo placa é obrigatorio.")]
    [StringLength(7,MinimumLength =7, ErrorMessage ="A placa teve ter o tamanho de 7 caracteres")]
    public string PlacaVeiculo { get => _placa; set => _placa = value.ToUpper();}

    [Required(ErrorMessage = "O campo modelo é obrigatorio.")]
    public string Modelo { get => _modelo; set => _modelo = value.ToUpper();}

    [Required(ErrorMessage = "A data de Entrada deve ser obrigatoria")]
    public DateTime DataEntrada { get; set; }

    public DateTime? DataSaida { get; set; }
    public Decimal? ValorPago { get; set; } = decimal.Zero;
    public int PagamentoEfetuado { get; set; } = 0;
}
