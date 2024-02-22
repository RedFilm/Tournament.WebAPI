using Tournaments.Domain.Models;
using Tournaments.Domain.Exceptions;

namespace Tournaments.Domain.Interfaces.Services
{
	public interface IAuthService
	{
		/// <summary>
		/// Authenticate the user.
		/// </summary>
		/// <param name="model">Model that contains user name and password.</param>
		/// <returns>Result of authenticating. Return model contains jwt token if user successfully authenticated.</returns>
		/// <exception cref="NotFoundException"></exception>
		/// <exception cref="AuthenticationFailedException"></exception>
		Task<AuthenticationResultModel> LoginAsync(LoginModel model);

		/// <summary>
		/// Register the user.
		/// </summary>
		/// <param name="model">Model that contain user name, email, password, birthday and phone number.</param>
		/// <returns>Result of registering. True if user successfully registered.</returns>
		/// <exception cref="RegisterFailedException"></exception>
		Task<bool> RegisterAsync(RegisterModel model);
	}
}