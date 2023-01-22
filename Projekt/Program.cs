using Microsoft.EntityFrameworkCore;
using Projekt.DATA;

var builder = WebApplication.CreateBuilder(args); 

builder.Services.AddAuthentication("CookieAuthentication")
  .AddCookie("CookieAuthentication", config =>
  {
      config.Cookie.HttpOnly = true;
      config.Cookie.SecurePolicy = CookieSecurePolicy.None;
      config.Cookie.Name = "UserLoginCookie";
      config.LoginPath = "/Login/LogIn";
      config.Cookie.SameSite = SameSiteMode.Strict;
  });
// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDbContext<OgloszeniaContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("OgloszeniaContext") ?? throw new InvalidOperationException("Connection string 'OgloszeniaContext' not found.")));

builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".Ogloszenia.Session";
    //options.IdleTimeout = TimeSpan.FromSeconds(10);
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

CreateDbIfNotExists(app);

static void CreateDbIfNotExists(IHost host)
{
    using (var scope = host.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        try
        {
            var context = services.GetRequiredService<OgloszeniaContext>();
            context.Database.EnsureCreated();
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred creating the DB.");
        }
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseCookiePolicy();
app.UseAuthentication();

app.UseAuthorization();
app.UseSession();
app.MapRazorPages();

app.Run();
