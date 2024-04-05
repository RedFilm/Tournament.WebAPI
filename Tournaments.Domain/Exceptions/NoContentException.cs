using Microsoft.AspNetCore.Http;
using Tournaments.Domain.Exceptions.BaseExceptions;

namespace Tournaments.Domain.Exceptions
{
	public class NoContentException : ExceptionWithStatusCode
	{
        public NoContentException(string message) : base (message)
        {
            
        }

        public override int StatusCode => StatusCodes.Status204NoContent;
    }
}
