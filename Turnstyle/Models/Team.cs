using System;
using System.Collections.Generic;

namespace Turnstyle.Models
{
    public class Team
    {
        public Team()
        {
            Teammember = new HashSet<Teammember>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }

        public virtual ICollection<Teammember> Teammember { get; set; }
    }
}
