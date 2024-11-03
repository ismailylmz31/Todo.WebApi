using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Core.Tokens.Configuration
{
    public class CustomTokenOptions
    {
        public string Issuer { get; set; }
        public List<string> Audience { get; set; }
        public int AccessTokenExpiration { get; set; }

        public string SecurityKey { get; set; }
    }
}
