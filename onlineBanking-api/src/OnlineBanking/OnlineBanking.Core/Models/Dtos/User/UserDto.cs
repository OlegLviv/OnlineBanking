using System;

namespace OnlineBanking.Core.Models.Dtos.User
{
    public class UserDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime Birthday { get; set; }
        public string Email { get; set; }
    }
}
