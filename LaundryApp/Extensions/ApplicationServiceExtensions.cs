using LaundryApp.Data;
using LaundryApp.Interfaces;
using LaundryApp.Mapper;
using LaundryApp.Repository;
using LaundryApp.Repository.IRepository;
using LaundryApp.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LaundryApp.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices (this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<ITokenService, TokenService>();

            services.AddDbContext<ApplicationDbContext>
                (options => options.UseLazyLoadingProxies().
                UseSqlServer(config.GetConnectionString("DefaultConnection")));

            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddAutoMapper(typeof(AccountMapper));

            services.AddAutoMapper(typeof(OrderDetailMapper));
            services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();

            services.AddAutoMapper(typeof(OrderMapper));
            services.AddScoped<IOrderRepository, OrderRepository>();

            services.AddAutoMapper(typeof(PaymentMapper));
            services.AddScoped<IPaymentRepository, PaymentRepository>();

            services.AddAutoMapper(typeof(ProductMapper));
            services.AddScoped<IProductRepository, ProductRepository>();


            return services;
        }
    }
}
