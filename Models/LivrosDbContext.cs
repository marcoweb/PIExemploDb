using Microsoft.EntityFrameworkCore;

namespace PIExemploDb.Models
{
    public class LivrosDbContext : DbContext
    {
        public DbSet<Genero> Generos { get; set; }
        public DbSet<Livro> Livros { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Define o arquivo que armazenará os dados
            optionsBuilder.UseSqlite("Data Source=livros.db");
        }
    }   
}
