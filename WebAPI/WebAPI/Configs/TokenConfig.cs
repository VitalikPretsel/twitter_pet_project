using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Configs
{
    public class TokenConfig
    {
        public string ValidIssuer { get; set; }
        public string ValidAudience { get; set; }
        public string SymmetricSecurityKey { get; set; }

    }
}
