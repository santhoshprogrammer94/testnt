﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Testnt.Main.Infrastructure.Data;

namespace Testnt.Main.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TestntDbContext>(cfg =>
            {
                cfg.UseNpgsql(configuration.GetConnectionString("PostgresTestntMainConnectionString"),
                    options =>
                    {
                        options.EnableRetryOnFailure(3);
                    });
            });

            return services;
        }
    }
}