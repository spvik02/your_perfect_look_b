using System;
using System.Collections.Generic;

#nullable disable

namespace yplBackend.Model
{
    public partial class ElementInCombination
    {
        public int Id { get; set; }
        public int? IdElement { get; set; }
        public int? IdCombination { get; set; }

        public virtual Combination IdCombinationNavigation { get; set; }
        public virtual Element IdElementNavigation { get; set; }
    }
}
