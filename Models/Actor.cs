using System;
using System.Collections.Generic;

namespace Projects.Models
{
    public partial class Actor
    {
        public int ActorId { get; set; }
        public string ActorName { get; set; }
        public int Age { get; set; }
        public string NetWorth { get; set; }
        public string MovieName { get; set; }

        public virtual Movie MovieNameNavigation { get; set; }
    }
}
