using emburns.Utils;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace emburns.PotatoModels.Auth
{
    public class AuthLogin
    {

        private readonly int _saltSize = 16;
        private readonly int _iterations = 8192;
        public string User { get; set; } = null!;
        public string Password { get; set; } = null!;

        /// <summary>
        /// Gets JWT token
        /// </summary>
        /// <returns></returns>
        public string GetToken(UserBaseQuery user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.RankName),
            };

            int expirationDays = ConfigurationBridge.ConfigManager.GetSection("Auth:JWTExpires").Get<int>();
            string jwtauthtoken = ConfigurationBridge.ConfigManager.GetSection("Auth:JWTKey").Get<string>();

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    jwtauthtoken
                    )
                );

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(expirationDays),
                signingCredentials: cred
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        /// <summary>
        /// Retrives user hashed password
        /// </summary>        
        /// <returns>Hashed password</returns>
        public string GetHash()
        {
            byte[] saltBuffer;
            byte[] hashBuffer;
            int hashSize = HashAlgorithm.Create(HashAlgorithmName.SHA256.Name).HashSize;

            using (var keyDerivation = new Rfc2898DeriveBytes(Password, _saltSize, _iterations, HashAlgorithmName.SHA256))
            {
                saltBuffer = keyDerivation.Salt;
                hashBuffer = keyDerivation.GetBytes(hashSize);

            }

            byte[] result = new byte[hashSize + _saltSize];
            Buffer.BlockCopy(hashBuffer, 0, result, 0, hashSize);
            Buffer.BlockCopy(saltBuffer, 0, result, hashSize, _saltSize);
            return Convert.ToBase64String(result);
        }

        public bool VerifyHashedPassword(string hashedPassword, string providedPassword)
        {
            byte[] hashedPasswordBytes = Convert.FromBase64String(hashedPassword);
            int hashSize = HashAlgorithm.Create(HashAlgorithmName.SHA256.Name).HashSize;


            if (hashedPasswordBytes.Length != hashSize + _saltSize)
            {
                return false;
            }

            byte[] hashBytes = new byte[hashSize];
            Buffer.BlockCopy(hashedPasswordBytes, 0, hashBytes, 0, hashSize);
            byte[] saltBytes = new byte[_saltSize];
            Buffer.BlockCopy(hashedPasswordBytes, hashSize, saltBytes, 0, _saltSize);

            byte[] providedHashBytes;
            using (var keyDerivation = new Rfc2898DeriveBytes(providedPassword, saltBytes, _iterations, HashAlgorithmName.SHA256))
            {
                providedHashBytes = keyDerivation.GetBytes(hashSize);
            }

            // check performance ¿¿            
            return hashBytes.SequenceEqual(providedHashBytes);
        }
    }
}
