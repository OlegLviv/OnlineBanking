using System;
using OnlineBanking.Core.Models.DomainModels.Abstract;

namespace OnlineBanking.Core.Models.DomainModels.User
{
    public class Passport : Entity
    {
        public virtual User User { get; set; }

        public Guid UserId { get; set; }

        public byte[] FirsPage { get; set; }

        public byte[] SecondPage { get; set; }

        public byte[] ResidencePage { get; set; }
    }
}
