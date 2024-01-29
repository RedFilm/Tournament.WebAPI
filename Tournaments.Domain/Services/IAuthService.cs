using Tournaments.Domain.ViewModels;

namespace Tournaments.Domain.Services
{
	public interface IAuthService
	{
		Task<bool> Login(LoginViewModel vm);
		Task<bool> Register(LoginViewModel vm);
		string GenerateToken(LoginViewModel vm);
	}
}