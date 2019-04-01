using System;
using System.Collections.Generic;

namespace Turnstyle.Models
{
    public class Tag
    {
        public Tag()
        {
            Threadtags = new HashSet<Threadtags>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }

        public virtual ICollection<Threadtags> Threadtags { get; set; }
    }
}
