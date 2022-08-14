using System;
using System.Collections.Generic;

#nullable disable

namespace yplBackend.Model
{
    public partial class Rating
    {
        public int IdRating { get; set; }
        public int? IdCombination { get; set; }
        public string IdUser { get; set; }
        public int? Rating1 { get; set; }

        public virtual Combination IdCombinationNavigation { get; set; }
        public virtual Appuser IdUserNavigation { get; set; }
    }
}
