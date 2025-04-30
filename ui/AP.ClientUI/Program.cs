var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient("ApiClient", client =>
{
    var baseUrl = builder.Configuration.GetSection("ApiSettings:BaseUrl").Value;
    client.BaseAddress = new Uri(baseUrl);
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder
                .AllowAnyOrigin()  
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

var app = builder.Build();
 


app.UseHttpsRedirection();
app.UseRouting();

app.MapControllers(); 
app.MapDefaultControllerRoute();  

app.UseAuthorization();

app.MapStaticAssets();

 
app.Run();
