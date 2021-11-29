using FluentValidation;
using FluentValidation.Results;
using System.Threading.Tasks;
using PaymentsDomain.SeedWork;

namespace PaymentsInfrastructure.Repositories
{
    public abstract class BaseRepository<TEntity>  where TEntity : Entity
    {
        private readonly AbstractValidator<TEntity> _validator;

        protected BaseRepository(AbstractValidator<TEntity> validator)
        {
            _validator = validator;
        }

        protected virtual async Task ValidateEntity(TEntity entity)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(entity);

            if (!validationResult.IsValid)
            {
                var message = "";

                foreach (var error in validationResult.Errors)
                {
                    message = $"{message}Error: {error.ErrorMessage}";
                }

                throw new ValidationException(message);
            }
        }
    }
}
