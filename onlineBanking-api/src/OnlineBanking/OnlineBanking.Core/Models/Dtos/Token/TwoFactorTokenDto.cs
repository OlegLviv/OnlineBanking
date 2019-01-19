using System;

namespace OnlineBanking.Core.Models.Dtos.Token
{
    public class TwoFactorTokenDto
    {
        public Guid UserId { get; set; }
        public string TwoFactorCode { get; set; }
    }
}
