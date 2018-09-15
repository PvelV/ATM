using ATM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATM.ViewModels
{
    public class TransactionViewModel
    {
        public Transaction Transaction { get; set; }
        public decimal Balance { get; set; }
    }
}