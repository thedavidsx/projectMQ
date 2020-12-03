using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace AdministradorEmailStandard.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email, string message, string subject);
    }
}
