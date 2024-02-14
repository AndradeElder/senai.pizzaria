using Microsoft.AspNetCore.Authentication.Cookies;
using ProjetoEmTresCamadas.Pizzaria.Mvc.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Adicionar serviços de criação do HttpClient 
builder.Services.AddHttpClient();
builder.Services.AddSession(options =>
{
    options.Cookie.IsEssential = true; // make the session cookie essential
});

// Adicionar schema de autenticação
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie(options =>
{
    options.LoginPath = "/Login/Index"; // Defenir página de login
    options.LogoutPath = "/Login/Logout"; // Defenir página logout
    options.AccessDeniedPath = "/Login/Denied";
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();

app.UseRouting();
app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<ValidarTokenMiddleware>();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
