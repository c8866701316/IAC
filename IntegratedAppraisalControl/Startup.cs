using AutoMapper;
using IntegratedAppraisalControl.Business;
using IntegratedAppraisalControl.Data;
using IntegratedAppraisalControl.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;

namespace IntegratedAppraisalControl
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
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                  .AddCookie(options =>
                  {
                      options.LoginPath = "/Account/Login";

                  });
            services.AddMvc().AddJsonOptions(
                options => options.SerializerSettings.ContractResolver = new DefaultContractResolver() /*JSON response setting*/
                );

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddTransient<IUserAccess, UserAccess>();
            services.AddTransient<IUserBusiness, UserBusiness>();

            services.AddTransient<IClientAccess, ClientAccess> ();
            services.AddTransient<IClientBusiness, ClientBusiness>();

            services.AddTransient<IBuildingBusiness, BuildingBusiness>();
            services.AddTransient<IBuildingAccess, BuildingAccess>();

            services.AddTransient<ICommonAccess, CommonAccess>();
            services.AddTransient<ICommonBusiness, CommonBusiness>();

            services.AddTransient<IInventoryAccess, InventoryAccess>();
            services.AddTransient<IInventoryBusiness, InventoryBusiness>();
            
            services.AddTransient<IDepartmentAccess, DepartmentAccess>();
            services.AddTransient<IDepartmentBusiness, DepartmentBusiness>();

            services.AddTransient<IRoomsAccess, RoomsAccess>();
            services.AddTransient<IRoomsBusiness, RoomsBusiness>();

            services.AddTransient<ICertifiedReportAccess, CertifiedReportAccess>();
            services.AddTransient<ICertifiedReportBusiness, CertifiedReportBusiness>();

            services.AddTransient<ILocationAccess, LocationAccess>();
            services.AddTransient<ILocationBusiness, LocationBusiness>();

            var connection = Configuration["ConnectionStrings:myConString"];

            //DbContextOptions<IntegratedAppraisalControl.Data.Models.IntegratedAppraisalControlContext> contextOptions = 
            //    new DbContextOptionsBuilder<IntegratedAppraisalControl.Data.Models.IntegratedAppraisalControlContext>().UseInMemoryDatabase("Context").Options

            DbContextOptions<IntegratedAppraisalControl.Data.Models.IntegratedAppraisalControlContext> contextOptions =
                new DbContextOptionsBuilder<IntegratedAppraisalControl.Data.Models.IntegratedAppraisalControlContext>().UseSqlServer(connection).Options;

            services.AddSingleton(contextOptions);

            //services.AddDbContext<IntegratedAppraisalControl.Data.Models.IntegratedAppraisalControlContext>(options => options.UseSqlServer(connection),ServiceLifetime.Singleton);
            
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseAuthentication();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
