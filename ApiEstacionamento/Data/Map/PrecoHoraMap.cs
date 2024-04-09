using ApiEstacionamento.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiEstacionamento.Data.Map
{
    public class PrecoHoraMap : IEntityTypeConfiguration<PrecoHoraModel>
    {
        public void Configure(EntityTypeBuilder<PrecoHoraModel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Preco).IsRequired();
            builder.Property(x => x.DataPrecoCadastrado);
        }
    }
}
