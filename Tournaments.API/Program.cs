using Tournaments.Domain.Options;
using Tournaments.Domain.Interfaces.Repositories;
using Tournaments.Domain.Services;
using Tournaments.Persistence.Extensions;
using Tournaments.Persistence.Repositories;
using Tournaments.API.Extensions;
using Tournaments.Domain.Mapping;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddCustomIdentity();
builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddTransient<ITournamentRepository, TournamentRepository>();
builder.Services.AddAutoMapper(typeof(TournamentProfile).Assembly);
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(nameof(JwtOptions)));

var jwtOptions = builder.Configuration.GetSection(nameof(JwtOptions)).Get<JwtOptions>();

if(jwtOptions is not null)
	builder.Services.AddJwtAuthentication(jwtOptions);

var app = builder.Build();

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