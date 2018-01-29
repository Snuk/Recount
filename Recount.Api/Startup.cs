using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Recount.DataAccess.Options;
using Recount.DataAccess.Providers;
using Swashbuckle.AspNetCore.Swagger;

namespace Recount.Api
{
    public class Startup
    {
        private const string ServiceName = "Recount API";
        private const string ServiceVersion = "v1";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddSwaggerGen(c => { c.SwaggerDoc(ServiceVersion, new Info { Title = ServiceName, Version = ServiceVersion }); });

          //  services.AddScoped<ApiExceptionFilter>();

            services.Configure<MongoOptions>(Configuration.GetSection("Mongo"))
                .AddSingleton<MongoFunctionsProvider>()
                .AddSingleton<MongoVariablesProvider>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint($"/swagger/{ServiceVersion}/swagger.json", $"{ServiceName} {ServiceVersion}"); });
        }
    }
}
