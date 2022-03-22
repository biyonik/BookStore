using AutoMapper;
using BookStore.API.Contexts;
using BookStore.API.DataTransferObjects.User;
using BookStore.API.TokenOperations;
using BookStore.API.TokenOperations.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.API.Operations.Command.User
{
    public class CreateTokenCommand
    {
        public CreateTokenDto CreateTokenDto { get; set; }
        private readonly IDbContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public CreateTokenCommand(IDbContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<Token> HandleAsync()
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == CreateTokenDto.Email && x.Password == CreateTokenDto.Password);
            if(user is not null) {
                // token yarat
                var tokenHandler = new TokenHandler(_configuration);
                Token token = tokenHandler.CreateAccessToken(user);
                user.RefreshToken = token.RefreshToken;
                user.RefreshTokenExpireDate = token.Expiration.AddMinutes(5);
                await _context.SaveChangesAsync();
                return token;
            }
            throw new InvalidOperationException("Kullanıcı adı veya şifre hatalı");
        }
    }
}