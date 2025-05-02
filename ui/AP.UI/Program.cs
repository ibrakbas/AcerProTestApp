var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<AP.UI.Services.IApiConnectionService, AP.UI.Services.ApiConnectionService>();
builder.Services.AddHttpClient<AP.UI.Services.IHttpClientService, AP.UI.Services.HttpClientService>();

// Session servisi  ekleniyor
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor(); // Session'a eriþim için gerekli

var app = builder.Build();

app.UseExceptionHandler("/Home/Error");
app.UseHsts();
app.UseHttpsRedirection();
app.UseRouting();
app.UseSession(); // Session middleware’i
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Login}/{id?}")
    .WithStaticAssets();


app.Run();
