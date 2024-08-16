using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using ChadsLibraryPortfolio.Helpers;
using ChadsLibraryPortfolio.Interfaces;
using ChadsLibraryPortfolio.Models;
using ChadsLibraryPortfolio.Services;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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
                    },
                },
            });
            doc.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("bearer"));
        });

        // DI FOR SERVICES GO BELOW HERE //
        services.AddScoped<IBookService, BookService>();
        services.AddScoped<ITestDataService, TestDataService>();
        services.AddScoped<IInventoryLogService, InventoryLogService>();
        services.AddScoped<IReviewService, ReviewService>();
        services.AddScoped<Services.AuthenticationService>();

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

        var jwtSettings = this.Configuration.GetSection("JwtSettings");
        services.AddAuthentication(opt =>
        {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings["validIssuer"],
                ValidAudience = jwtSettings["validAudience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                    .GetBytes(jwtSettings.GetSection("securityKey").Value))
            };
        });

        services.ConfigureApplicationCookie(options =>
        {
            options.Cookie.HttpOnly = true;
            options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

            options.LoginPath = "/Identity/Account/Login";
            options.AccessDeniedPath = "/Identity/Account/AccessDenied";
            options.SlidingExpiration = true;
        });

        services.AddBasicServices();
        services.AddAutoMappers();
        services.AddValidatorsFromAssembly(Assembly.Load("ViewModels"));
        services.AddControllersWithViews();
        services.AddAuthorization();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime applicationLifetime)
    {
        var aWarningSaidThisHadToBeUsed = applicationLifetime;

        app.UseOpenApi();
        app.UseSwaggerUi(config =>
        {
            config.OAuth2Client = new OAuth2ClientSettings()
            {
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

