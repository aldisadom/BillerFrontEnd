using Clients.Clients;
using Clients.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Clients;

public static class DependencyInjection
{
    public static void AddClients(this IServiceCollection services)
    {
        services.AddHttpClient();

        //inject client
        services.AddScoped<IUserClient, UserClient>();
        services.AddScoped<IItemClient, ItemClient>();
        services.AddScoped<ICustomerClient, CustomerClient>();
        services.AddScoped<ISellerClient, SellerClient>();
    }
}