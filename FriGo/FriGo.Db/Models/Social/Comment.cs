using System;
using FriGo.Db.Models.Authentication;
using FriGo.Db.Models.Recipes;

namespace FriGo.Db.Models.Social
{
    public class Comment : Entity
    {
        public Comment()
        {
            CreatedAt = DateTime.Now;
        }

        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
        public byte[] Picture { get; set; }


        public virtual User User { get; set; }
        public virtual Recipe Recipe { get; set; }
    }
}