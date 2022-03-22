using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.API.DataTransferObjects.User
{
    public class CreateTokenDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}