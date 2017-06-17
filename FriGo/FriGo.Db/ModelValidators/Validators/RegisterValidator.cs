using FluentValidation;
using FriGo.Db.DAL;
using FriGo.Db.Models.Authentication;
using FriGo.Db.ModelValidators.Interfaces;
using FriGo.Interfaces.Dependencies;

namespace FriGo.Db.ModelValidators.Validators
{
    public class RegisterValidator : AbstractDatabaseValidator<RegisterBindingModel>, IRegisterValidator, IRequestDependency
    {
        public RegisterValidator(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            RuleFor(register => register.Email)
                .Must((model, s) => IsFieldUnique<User>(user => user.Email == model.Email))
                .WithMessage(Properties.Resources.EmailExistsValidationMessage)
                .EmailAddress()
                .WithMessage(Properties.Resources.EmailRegexValidationMessage)
                .NotEmpty()
                .WithMessage(string.Format(Properties.Resources.EmptyGenericValidationMessage, nameof(RegisterBindingModel.Email)));

            RuleFor(register => register.Username)
                .Must((model, s) => IsFieldUnique<User>(user => user.UserName == model.Username))
                .WithMessage(Properties.Resources.UsernameExistsValidationMessage)
                .NotEmpty()
                .WithMessage(string.Format(Properties.Resources.EmptyGenericValidationMessage, nameof(RegisterBindingModel.Username)));

            const int minimalPasswordLength = 10;

            RuleFor(register => register.Password)
                .Length(minimalPasswordLength, 100)
                .WithMessage(string.Format(Properties.Resources.PasswordLengthValidationMessage, minimalPasswordLength))
                .NotEmpty()
                .WithMessage(string.Format(Properties.Resources.EmptyGenericValidationMessage,
                    nameof(ChangePasswordBindingModel.NewPassword)));

            RuleFor(register => register.ConfirmPassword)
                .Equal(register => register.Password)
                .NotEmpty()
                .WithMessage(Properties.Resources.ConfirmPasswordValidationMessage);
        }
    }
}