using System.ComponentModel.DataAnnotations;

namespace OnlineBanking.Core.Models.Dtos.User
{
    public class LoginUserDto
    {
        [Required]
        [MinLength(2)]
        public string UserName { get; set; }

        [Required]
        [MinLength(6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
