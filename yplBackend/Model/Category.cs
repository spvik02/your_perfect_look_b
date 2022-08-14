using System;
using System.Collections.Generic;

#nullable disable

namespace yplBackend.Model
{
    public partial class Category
    {
        public Category()
        {
            Elements = new HashSet<Element>();
        }

        public int IdCategory { get; set; }
        public string NameCategory { get; set; }

        public virtual ICollection<Element> Elements { get; set; }
    }
}
