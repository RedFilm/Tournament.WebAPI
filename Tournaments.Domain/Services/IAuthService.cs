using Tournaments.Domain.Models;

namespace Tournaments.Domain.Services
{
	public interface IAuthService
	{
		Task<bool> Login(LoginModel vm);
		Task<bool> Register(LoginModel vm);
		string GenerateToken(LoginModel vm);
	}
}