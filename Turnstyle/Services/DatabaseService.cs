using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Turnstyle.Models;

namespace Turnstyle.Services
{
    public class DatabaseService
    {
        private triumphContext context;
        public DatabaseService(triumphContext triumphContext)
        {
            context = triumphContext;
        }

        public async Task<Tuple<Account, string>> GetAccount(string Username)
        {
            Account account;
            if (Username.Contains('@'))
            {
                account = await context.Account.SingleAsync(a => a.Email == Username);
            }
            else
            {
                account = await context.Account.SingleAsync(a => a.Username == Username);
            }
            return account == null ? new Tuple<Account, string>(null, "Account not found.") : new Tuple<Account, string>(account, null);
        }

        public async Task<Account> SaveAccount(Account newAccount)
        {
            context.Add(newAccount);
            context.SaveChanges();
            return newAccount;
        }
        
    }
}
