using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineBanking.Core.Models.DomainModels.Abstract
{
    public interface IEntity
    {
        [Key]
        Guid Id { get; set; }
    }
}
