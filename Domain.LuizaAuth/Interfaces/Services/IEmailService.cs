using Domain.LuizaAuth.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.LuizaAuth.Interfaces
{
    public interface IEmailService
    {
        Task SendEmail(UserDto userViewModel, string subject, string body);
    }
}
