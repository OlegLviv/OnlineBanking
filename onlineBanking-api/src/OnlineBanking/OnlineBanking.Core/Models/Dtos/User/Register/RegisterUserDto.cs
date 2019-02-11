using System;
using System.ComponentModel.DataAnnotations;
using OnlineBanking.Core.Models.DomainModels.User;

namespace OnlineBanking.Core.Models.Dtos.User.Register
{
    public class RegisterUserDto
    {
        [Required]
        [MinLength(2)]
        public string Name { get; set; }

        [Required]
        [MinLength(2)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        public DateTime? Birthday { get; set; }

        [Required]
        [Phone]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Required]
        public RegisterPassportDto Passport { get; set; }

        [Required]
        public RegisterTaxpayerСard TaxpayerСard { get; set; }
    }
}
