using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using WebApp.Helpers;
using WebApp.Models;
using WebApp.ModelValidators;


namespace WebApp
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
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = Configuration.GetValue<string>("Authentication:Issuer"),
                ValidAudience = Configuration.GetValue<string>("Authentication:Issuer"),
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetValue<string>("Authentication:Secret"))),
                ClockSkew = TimeSpan.Zero
            };
        });

            services
                .AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                    options.JsonSerializerOptions.IgnoreNullValues = true;
                })


                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddTransient<IValidator<Reservation>, ReservationValidator>();
            services.AddDbContext<ReservationsDbContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("ReservationsDbConnectionString")));


            services
                .AddMvc(options =>
                {
                   // AuthorizationPolicy policy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme).RequireAuthenticatedUser().Build();

                   // options.Filters.Add(new Microsoft.AspNetCore.Mvc.Authorization.AuthorizeFilter(policy));

                    options.EnableEndpointRouting = false;
                });

            services.AddIdentity<IdentityUser, IdentityRole>()
                          .AddDefaultTokenProviders()
                          .AddEntityFrameworkStores<ReservationsDbContext>();

            // Register the Swagger generator, defining 1 or more Swagger documents

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Agentie API",
                    Description = "A simple example ASP.NET Core Agentie API",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Pop Oana",
                        Email = string.Empty,
                        Url = new Uri("https://twitter.com/"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under LICX",
                        Url = new Uri("https://example.com/license"),
                    }
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                //c.IncludeXmlComments(xmlPath);
            });

            
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "wwwroot/dist";
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseSpaStaticFiles();


            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My Reservation API V1");
            });


            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "wwwroot";

            });
        }
    }
}
