using System.Reflection;
using System.Text.Json.Serialization;
using ChadsLibraryPortfolio.Helpers;
using ChadsLibraryPortfolio.Interfaces;
using ChadsLibraryPortfolio.Models;
using ChadsLibraryPortfolio.Services;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NSwag;
using NSwag.AspNetCore;
using NSwag.Generation.Processors.Security;

namespace API.WebApi;

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

        string appPath = AppDomain.CurrentDomain.BaseDirectory;
        string jsonPath = Path.Combine(Directory.GetParent(Directory.GetParent(appPath).FullName).FullName, "appsettings.json");

        IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(appPath)
            .AddJsonFile(jsonPath, optional: true, reloadOnChange: true)
            .Build();


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

        services.AddScoped<IBookService, BookService>();
        services.AddScoped<ITestDataService, TestDataService>();
        services.AddScoped<IInventoryLogService, InventoryLogService>();
        services.AddScoped<IReviewService, ReviewService>();

        //DI FOR ENTITY FRAMEWORK DBCONTEXT GO BELOW HERE //
        services.AddDbContext<LibraryContext>(options =>
        {
            options.UseSqlServer(this.Configuration.GetConnectionString(Constants.DbConnectionString));
            options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            options.EnableSensitiveDataLogging();
        });
        services.AddDatabaseDeveloperPageExceptionFilter();

        services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<LibraryContext>();

        services.Configure<IdentityOptions>(options =>
        {
            // Password settings.
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireUppercase = true;
            options.Password.RequiredLength = 6;
            options.Password.RequiredUniqueChars = 1;

            // Lockout settings.
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = true;

            // User settings.
            options.User.AllowedUserNameCharacters =
            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
            options.User.RequireUniqueEmail = false;
        });

        services.ConfigureApplicationCookie(options =>
        {
            // Cookie settings
            options.Cookie.HttpOnly = true;
            options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

            options.LoginPath = "/Identity/Account/Login";
            options.AccessDeniedPath = "/Identity/Account/AccessDenied";
            options.SlidingExpiration = true;
        });

        // These are located in the Extensions project
        services.AddBasicServices();
        services.AddAutoMappers();

        services.AddValidatorsFromAssembly(Assembly.Load("ViewModels"));

        // Add services to the container.
        services.AddControllersWithViews();

        //services.AddIdentityApiEndpoints<User>();
        //services.AddIdentityApiEndpoints<IdentityUser>().AddEntityFrameworkStores<SecurityDbContext>();
        //services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<SecurityDbContext>();

        services.AddAuthorization();
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

        app.UseRouting();
        app.UseCors("ChadsLibraryPortfolio");
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseMiddleware<ExceptionMiddleware>();

        using (var scope = app.ApplicationServices.CreateScope())
        {
            var services = scope.ServiceProvider;

            var libraryContext = services.GetRequiredService<LibraryContext>();
            libraryContext.Database.Migrate();

            var testData = services.GetRequiredService<ITestDataService>();
            testData?.AddTestData();
        }
    }
}

