using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Tournaments.Domain.Models;
using System.IdentityModel.Tokens.Jwt;
using Tournaments.Domain.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Tournaments.Domain.Services
{
	public class AuthService : IAuthService
	{
		private UserManager<AppUser> _usrMngr;
		private SignInManager<AppUser> _snMngr;
		private IConfiguration _config;

		public AuthService(UserManager<AppUser> usrMngr, SignInManager<AppUser> snMngr, IConfiguration config)
		{
			_usrMngr = usrMngr;
			_snMngr = snMngr;
			_config = config;
		}
		public async Task<bool> Register(LoginViewModel vm)
		{
			var user = new AppUser()
			{
				UserName = vm.UserName,
				Email = vm.Email
			};

			var result = await _usrMngr.CreateAsync(user, vm.Password);

			return result.Succeeded;
		}
		public async Task<bool> Login(LoginViewModel vm)
		{
			var user = await _usrMngr.FindByNameAsync(vm.UserName);

			if (user == null)
				return await Task.FromResult(false);

			var result = await _snMngr.PasswordSignInAsync(user, vm.Password, false, false);

			return result.Succeeded;
		}

		public string GenerateToken(LoginViewModel vm)
		{
			var claims = new List<Claim>()
			{
				new Claim(ClaimTypes.Email, vm.Email),
				new Claim(ClaimTypes.Role, "Admin")
			};

			var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("Jwt:Key").Value));

			var signingCredentials = new SigningCredentials(
				secretKey, SecurityAlgorithms.HmacSha512Signature);

			var securiryToken = new JwtSecurityToken(
				claims: claims,
				expires: DateTime.UtcNow.AddMinutes(60),
				issuer: _config.GetSection("Jwt:Issuer").Value,
				audience: _config.GetSection("Jwt:Audience").Value,
				signingCredentials: signingCredentials);

			var tokenString = new JwtSecurityTokenHandler().WriteToken(securiryToken);

			return tokenString;
		}
	}
}
