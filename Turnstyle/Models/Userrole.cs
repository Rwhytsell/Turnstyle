using System;
using System.Collections.Generic;

namespace Turnstyle.Models
{
    public class Userrole
    {
        public int Id { get; set; }
        public int Userid { get; set; }
        public int Roleid { get; set; }
        public int Grantedby { get; set; }

        public virtual Account GrantedbyNavigation { get; set; }
        public virtual Role Role { get; set; }
        public virtual Account User { get; set; }
    }
}
