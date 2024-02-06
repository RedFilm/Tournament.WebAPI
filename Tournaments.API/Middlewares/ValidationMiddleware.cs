using FluentValidation;
using System.ComponentModel.DataAnnotations;
using Tournaments.Domain.Models;
using Tournaments.Domain.Validators;

namespace Tournaments.API.Middlewares
{
	public class ValidationMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly IValidator<TournamentModelValidator> _tournamentValidator;

		public ValidationMiddleware(RequestDelegate next, IValidator<TournamentModelValidator> tournamentValidator)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
			_tournamentValidator = tournamentValidator ?? throw new NullReferenceException(nameof(tournamentValidator));
        }

		public async Task Invoke(HttpContext context)
		{
			var method = context.Request.Method;

			if (method == HttpMethods.Post || method == HttpMethods.Post)
			{
				try
				{
					var body = await new System.IO.StreamReader(context.Request.Body).ReadToEndAsync();
					var model = JsonConvert.DeserializeObject<YourModelType>(body);
				}
				catch (System.ComponentModel.DataAnnotations.ValidationException ex)
				{

				}
			}
		}

		private void ValidateModel(TournamentModel model)
		{
			var validationResult = _touranmentValidator.Validate(model);

			if (!validationResult.IsValid)
			{
				// Если есть ошибки валидации, выбрасываем исключение
				throw new ValidationException(validationResult.Errors);
			}
		}

	}
}
