using Tournaments.Domain.Models;

namespace Tournaments.Domain.Services
{
	public interface IAuthService
	{
		Task<bool> Login(LoginModel model);
		Task<bool> Register(RegisterModel model);
		string GenerateToken(LoginModel model);
	}
}