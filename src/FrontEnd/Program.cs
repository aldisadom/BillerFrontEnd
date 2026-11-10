using BillerContracts.Clients;
using FrontEnd.Components;
using Serilog;

namespace FrontEnd;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        bool detailedErrors = builder.Configuration.GetValue<bool>("DetailedErrors");

        builder.Services.AddBillerClients(builder.Configuration.GetSection("ClientsOptions"));

        // Add services to the container.
        builder.Services
            .AddRazorComponents()
            .AddInteractiveServerComponents();

        builder.Host.UseSerilog((context, configuration) => configuration
            .ReadFrom.Configuration(context.Configuration)
            .Enrich.FromLogContext());

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
        app.UseSerilogRequestLogging();
        app.UseAntiforgery();

        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        app.Run();
    }
}
