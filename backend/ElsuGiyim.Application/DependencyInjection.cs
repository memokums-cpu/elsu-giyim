using ElsuGiyim.Application.Auth;
using ElsuGiyim.Application.Categories;
using ElsuGiyim.Application.Products;
using Microsoft.Extensions.DependencyInjection;

namespace ElsuGiyim.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IAuthService, AuthService>();
        return services;
    }
}
