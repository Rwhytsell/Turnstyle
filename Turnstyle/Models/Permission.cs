using System;
using System.Collections.Generic;

namespace Turnstyle.Models
{
    public class Permission
    {
        public Permission()
        {
            Rolepermissions = new HashSet<Rolepermissions>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Rolepermissions> Rolepermissions { get; set; }
    }
}
