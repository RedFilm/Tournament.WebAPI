using Microsoft.AspNetCore.Http;
using Tournaments.Domain.Exceptions.BaseExceptions;

namespace Tournaments.Domain.Exceptions
{
    public class BadRequestException : ExceptionWithStatusCode
	{
        public BadRequestException(string message) : base(message) 
        {
            
        }

        public override int StatusCode => StatusCodes.Status400BadRequest;
	}
}
