using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Freelance_Api.Services
{
    public class HashingService
    {
        public string ValidateHash(string password)
        {
            return "Invalid Username or Password";
        }

        public string GenerateHash(string password)
        {
            var salt = new byte[128 / 8];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            var hashedpw = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password,
                salt,
                KeyDerivationPrf.HMACSHA1,
                10000,
                256 / 8));
            return hashedpw;
        }
    }
}