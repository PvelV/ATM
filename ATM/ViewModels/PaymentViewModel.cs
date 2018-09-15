using ATM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATM.ViewModels
{
    public class PaymentViewModel
    {
        public int RecipientAccountId { get; set; }
        public Transaction Transaction{ get; set; }
        public decimal Balance { get; set; }
    }
}