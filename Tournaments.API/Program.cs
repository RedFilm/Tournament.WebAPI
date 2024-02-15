using Tournaments.Domain.Options;
using Tournaments.Domain.Interfaces.Repositories;
using Tournaments.Application.Services;
using Tournaments.Persistence.Extensions;
using Tournaments.Persistence.Repositories;
using Tournaments.API.Extensions;
using Tournaments.Domain.Mapping;
using Tournaments.Domain.Validators;
using FluentValidation;
using Tournaments.Domain.Interfaces.Services;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//.AddJsonOptions(options =>
//{
//	options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
//	options.JsonSerializerOptions.WriteIndented = true;
//});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddCustomIdentity();

builder.Services.AddScoped<ITournamentRepository, TournamentRepository>();
builder.Services.AddScoped<ITeamRepository, TeamRepository>();
builder.Services.AddScoped<ITeamUserRepository, TeamUserRepository>();
builder.Services.AddScoped<ITournamentTeamRepository, TournamentTeamRepository>();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITournamentService, TournamentService>();
builder.Services.AddScoped<ITeamService, TeamService>();

builder.Services.AddValidatorsFromAssemblyContaining<LoginModelValidator>();
builder.Services.AddAutoMapper(typeof(TournamentProfile).Assembly);

builder.Services.ConfigureAndValidate<JwtOptions>(builder.Configuration.Bind)
		.Configure<JwtOptions>(builder.Configuration.GetSection(JwtOptions.ConfigurationPath));

var jwtOptions = builder.Configuration.GetSection(JwtOptions.ConfigurationPath).Get<JwtOptions>();
builder.Services.AddJwtAuthentication(jwtOptions!);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}
app.UseExceptionHandlerMiddleware();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();