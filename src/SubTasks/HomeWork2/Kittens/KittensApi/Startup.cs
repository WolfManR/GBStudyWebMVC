using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using BusinessLayer;
using DataBase.EF;
using DataLayer;
using Mapster;

using MapsterMapper;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

using System.Text;
using BusinessLayer.Abstractions.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace KittensApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;
        private const string AuthPolicy = "AuthPolicy";
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextFactory<KittensContext>(options=>options.UseNpgsql(Configuration.GetConnectionString("Default")));
            services
                .AddDataLayer()
                .AddBusinessLayer();

            var jwtSection = Configuration.GetSection(nameof(JwtSettings));
            services.Configure<JwtSettings>(jwtSection);
            var jwtSettings = jwtSection.Get<JwtSettings>();
            var secureCode = Encoding.ASCII.GetBytes(jwtSettings.SecureCode);

            services.AddCors(x => x.AddPolicy(AuthPolicy, b => b
                .SetIsOriginAllowed(origin => true)
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()));

            services
                .AddAuthentication(opt =>
                {
                    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x => {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(secureCode),
                        ValidateAudience = true,
                        ValidateIssuer = true,
                        ValidAudience = jwtSettings.ValidAudience,
                        ValidIssuer = jwtSettings.ValidIssuer,
                        ClockSkew = TimeSpan.Zero
                    };
                });

            services.AddAuthorization(options =>
                options.AddPolicy("UserOnly", policy => policy.RequireClaim(ClaimTypes.Role, "User")));

            TypeAdapterConfig.GlobalSettings.Scan(typeof(Startup).Assembly);
            services.AddScoped<IMapper, Mapper>();
            
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "KittensApi", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
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
                        Array.Empty<string>()
                    }
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "KittensApi v1"));
            }

            app.UseRouting();

            app.UseCors(AuthPolicy);
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
