using BookMillApp_Application.Services.Abstraction;
using BookMillApp_Application.Services.Implementation;
using BookMillApp_Domain.Model;
using BookMillApp_Infrastructure.Persistence;
using BookMillApp_Infrastructure.UnitOfWork.Abstraction;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PaperFineryApp_Infrastructure.UnitOfWork.Implementation;
using System.Text;

namespace BookMillApp.Extension
{
    public static class ServiceExtension
    {
        public static void ResolveDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<IManufacturerService, ManufacturerService>();
            services.AddScoped<ISupplierService, SupplierService>();
            services.AddScoped<IAuthServices, AuthServices>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICloudinaryService, CloudinaryService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IReviewService, ReviewService>();
        }

        public static void ConfigureIdentity(this IServiceCollection services)
        {
            var builder = services.AddIdentity<User, IdentityRole>(o =>
            {
                o.Password.RequireDigit = false;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = true;
                o.Password.RequiredLength = 10;
                o.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();
        }

        public static void ResolveJwt(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("Jwt");
            var key = Environment.GetEnvironmentVariable("KEY");

            var issuer = configuration["Jwt : Issuer"];
            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    //ValidIssuer = jwtSettings.GetSection("Issuer").Value,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
                };
            });
        }
        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(opt =>

            {

                opt.SwaggerDoc("v1", new OpenApiInfo { Title = "DropMate API", Version = "v1" });

                opt.SchemaFilter<EnumSchemaFilter>();

                opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme

                {

                    In = ParameterLocation.Header,

                    Description = "Please enter an access token",

                    Name = "Authorization",

                    Type = SecuritySchemeType.Http,

                    BearerFormat = "JWT",

                    Scheme = "bearer"

                });



                opt.AddSecurityRequirement(new OpenApiSecurityRequirement

                {

                    {

                        new OpenApiSecurityScheme

                        {

                            Reference = new OpenApiReference

                            {

                                Type=ReferenceType.SecurityScheme,

                                Id="Bearer"

                            }

                        },

                        new string[]{}

                    }

                });

            });

        }
    }
}
