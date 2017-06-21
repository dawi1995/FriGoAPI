using System;
using FriGo.Db.Models.Authentication;
using FriGo.Db.Models.Recipes;

namespace FriGo.Db.Models
{
    public class Image : Entity
    {
        public byte[] ImageBytes { get; set; }
        public Guid UserId { get; set; }
    }
}