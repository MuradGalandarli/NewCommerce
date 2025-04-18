﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using NewCommerce.Application.Abstractions.Configurations;
using NewCommerce.Application.Abstractions.Services;
using NewCommerce.Application.Abstractions.Storage;
using NewCommerce.Application.Abstractions.Storage.Azure;
using NewCommerce.Application.Abstractions.Storage.Local;
using NewCommerce.Application.Abstractions.Token;
using NewCommerce.Application.Services;
using NewCommerce.Infrastructure.Services;
using NewCommerce.Infrastructure.Services.Configurations;
using NewCommerce.Infrastructure.Services.Storage;
using NewCommerce.Infrastructure.Services.Storage.Local;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewCommerce.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices( this IServiceCollection services)
        {
            services.AddScoped<IStorageService, StorageService>();
            services.AddScoped<ITokenHandler,Services.TokenHandler>();
            services.AddScoped <IMailService,MailService>();
            services.AddScoped <IApplicationService, ApplicationService>();
            services.AddScoped<IQRCodeService, QRCodeService>();

            // services.AddScoped<IAzureStorage, AzureStorage> ();




        }

        public static void AddStorage<T> (this IServiceCollection service) where T : Storages ,IStorage
        {
            service.AddScoped<IStorage, T>();
        } 
    }
}
