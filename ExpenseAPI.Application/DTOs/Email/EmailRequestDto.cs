using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseAPI.Application.DTOs.Email
{
    public class EmailRequestDto
    {
        public string To { get; set; }

        public string Body { get; set; }

        public string Subject { get; set; }
    }
}
