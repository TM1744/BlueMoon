using BlueMoon.Entities.Models;
using BlueMoon.Mapping;
using Microsoft.EntityFrameworkCore;

namespace BlueMoon.Context
{
    public class MySqlDataBaseContext : DbContext
    {

        public DbSet<Produto> Produtos { get; set; }

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
        }
    }
}