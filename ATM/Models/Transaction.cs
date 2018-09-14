using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ATM.Models
{
    public enum TransactionTypes { Withdrawal, Deposit}

    public class Transaction
    {
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Range(0, double.PositiveInfinity)]
        public decimal Amount { get; set; }
        
        [Required]
        public int CheckingAccountId { get; set; }
        public virtual CheckingAccount CheckingAccount { get; set; }

        [Required]
        public virtual TransactionTypes TransactionType { get; set; }
    }
}