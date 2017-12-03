using Microsoft.AspNetCore.Rewrite;
using Swashbuckle.AspNetCore.Swagger;

namespace DemoDotNetCore.WebApi
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Models;
    using Repositories;


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
            services.AddMvc();
            
            services
                .AddDbContext<BloggingContext>(
                    options => options
                    .UseSqlServer(Configuration.GetConnectionString("DemoDotNetCoreDB")));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "DemoDotNetCore Web API", Version = "v1" });
            });

            services.AddScoped<Blogs>();
            services.AddScoped<Posts>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "DemoDotNetCore Web API");
            });

            app.UseMvc();

            var redirectRootToSwagger = new RewriteOptions()
                .AddRedirect("^$", "swagger");
            app.UseRewriter(redirectRootToSwagger);
        }
    }
}
