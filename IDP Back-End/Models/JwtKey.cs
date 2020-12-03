using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDP_Back_End.Models
{
    public class JwtKey
    {
        private static byte[] secretBytes = Encoding.UTF8.GetBytes("SuchSecret42ManyEncryption13MuchSafety55");

        public static SymmetricSecurityKey Key
        {
            get { return new SymmetricSecurityKey(secretBytes); }
        }

        public static void SetSecret(string secret)
        {
            secretBytes = Encoding.UTF8.GetBytes(secret);
        }

    }
}
