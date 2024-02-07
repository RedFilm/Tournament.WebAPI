using System.Net;
using System;
using System.Security.Authentication;
using Tournaments.Domain.Exceptions;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

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
			catch (Exception ex)
			{
				await HandleExceptionAsync(context, ex);
			}
		}

		private async Task HandleExceptionAsync(HttpContext context, Exception exception)
		{
			context.Response.ContentType = "application/json";
			var statusCode = GetStatusCode(exception);

			var result = JsonConvert.SerializeObject(new
			{
				StatusCode = statusCode,
				ErrorMessage = exception.Message,
				Details = GetDetails(exception)
			});

			context.Response.StatusCode = statusCode;
			await context.Response.WriteAsync(result);
		}

		private int GetStatusCode(Exception exception)
		{
			if (exception is AuthenticationFailedException)
			{
				return StatusCodes.Status400BadRequest;
			}
			else if (exception is RegisterFailedException)
			{
				return StatusCodes.Status400BadRequest;
			}
			else if (exception is NotFoundException)
			{
				return StatusCodes.Status404NotFound;
			}
			else if (exception is BadRequestException)
			{
				return StatusCodes.Status400BadRequest;
			}
			else
			{
				return StatusCodes.Status500InternalServerError;
			}
		}

		private string GetDetails(Exception exception)
		{
			if (exception is AuthenticationFailedException authEx)
			{
				return authEx.SignInResult.ToString();
			}
			else if (exception is RegisterFailedException regEx)
			{
				return regEx.IdentityResult.ToString();
			}

			return string.Empty;
		}












		//public async Task Invoke(HttpContext context)
		//{
		//	try
		//	{
		//		await _next.Invoke(context);
		//	}
		//	catch (AuthenticationFailedException ex)
		//	{
		//		context.Response.ContentType = "application/json";
		//		var statusCode = StatusCodes.Status400BadRequest;

		//		var result = JsonConvert.SerializeObject(new
		//		{
		//			StatusCode = statusCode,
		//			ErrorMessage = ex.Message,
		//			Details = ex.SignInResult
		//		});
				
		//		context.Response.StatusCode = statusCode;
		//		await context.Response.WriteAsync(result);
		//	}
		//	catch (RegisterFailedException ex)
		//	{
		//		context.Response.ContentType = "application/json";
		//		var statusCode = StatusCodes.Status400BadRequest;

		//		var result = JsonConvert.SerializeObject(new
		//		{
		//			StatusCode = statusCode,
		//			ErrorMessage = ex.Message,
		//			Details = ex.IdentityResult
		//		});

		//		context.Response.StatusCode = statusCode;
		//		await context.Response.WriteAsync(result);
		//	}
		//	catch (NotFoundException ex)
		//	{
		//		context.Response.ContentType = "application/json";
		//		var statusCode = StatusCodes.Status404NotFound;

		//		var result = JsonConvert.SerializeObject(new
		//		{
		//			StatusCode = statusCode,
		//			ErrorMessage = ex.Message,
		//		});

		//		context.Response.StatusCode = statusCode;
		//		await context.Response.WriteAsync(result);
		//	}
		//	catch (BadRequestException ex)
		//	{
		//		context.Response.ContentType = "application/json";
		//		var statusCode = StatusCodes.Status400BadRequest;

		//		var result = JsonConvert.SerializeObject(new
		//		{
		//			StatusCode = statusCode,
		//			ErrorMessage = ex.Message
		//		});

		//		context.Response.StatusCode = statusCode;
		//		await context.Response.WriteAsync(result);
		//	}
		//	catch (Exception ex) 
		//	{
		//		context.Response.ContentType = "application/json";
		//		var statusCode = StatusCodes.Status500InternalServerError;

		//		var result = JsonConvert.SerializeObject(new
		//		{
		//			StatusCode = statusCode,
		//			ErrorMessage = ex.Message
		//		});

		//		context.Response.StatusCode = statusCode;
		//		await context.Response.WriteAsync(result);
		//	}
		//}
	}
}
