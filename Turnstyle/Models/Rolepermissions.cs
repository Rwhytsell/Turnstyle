using System;
using System.Collections.Generic;

namespace Turnstyle.Models
{
    public class Rolepermissions
    {
        public int Id { get; set; }
        public int Roleid { get; set; }
        public int Permissionid { get; set; }

        public virtual Permission Permission { get; set; }
        public virtual Role Role { get; set; }
    }
}
