using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace OnlineBanking.Core.Models.Dtos.User.Register
{
    public class RegisterTaxpayerСard
    {
        [Required]
        [MinLength(10)]
        public string Code { get; set; }

        public IFormFile Photo { get; set; }
    }
}
