using BlueMoon.Entities.Models;
using BlueMoon.Mapping;
using Microsoft.EntityFrameworkCore;

namespace BlueMoon.Context
{
    public class MySqlDataBaseContext : DbContext
    {

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<ItemVenda> ItemVendas { get; set; }
        public DbSet<Venda> Vendas { get; set; }

        public MySqlDataBaseContext(DbContextOptions<MySqlDataBaseContext> options)
            : base(options)
        {
            if (Database.GetPendingMigrations().Any())
            {
                Database.Migrate();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //adicionar classes mapeadas aqui
            modelBuilder.ApplyConfiguration(new ProdutoMap());
            modelBuilder.ApplyConfiguration(new PessoaMap());
            modelBuilder.ApplyConfiguration(new UsuarioMap());
            modelBuilder.ApplyConfiguration(new ItemVendaMap());
            modelBuilder.ApplyConfiguration(new VendaMap());
        }
    }
}