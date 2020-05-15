using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Utils.Security.JWT
{
    public class JWT
    {
        public byte[] Key { get; set; }
        public String Algorithm { get; set; }
        public DateTime Expires { get; set; }

        public JWT(byte[] key, String algorithm, DateTime expires)
        {
            Key = key;
            Algorithm = algorithm;
            Expires = expires;
        }

        public String GetToken(String identifier, String role)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, identifier),
                    new Claim(ClaimTypes.Role, role)
                }),
                Expires = this.Expires,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Key), Algorithm)
            };
            return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
        }
    }
}
