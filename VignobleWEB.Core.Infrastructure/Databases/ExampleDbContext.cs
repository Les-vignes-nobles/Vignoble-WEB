using VignobleWEB.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace VignobleWEB.Core.Infrastructure.Databases
{
    public class ExampleDbContext : DbContext
    {
        #region Contructeurs
        public ExampleDbContext(DbContextOptions options) : base(options)
        {

        }

        public ExampleDbContext()
        {

        }
        #endregion

        #region Public Methods
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EntityConfiguration.ExampleEntityTypeConfigurations());

            //Permet d'empêcher la suppression de ligne si une clé étrangère existe
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().Where(e => !e.IsOwned()).SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

        }
        #endregion

        #region Propriétés
        public DbSet<Example> Example { get; set; }
       
        #endregion
    }
}
