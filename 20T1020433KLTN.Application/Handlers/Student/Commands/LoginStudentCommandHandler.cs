using _20T1020433KLTN.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handlers.Student.Commands
{
    public class LoginStudentCommandHandler : IRequestHandler<LoginStudentCommand, string>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IConfiguration _configuration;

        public LoginStudentCommandHandler(IStudentRepository studentRepository, IConfiguration configuration)
        {
            _studentRepository = studentRepository;
            _configuration = configuration;
        }

        public async Task<string> Handle(LoginStudentCommand request, CancellationToken cancellationToken)
        {
            var student = await _studentRepository.GetByUsername(request.Username);

            if (student == null || !VerifyPassword(student.PasswordHash, request.Password))
            {
                return null;
            }

            var token = GenerateJwtToken(student); // Function to generate JWT token
            return token;
        }

        // Function to verify password
        private bool VerifyPassword(string storedHash, string password)
        {
            // Implement your password verification logic here
        }

        // Function to generate JWT token
        private string GenerateJwtToken(Student student)
        {
            // Implement your JWT token generation logic here
        }
    }
}
