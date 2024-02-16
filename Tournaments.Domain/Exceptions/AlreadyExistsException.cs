using Microsoft.AspNetCore.Http;
using Tournaments.Domain.Exceptions.BaseExceptions;

namespace Tournaments.Domain.Exceptions
{
	public class AlreadyExistsException : ExceptionWithStatusCode
    { 
        public AlreadyExistsException(string message) : base (message) 
        { 

        }

        public override int StatusCode => StatusCodes.Status409Conflict;
	}
}
