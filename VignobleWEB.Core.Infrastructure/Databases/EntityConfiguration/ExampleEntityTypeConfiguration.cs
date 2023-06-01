using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using VignobleWEB.Core.Models;

namespace VignobleWEB.Core.Infrastructure.Databases.EntityConfiguration
{
    internal class ExampleEntityTypeConfigurations : IEntityTypeConfiguration<Example>
    {
        #region Méthode publique
        public void Configure(EntityTypeBuilder<Example> builder)
        {
            //Défini la clé primaire de la BDD
            builder.HasKey(item => item.Id);

            //Défini le nom de la table (Utile si le nom du model et de la table différent)
            builder.ToTable("Example");

            //Définition du nombre de caractère :
            builder.Property(item => item.Name).HasMaxLength(17);


        }
        #endregion

    }
}
