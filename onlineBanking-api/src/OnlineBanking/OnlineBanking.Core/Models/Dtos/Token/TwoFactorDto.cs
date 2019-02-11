using System;
using System.Collections.Generic;

namespace OnlineBanking.Core.Models.Dtos.Token
{
    public class TwoFactorDto
    {
        public Guid UserId { get; set; }

        public List<string> Roles { get; set; }
    }
}
