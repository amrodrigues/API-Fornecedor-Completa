using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Context
{
    public class MeuDbContext : DbContext
    {
        // Construtor sem parâmetros, necessário para as ferramentas de migração
        //public MeuDbContext()
        //{
        //}

        // Construtor com opções, usado em tempo de execução pela sua API
        public MeuDbContext(DbContextOptions<MeuDbContext> options) : base(options) { }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configura todas as propriedades string para terem o tipo de coluna "varchar(100)"
            //foreach (var property in modelBuilder.Model.GetEntityTypes()
            //    .SelectMany(e => e.GetProperties())
            //    .Where(p => p.ClrType == typeof(string)))
            //{
            //    property.SetColumnType("varchar(100)");
            //}


            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MeuDbContext).Assembly);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;
            }

            base.OnModelCreating(modelBuilder);
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        // Este é o método que configura a string de conexão e o provedor
        //        // quando a injeção de dependência não está em uso (como nas migrações).
        //        optionsBuilder.UseSqlServer("Server=localhost;Database=FornecedorDb;Trusted_Connection=True;MultipleActiveResultSets=true");
        //    }
        //}

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DataCadastro") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("DataCadastro").CurrentValue = DateTime.Now;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("DataCadastro").IsModified = false;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
