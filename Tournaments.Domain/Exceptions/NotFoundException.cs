using Microsoft.AspNetCore.Http;
using Tournaments.Domain.Exceptions.BaseExceptions;

namespace Tournaments.Domain.Exceptions
{
    public class NotFoundException : ExceptionWithStatusCode
	{
        public NotFoundException(string message) : base(message)
        {
            
        }

        public override int StatusCode => StatusCodes.Status404NotFound;
    }
}
