using System;
using System.Text;
using System.Threading.Tasks;
using Konscious.Security.Cryptography;

namespace Spotify.Helpers
{
    public class Argon2iEncryption
    {
        public static async Task<string> HashPassword(string password)
        {
            var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
            {
                Salt = new byte[] { 0x69, 0x42, 0x99, 0x66, 0x15, 0x09, 0x00, 0x16, 0x69, 0x42, 0x99, 0x66, 0x15, 0x09, 0x00, 0x16 },
                DegreeOfParallelism = 12,
                Iterations = 4,
                MemorySize = 1024 * 1024
            };

            return Convert.ToBase64String(await argon2.GetBytesAsync(16));
        }
    }
}