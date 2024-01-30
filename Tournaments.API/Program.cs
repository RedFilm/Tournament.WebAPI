using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Tournaments.Domain.Options;
using Tournaments.Domain.Interfaces.Repositories;
using Tournaments.Domain.Services;
using Tournaments.Persistence.Extensions;
using Tournaments.Persistence.Repositories;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddCustomIdentity();
builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddTransient<ITournamentRepository, TournamentRepository>();
//builder.Services.Configure<ConnectionStringOptions>(builder.Configuration.GetSection("ConnectionStrings"));
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JwtOptions"));

builder.Services.AddAuthentication(options =>
{
	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
	options.TokenValidationParameters = new TokenValidationParameters()
	{
		ValidateActor = true,
		ValidateIssuer = true,
		ValidateAudience = true,
		RequireExpirationTime = true,
		ValidateIssuerSigningKey = true,
		ValidIssuer = builder.Configuration.GetSection("Jwt:Issuer").Value,
		ValidAudience = builder.Configuration.GetSection("Jwt:Audience").Value,
		IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
			builder.Configuration.GetSection("Jwt:Key").Value))
	};
});


var app = builder.Build();

var jwtOptions = app.Services.GetRequiredService<IOptions<JwtOptions>>().Value;
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
