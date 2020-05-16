using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Utils.Security.JWT
{
    public class JWT
    {
        public byte[] Key { get; set; }
        public string Algorithm { get; set; }

        public JWT(byte[] key, string algorithm)
        {
            Key = key;
            Algorithm = algorithm;
        }

        public string GetToken(string identifier, string role, DateTime expires)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, identifier),
                    new Claim(ClaimTypes.Role, role)
                }),
                Expires = expires,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Key), Algorithm)
            };
            return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
        }
    }
}
