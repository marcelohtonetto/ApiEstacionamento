using ApiEstacionamento.Data.Map;
using ApiEstacionamento.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiEstacionamento.Data;

public class EstacionamentoDBContext : DbContext
{

    public EstacionamentoDBContext(DbContextOptions<EstacionamentoDBContext> options) : base(options)
    {

    }

    public DbSet<VeiculoModel> VeiculosEntrada {  get; set; }
    public DbSet<PrecoHoraModel> PrecoHora { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new VeiculoMap());
        modelBuilder.ApplyConfiguration(new PrecoHoraMap());
        base.OnModelCreating(modelBuilder);
    }
}
