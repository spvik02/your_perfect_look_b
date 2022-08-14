using System;
using System.Collections.Generic;

#nullable disable

namespace yplBackend.Model
{
    public partial class Appuser
    {
        public Appuser()
        {
            Combinations = new HashSet<Combination>();
            Elements = new HashSet<Element>();
            FriendIdFriendNavigations = new HashSet<Friend>();
            FriendIdUserNavigations = new HashSet<Friend>();
            Ratings = new HashSet<Rating>();
        }

        public string IdUser { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Psw { get; set; }
        public bool? IsAdmin { get; set; }
        public double? Height { get; set; }
        public double? Collar { get; set; }
        public double? Chest { get; set; }
        public double? Sleeve { get; set; }
        public double? Waist { get; set; }
        public double? InsideLeg { get; set; }
        public double? Hips { get; set; }

        public virtual ICollection<Combination> Combinations { get; set; }
        public virtual ICollection<Element> Elements { get; set; }
        public virtual ICollection<Friend> FriendIdFriendNavigations { get; set; }
        public virtual ICollection<Friend> FriendIdUserNavigations { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
    }
}
