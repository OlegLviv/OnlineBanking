using System;

namespace OnlineBanking.Core.Models.DomainModels.Abstract
{
    public class Entity : IEntity
    {
        public Guid Id { get; set; }

        public Entity()
        {
            Id = Guid.NewGuid();
        }
    }
}
