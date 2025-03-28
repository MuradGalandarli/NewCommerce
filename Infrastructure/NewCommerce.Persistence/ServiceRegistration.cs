using ETicaretAPI.Persistence.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NewCommerce.Application;
using NewCommerce.Application.Abstractions.Services;
using NewCommerce.Application.Abstractions.Services.Authentications;
using NewCommerce.Application.Repositoryes;
using NewCommerce.Application.Repositoryes.Basket;
using NewCommerce.Application.Repositoryes.BasketItem;
using NewCommerce.Application.Repositoryes.CompletedOrder;
using NewCommerce.Application.Repositoryes.Endpoint;
using NewCommerce.Application.Repositoryes.Menu;
using NewCommerce.Domain.Entitys.Common;
using NewCommerce.Domain.Identity;
using NewCommerce.Persistence.Context;
using NewCommerce.Persistence.Repositoryes;
using NewCommerce.Persistence.Repositoryes.Basket;
using NewCommerce.Persistence.Repositoryes.BasketItem;
using NewCommerce.Persistence.Repositoryes.CompletedOrder;
using NewCommerce.Persistence.Repositoryes.Endpoint;
using NewCommerce.Persistence.Repositoryes.InvoiceFile;
using NewCommerce.Persistence.Repositoryes.Menu;
using NewCommerce.Persistence.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewCommerce.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services)
        {
          
            services.AddDbContext<NewCommerceDb>(options => options.UseNpgsql(Configuration.ConnectionString));

            services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.Password.RequiredLength = 3;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
            }).AddEntityFrameworkStores<NewCommerceDb>()
            .AddDefaultTokenProviders();

            services.AddScoped<ICustomerReadRepository, CustomerReadRepository>();
            services.AddScoped<IProductReadRepository, ProductReadRepository>();
            services.AddScoped<IOrderReadRepository, OrderReadRepository>();

            services.AddScoped<ICustomerWriteRepository, CustomerWriteRepository>();
            services.AddScoped<IProductWriteRepository, ProductWriteRepository>();
            services.AddScoped<IOrderWriteRepository, OrderWriteRepository>();
            
            services.AddScoped<IFileReadRepository, FileReadRepository>();
            services.AddScoped<IInvoiceFileReadRepository, InvoiceFileReadRepository>();
            services.AddScoped<IProductImageReadRepository, ProductImageReadRepository>();

            services.AddScoped<IFileWriteRepository, FileWriteRepository>();
            services.AddScoped<IInvoiceFileWriteRepository, InvoiceFileWriteRepository>();
            services.AddScoped<IProductImageWriteRepository, ProductImageWriteRepository>();


            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped <IExternalAuthentication, AuthService>();
            services.AddScoped<IInternalAuthentication, AuthService>();

            services.AddScoped<IBasketItemReadRepository, BasketItemReadRepository>();
            services.AddScoped<IBasketItemWriteRepository, BasketItemWriteRepository>();
            services.AddScoped<IBasketReadRepository, BasketReadRepository>();
            services.AddScoped<IBasketWriteRepository, BasketWriteRepository>();
            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<ICompletedOrderReadRepository, CompletedOrderReadRepository>();
            services.AddScoped<ICompletedOrderWriteRepository, CompletedOrderWriteRepository>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IMenuReadRepository,MenuReadRepository>();
            services.AddScoped<IMenuWriteRepository,MenuWriteRepository>();
            services.AddScoped<IEndpointWriteRepository,EndpointWriteRepository>();
            services.AddScoped<IEndpointReadRepository, EndpointReadRepository>();
            services.AddScoped<IAuthorizationEndpointService, AuthorizationEndpointService>();
            services.AddScoped<IProductService, ProductService>();



        }
    }
}
