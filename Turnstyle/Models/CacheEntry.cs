using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Turnstyle.Models
{
    public class CacheEntry
    {
        private Account account;
        private DateTime TTL;

        public CacheEntry(Account user, DateTime end)
        {
            account = user;
            TTL = end;
        }

        public Account GetAccount()
        {
            return account;
        }

        public bool IsValid()
        {
            return TTL > DateTime.Now;
        }

        public void BumpTime(int days)
        {
            TTL = DateTime.Now.AddDays(days);
        }
    }
}
