using ATM.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ATM.ViewModels
{
    public class PaymentViewModel
    {
        [Required]
        [StringLength(10)]
        [Column(TypeName = "varchar")]
        [RegularExpression(@"\d{6,10}", ErrorMessage = "Account # must be between 6 and 10 digits ")]
        [Display(Name = "Recipient Account")]
        public string RecipientAccountNumber { get; set; }
        [Required]
        public Transaction Transaction{ get; set; }

        public decimal Balance { get; set; }


        List<CheckingAccountDTO> Accounts { get; set; }
    }
}