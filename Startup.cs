using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NodeCMBAPI.Services;
using NodeCMBAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace NodeCMBAPI
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
            services.AddTransient<IDataService, FoodPortionService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IHotelService, HotelService>();
            services.AddTransient<IRestaurentService, RestaurentService>();
            services.AddTransient<INodeCustomersService, NodeCustomersService>();
            services.AddTransient<IFoodService, FoodService>();
            services.AddTransient<IQuantityTypesService, QuantityTypesService>();
            services.AddTransient<IRawMaterialsService, RawMaterialsService>();
            services.AddTransient<IAddtionitemsmenuService, AddtionitemsmenuService>();
            services.AddTransient<IHappyHourService, HappyHourService>();
            services.AddTransient<IMenuService, MenuService>();
            services.AddTransient<IPriceService, PriceService>();
            services.AddTransient<IMenuItemsService, MenuItemsService>();
            services.AddTransient<IIngredientQtyPerDishService, IngredientQtyPerDishService>();
            services.AddTransient<ITablesService, TablesService>();
            services.AddTransient<ITaxService, TaxService>();
            services.AddTransient<IUserRolesService, UserRolesService>();
            services.AddTransient<ISubMenuMasterService, SubMenuMasterService>();
            services.AddTransient<IOrderTypeService, OrderTypeService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IOrderedItemsService, OrderedItemsService>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);


            // configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            // configure jwt authentication
            var secret = Configuration["Logging:AppSettings:Secret"];
            var key = Encoding.ASCII.GetBytes(secret);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        var userService = context.HttpContext.RequestServices.GetRequiredService<IUserService>();
                        var userId = int.Parse(context.Principal.Identity.Name);
                        var user = userService.GetById(userId);
                        if (user == null)
                        {
                            // return unauthorized if user no longer exists
                            context.Fail("Unauthorized");
                        }
                        return Task.CompletedTask;
                    }
                };
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            // configure DI for application services
            services.AddScoped<IUserService, UserService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            var connectionString = Configuration["Logging:ConnectionStrings:NodeCMB_Connection"];
            var secret = Configuration["Logging:AppSettings:Secret"];
            DBConnection.connectionString = connectionString;
            AppSettings.Secret = secret;
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // global cors policy
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
