using Microsoft.AspNetCore.Identity;

namespace Tournaments.Domain.Exceptions
{
	public class AuthenticationFailedException : Exception
	{
		public SignInResult SignInResult { get; }
		public AuthenticationFailedException(string message, SignInResult signInResult)
			: base(message)
		{
			SignInResult = signInResult;
		}

	}
}
