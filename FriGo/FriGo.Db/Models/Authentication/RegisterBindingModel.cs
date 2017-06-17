using FluentValidation.Attributes;
using FriGo.Db.ModelValidators;
using FriGo.Db.ModelValidators.Validators;

namespace FriGo.Db.Models.Authentication
{
    public class RegisterBindingModel
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
