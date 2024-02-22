using FluentValidation;
using Tournaments.Domain.Exceptions.BaseExceptions;
using Tournaments.Domain.Models;

namespace Tournaments.API.Extensions
{
	public static class ExceptionModelExtensions
	{
		public static ExceptionResponseModel GetModel(this ValidationException exception)
		{
			return new ExceptionResponseModel
			{
				Errors = exception.Errors.Select(x => $"'{x.PropertyName}': {x.ErrorMessage}").ToList()
			};
		}
		public static ExceptionResponseModel GetModel(this ExceptionWithStatusCode exception)
		{
			return new ExceptionResponseModel
			{
				Errors = [exception.Message],
			};
		}
		public static ExceptionResponseModel GetModel(this Exception exception)
		{
			var internalError = "Something went wrong";
			return new ExceptionResponseModel
			{
				Errors = [internalError]
			};
		}
	}
}
