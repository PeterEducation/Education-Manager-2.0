using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services;
using Services.Mapping;
using Microsoft.AspNetCore.Mvc.Razor;
using System.Web.Mvc;

namespace EducationManager2
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
            services.AddControllersWithViews();
            //services.AddScoped(_ => new SchoolContext(Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped(_ => new SchoolContext(Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IGradeRepository, GradeRepository>();
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IGradeService, GradeService>();
            services.AddScoped<ICourseService, CourseService>();

            // Auto Mapper Configurations
            //services.AddSingleton<AutoMapper.IConfigurationProvider>(_ => new MapperConfiguration(configuration => _.GetServices<Profile>().ToList().ForEach(configuration.AddProfile)));
            services.AddSingleton<AutoMapper.IConfigurationProvider>(_ => new MapperConfiguration(configuration =>
            {
                configuration.AddProfile<GradeMappingProfile>();
                configuration.AddProfile<CourseMappingProfile>();
            }));
            services.AddSingleton<IMapper>(_ => new Mapper(_.GetRequiredService<AutoMapper.IConfigurationProvider>(), _.GetService));

            services.Configure<RazorViewEngineOptions>(options =>
            {
                options.AreaViewLocationFormats.Clear();
                options.AreaViewLocationFormats.Add("/Areas/{2}/Views/{0}.cshtml");
                //options.AreaViewLocationFormats.Add("/Views/Home/{0}.cshtml");
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            var configurationProvider = app.ApplicationServices.GetRequiredService<AutoMapper.IConfigurationProvider>();
            configurationProvider.AssertConfigurationIsValid();
        }
    }
}
