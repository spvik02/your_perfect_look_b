using System;
using System.Collections.Generic;

#nullable disable

namespace yplBackend.Model
{
    public partial class Friend
    {
        public int Id { get; set; }
        public string IdUser { get; set; }
        public string IdFriend { get; set; }
        public bool? IsAccepted { get; set; }

        public virtual Appuser IdFriendNavigation { get; set; }
        public virtual Appuser IdUserNavigation { get; set; }
    }
}
