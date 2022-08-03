using Diary.Api.Extensions;
using Diary.Api.Filters;
using Diary.Domain.Aggregates.User;
using Diary.Infrastructure.ApplicationDbContext;
using Diary.Infrastructure.Settings;
using Diary.IoC;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Diary
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {

            services.Configure<ApiBehaviorOptions>(x => x.SuppressModelStateInvalidFilter = true);

            services.AddCors(
            options =>
            {
                options.AddPolicy(
                    "AllowAll",
                    builder =>
                    {
                        builder.AllowAnyMethod()
                                .AllowAnyHeader()
                                .SetIsOriginAllowed(_ => true)
                                .AllowCredentials();
                    });
            });
            services.AddSwaggerGen(
                c =>
                {
                    c.CustomSchemaIds(x => x.FullName);
                    c.SwaggerDoc("api", new OpenApiInfo { Title = "Kholodok API", Version = "v1" });
                    c.AddSecurityDefinition(
                        "Bearer",
                        new OpenApiSecurityScheme
                        {
                            In = ParameterLocation.Header,
                            Description = "Please insert JWT with Bearer into field",
                            Name = "Authorization",
                            Type = SecuritySchemeType.ApiKey
                        });

                    c.AddSecurityRequirement(
                        new OpenApiSecurityRequirement
                        {
                            {
                                new OpenApiSecurityScheme
                                    {
                                        Description =
                                            "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                                        Name = "Authorization",
                                        Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" },
                                        Scheme = "oauth2",
                                        In = ParameterLocation.Header,
                                    },
                                new List<string>()
                            }
                        });
                });
            services
                .AddSingleton(_ => new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build());
            services.AddControllers();
            services
                .AddIdentity<User, Role>()
                .AddEntityFrameworkStores<DiaryDbContext>()
                .AddSignInManager<SignInManager<User>>()
                .AddDefaultTokenProviders();
            services
                .Configure<AuthSettings>(this.Configuration.GetSection(nameof(AuthSettings)));
            services.AddSingleton(this.Configuration.GetSection("FileStoreSettings").Get<FileStoreSettings>());

            var diaryDbConnection = Configuration.GetConnectionString("DiaryDbConnection");
            services
                .AddEntityFramework(diaryDbConnection);
            services.AddDbContext<DiaryDbContext>(x => x
                    .UseMySql(diaryDbConnection, ServerVersion.AutoDetect(diaryDbConnection),
                        o => o.SchemaBehavior(MySqlSchemaBehavior.Ignore)));

            services
                .AddAuthentication(
                    options =>
                    {
                        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    })
                .AddJwtBearer(
                    options =>
                    {
                        var settings = this.Configuration.GetSection("AuthSettings").Get<AuthSettings>();

                        options.RequireHttpsMetadata = false;
                        options.SaveToken = true;
                        options.TokenValidationParameters =
                            new TokenValidationParameters
                            {
                                ValidateIssuer = true,
                                ValidateAudience = true,
                                ValidateLifetime = true,
                                ValidateIssuerSigningKey = true,
                                ValidIssuer = settings.Issuer,
                                ValidAudience = settings.Audience,
                                IssuerSigningKey = settings.SecurityKey,
                                ClockSkew = TimeSpan.Zero
                            };

                        options.Events = new JwtBearerEvents
                        {
                            OnAuthenticationFailed = context =>
                            {
                                if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                                {
                                    context.Response.Headers.Add("Token-Expired", "true");
                                }

                                return Task.CompletedTask;
                            }
                        };
                    });
            services.RegisterDependencyInjection(Configuration);
            services.AddControllers(x =>
            {
                x.Filters.Add<HttpGlobalExceptionFilter>();
            });
            services.AddHttpContextAccessor();
            services.AddMemoryCache();
            services.AddAuthentication();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage()
                    .UseCors("AllowAll");
                app.UseSwagger();
                app.UseSwaggerUI(
                    c => { c.SwaggerEndpoint("/swagger/api/swagger.json", "Diary API");
                        c.RoutePrefix = "";
                    });
            }
            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseDefaultFiles();
            app.UseStaticFiles();
        }
    }
}