using FluentValidation;
using FriGo.Db.Models.Authentication;
using FriGo.Db.ModelValidators.Interfaces;
using FriGo.Interfaces.Dependencies;

namespace FriGo.Db.ModelValidators.Validators
{
    public class ChangePasswordValidator : AbstractValidator<ChangePasswordBindingModel>, IChangePasswordValidator, IRequestDependency
    {
        public ChangePasswordValidator()
        {
            const int minimalPasswordLength = 10;

            RuleFor(changePassword => changePassword.OldPassword)
                .NotEmpty()
                .WithMessage(string.Format(Properties.Resources.EmptyGenericValidationMessage, nameof(ChangePasswordBindingModel.OldPassword)));

            RuleFor(changePassword => changePassword.NewPassword)
                .Length(minimalPasswordLength, 100)
                .WithMessage(string.Format(Properties.Resources.PasswordLengthValidationMessage, minimalPasswordLength))
                .NotEmpty()
                .WithMessage(string.Format(Properties.Resources.EmptyGenericValidationMessage,
                    nameof(ChangePasswordBindingModel.NewPassword)));

            RuleFor(changePassword => changePassword.ConfirmPassword)
                .Matches(changePassword => changePassword.NewPassword)
                .WithMessage(Properties.Resources.ConfirmPasswordValidationMessage);
        }
    }
}