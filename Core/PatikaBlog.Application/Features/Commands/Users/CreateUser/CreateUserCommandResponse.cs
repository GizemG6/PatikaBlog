using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatikaBlog.Application.Features.Commands.Users.CreateUser
{
    public class CreateUserCommandResponse
    {
        public bool Successed { get; set; }
        public string Message { get; set; }
    }
}
