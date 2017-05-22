using System;
using FriGo.Db.DTO.Account;

namespace FriGo.Db.DTO.Social
{
    public class CreateComment
    {        
        public Guid RecipeId { get; set; }     
        public string Text { get; set; }
        public string Base64Picture { get; set; }
    }
}