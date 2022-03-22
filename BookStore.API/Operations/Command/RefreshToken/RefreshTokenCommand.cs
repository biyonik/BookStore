using BookStore.API.Contexts;
using BookStore.API.TokenOperations;
using BookStore.API.TokenOperations.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.API.Operations.Command.RefreshToken
{
    public class RefreshTokenCommand
    {
        public string RefreshToken { get; set; }
        private readonly IConfiguration _configuration;
        private readonly IDbContext _context;

        public RefreshTokenCommand(IDbContext context, IConfiguration configuration)
        {
            _configuration = configuration;
            _context = context;
        }

        public async Task<Token> HandleAsync()
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.RefreshToken == RefreshToken && x.RefreshTokenExpireDate > DateTime.Now);
            if(user is not null) {
                // token yarat
                var tokenHandler = new TokenHandler(_configuration);
                Token token = tokenHandler.CreateAccessToken(user);
                user.RefreshToken = token.RefreshToken;
                user.RefreshTokenExpireDate = token.Expiration.AddMinutes(5);
                await _context.SaveChangesAsync();
                return token;
            }
            throw new InvalidOperationException("Geçerli bir refresh token bulunamadı");
        }
    }
}