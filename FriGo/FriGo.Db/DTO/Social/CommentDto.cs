﻿using System;
using FriGo.Db.DTO.Account;
using FriGo.Db.DTO.Recipe;

namespace FriGo.Db.DTO.Social
{
    public class CommentDto
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
        public UserStub User { get; set; }
        public RecipeStub Recipe { get; set; }
    }
}