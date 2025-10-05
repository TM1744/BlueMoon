using BlueMoon.Models.Modelling;
using Microsoft.EntityFrameworkCore;

namespace BlueMoon.Context
{
    public class MySqlDataBaseContext : DbContext
    {
        public DbSet<Cupom> Cupons { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Telefone> Telefones { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<ProdutoCupom> ProdutoCupoms { get; set; }
        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<ItemVenda> ItensVendas { get; set; }
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
        }
    }
}