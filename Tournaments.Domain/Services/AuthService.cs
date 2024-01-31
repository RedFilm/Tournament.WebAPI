using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Tournaments.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using Tournaments.Domain.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Options;
using Tournaments.Domain.Options;

namespace Tournaments.Domain.Services
{
	public class AuthService : IAuthService
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signinManager;
		private readonly JwtOptions jwtOptions;

		public AuthService(UserManager<AppUser> userManager, SignInManager<AppUser> signinManager, IOptions<JwtOptions> options)
		{
			_userManager = userManager;
			_signinManager = signinManager;
			jwtOptions = options.Value;
		}
		public async Task<bool> Register(LoginViewModel vm)
		{
			var user = new AppUser()
			{
				UserName = vm.UserName,
				Email = vm.Email
			};

			var result = await _userManager.CreateAsync(user, vm.Password);

			return result.Succeeded;
		}
		public async Task<bool> Login(LoginViewModel vm)
		{
			var user = await _userManager.FindByNameAsync(vm.UserName);

			if (user is null)
				return false;

			var result = await _signinManager.PasswordSignInAsync(user, vm.Password, false, false);

			return result.Succeeded;
		}

		public string GenerateToken(LoginViewModel vm)
		{
			var claims = new List<Claim>()
			{
				new Claim(ClaimTypes.Email, vm.Email),
				new Claim(ClaimTypes.Role, "User")
			};

			var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key));

			var signingCredentials = new SigningCredentials(
				secretKey, SecurityAlgorithms.HmacSha512Signature);

			var securityToken = new JwtSecurityToken(
				claims: claims,
				expires: DateTime.UtcNow.AddMinutes(60),
				issuer: jwtOptions.Issuer,
				audience: jwtOptions.Audience,
				signingCredentials: signingCredentials);

			var tokenString = new JwtSecurityTokenHandler().WriteToken(securityToken);

			return tokenString;
		}
	}
}
