using System;
using System.Collections.Generic;

namespace Turnstyle.Models
{
    public class Teammember
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public DateTime Lastchanged { get; set; }
        public int Teamid { get; set; }
        public int Changedby { get; set; }

        public virtual Account ChangedbyNavigation { get; set; }
        public virtual Team Team { get; set; }
    }
}
