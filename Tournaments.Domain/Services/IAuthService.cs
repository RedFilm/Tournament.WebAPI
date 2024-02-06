using Tournaments.Domain.Models;

namespace Tournaments.Domain.Services
{
    public interface IAuthService
	{
		/// <summary>
		/// Authenticate the user.
		/// </summary>
		/// <param name="model">Model that contains user name and password</param>
		/// <returns>Result of authenticating. True if user successfully authenticated.</returns>
		Task<AuthenticationResultModel> LoginAsync(LoginModel model);

		/// <summary>
		/// Register the user.
		/// </summary>
		/// <param name="model">Model that contain user name, email, password, birthday and phone number.</param>
		/// <returns>Result of registring. True if user successfully registred.</returns>
		Task<bool> RegisterAsync(RegisterModel model);
	}
}