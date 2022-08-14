using System;
using System.Collections.Generic;

#nullable disable

namespace yplBackend.Model
{
    public partial class Element
    {
        public Element()
        {
            ElementInCombinations = new HashSet<ElementInCombination>();
        }

        public int IdElement { get; set; }
        public string NameElement { get; set; }
        public int IdCategory { get; set; }
        public double? Price { get; set; }
        public string Note { get; set; }
        public string PicPath { get; set; }
        public string Pic { get; set; }
        public string IdUser { get; set; }

        public virtual Category IdCategoryNavigation { get; set; }
        public virtual Appuser IdUserNavigation { get; set; }
        public virtual ICollection<ElementInCombination> ElementInCombinations { get; set; }
    }
}
