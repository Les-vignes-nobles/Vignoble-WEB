using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VignobleWEB.Core.Interfaces.Infrastructure.Token
{
    public interface ITokenAPI
    {
        string readTokenAPI();

        void updateTokenAPI(string token);

        Task getTokenAPI();
    }
}
