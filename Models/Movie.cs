using System;
using System.Collections.Generic;

namespace Projects.Models
{
    public partial class Movie
    {
        public Movie()
        {
            Actor = new HashSet<Actor>();
            Actress = new HashSet<Actress>();
        }

        public string MovieId { get; set; }
        public string MovieName { get; set; }
        public string Director { get; set; }
        public string Lead { get; set; }

        public virtual ICollection<Actor> Actor { get; set; }
        public virtual ICollection<Actress> Actress { get; set; }

        public static explicit operator Movie(string v)
        {
            throw new NotImplementedException();
        }
    }
}
