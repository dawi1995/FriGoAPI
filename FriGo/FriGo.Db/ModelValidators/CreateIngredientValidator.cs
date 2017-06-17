using FluentValidation;
using FriGo.Db.DTO.Ingredients;

namespace FriGo.Db.ModelValidators
{
    public class CreateIngredientValidator : AbstractValidator<CreateIngredient>
    {
        public CreateIngredientValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
           
            RuleFor(createIngredient => createIngredient.Name)
                .NotEmpty()
                .WithMessage(Properties.Resources.IngredientNameEmptyValidationMessage)
                .Length(1, 100)
                .WithMessage(Properties.Resources.IngredientNameLengthValidationMessage);

            RuleFor(createIngredient => createIngredient.UnitId)
                .NotEmpty()
                .WithMessage(string.Format(Properties.Resources.EmptyGenericValidationMessage,
                    nameof(CreateIngredient.UnitId)));
        }
    }
}