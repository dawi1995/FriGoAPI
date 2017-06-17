using FluentValidation;
using FriGo.Db.Models.Authentication;
using FriGo.Db.ModelValidators.Interfaces;

namespace FriGo.Db.ModelValidators.Validators
{
    public class ChangePasswordValidator : AbstractValidator<ChangePasswordBindingModel>, IChangePasswordValidator
    {
        public ChangePasswordValidator()
        {
            const int minimalPasswordLength = 10;

            RuleFor(changePassword => changePassword.OldPassword)
                .NotEmpty();

            RuleFor(changePassword => changePassword.NewPassword)
                .Length(minimalPasswordLength, 100)
                .WithMessage(string.Format(Properties.Resources.PasswordLengthValidationMessage, minimalPasswordLength))
                .NotEmpty();

            RuleFor(changePassword => changePassword.ConfirmPassword)
                .Matches(changePassword => changePassword.NewPassword)
                .WithMessage(Properties.Resources.ConfirmPasswordValidationMessage);
        }
    }
}