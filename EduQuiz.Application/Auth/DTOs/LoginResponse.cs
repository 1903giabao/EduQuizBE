using EduQuiz.Application.Auth.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduQuiz.Application.Auth
{
    public class LoginResponse
    {
        public TokenModel Token { get; set; }
        public string Role { get; set; } = null!;
        public Guid AccountId { get; set; }
    }
}
