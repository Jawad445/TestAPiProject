using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIDev.Services
{
    public class JwtOptions
    {
        public string key { get; set; }
        public string issuer { get; set; }
    }
}
