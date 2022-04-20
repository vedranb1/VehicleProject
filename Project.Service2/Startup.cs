using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Ninject;
using Project.Service.Bindings;
using Project.Service.Data;
using Project.Service.Repository;
using Project.Service.Repository.IRepository;
using Project.Service.VehicleMapper;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;

namespace Project.Service
{
    public class Startup
    {

        public static void RegisterComponents()
        {
            IKernel _standardKernel = new StandardKernel();
            _standardKernel.Bind<IVehicleModelRepository>().To<VehicleModelRepository>();
            _standardKernel.Load(new Bindings.Bindings());
            _standardKernel.GetAll<IVehicleModelRepository>();
            _standardKernel.GetAll<IVehicleMakeRepository>();
        }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //DI.Initialize();
            services.AddTransient<IVehicleMakeRepository, VehicleMakeRepository>();
            services.AddTransient<IVehicleModelRepository, VehicleModelRepository>();
            
            services.AddAutoMapper(typeof(VehicleMappings));

            services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ReportApiVersions = true;
            });

            services.AddVersionedApiExplorer(options => options.GroupNameFormat = "'v'VVV");
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("VehicleProjectAPISpec",
                    new Microsoft.OpenApi.Models.OpenApiInfo()
                    {
                        Title = "Vehicle Project API",
                        Version = "1",
                        Description = "Vehicle Project API",
                        Contact = new Microsoft.OpenApi.Models.OpenApiContact()
                        {
                            Email = "vedran.baricevic95@gmail.com",
                            Name = "Vedran Baricevic",
                            Url = new Uri("https://github.com/vedranb1?tab=repositories")
                        },
                        License = new Microsoft.OpenApi.Models.OpenApiLicense()
                        {
                            Name = "MIT License",
                            Url = new Uri("https://en.wikipedia.org/wiki/MIT_License")
                        }
                    });
            });

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                foreach (var desc in provider.ApiVersionDescriptions)
                
                    options.SwaggerEndpoint($"/swagger/{desc.GroupName}/swagger.json",
                        desc.GroupName.ToUpperInvariant());
                
            options.RoutePrefix = "";
            });

            app.UseRouting();

            app.UseAuthorization();
            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
