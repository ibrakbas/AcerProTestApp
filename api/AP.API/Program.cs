using AP.API.ErrorHandling;
using AP.Data.context;
using AP.Generic;
using AP.Generic.Services;
using AP.Utils.Auth;
using AP.Utils.Service;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

//DbContext servis eklemesi
builder.Services
        .AddDbContext<APDbContext>(
                                optionsAction => optionsAction.UseSqlServer(builder.Configuration.GetConnectionString("Main")));



#region Services DI
//UnitOfWork  servis eklemesi, veritabaný iþlemleri için
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// ErrorHandling servis eklemesi, Hata yakalama iþlemleri için
builder.Services.AddTransient<ErrorMiddleware>();

// AutoMapper servis eklemesi, DTO ve Entity dönüþümleri için, AP.Data.mapping klasöründe yapýlýr
builder.Services.AddAutoMapper(typeof(AP.Data.AssemblyReference).Assembly);


// EasyRepository servis eklemesi, repository pattern için
builder.Services.ApplyEasyRepository<AP.Data.context.APDbContext>();

// JWT servis eklemesi, JWT iþlemleri için AP.Utils.Auth klasöründe yapýlýr
builder.Services.AddScoped<IJWTProvider, JWTProvider>();

// JWT ayarlarý konfigure edilir, AP.API.Options klasöründe yapýlýr
builder.Services.ConfigureOptions<AP.API.Options.JwtOptions>();
#endregion

// Presentation katmaný için servis eklemesi, controller ve endpoint'leri için AP.Presentation.Controllers klasöründe yapýlýr
builder.Services.AddControllers()
    .AddApplicationPart(typeof(AP.Presentation.AssemblyReference).Assembly)
    .AddNewtonsoftJson(o =>
    {
        o.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
    });

// Swagger ayarlarý, API dökümantasyonu için
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS ayarlarý, API'ye dýþarýdan eriþim için
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


