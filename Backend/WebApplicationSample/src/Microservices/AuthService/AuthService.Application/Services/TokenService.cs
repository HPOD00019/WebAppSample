using AuthService.Domain.Models;
using AuthService.Domain.Services;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;


namespace AuthService.Application.Services
{
    
    public class TokenService : ITokenService
    {
        private ITokenEncoder _tokenEncoder;
        private ITokenEncrypter _tokenEncrypter;

        public TokenService(ITokenEncoder tokenEncoder, ITokenEncrypter tokenEncrypter)
        {
            _tokenEncoder = tokenEncoder;
            _tokenEncrypter = tokenEncrypter;
        }

        public Task<string> GenerateAccessToken(string refreshToken)
        {

            throw new NotImplementedException();
        }

        public Task<string> GenerateRefreshToken(User user)
        {

            //1. Make Our Json. How?
            //  1.1 Make header
            //  1.2 Make payload
            //  
            //  1.3 Encrypt our data (header and payload), result -> signature (elecronic sign) 
            //1.End -> We got our complete Json
            //2. Json we code and return coded version
            
            var tokenHandler = new JwtSecurityTokenHandler();


            var encrypter = RSA.Create();
            var claims = new[]
            {
                new Claim("UserId", user.Id.ToString()),
                new Claim("Role", user.Role.ToString()),
                
            };
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                //SigningCredentials = new SigningCredentials(key)
            };
            



            var alg = _tokenEncrypter.GetEncryptionType();
            var tokenHeader = new
            {
                type = "JWT",
                encryptionAlgorithm = alg,
            };

            var tokenBody = new
            {
                UserId = user.Id,
                Role = user.Role.ToString(),

            };

            throw new NotImplementedException();
        }
    }
}


