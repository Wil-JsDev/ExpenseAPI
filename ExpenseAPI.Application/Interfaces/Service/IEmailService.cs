using ExpenseAPI.Application.DTOs.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseAPI.Application.Interfaces.Service
{
    public interface IEmailService
    {
        Task Execute(EmailRequestDto requestDto);
    }
}
