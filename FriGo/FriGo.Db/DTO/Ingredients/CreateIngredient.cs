using System;
using FriGo.Db.ModelValidators;

namespace FriGo.Db.DTO.Ingredients
{
    [FluentValidation.Attributes.Validator(typeof(CreateIngredientValidator))]
    public class CreateIngredient
    {
        public Guid UnitId { get; set; }
        public string Name { get; set; }
    }
}