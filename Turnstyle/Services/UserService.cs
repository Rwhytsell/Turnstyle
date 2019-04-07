using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json.Linq;
using Turnstyle.Models;
using Sodium;

namespace Turnstyle.Services
{
    public class UserService
    {
        public static Tuple<string, string> LoginAccount(JObject data, DatabaseService dbs)
        {
            var username = data.GetValue("username").ToString();
            if(username.Any(c => !char.IsLetterOrDigit(c))) 
                return new Tuple<string, string>(null, "Username can only contain letters and numbers");
            
            var password = data.GetValue("password").ToString();
            var (account, error) = dbs.GetAccount(username).Result;
            
            if (error != null) return new Tuple<string, string>(null, error);

            if (CheckPassword(account.Password, password))
            {
                return new Tuple<string, string>(SessionService.NewSession(account), null);
            }
            return new Tuple<string, string>(null, "Password was incorrect.");
        }
        
        public static Tuple<Account, string> RegisterAccount(JObject data, DatabaseService dbs)
        {
            var (newAccount, error) = ParseAccount(data);
            if (error != null) return new Tuple<Account, string>(null, error);
            
            try
            {
                var account = dbs.SaveAccount(newAccount).Result;
                return new Tuple<Account, string>(account, null);
            }
            catch (Exception e)
            {
                return new Tuple<Account, string>(null, e.ToString());
            }  
        }
        
        private static Tuple<Account, string> ParseAccount(JObject data)
        {
            var newAccount = new Account
            {
                Username = data.GetValue("username").ToString(),
                Password = HashPassword(data.GetValue("password").ToString()),
                Email = data.GetValue("email").ToString(),
                CreatedOn = DateTime.Now,
                Lastlogin = DateTime.Now,
                Image = "https://picsum.photos/g/400/?random"
            };
            if(newAccount.Username.Any(c => !char.IsLetterOrDigit(c))) 
                return new Tuple<Account, string>(null, "Username can only contain letters and numbers");
            if(data.GetValue("password").ToString() != data.GetValue("password_confirmation").ToString()) 
                return new Tuple<Account, string>(null, "Password does not match confirmation.");
            if(!IsValidEmail(newAccount.Email))
                return new Tuple<Account, string>(null, "Email address is invalid.");
            
            return new Tuple<Account, string>(newAccount, null);
        }

        public static string HashPassword(string pass)
        {
            var hash = PasswordHash.ScryptHashString(pass, PasswordHash.Strength.Medium);
            return hash.Trim('\0');
        }

        public static bool CheckPassword( string hash, string password )
        {
            return PasswordHash.ScryptHashStringVerify(hash, password);
        }
        private static bool IsValidEmail(string email)
        {
            try {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch {
                return false;
            }
        }

    }
}
