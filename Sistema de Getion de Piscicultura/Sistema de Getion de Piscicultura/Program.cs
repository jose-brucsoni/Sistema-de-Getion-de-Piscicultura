using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Sistema_de_Getion_de_Piscicultura.Client.Pages;
using Sistema_de_Getion_de_Piscicultura.Components;
using Sistema_de_Getion_de_Piscicultura.Infraestructura;
using Sistema_de_Getion_de_Piscicultura.Servicios;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();
builder.Services.AddCascadingAuthenticationState();
builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/inicio-de-sesion";
        options.LogoutPath = "/auth/logout";
        options.ExpireTimeSpan = TimeSpan.FromHours(8);
        options.SlidingExpiration = true;
    });
builder.Services.AddAuthorization();
builder.Services.AddSingleton<IDbConnectionFactory, SqlConnectionFactory>();
builder.Services.AddScoped<Autenticacion_Service>();
builder.Services.AddScoped<Catalogos_Service>();
builder.Services.AddScoped<Crianza_Service>();
builder.Services.AddScoped<Lotes_Service>();
builder.Services.AddScoped<ParametrosAgua_Service>();
builder.Services.AddScoped<Inventario_Service>();
builder.Services.AddScoped<AlimentacionInventario_Service>();
builder.Services.AddScoped<Alertas_Service>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseAntiforgery();

app.MapPost("/auth/login", async (HttpContext httpContext, Autenticacion_Service autenticacionService) =>
{
    var form = await httpContext.Request.ReadFormAsync();
    var username = form["username"].ToString();
    var password = form["password"].ToString();
    var returnUrl = form["returnUrl"].ToString();

    var usuario = await autenticacionService.ValidarCredencialesAsync(username, password);
    if (usuario is null)
    {
        return Results.Redirect("/inicio-de-sesion?error=credenciales");
    }

    var claims = new List<Claim>
    {
        new(ClaimTypes.NameIdentifier, usuario.IdUsuario.ToString()),
        new(ClaimTypes.Name, usuario.Username),
        new(ClaimTypes.Role, usuario.RolNombre)
    };

    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
    var principal = new ClaimsPrincipal(identity);
    await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

    if (!string.IsNullOrWhiteSpace(returnUrl) && Uri.IsWellFormedUriString(returnUrl, UriKind.Relative))
    {
        return Results.Redirect(returnUrl);
    }

    return Results.Redirect("/principal");
}).DisableAntiforgery();

app.MapGet("/auth/logout", async (HttpContext httpContext) =>
{
    await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    return Results.Redirect("/inicio-de-sesion");
});

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Sistema_de_Getion_de_Piscicultura.Client._Imports).Assembly);

app.Run();
