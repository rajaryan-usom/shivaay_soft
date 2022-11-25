using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShivaaySoft.Contract;
using ShivaaySoft.Data;
using ShivaaySoft.Repositories;

namespace ShivaaySoft.Services
{
    public class ApplicationServicesConfiguration
    {
        public static void AddServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("shivaaySoftDb")));

            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();

        }
    }
}
