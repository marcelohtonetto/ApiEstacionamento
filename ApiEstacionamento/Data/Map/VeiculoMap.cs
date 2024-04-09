using ApiEstacionamento.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiEstacionamento.Data.Map;

public class VeiculoMap : IEntityTypeConfiguration<VeiculoModel>
{
    public void Configure(EntityTypeBuilder<VeiculoModel> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.PlacaVeiculo).IsRequired().HasMaxLength(7);
        builder.Property(x => x.Modelo).IsRequired().HasMaxLength(100);
        builder.Property(x => x.DataEntrada).IsRequired();
        builder.Property(x => x.DataSaida);
        builder.Property(x => x.ValorPago);
        builder.Property(x => x.PagamentoEfetuado);

    }
}
