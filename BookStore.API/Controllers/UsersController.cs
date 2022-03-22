using AutoMapper;
using BookStore.API.Contexts;
using BookStore.API.DataTransferObjects.User;
using BookStore.API.Models;
using BookStore.API.Operations.Command.RefreshToken;
using BookStore.API.Operations.Command.User;
using BookStore.API.TokenOperations.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class UsersController: ControllerBase
    {
        private readonly IDbContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public UsersController(IDbContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
            _mapper = mapper;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Register([FromBody] CreateUserDto createUserDto) 
        {
            CreateUserCommand createUserCommand = new CreateUserCommand(_context, _mapper);
            createUserCommand.CreateUserDto = createUserDto;
            await createUserCommand.HandleAsync();

            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login([FromBody] CreateTokenDto createTokenDto) 
        {
            CreateTokenCommand createTokenCommand = new CreateTokenCommand(_context, _mapper, _configuration);
            createTokenCommand.CreateTokenDto = createTokenDto;
            var token = await createTokenCommand.HandleAsync();
            return Ok(token);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> RefreshToken([FromQuery] string token)
        {
            RefreshTokenCommand refreshTokenCommand = new RefreshTokenCommand(_context, _configuration);
            refreshTokenCommand.RefreshToken = token;
            var result = await refreshTokenCommand.HandleAsync();
            return Ok(result);
        }
    }
}

