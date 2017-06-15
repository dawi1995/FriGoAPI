using FluentValidation;
using FriGo.Db.Models.Authentication;

namespace FriGo.Db.ModelValidators
{
    class RecipeValidator : AbstractValidator<RecipeBindingModel>
    {
        public RecipeValidator()
        {
            RuleFor(register => register.Description)
                .Length(0, 2000)
                .NotEmpty();

            RuleFor(register => register.Title)
                .Length(0, 200)
                .NotEmpty();
        }
    }
}
