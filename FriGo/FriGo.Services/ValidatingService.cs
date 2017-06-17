using System.Net;
using System.Text;
using FluentValidation;
using FluentValidation.Results;
using FriGo.Db.Models;
using FriGo.Interfaces.Dependencies;
using FriGo.ServiceInterfaces;

namespace FriGo.Services
{
    public class ValidatingService : IValidatingService, IRequestDependency
    {
        public bool IsValid<T>(IValidator validator,T entity)
        {
            ValidationResult result = UseValidator(validator, entity);

            return result.IsValid;
        }

        public Error GenerateError<T>(IValidator validator, T entity)
        {
            var stringBuilder = new StringBuilder();
            ValidationResult result = UseValidator(validator, entity);

            foreach (ValidationFailure error in result.Errors)
                stringBuilder.AppendLine(error.ErrorMessage);

            return new Error
            {
                Code = (int) HttpStatusCode.BadRequest,
                Message = stringBuilder.ToString()
            };
        }

        public HttpStatusCode GetStatusCode()
        {
            return HttpStatusCode.BadRequest;
        }

        private ValidationResult UseValidator<T>(IValidator validator, T entity)
        {
            return validator?.Validate(entity);
        }
    }
}