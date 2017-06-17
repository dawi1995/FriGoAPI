using System;
using FluentValidation.Attributes;
using FriGo.Db.ModelValidators;

namespace FriGo.Db.Models.Ingredients
{
    [Validator(typeof(CreateIngredientValidator))]
    public class Ingredient : Entity
    {
        public string Name { get; set; }
        public Guid UnitId { get; set; }
        public virtual Unit Unit { get; set; }
    }
}