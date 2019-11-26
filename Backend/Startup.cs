using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using AspNetCore.Identity.Mongo;
using AutoMapper;
using Freelance_Api.DatabaseContext;
using Freelance_Api.Helpers;
using Freelance_Api.Models.Identity;
using Freelance_Api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;



namespace Freelance_Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            var conf = new ConfigurationBuilder().AddConfiguration(configuration).AddEnvironmentVariables().Build();
            Configuration = conf;
        }

        private const string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)

        {
            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                    builder =>
                    {
                        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                        //  builder.WithOrigins("https://freelance-portal.herokuapp.com/");
                    });
            });


            var dbcontext = new MongoDbContext(Environment.GetEnvironmentVariable("ConnectionString"),
                Environment.GetEnvironmentVariable("DatabaseName"),
                Environment.GetEnvironmentVariable("JobCollectionName"));
            services.AddSingleton<IMongoDbContext>(dbcontext);
            services.AddIdentityMongoDbProvider<AppUserModel, AppRoleModel>(identityOptions =>
                {
                    identityOptions.Password.RequiredLength = 8;
                    identityOptions.Password.RequireLowercase = true;
                    identityOptions.Password.RequireUppercase = true;
                    identityOptions.Password.RequireNonAlphanumeric = false;
                    identityOptions.Password.RequireDigit = true;
                    identityOptions.Lockout.MaxFailedAccessAttempts = 3;
                    identityOptions.User.RequireUniqueEmail = true;
                },
                mongoIdentityOptions =>
                {
                    mongoIdentityOptions.ConnectionString = Environment.GetEnvironmentVariable("ConnectionString");
                });


            // JWT setup in the followin
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services.AddAuthentication(options =>
            {
                //Set  Authentication Schema as Bearer
                options.DefaultAuthenticateScheme =
                    JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme =
                    JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme =
                    JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters =
                    new TokenValidationParameters
                    {
                        ValidIssuer = Environment.GetEnvironmentVariable("JwtIssuer"),
                        ValidAudience = Environment.GetEnvironmentVariable("JwtIssuer"),
                        IssuerSigningKey =
                            new SymmetricSecurityKey(
                                Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JwtKey"))),
                        ClockSkew = TimeSpan.Zero
                    };
            });

            //Swagger setup 
            services.AddSwaggerGen(c =>
            {
                c.IgnoreObsoleteActions();
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description =
                        "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
               

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });
                
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "Freelance API", Version = "v1"});
            });
            var mappingConfig = new MapperConfiguration(mc => { mc.AddProfile(new MappingProfile()); });

            var mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            services.AddControllers()
                .AddNewtonsoftJson();

            services.AddSingleton<HttpService>();
            services.AddSingleton<JobService>();
            services.AddSingleton<StudentService>();
            services.AddSingleton<CompanyService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var basePath = "/backend";
            app.UsePathBase(new PathString(basePath));
           
            app.UseSwagger(c =>
            {
                c.SerializeAsV2 = true;
                c.RouteTemplate = "swagger/{documentName}/swagger.json";
                c.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
                {
                    swaggerDoc.Servers = new List<OpenApiServer> { new OpenApiServer { Url = $"https://{httpReq.Host.Value}{basePath}" } };
                });
            });
            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = string.Empty;
                c.SwaggerEndpoint("./swagger/v1/swagger.json", "Freelance API V1");
            });
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseHttpsRedirection();
            }
            
            app.UseRouting();
            app.UseCors(MyAllowSpecificOrigins);
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}