using Sistema_de_Getion_de_Piscicultura.Client.Pages;
using Sistema_de_Getion_de_Piscicultura.Components;
using Sistema_de_Getion_de_Piscicultura.Servicios;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();
builder.Services.AddScoped<Lotes_Service>();
builder.Services.AddScoped<AlimentacionInventario_Service>();
builder.Services.AddScoped<Inventario_Service>();

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


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Sistema_de_Getion_de_Piscicultura.Client._Imports).Assembly);

app.Run();
