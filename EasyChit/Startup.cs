// using System;
// using System.Collections.Generic;
// using System.IO;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;
// using Easychit_Api.Exeption_Handling;
// using Easychit_Api.Security.Authentication;
// using Easychit_Repository.Interfaces.Security;
// using Microsoft.AspNetCore.Authentication.JwtBearer;
// using Microsoft.AspNetCore.Builder;
// using Microsoft.AspNetCore.Hosting;
// using Microsoft.AspNetCore.HttpsPolicy;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.Extensions.Configuration;
// using Microsoft.Extensions.DependencyInjection;
// using Microsoft.Extensions.Hosting;
// using Microsoft.Extensions.Logging;
// using Microsoft.IdentityModel.Tokens;
// using Microsoft.OpenApi.Models;
// using Serilog;

// namespace EasyChit
// {
//     public class Startup
//     {
//         public Startup(IConfiguration configuration)
//         {
//             Configuration = configuration;
//             Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();
//             Configuration = configuration;
//         }

//         public IConfiguration Configuration { get; }

//         // This method gets called by the runtime. Use this method to add services to the container.
//         public void ConfigureServices(IServiceCollection services)
//         {
//             services.AddControllers();
//             // Register the Swagger generator, defining 1 or more Swagger documents
//             //services.AddSwaggerGen(c =>
//             //{
//             //    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Easychit", Version = "Sprint 1" });
//             //});
//             services.AddHttpContextAccessor();
//             services.AddSwaggerGen(c =>
//             {
//                 c.SwaggerDoc("v1", new OpenApiInfo
//                 {
//                     Version = "v1",
//                     Title = "Dar Inventory",
//                     Description = "Sprint 1"
//                 });
//                 c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
//                 {
//                     Name = "Authorization",
//                     Type = SecuritySchemeType.ApiKey,
//                     Scheme = "Bearer",
//                     BearerFormat = "JWT",
//                     In = ParameterLocation.Header,
//                     Description = "JWT Authorization header using the Bearer scheme."
//                 });
//                 c.AddSecurityRequirement(new OpenApiSecurityRequirement
//                 {
//                     {
//                           new OpenApiSecurityScheme
//                             {
//                                 Reference = new OpenApiReference
//                                 {
//                                     Type = ReferenceType.SecurityScheme,
//                                     Id = "Bearer"
//                                 }
//                             },
//                             new string[] {}
//                     }
//                 });

//             });

//             services.AddCors();
//             services.AddCors(options =>
//             {
//                 options.AddPolicy("AllowAngular",
//                     builder =>
//                     {
//                         builder.AllowAnyOrigin()
//             .AllowAnyHeader()
//             .AllowAnyMethod(); ;
//                     });
//             });

//             services.AddMvc(config =>
//             {
//                 config.Filters.Add(typeof(Easychit_ExceptionFilter));
//             });
//             // configure strongly typed settings objects
//             var appSettingsSection = Configuration.GetSection("AppSettings");
//             services.Configure<AppSettings>(appSettingsSection);

//             // configure jwt authentication
//             var appSettings = appSettingsSection.Get<AppSettings>();
//             var key = Encoding.ASCII.GetBytes(appSettings.Secret);
//             services.AddAuthentication(x =>
//             {
//                 x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//                 x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//             })
//             .AddJwtBearer(x =>
//             {
//                 x.RequireHttpsMetadata = false;
//                 x.SaveToken = true;
//                 x.TokenValidationParameters = new TokenValidationParameters
//                 {
//                     ValidateIssuerSigningKey = true,
//                     IssuerSigningKey = new SymmetricSecurityKey(key),
//                     ValidateIssuer = false,
//                     ValidateAudience = false
//                 };
//             });

//             // configure DI for application services
//             //services.AddSingleton<IpasswordHasher, PasswordHasher>();
//             services.AddScoped<IUserService, UserService>();
//         }

//         // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
//         public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
//         {
//             if (env.IsDevelopment())
//             {
//                 app.UseDeveloperExceptionPage();
//             }
//             // logging
//             loggerFactory.AddSerilog();
//             // Enable middleware to serve generated Swagger as a JSON endpoint.
//             app.UseSwagger();

//             // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
//             // specifying the Swagger JSON endpoint.
//             app.UseSwaggerUI(c =>
//             {
//                 //c.SwaggerEndpoint("v1/swagger.json", "Easychit");
//                 c.SwaggerEndpoint("/swagger/v1/swagger.json", "Dar Inventory");
//             });
//             app.UseHttpsRedirection();

//             app.UseRouting();
//             app.UseCors("AllowAngular");
//             app.UseStaticFiles();
//             app.UseAuthentication();
//             app.UseAuthorization();

//             app.UseEndpoints(endpoints =>
//             {
//                 endpoints.MapControllers();
//             });
//         }
//     }
// }


using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Easychit_Api.Exeption_Handling;
using Easychit_Api.Security.Authentication;
using Easychit_Repository.Interfaces.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;

namespace EasyChit
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddHttpContextAccessor();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Dar Inventory",
                    Description = "Sprint 1"
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme."
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAngular",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyHeader()
                               .AllowAnyMethod();
                    });
            });

            services.AddMvc(config =>
            {
                config.Filters.Add(typeof(Easychit_ExceptionFilter));
            });

            // Configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            // Configure JWT authentication
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
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

            services.AddScoped<IUserService, UserService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Logging
            loggerFactory.AddSerilog();

            // Enable Swagger middleware
            app.UseSwagger();

            // ✅ FIX: RoutePrefix = string.Empty makes Swagger open at root URL "/"
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Dar Inventory");
                c.RoutePrefix = string.Empty; // ✅ Opens Swagger at https://localhost:port/
            });

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors("AllowAngular");
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}