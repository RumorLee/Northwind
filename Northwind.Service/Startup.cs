using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Northwind.DomainLayer.Persistence.Contexts;
using Northwind.DomainLayer.Repositorys;
using Northwind.DomainLayer.Persistence.Repositorys;
using Northwind.DomainLayer.Services;
using Northwind.DomainLayer.Mapping;
using Northwind.ServicesLayer.Services;
using System.Reflection;
using System.IO;
using Microsoft.OpenApi.Models;

namespace Northwind
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDbContext<NorthwindContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Northwind")));

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IOrdersRepository, OrderRepository>();
            services.AddScoped<IOrdersServices, OrdersServices>();

            services.AddAutoMapper(typeof(ModelToResourceProfile));
            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
