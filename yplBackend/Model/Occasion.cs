using System;
using System.Collections.Generic;

#nullable disable

namespace yplBackend.Model
{
    public partial class Occasion
    {
        public Occasion()
        {
            Combinations = new HashSet<Combination>();
        }

        public int IdOccasion { get; set; }
        public string NameOccasion { get; set; }

        public virtual ICollection<Combination> Combinations { get; set; }
    }
}
