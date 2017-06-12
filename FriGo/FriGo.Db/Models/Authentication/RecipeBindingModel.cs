using FluentValidation.Attributes;
using FriGo.Db.ModelValidators;

namespace FriGo.Db.Models.Authentication
{
    [Validator(typeof(RecipeValidator))]
    class RecipeBindingModel
    {
        public string Title { get; set; }
        public string Description { get; set; } 
    }
}
