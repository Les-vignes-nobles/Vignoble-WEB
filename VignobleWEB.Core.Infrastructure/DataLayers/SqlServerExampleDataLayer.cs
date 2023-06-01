using VignobleWEB.Core.Infrastructure.Databases;
using VignobleWEB.Core.Infrastructure.ExceptionPersonnalisee;
using VignobleWEB.Core.Interfaces.Infrastructure.DataLayers;
using VignobleWEB.Core.Interfaces.Infrastructure.Tools;
using VignobleWEB.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace VignobleWEB.Core.Infrastructure.DataLayers
{
    public class SqlServerExampleDataLayer : IExampleDataLayer
    {
        #region Champs
        private readonly ExampleDbContext _context = null;
        private readonly ILogInfrastructure _logInfrastructure;
        #endregion

        #region Constructeur
        public SqlServerExampleDataLayer(ExampleDbContext context, ILogInfrastructure logInfrastructure)
        {
            _context = context;
            _logInfrastructure = logInfrastructure;
        }
        #endregion

        #region Read (Lecture)
        public Example RecupererUnExample(int idExample)
        {
            return _context.Example.Where(item => item.Id == idExample).Single();
        }
        #endregion
    }
}
