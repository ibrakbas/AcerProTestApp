using AP.API.ErrorHandling;
using AP.Data.context;
using AP.Generic;
using AP.Generic.Services;
using AP.Utils.Auth;
using AP.Utils.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

//DbContext servis eklemesi
builder.Services
        .AddDbContext<APDbContext>(
                                optionsAction => optionsAction.UseSqlServer(builder.Configuration.GetConnectionString("Main")));

 



builder.Services.AddScoped<IUnitOfWork, UnitOfWork>(); 

builder.Services.AddTransient<ErrorMiddleware>();

builder.Services.AddAutoMapper(typeof(AP.Data.AssemblyReference).Assembly);

builder.Services.ApplyEasyRepository<AP.Data.context.APDbContext>();

builder.Services.AddScoped<IJWTProvider, JWTProvider>();


builder.Services.ConfigureOptions<AP.API.Options.JwtOptions>();

// Presentation katmaný için servis eklemesi
builder.Services.AddControllers()
    .AddApplicationPart(typeof(AP.Presentation.AssemblyReference).Assembly)
    .AddNewtonsoftJson(o =>
    {
        o.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
    });

builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder
                   .AllowAnyMethod()
                   .AllowAnyHeader()
                   .AllowCredentials()
                   .SetIsOriginAllowed(builder => true);
        });
});

var app = builder.Build();

app.UseErrorMiddleware();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapOpenApi();

app.UseCors("AllowAllOrigins"); // Enable CORS with the specified policy

app.UseSwagger(); // Enable the Swagger JSON endpoint
app.UseSwaggerUI(); // Enable the Swagger UI

app.Run();
app.Run();


