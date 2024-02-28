using Newtonsoft.Json;
using Tournaments.Domain.Models;
using FluentValidation;
using Tournaments.API.Extensions;
using Tournaments.Domain.Exceptions.BaseExceptions;

namespace Tournaments.API.Middlewares
{
	public class ExceptionHandlerMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<ExceptionHandlerMiddleware> _logger;

		public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
		{
			_next = next;
			_logger = logger;
		}

		public async Task Invoke(HttpContext context)
		{
			try
			{
				await _next.Invoke(context);
			}
			catch (ValidationException exception)
			{
				_logger.LogInformation("There's an validation error {Message}.", exception.Message);

				await HandleExceptionAsync(context,
					StatusCodes.Status400BadRequest,
					exception.GetModel());
			}
			catch (ExceptionWithStatusCode exception)
			{
				_logger.LogWarning("Bad request {Message}. Status code : {StatuseCode}.",
					exception.Message, exception.StatusCode);

				await HandleExceptionAsync(context,
					exception.StatusCode,
					exception.GetModel());
			}
			catch (Exception exception)
			{
				_logger.LogError("Internal server error {exception}", exception);

				await HandleExceptionAsync(context,
					StatusCodes.Status500InternalServerError,
					exception.GetModel());
			}
		}

		private async Task HandleExceptionAsync(HttpContext context,
			int statusCode,
			ExceptionResponseModel responseModel)
		{
			context.Response.StatusCode = statusCode;
			context.Response.ContentType = "application/json";

			var response = JsonConvert.SerializeObject(responseModel);

			await context.Response.WriteAsync(response);
		}
	}
}
