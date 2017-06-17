using FluentValidation;
using FriGo.Db.Models;

namespace FriGo.ServiceInterfaces
{
    public interface IValidatingService
    {
        bool IsValid<T>(AbstractValidator<T> validator, T entity);
        Error GenerateError<T>(AbstractValidator<T> validator, T entity);
    }
}