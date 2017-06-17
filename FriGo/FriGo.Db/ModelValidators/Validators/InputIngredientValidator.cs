using FluentValidation;
using FriGo.Db.DAL;
using FriGo.Db.DTO.Ingredients;
using FriGo.Db.Models.Ingredients;
using FriGo.Db.ModelValidators.Interfaces;
using FriGo.Interfaces.Dependencies;

namespace FriGo.Db.ModelValidators.Validators
{
    public class InputIngredientValidator : AbstractDatabaseValidator<InputIngredient>, IInputIngredientValidator, IRequestDependency
    {
        public InputIngredientValidator(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            RuleFor(createIngredient => createIngredient.Name)
                .NotEmpty()
                .WithMessage(Properties.Resources.IngredientNameEmptyValidationMessage)
                .Length(1, 100)
                .WithMessage(Properties.Resources.IngredientNameLengthValidationMessage);

            RuleFor(createIngredient => createIngredient.UnitId)
                .NotEmpty()
                .WithMessage(string.Format(Properties.Resources.EmptyGenericValidationMessage,
                    nameof(CreateIngredient.UnitId)))
                .Must((createIngredient, guid) => EntityExists<Unit>(createIngredient.UnitId))
                .WithMessage(Properties.Resources.IngredientUnitExistMessage)
                .Must((createIngredient, guid) => IsFieldUnique<Ingredient>(
                    ingredient => ingredient.Name == createIngredient.Name))
                .WithMessage(Properties.Resources.IngredientNameUniqueValidationMessage);
        }
    }
}