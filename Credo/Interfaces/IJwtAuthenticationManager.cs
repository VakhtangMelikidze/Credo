using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Credo.Interfaces
{
    public interface IJwtAuthenticationManager
    {
        string Authenticate(string username);
    }
}
