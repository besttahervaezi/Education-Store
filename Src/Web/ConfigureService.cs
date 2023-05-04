using Domain.Exceptions;
using Infrastructure.Persistance.Context;
using Infrastructure.Persistance.SeedData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Web;

public static class ConfigureService
{
    public static IServiceCollection AddWebConfigureService(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        ApiBehaivorOptions(builder);
        builder.Services.AddSwaggerGen();
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddDistributedMemoryCache();
        return builder.Services;
    }

    private static void ApiBehaivorOptions(WebApplicationBuilder builder)
    {
        builder.Services.Configure<ApiBehaviorOptions>(options =>
        {
            options.InvalidModelStateResponseFactory = ActionContext =>
            {
                var errors = ActionContext.ModelState.Where(e => e.Value.Errors.Count > 0)
                    .SelectMany(v => v.Value.Errors).Select(x => x.ErrorMessage).ToList();
                return new BadRequestObjectResult(new ApiToReturn(400, errors));
            };
        });
    }

    public static async Task<IApplicationBuilder> AddWebAppService(this WebApplication app)
    {
        var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;
        var loggerfactory = services.GetRequiredService<ILoggerFactory>();
        var context = services.GetRequiredService<ApplicationDbContext>();
        try
        {

            await context.Database.MigrateAsync();
            await GenerateFakeData.SeedDataAsync(context, loggerfactory);
        }
        catch (Exception e)
        {
            var logger = loggerfactory.CreateLogger<Program>();
            logger.LogError(e, "error is migration");
        }
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthorization();

        app.MapControllers();

       await app.RunAsync();
        return app;
    }
}