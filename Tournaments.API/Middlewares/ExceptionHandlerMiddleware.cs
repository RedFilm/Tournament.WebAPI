using System.Net;
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

		public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

		public async Task Invoke(HttpContext context)
		{
			try
			{
				await _next.Invoke(context);
			}
			catch (ValidationException exception)
			{
				await HandleExceptionAsync(context,
					StatusCodes.Status400BadRequest,
					exception.GetModel());
			}
			catch (ExceptionWithStatusCode exception)
			{
				await HandleExceptionAsync(context,
					exception.StatusCode,
					exception.GetModel());
			}
			catch (Exception exception)
			{
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
