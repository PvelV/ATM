﻿using ATM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATM.Services
{
    public class CheckingAccountService
    {
        private ApplicationDbContext db;

        public CheckingAccountService(ApplicationDbContext dbContext)
        {
            db = dbContext;
        }

        public void CreateCheckingAccount(string firstName, string lastName, string userId, decimal initialBalance)
        {
            var checkingAccount = new CheckingAccount
            {
                FirstName = firstName,
                LastName = lastName,
                AccountNumber = (123456 + db.CheckingAccounts.Count() + 5).ToString().PadLeft(10, '0'),
                Balance = initialBalance,
                ApplicationUserId = userId
            };
            db.CheckingAccounts.Add(checkingAccount);
            db.SaveChanges();
        }
    }
}