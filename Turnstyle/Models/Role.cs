using System;
using System.Collections.Generic;

namespace Turnstyle.Models
{
    public class Role
    {
        public Role()
        {
            Rolepermissions = new HashSet<Rolepermissions>();
            Userrole = new HashSet<Userrole>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Rolepermissions> Rolepermissions { get; set; }
        public virtual ICollection<Userrole> Userrole { get; set; }
    }
}
