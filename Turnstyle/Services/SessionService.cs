using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Turnstyle.Models;

namespace Turnstyle.Services
{
    public static class SessionService
    {
        private static readonly RNGCryptoServiceProvider Provider = new RNGCryptoServiceProvider();
        private static Dictionary<string, CacheEntry> Cache = new Dictionary<string, CacheEntry>();
        private const int DaysUntilDelete = 7;
        private static DateTime _lastClean;
        
        // TODO: Max sessions?
        
        // Future: Having references to the account may use too much memory, if so, only store the account id

        public static string NewSession(Account account)
        {
            bool isKeyUsed;
            string key;
            do{
                key = GenerateKey();
                isKeyUsed = CheckToken(key);
            } while (isKeyUsed);

            var newEntry = new CacheEntry(account, DateTime.Now.AddDays(DaysUntilDelete));
            
            Cache.Add(key, newEntry);

            return key;
        }

        public static bool CheckToken(string key)
        {
            var isValid = Cache.TryGetValue(key, out var val);
            if (!isValid) return false;
            if (val.IsValid()) return true;
            InvalidateToken(key);
            return false;
        }
        
        public static Tuple<Account, string> GetValue(string key)
        {
            var isValid = Cache.TryGetValue(key, out var value);
            if (isValid)
            {
                if (!value.IsValid())
                {
                    InvalidateToken(key);
                    return new Tuple<Account, string>(null, "Session is outdated.");
                }
                value.BumpTime(DaysUntilDelete);
                return new Tuple<Account, string>(value.GetAccount(), null);
            }
            else
            {
                return new Tuple<Account, string>(null, "Session not found");
            }
        }

        public static void InvalidateToken(string key)
        {
            Cache.Remove(key);
            CleanOldSessions();
        }

        public static string GenerateKey()
        {
            var byteArray = new byte[32];
            Provider.GetBytes(byteArray);
            return BitConverter.ToString(byteArray, 0);
        }

        private static async void CleanOldSessions()
        {
            if (_lastClean > DateTime.Now.AddDays(-1)) return;
            _lastClean = DateTime.Now;
            await Task.Run(() =>
            {
                foreach (var (key, value) in Cache)
                {
                    if (!value.IsValid())
                    {
                        Cache.Remove(key);
                    }
                }
                
            });
        }
    }
}