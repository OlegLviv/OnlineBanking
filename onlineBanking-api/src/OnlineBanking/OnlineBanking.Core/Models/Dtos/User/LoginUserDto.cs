using System.ComponentModel.DataAnnotations;

namespace OnlineBanking.Core.Models.Dtos.User
{
    public class LoginUserDto
    {
        [Required]
        [RegularExpression("^\\+380[0-9]{9}$", ErrorMessage = "Invalid phone number")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Required]
        [MinLength(6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
