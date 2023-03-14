using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Text;

namespace KanbanBackend
{
    public class PasswordHasher
    {
        public static string Hash(string password, string saltString)
        {
            var salt = Encoding.ASCII.GetBytes(saltString);

            var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password!,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));

            return hashed;
        }
    }
}
