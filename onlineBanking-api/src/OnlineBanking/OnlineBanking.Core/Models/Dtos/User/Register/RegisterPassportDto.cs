using Microsoft.AspNetCore.Http;

namespace OnlineBanking.Core.Models.Dtos.User
{
    public class RegisterPassportDto
    {
        public IFormFile FirsPage { get; set; }

        public IFormFile SecondPage { get; set; }

        public IFormFile ResidencePage { get; set; }
    }
}
