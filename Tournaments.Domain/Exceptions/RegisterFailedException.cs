using Microsoft.AspNetCore.Identity;

namespace Tournaments.Domain.Exceptions
{
	public class RegisterFailedException : Exception
	{
        public IdentityResult IdentityResult { get; }
        public RegisterFailedException(string message, IdentityResult identityResult)
            : base (message)
        {
            IdentityResult = identityResult;
        }
    }
}
