using System.Reflection;
using MudBlazor.Services;
using Orleans.Configuration;
using Softylines.OrleansApp.Blazor.Components;
using Softylines.OrleansApp.Blazor.Services;

var builder = WebApplication.CreateBuilder(args);
 
string sqlServerConnectionString = builder.Configuration["ConnectionStrings:SqlServer"] ?? string.Empty;

builder.Host
    .UseOrleansClient(client =>
    {
        client
            .UseAdoNetClustering(options =>
            {
                options.Invariant = "System.Data.SqlClient";
                options.ConnectionString = sqlServerConnectionString;
            })
            .Configure<ClusterOptions>(options =>
            {
                options.ClusterId = "dev";
                options.ServiceId = "aniss";
            });
    })
    .ConfigureLogging(logging => logging.AddConsole());
// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
 builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddMudServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();