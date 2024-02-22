using Tournaments.Domain.Options;
using Tournaments.Domain.Interfaces.Repositories;
using Tournaments.Application.Services;
using Tournaments.Persistence.Extensions;
using Tournaments.Persistence.Repositories;
using Tournaments.API.Extensions;
using Tournaments.Domain.Mapping;
using FluentValidation;
using Tournaments.Domain.Interfaces.Services;
using Tournaments.Persistence.Initializers;
using Microsoft.AspNetCore.Identity;
using Tournaments.Domain.Validators.AuthValidators;

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
builder.Services.AddApplication();

builder.Services.AddValidatorsFromAssemblyContaining<LoginModelValidator>();
builder.Services.AddAutoMapper(typeof(TournamentProfile).Assembly);

builder.Services.ConfigureAndValidate<JwtOptions>(builder.Configuration.Bind)
		.Configure<JwtOptions>(builder.Configuration.GetSection(JwtOptions.ConfigurationPath));

var jwtOptions = builder.Configuration.GetSection(JwtOptions.ConfigurationPath).Get<JwtOptions>();
builder.Services.AddJwtAuthentication(jwtOptions!);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
	var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<long>>>();

	await DbInitializer.InitializeRoles(roleManager);
}

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