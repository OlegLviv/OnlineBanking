using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineBanking.Core.Models.DomainModels
{
    public interface IEntity
    {
        [Key]
        Guid Id { get; set; }
    }
}
