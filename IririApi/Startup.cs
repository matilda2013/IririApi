using IririApi.Libs.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Autofac;
using IririApi.Libs.Common.iocContainer;
using IririApi.Libs.Common.NewFolder;
using IririApi.Libs.Model.IService;
using IririApi.Libs.Service;
using IririApi.Libs.Repository;
using IririApi.Libs.Model.IRepository;

namespace IririApi
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "IririApi", Version = "v1" });
            });


            services.AddMvc(options => options.EnableEndpointRouting = false);


            services.AddDbContext<AuthenticationContext>(options =>

               //  options.UseSqlServer("Data Source=ICT-22\\SQLEXPRESS;Initial Catalog=IrirDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;"));

               // options.UseSqlServer("Data Source=192.168.4.3;Database=IririDb;Persist security info=True;User Id=Epayplus;Password=Ep@yplusng.com"));
               options.UseSqlServer("Data Source=ICT-56;Database=IririDb;Persist security info=True;Integrated Security=SSPI"));
           
            services.AddIdentityCore<MemberRegistrationUser>(options => options.SignIn.RequireConfirmedAccount = true)
               .AddRoles<IdentityRole>()

               .AddEntityFrameworkStores<AuthenticationContext>().
               AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {

                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 4;

            });

            //services.AddSingleton<IAdminService, AdminService>();
            //services.AddSingleton<IPaymentService, PaymentService>();
            //services.AddSingleton<IAdminService, AdminService>();
            //services.AddSingleton<IAdminService, AdminService>();

            services.AddCors();

            var key = Encoding.UTF8.GetBytes("1234567890123456");
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = false;
                x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
            });

            services.AddControllers().AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);


            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "IririApi v1"));
            }


            app.UseCors(builder => builder
           .AllowAnyOrigin()
          .AllowAnyHeader()
           .AllowAnyMethod()

           );

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseMvc();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new ExternalServicesAutofacModule());
            builder.RegisterModule(new ExternalServicesAutofacModule2());

           
        }

    }
}