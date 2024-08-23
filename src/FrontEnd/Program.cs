using Clients;
using Domain.IOptions;
using FrontEnd.Components;
using FrontEnd.Capabilities;

namespace FrontEnd;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.Configure<ClientsOptions>(builder.Configuration.GetSection("ClientsOptions"));
        bool detailedErrors = builder.Configuration.GetValue<bool>("DetailedErrors");

        builder.Services.AddClients();

        // Add services to the container.
        builder.Services
            .ConfigureAutoMapper()
            .AddRazorComponents()
            .AddInteractiveServerComponents();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (detailedErrors)
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseStaticFiles();
        app.UseAntiforgery();

        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        app.Run();
    }
}
