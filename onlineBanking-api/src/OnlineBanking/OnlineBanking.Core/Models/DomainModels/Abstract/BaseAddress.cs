namespace OnlineBanking.Core.Models.DomainModels.Abstract
{
    public class BaseAddress : Entity
    {
        public string City { get; set; }

        public string Village { get; set; }

        public string HouseNumber { get; set; }

        public string RoomNumber { get; set; }
    }
}
