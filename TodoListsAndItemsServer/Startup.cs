using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoListsAndItemsServer.DataAccess;
using TodoListsAndItemsServer.Services;
using TodoListsAndItemsServer.Services.Repositories;

namespace TodoListsAndItemsServer
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // Here we resolve the dependancy injection
        public void ConfigureServices(IServiceCollection services)
        {
            //SQL:
            //In order to use the one below, install Microsoft.EntityFrameworkCore.SqlServer
            services.AddDbContext<DataContext>(
                options => options.UseSqlServer("name=ConnectionStrings:TodoListsAndItemsServer")); //get name from appsettings.json
            services.AddScoped<ITodosRepositoryService, SqlTodoRepositoryService>();

            //Json:
            //services.AddTransient<IItemDataReaderService, JsonDataReaderService>();//New Object for each call
            //services.AddScoped<ITodosRepositoryService, JsonTodoRepositoryService>();//Same Object for all calls

            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  builder =>
                                  {
                                      builder.WithOrigins("http://localhost:4200",
                                                          "http://localhost:5000")
                                                          .AllowAnyHeader()
                                                          .AllowAnyMethod();
                                  });
            });

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseCors(MyAllowSpecificOrigins);

            app.UseAuthorization();

            app.UseEndpoints(endpoints => //web api
            {
                endpoints.MapControllers(); //scans the app at runtime, add controllers
            });
        }
    }
}
