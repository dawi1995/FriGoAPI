using System;
using FluentValidation.Attributes;
using FriGo.Db.ModelValidators;
using FriGo.Db.ModelValidators.Validators;

namespace FriGo.Db.Models.Ingredients
{
    public class Ingredient : Entity
    {
        public string Name { get; set; }
        public Guid UnitId { get; set; }
        public virtual Unit Unit { get; set; }
    }
}