using System.Security.Cryptography;
using System.Text;

namespace blog_BackEnd.Service
{
    public class Slug
    {
        public string GerarIdentificadorUnico(string nome)
        {
            using (var sha256 = SHA256.Create())
            {
                var randomBytes = new byte[8]; // 8 bytes para um hash curto
                RandomNumberGenerator.Fill(randomBytes);

                string input = $"{nome}-{BitConverter.ToString(randomBytes)}";
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));

                return Convert.ToBase64String(hashBytes)
                    .Replace("/", "") // Remover caracteres que podem causar problemas em URLs
                    .Replace("+", "")
                    .Substring(0, 12); // Tamanho controlado
            }
        }
    }

}