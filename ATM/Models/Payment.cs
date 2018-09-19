using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ATM.Models
{
    
    public class Payment
    {
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Range(0, double.PositiveInfinity)]
        public decimal Amount { get; set; }
        
        [Required]
        public int SenderCheckingAccountId { get; set; }
        [Required]
        public string RecipientCheckingAccountNumber { get; set; }

        public virtual CheckingAccount SenderCheckingAccount { get; set; }
        public virtual CheckingAccount RecipientCheckingAccount { get; set; }
    }
}