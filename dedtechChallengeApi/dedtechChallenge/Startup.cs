using DedtechChallenge.Data;
using DedtechChallenge.Repositories;
using DedtechChallenge.Repositories.Interfaces;
using DedtechChallenge.Services;
using DedtechChallenge.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace DedtechChallenge
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DedtechChallenge", Version = "v1" });
            });


            services.AddControllers();

            services.AddDbContext<DedtechChallengeContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DedtechChallengeContext")));


            services.AddScoped<IProductRepository, ProductRepository>();


            services.AddScoped<IProductService, ProductService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());


            //app.UsePathBase("/api/v1");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
