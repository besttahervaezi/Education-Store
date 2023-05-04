using Application;
using Infrastructure;
using Infrastructure.Persistance;
using Infrastructure.Persistance.SeedData;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Web;
using Web.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddApplicationServices();
builder.Services.AddInfraStructureServices(builder.Configuration);
builder.AddWebConfigureService();
var app = builder.Build();
app.UseMiddleware<MiddlewareExceptionHandler>();
app.UseStaticFiles();
await app.AddWebAppService().ConfigureAwait(false);