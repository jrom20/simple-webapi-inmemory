using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
using ngServer.Infrastructure;
using ngServer.Infrastructure.Contexts;
using ngServer.Infrastructure.Security;
using System.Linq;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace ngServer
{
    public class Startup
    {
        private readonly IConfiguration _config;
        public Startup(IConfiguration configuration)
        {
            _config = configuration;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>(cfg =>
            {
                cfg.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<AppIdentityDbContext>();

            services.AddDbContext<AppIdentityDbContext>(options => options.UseInMemoryDatabase(databaseName: "universidad.secure"));

            //services.AddDbContext<AppIdentityDbContext>(cfg =>
            //{
            //    cfg.UseSqlServer(_config.GetConnectionString("IdentityConnectionString"));
            //});
            
            services.AddAuthentication().AddCookie().AddJwtBearer(
                cfg => cfg.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = _config["Tokens:Issuer"],
                    ValidAudience = _config["Tokens:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]))
                });

            services.AddDbContext<ClasesContext>(options => options.UseInMemoryDatabase(databaseName: "universidad"));

            //services.AddDbContext<ClasesContext>(c =>
            //    c.UseSqlServer(_config.GetConnectionString("DataConnectionString")));

            //Dependency Injection
            services.AddTransient<AppIdentityDbContextSeed>();
            services.AddTransient<ClasesContextSeed>();
            
            services.AddSwaggerGen(s => {
                s.SwaggerDoc("LibraryOpenAPISpecification", new Microsoft.OpenApi.Models.OpenApiInfo()
                {
                    Title = "API Library",
                    Version = "1"
                });
            });


            services.AddCors(options =>
            {
                options.AddPolicy(name: "AllowMyOrigin",
                    builder =>
                        {
                            builder
                                .WithOrigins(_config["App:CorsOrigins"]
                                    .Split(",", StringSplitOptions.RemoveEmptyEntries).Select(o => o.RemovePostFix("/"))
                                    .ToArray())
                                .AllowAnyHeader()
                                .AllowAnyMethod()
                                .AllowCredentials()
                                .Build();
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

            app.UseHttpsRedirection();
            
            app.UseSwagger();
            app.UseSwaggerUI(setupAction => {
                setupAction.SwaggerEndpoint("/swagger/LibraryOpenAPISpecification/swagger.json", "Library API");
            });

            app.UseRouting();
            app.UseCors("AllowMyOrigin");
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("Default", "{controller}/{action}/{id?}", new { controller = "Account", Action = "Login" });
            });
        }
    }

    public static class Extensions
    {
        /// <summary>
        /// Removes first occurrence of the given postfixes from end of the given string.
        /// Ordering is important. If one of the postFixes is matched, others will not be tested.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="postFixes">one or more postfix.</param>
        /// <returns>Modified string or the same string if it has not any of given postfixes</returns>
        public static string RemovePostFix(this string str, params string[] postFixes)
        {
            if (str == null)
            {
                return null;
            }

            if (str == string.Empty)
            {
                return string.Empty;
            }

            //if (postFixes.IsNullOrEmpty())
            if (postFixes.Count() == 0)
            {
                return str;
            }

            foreach (var postFix in postFixes)
            {
                if (str.EndsWith(postFix))
                {
                    return str.Left(str.Length - postFix.Length);
                }
            }

            return str;
        }
        /// <summary>
        /// Gets a substring of a string from beginning of the string.
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="str"/> is null</exception>
        /// <exception cref="ArgumentException">Thrown if <paramref name="len"/> is bigger that string's length</exception>
        public static string Left(this string str, int len)
        {
            if (str == null)
            {
                throw new ArgumentNullException("str");
            }

            if (str.Length < len)
            {
                throw new ArgumentException("len argument can not be bigger than given string's length!");
            }

            return str.Substring(0, len);
        }

    }
}
