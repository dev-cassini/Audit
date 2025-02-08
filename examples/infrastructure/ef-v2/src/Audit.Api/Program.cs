using Audit.Api.Endpoints;
using Audit.Domain;
using Audit.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.JsonWebTokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen();

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer();

builder.Services.AddAuthorization();

JsonWebTokenHandler.DefaultInboundClaimTypeMap.Clear();

builder.Services
    .AddDomainTooling()
    .AddInfrastructure(builder.Configuration);

var app = builder.Build();

app.RegisterEndpoints();
app
    .UseAuthentication()
    .UseRouting()
    .UseCors()
    .UseAuthorization();

app.Services
    .UseInfrastructure();

app.UseHttpsRedirection();

app.Run();