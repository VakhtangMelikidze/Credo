using Credo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Credo
{
    public class JwtAuthenticationManager : IJwtAuthenticationManager
    {
        private readonly IDictionary<string, string> users = new Dictionary<string, string>
        { {"user1", "password123" }, { "user2", "password321"} };

        public string Authenticate(string username, string password)
        {
            if (!users.Any(p => p.Key == username && p.Value == password))
            {
                return string.Empty;
            }
            return string.Empty;
        }
    }
}
