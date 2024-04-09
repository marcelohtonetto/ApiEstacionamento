using System.ComponentModel.DataAnnotations;

namespace ApiEstacionamento.Models;

public class PrecoHoraModel
{
    [Key]
    [Required]
    public int Id { get; set; }
    [Required(ErrorMessage = "O Preço é obrigatorio")]
    public decimal Preco { get; set; }
    [Required(ErrorMessage = "A data é obrigatoria")]
    public DateTime DataPrecoCadastrado { get; set; }
}
