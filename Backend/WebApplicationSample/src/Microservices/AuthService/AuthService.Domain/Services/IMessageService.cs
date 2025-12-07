using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthService.Domain.Models;

namespace AuthService.Domain.Services
{
    public interface IMessageService
    {
        public Task SendRegisterUserEvent(User user);
    }
}
