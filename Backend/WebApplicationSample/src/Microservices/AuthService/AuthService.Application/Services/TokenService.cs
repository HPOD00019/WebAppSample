﻿using AuthService.Domain.Errors;
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
        private readonly string _privateRsaKey;
        private readonly string _publicRsaKey;

        private readonly RSA _rsaEncrypter;
        private readonly JwtSecurityTokenHandler _tokenHandler;

        public TokenService(string publicKey, string privateKey)
        {
            _privateRsaKey = privateKey;
            _publicRsaKey = publicKey;

            _tokenHandler = new JwtSecurityTokenHandler();

            _rsaEncrypter = RSA.Create();
            _rsaEncrypter.ImportFromPem(_publicRsaKey);
            _rsaEncrypter.ImportFromPem(_privateRsaKey);
        }

        public async Task<Result<string>> GenerateAccessToken(string refreshToken)
        {
            var validationResult = this.ValidateRefreshToken(refreshToken);
            if (!validationResult.IsSuccess) return validationResult;

            var token = _tokenHandler.ReadJwtToken(refreshToken);
            var userId = token.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId.Value)
            };

            var securityKey = new RsaSecurityKey(_rsaEncrypter);
            var sign = new SigningCredentials(securityKey, SecurityAlgorithms.RsaSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = sign,

            };


            var Token = _tokenHandler.CreateToken(tokenDescriptor);
            var jwt = _tokenHandler.WriteToken(Token);

            var tokenResult = Result<string>.OnSuccess(jwt);

            return tokenResult;
        }

        public async Task<Result<string>> GenerateRefreshToken(User user)
        {
            var securityKey = new RsaSecurityKey(_rsaEncrypter);


            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim("UserRole", user.Role.ToString())
            };

            var sign = new SigningCredentials(securityKey, SecurityAlgorithms.RsaSha256);

            var tokenDescriptor = new SecurityTokenDescriptor 
            {
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = sign,
                
            };
            

            
            var token = _tokenHandler.CreateToken(tokenDescriptor);
            var jwt = _tokenHandler.WriteToken(token);

            var tokenResult = Result<string>.OnSuccess(jwt);

            return tokenResult;
        }

        public Result<string> ValidateRefreshToken(string token)
        {
            var publicKey = new RsaSecurityKey(_rsaEncrypter.ExportParameters(false));

            var parameters = new TokenValidationParameters
            {
                IssuerSigningKey = publicKey,
                ValidateLifetime = false,
                ValidateIssuer = false,
                ValidateAudience = false,
            };

            try
            {
                _tokenHandler.ValidateToken(token, parameters, out _);
                var result = Result<string>.OnSuccess(token);

                return result;
            }
            catch (SecurityTokenException ex)
            {
                var error = new Error(ErrorCode.RefreshTokenInvalid);
                var result = Result<string>.OnFailure(error);

                return result;
            }
        }
    }
}


