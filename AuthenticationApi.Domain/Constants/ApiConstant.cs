using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationApi.Domain.Constants
{
    public static class ApiConstant
    {
        private static string issuer = "OmniTricks";
        public static string Issuer { get => issuer; set => issuer = value; }
    }
}
