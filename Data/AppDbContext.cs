using Microsoft.EntityFrameworkCore;
using OrçamentoObra.Models;
using System.Collections.Generic;

namespace OrçamentoObra.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<EmpresaModel> Empresas { get; set; }
        public DbSet <ObraModel> Obras { get; set; }
        public DbSet <CategoriaModel> Categorias { get; set; }
        public DbSet <ClienteModel> Clientes { get; set; }
        public DbSet <ItemModel> Items { get; set; }
        public DbSet <OrcamentoModel> Orcamentos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Enum Status como string
            modelBuilder.Entity<ObraModel>()
                .Property(o => o.Status)
                .HasConversion<string>();

            base.OnModelCreating(modelBuilder);
        }


    }
}
