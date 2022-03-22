using System.IdentityModel.Tokens.Jwt;
using System.Text;
using BookStore.API.Models;
using BookStore.API.TokenOperations.Models;
using Microsoft.IdentityModel.Tokens;

namespace BookStore.API.TokenOperations
{
    public class TokenHandler
    {
        private readonly IConfiguration _configuration;
        public TokenHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Token CreateAccessToken(User user)
        {
            Token tokenModel = new Token();
            SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));
            SigningCredentials signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha512);
            tokenModel.Expiration = DateTime.Now.AddMinutes(15);

            var issuer = _configuration["Token:Issuer"];
            var audience = _configuration["Token:Audience"];

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                expires: tokenModel.Expiration,
                notBefore: DateTime.Now,
                signingCredentials: signingCredentials
            );

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var accessToken = jwtSecurityTokenHandler.WriteToken(jwtSecurityToken);
            tokenModel.AccessToken = accessToken;
            tokenModel.RefreshToken = CreateRefreshToken();
            return tokenModel;
        }

        public string CreateRefreshToken()
        {
            return Guid.NewGuid().ToString();
        }        
    }
}