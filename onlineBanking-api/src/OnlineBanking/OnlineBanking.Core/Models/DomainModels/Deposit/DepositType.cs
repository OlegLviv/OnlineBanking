using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using OnlineBanking.Core.Models.DomainModels.Abstract;

namespace OnlineBanking.Core.Models.DomainModels.Deposit
{
    public class DepositType : Entity
    {
        [Required]
        public string Currency { get; set; }

        [Range(0, int.MaxValue)]
        public int Months { get; set; }

        [Required]
        public string Name { get; set; }

        [Range(0, 100)]
        public double Percentages { get; set; }

        public virtual List<Deposit> Deposits { get; set; }
    }
}
