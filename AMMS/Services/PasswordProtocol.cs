using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace AMMS.Services
{
    public class PasswordProtocol
    {
        public static string PasswordSalt
        {
            get
            {
                // encryption service
                var rng = new RNGCryptoServiceProvider();

                // empty buffer
                var buff = new byte[64];

                // fill buffer with salt
                rng.GetBytes(buff);

                // convert salt to string and return
                return Convert.ToBase64String(buff);
            }
        }

        public static string CalculateHash(string password, string salt)
        {
            var bytes = KeyDerivation.Pbkdf2(salt+password, Convert.FromBase64String(salt), KeyDerivationPrf.HMACSHA512, 12345, 64);
            return Convert.ToBase64String(bytes);
        }
    }
}
