using System;
using System.Collections.Generic;

#nullable disable

namespace yplBackend.Model
{
    public partial class Combination
    {
        public Combination()
        {
            ElementInCombinations = new HashSet<ElementInCombination>();
            Ratings = new HashSet<Rating>();
        }

        public int IdCombination { get; set; }
        public string NameCombination { get; set; }
        public string Note { get; set; }
        public int? IdOccasion { get; set; }
        public string IdUser { get; set; }
        public bool? IsPosted { get; set; }

        public virtual Occasion IdOccasionNavigation { get; set; }
        public virtual Appuser IdUserNavigation { get; set; }
        public virtual ICollection<ElementInCombination> ElementInCombinations { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
    }
}
