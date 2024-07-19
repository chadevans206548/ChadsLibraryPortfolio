using ChadsLibraryPortfolio.Helpers;
using ChadsLibraryPortfolio.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using FluentValidation;
using NSwag;
using NSwag.AspNetCore;
using NSwag.Generation.Processors.Security;
using System.Reflection;
using System.Text.Json.Serialization;

namespace ChadsLibraryPortfolio.API.WebApi;

public class Startup
{
    private AppSettings _appSettings;

    public IConfiguration Configuration { get; }
    public Startup(IConfiguration configuration)
    {
        this.Configuration = configuration;
    }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddMemoryCache();
        services.AddMvc().AddJsonOptions(o =>
        {
            o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        });

        services.AddSingleton<Microsoft.AspNetCore.Http.IHttpContextAccessor, Microsoft.AspNetCore.Http.HttpContextAccessor>();

        var appSettingsSection = this.Configuration.GetSection("AppSettings");
        services.Configure<AppSettings>(appSettingsSection);
        this._appSettings = appSettingsSection.Get<AppSettings>();

        services.AddCors(options =>
        {
            options.AddPolicy("ChadsLibraryPortfolio",
                builder => builder.WithOrigins(this._appSettings.CORS_Origins)
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials());
        });

        //services.AddMicrosoftIdentityWebApiAuthentication(this.Configuration, "AzureAd");

        services.AddOpenApiDocument(doc =>
        {
            doc.DocumentName = "Chads Library Portfolio Web API";
            doc.Version = "v1";
            doc.Title = "Chads Library Portfolio Web API";
            doc.AddSecurity("bearer", Enumerable.Empty<string>(), new OpenApiSecurityScheme
            {
                Type = OpenApiSecuritySchemeType.OAuth2,
                Description = "Azure AAD Authentication",
                Flow = OpenApiOAuth2Flow.Implicit,
                Flows = new OpenApiOAuthFlows()
                {
                    Implicit = new OpenApiOAuthFlow()
                    {
                        //Scopes = new Dictionary<string, string>
                        //{
                        //    { this._appSettings.ApiScope, "TRS API API" }
                        //},
                        //AuthorizationUrl = $"{this._appSettings.AuthApiBasePath}/authorize",
                        //TokenUrl = $"{this._appSettings.AuthApiBasePath}/token",
                    },
                },
            });
            doc.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("bearer"));
        });

        //services.AddAuthorization(options =>
        //{
        //    Intended for this application, not claim specific

        //   options.AddPolicy(Helpers.Constants.AuthPolicy.AuthenticatedUser, policy =>
        //   {
        //       policy.RequireAuthenticatedUser();
        //       policy.RequireScope(this._appSettings.ApiResourceName);
        //   });

        //    User Manager Policy

        //   options.AddPolicy(Helpers.Constants.AuthPolicy.UserManager, policy =>
        //   {
        //       policy.RequireAuthenticatedUser();
        //       policy.RequireClaim(Helpers.Constants.ClaimType.General, Helpers.Constants.Claim.UserManage);
        //   });

        //    System Admin Policy
        //    options.AddPolicy(Helpers.Constants.AuthPolicy.SystemAdmin, policy =>
        //    {
        //        policy.RequireAuthenticatedUser();
        //        policy.RequireClaim(Helpers.Constants.ClaimType.General, Helpers.Constants.Claim.SystemAdmin);
        //    });
        //});

        //services.AddSingleton<Microsoft.AspNetCore.Authorization.IAuthorizationPolicyProvider, AuthorizationPolicyProvider>();

        // DI FOR SERVICES GO BELOW HERE //
        //services.AddScoped<IIdentityService, IdentityService>();
        //services.AddScoped<IAuthorizationService, AuthorizationService>();
        //services.AddScoped<ITransactionCoordinator, SQLTransactionCoordinator>();
        //services.AddScoped<IHostingEnvironmentService, HostingEnvironmentService>();
        //services.AddScoped<IClaimsTransformation, ClaimsTransformation>();

        // DI FOR ENTITY FRAMEWORK DBCONTEXT GO BELOW HERE //
        services.AddDbContext<LibraryContext>(options =>
        {
            options.UseSqlServer(this.Configuration.GetConnectionString(Constants.DbConnectionString));
            options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            options.EnableSensitiveDataLogging();
        });

        // These are located in the Extensions project
        services.AddBasicServices();
        services.AddAutoMappers();

        services.AddValidatorsFromAssembly(Assembly.Load("ViewModels"));
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime applicationLifetime)
    {
        var aWarningSaidThisHadToBeUsed = applicationLifetime;

        app.UseOpenApi();
        app.UseSwaggerUi(config =>
        {
            config.OAuth2Client = new OAuth2ClientSettings()
            {
                //ClientId = this._appSettings.ApiClientId,
                AppName = "API - Swagger",
                UsePkceWithAuthorizationCodeGrant = true
            };
        });

        app.UseCors("ChadsLibraryPortfolio");
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}

