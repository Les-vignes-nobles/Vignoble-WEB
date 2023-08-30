using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VignobleWEB.Core.Interfaces.Infrastructure.Token
{
    public interface ITokenAPI
    {
        string ReadTokenAPI();

        void UpdateTokenAPI(string token);

        Task<string> GetTokenAPI();
    }
}
