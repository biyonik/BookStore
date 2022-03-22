using AutoMapper;
using BookStore.API.Contexts;
using BookStore.API.DataTransferObjects.User;
using Microsoft.EntityFrameworkCore;

namespace BookStore.API.Operations.Command.User
{
    public class CreateUserCommand
    {
        private readonly IDbContext _context;
        private readonly IMapper _mapper;
        public CreateUserDto CreateUserDto { get; set; }

        public CreateUserCommand(IDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task HandleAsync()
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Email == CreateUserDto.Email);
            if(user is not null) {
                throw new InvalidOperationException("Bu e-posta adresi ile daha önce sisteme kayıt olunmuş!");
            }
            user = _mapper.Map<Models.User>(CreateUserDto);
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }       
    }
}
