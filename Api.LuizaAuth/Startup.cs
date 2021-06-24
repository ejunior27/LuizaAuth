using Api.LuizaAuth.Helpers;
using Domain.LuizaAuth.Configurations;
using Domain.LuizaAuth.Interfaces;
using Infra.LuizaAuth.Context;
using Infra.LuizaAuth.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Service.LuizaAuth.AutoMapper;
using Service.LuizaAuth.Services;

namespace Api.LuizaAuth
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

            ConfigureBusinessServices(services);
            ConfigureRepositories(services);

            services.AddDbContext<LuizaAuthContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")).EnableSensitiveDataLogging());

            services.AddAutoMapper(typeof(AutoMapperSetup));

            services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));
            services.Configure<ClientSettings>(Configuration.GetSection("ClientSettings"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.ConfigureCustomExceptionMiddleware();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public virtual void ConfigureRepositories(IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
        }

        public virtual void ConfigureBusinessServices(IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IEmailService, EmailService>();
        }
    }
}
