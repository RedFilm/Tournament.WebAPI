using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Tournaments.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using Tournaments.Domain.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Options;
using Tournaments.Domain.Options;
using AutoMapper;
using Tournaments.Domain.Exceptions;
using Tournaments.Domain.Interfaces.Services;
using Tournaments.Domain.Enums;

namespace Tournaments.Application.Services
{
    public class AuthService : IAuthService
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signinManager;
		private readonly JwtOptions jwtOptions;
		private readonly IMapper _mapper;

		public AuthService(UserManager<AppUser> userManager, 
			SignInManager<AppUser> signinManager,
			IOptions<JwtOptions> options,
			IMapper mapper)
		{
			_userManager = userManager;
			_signinManager = signinManager;
			jwtOptions = options.Value;
			_mapper = mapper;
		}
		public async Task<bool> RegisterAsync(RegisterModel model)
		{
			var existingUser = _userManager.Users.FirstOrDefault(u => u.UserName == model.UserName);
			if (existingUser is not null)
				throw new BadRequestException("User with the same username already exists.");

			var user = _mapper.Map<AppUser>(model);

			await _userManager.AddToRoleAsync(user, IdentityRoles.User.ToString());
			var result = await _userManager.CreateAsync(user, model.Password);

			if (!result.Succeeded)
				throw new RegisterFailedException("Register failed");

			return result.Succeeded;
		}
		public async Task<AuthenticationResultModel> LoginAsync(LoginModel model)
		{
			var user = await _userManager.FindByNameAsync(model.UserName);

			if (user is null)
				throw new NotFoundException("User not found");

			var result = await _signinManager.PasswordSignInAsync(user, model.Password, false, false);

			if (!result.Succeeded)
				throw new AuthenticationFailedException("Authentication failed");

			var jwtToken = GenerateTokenAsync(model);

			return new AuthenticationResultModel { Token = jwtToken.Result};
		}

		private async Task<string> GenerateTokenAsync(LoginModel model)
		{
			var user = await _userManager.FindByNameAsync(model.UserName);
			
			var claims = new List<Claim>()
			{
				new Claim(ClaimTypes.NameIdentifier, user!.Id.ToString()),
				new Claim(ClaimTypes.Name, model.UserName),
				new Claim(ClaimTypes.Role, IdentityRoles.User.ToString())
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
