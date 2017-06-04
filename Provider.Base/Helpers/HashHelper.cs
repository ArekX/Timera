using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Provider.Base.Helpers
{
    public static class HashHelper
    {
        public static string GetSHA256(string input) {
            StringBuilder shaString = new StringBuilder();

            byte[] hashedBytes = GetSHA256Bytes(input);

            foreach (byte hashByte in hashedBytes) {
                shaString.Append(hashByte.ToString("x2"));
            }

            return shaString.ToString();
        }


        public static byte[] GetSHA256Bytes(string input) {
            SHA256Managed hashstring = new SHA256Managed();
            return hashstring.ComputeHash(Encoding.UTF8.GetBytes(input));
        }

    }
}
