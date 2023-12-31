﻿using BookMillApp_Application.Services.Abstraction;
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
                opt.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "BookMill API",
                    Description = "The BookMill API,Introducing an innovative app tailored for Recycling companies (Manufacturers) and Suppliers, streamlining the collection of old and used papers/books from businesses, residences, and educational institutions. On this platform, suppliers can efficiently catalog and monetize their collections, while also forging valuable connections with recycling manufacturers. From librarians with stacks of outdated editions to bookstores with excess inventory, corporate entities, newspaper vendors, and individuals, BookMill ensures your books are redirected to their most suitable next stop.\r\n\r\nEmbrace a more intelligent way to declutter, while also turning your old papers into profit. – it's time to BookMill.",
                    Contact = new OpenApiContact
                    {
                        Name = "BookMill Inc",
                        Email = "joycegabriels32@gmail.com",
                        Url = new Uri("https://bookmill.com")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "IP License",
                        Url = new Uri("https://dropmate.com/licence")
                    },
                    Version = "v1"
                });
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
                string path = @"C:\Users\joyce.gabriel\OneDrive - Africa Prudential\Desktop\Code\BookMillApp\BookMillApp\Controllers";
                var xmlFile = $"Documentation.xml";
                var xmlPath = Path.Combine(path, xmlFile);
                var documentationPath = Path.GetFullPath(xmlPath);

                opt.IncludeXmlComments(documentationPath);
            });
        }
    }
}
