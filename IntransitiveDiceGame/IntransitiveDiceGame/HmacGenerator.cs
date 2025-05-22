using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace IntransitiveDiceGame
{
    public class HmacGenerator
    {
        private static readonly int keySize = 256;
        private static byte[] _key = new byte[keySize];
        public HmacGenerator() { }

        public static byte[] GenerateSecureKey()
        {
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(_key);
            }
            return _key;
        }

        public static byte[] GenerateHMAC(int randNumber, byte[] key)
        {
            using (var hmac = new HMACSHA3_256(key))
            {
                return hmac.ComputeHash(Encoding.UTF8.GetBytes(randNumber.ToString()));
            }
        }
    }
}
