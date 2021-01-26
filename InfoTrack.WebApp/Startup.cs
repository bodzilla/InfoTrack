using System.Data;
using System.Data.SqlClient;
using InfoTrack.Core.Contracts;
using InfoTrack.Core.Models;
using InfoTrack.Core.Services;
using InfoTrack.Persistence;
using InfoTrack.Scraper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace InfoTrack.WebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            new DapperConfiguration().ConfigureMappings();
            services.AddControllersWithViews();

            var connection = Configuration.GetConnectionString("DefaultConnection");
            var resultLimit = int.Parse(Configuration.GetSection("ResultLimit").Value);

            services.AddScoped<IDbConnection>(x => new SqlConnection(connection));
            services.AddScoped<IScraper, GoogleScraper>(x => new GoogleScraper(resultLimit));
            services.AddScoped<IRepository<Search>, SearchRepository>();
            services.AddScoped<IRepository<Article>, ArticleRepository>();
            services.AddScoped<IScraperService, ScraperService>();
            services.AddScoped<ISearchService, SearchService>();
            services.AddScoped<IArticleService, ArticleService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}"); });
        }
    }
}
