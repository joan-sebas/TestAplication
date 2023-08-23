using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TestAplication.Tools
{
    public class Hasher
    {
        public static (string hash, string salt) HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                // Genera un salt único
                byte[] saltBytes = new byte[16];
                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(saltBytes);
                }
                string salt = Convert.ToBase64String(saltBytes);

                // Concatena el salt y la contraseña
                string saltedPassword = string.Concat(password, salt);

                // Calcula el hash
                byte[] passwordBytes = Encoding.UTF8.GetBytes(saltedPassword);
                byte[] hashBytes = sha256.ComputeHash(passwordBytes);
                string hash = Convert.ToBase64String(hashBytes);

                return (hash, salt);
            }
        }

        public static bool VerifyPassword(string password, string salt, string storedHash)
        {
            using (var sha256 = SHA256.Create())
            {
                // Concatena el salt y la contraseña
                string saltedPassword = string.Concat(password, salt);

                // Calcula el hash
                byte[] passwordBytes = Encoding.UTF8.GetBytes(saltedPassword);
                byte[] hashBytes = sha256.ComputeHash(passwordBytes);
                string hash = Convert.ToBase64String(hashBytes);

                return hash == storedHash;
            }
        }
    }
}
