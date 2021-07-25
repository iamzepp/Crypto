using System;
using CryptoApp.DataAccess.Common.Configuration;
using CryptoApp.DataAccess.Common.Db;
using CryptoApp.Domain.Services.Implementation;
using CryptoApp.Domain.Services.Interface;
using CryptoApp.Web.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using Npgsql;

namespace CryptoApp.Web
{
    public class Startup
    {
        private IConfiguration _configuration { get; }
        
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddScoped<ICommandService, CommandService>()
                .AddTelegramBotClient(_configuration)
                .AddControllers()
                .AddNewtonsoftJson(options => 
                {
                    options.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "CryptoApp.Web", Version = "v1"});
            });

            RegisterInjections(services);
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CryptoApp.Web v1"));
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void RegisterInjections(IServiceCollection services)
        {
            var configurationModel = 
                _configuration.GetSection("Settings").Get<ConfigurationModel>() 
                ?? throw new ArgumentException($"Not found \'Settings\' section.");

            services.AddSingleton<IConfig>(x => configurationModel);
            services.AddTransient<IMainDbConnection>(x => new DbProxy(new NpgsqlConnection(configurationModel.MainDbConnectionString)));
        }
    }
}