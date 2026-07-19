using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

using NPE.API.Filters;
using NPE.API.Middleware;

using NPE.Core.Common;
using NPE.Core.Common.CaseNumbers;
using NPE.Core.Common.Context.Services;
using NPE.Core.Common.Identity;
using NPE.Core.Common.Security;
using NPE.Core.Common.Settings;
using NPE.Core.Common.Tenancy.Services;
using NPE.Core.Common.UnitOfWork;
using NPE.Core.Common.Validation;
using NPE.Core.Modules.Auth.BusinessObjects;
using NPE.Core.Modules.Bootstrap.Services;
using NPE.Core.Modules.Cases.BusinessObjects;
using NPE.Core.Modules.iLock.BusinessObjects;
using NPE.Core.Modules.Laboratory.BatchProcessing.BusinessObjects;
using NPE.Core.Modules.Laboratory.ResultEntry.BusinessObjects;
using NPE.Core.Modules.Laboratory.SampleProcessing.BusinessObjects;
using NPE.Core.Modules.Laboratory.TestHistory.BusinessObjects;
using NPE.Core.Modules.Lookups.Services;
using NPE.Core.Modules.Management.Center.Models;
using NPE.Core.Modules.Management.Centers.Services;
using NPE.Core.Modules.Management.Centers.Validation;
using NPE.Core.Modules.Management.Consultant.BusinessObjects;
using NPE.Core.Modules.Management.Consultant.DTOs;
using NPE.Core.Modules.Management.Consultants.Validation;
using NPE.Core.Modules.Management.KeyValue.BusinessObjects;
using NPE.Core.Modules.Management.Reference.BusinessObjects;
using NPE.Core.Modules.Management.Reference.DTOs;
using NPE.Core.Modules.Management.References.Validation;
using NPE.Core.Modules.Patients.BusinessObjects;
using NPE.Core.Modules.Signup.BusinessObjects;
using NPE.Core.Modules.Tests.BusinessObjects;
using NPE.Core.Modules.Users.BusinessObjects;

using NPE.Infrastructure;
using NPE.Infrastructure.Common;
using NPE.Infrastructure.Common.Context.Services;
using NPE.Infrastructure.Common.Data;
using NPE.Infrastructure.Common.Identity.Services;
using NPE.Infrastructure.Common.Security;
using NPE.Infrastructure.Common.Settings;
using NPE.Infrastructure.Common.Tenancy.Services;
using NPE.Infrastructure.Common.UnitOfWork;
using NPE.Infrastructure.Modules.Auth.Authorization;
using NPE.Infrastructure.Modules.Auth.Services;
using NPE.Infrastructure.Modules.Bootstrap.Services;
using NPE.Infrastructure.Modules.Cases.Services;
using NPE.Infrastructure.Modules.iLock.Builders;
using NPE.Infrastructure.Modules.iLock.Services;
using NPE.Infrastructure.Modules.Laboratory.BatchProcessing.Services;
using NPE.Infrastructure.Modules.Laboratory.ResultEntry.Services;
using NPE.Infrastructure.Modules.Laboratory.SampleProcessing.Services;
using NPE.Infrastructure.Modules.Laboratory.TestHistory.Services;
using NPE.Infrastructure.Modules.Lookups.Services;
using NPE.Infrastructure.Modules.Management.Centers.Services;
using NPE.Infrastructure.Modules.Management.Consultant.Services;
using NPE.Infrastructure.Modules.Management.Reference.Services;
using NPE.Infrastructure.Modules.Management.Shared.Services;
using NPE.Infrastructure.Modules.Patients.Services;
using NPE.Infrastructure.Modules.Signup.Services;
using NPE.Infrastructure.Modules.Tenancy.Services;
using NPE.Infrastructure.Modules.Tests.Services;
using NPE.Infrastructure.Modules.Users.Services;
using Swashbuckle.AspNetCore.Filters;

using System.Text;
using static NPE.Core.Modules.Laboratory.BatchProcessing.BusinessObjects.BatchProcessingBO;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.CaptureStartupErrors(true);
builder.WebHost.UseSetting("detailedErrors", "true");

try
{
    #region Database

    var connectionString =
        builder.Configuration
            .GetConnectionString(
                "DefaultConnection")
        ?? throw new Exception(
            "Connection string missing");

    var dbPassword =
        builder.Configuration[
            "DbPassword"];

    var sqlBuilder =
        new SqlConnectionStringBuilder(
            connectionString)
        {
            Password =
                dbPassword
        };

    builder.Services
        .AddDbContext<ApplicationDbContext>(
            options =>
                options.UseSqlServer(
                    sqlBuilder.ConnectionString));

    builder.Services
    .AddScoped<
        ILookupPolicyService,
        LookupPolicyService>();

    builder.Services
    .AddScoped<
        ILookupResolverService,
        LookupResolverService>();

    #endregion

    #region Controllers

    builder.Services
        .AddControllers(
            options =>
            {
                options.Filters
                    .Add<ApiResponseFilter>();
            });

    builder.Services
        .AddEndpointsApiExplorer();

    builder.Services
        .AddHttpContextAccessor();

    builder.Services
        .AddProblemDetails();

    #endregion

    #region Current User

    builder.Services
        .AddScoped<
            ICurrentUser,
            CurrentUser>();

    #endregion

    #region JWT

    var jwtSettings =
        builder.Configuration
            .GetSection(
                "JwtSettings")
            .Get<JwtSettings>()

        ?? throw new Exception(
            "JwtSettings missing");

    builder.Services
        .AddSingleton(
            jwtSettings);

    builder.Services
        .AddAuthentication(
            JwtBearerDefaults
                .AuthenticationScheme)

        .AddJwtBearer(
            options =>
            {
                options
                .TokenValidationParameters =
                    new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer =
                            jwtSettings.Issuer,

                        ValidAudience =
                            jwtSettings.Audience,

                        IssuerSigningKey =
                            new SymmetricSecurityKey(
                                Encoding.UTF8.GetBytes(
                                    jwtSettings.SecretKey))
                    };
            });

    #endregion

    #region Authorization

    builder.Services
        .AddAuthorization();

    builder.Services
        .AddSingleton<
            IAuthorizationPolicyProvider,
            PermissionPolicyProvider>();

    builder.Services
        .AddScoped<
            IAuthorizationHandler,
            PermissionAuthorizationHandler>();

    #endregion

    #region Swagger

    builder.Services
        .AddSwaggerExamplesFromAssemblyOf<Program>();

    builder.Services
        .AddSwaggerGen(
            c =>
            {
                c.SwaggerDoc(
                    "v1",
                    new OpenApiInfo
                    {
                        Title =
                            "NPE API",

                        Version =
                            "v1"
                    });

                c.ExampleFilters();

                c.AddSecurityDefinition(
                    "Bearer",
                    new OpenApiSecurityScheme
                    {
                        Name =
                            "Authorization",

                        Type =
                            SecuritySchemeType.Http,

                        Scheme =
                            "bearer",

                        BearerFormat =
                            "JWT",

                        In =
                            ParameterLocation.Header,

                        Description =
                            "JWT Authorization"
                    });

                c.AddSecurityRequirement(
                    new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference =
                                    new OpenApiReference
                                    {
                                        Type =
                                            ReferenceType.SecurityScheme,

                                        Id =
                                            "Bearer"
                                    }
                            },

                            Array.Empty<string>()
                        }
                    });
            });

    #endregion

    #region Dependency Injection – Common

    builder.Services.AddScoped<IIdentityService, IdentityService>();
    builder.Services.AddScoped<IdentityService>();

    builder.Services.AddScoped<ICaseNumberService, CaseNumberService>();
    builder.Services.AddScoped<IPatientNumberService, PatientNumberService>();
    builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
    builder.Services.AddScoped<IStoredProcedureExecutor, StoredProcedureExecutor>();
    builder.Services.AddScoped<ITestStatusUpdateService, TestStatusUpdateService>();
    builder.Services.AddScoped<ICaseStatusWorkflowService, CaseStatusWorkflowService>();
    builder.Services.AddScoped<IDeploymentSettingsService, DeploymentSettingsService>();
    builder.Services.AddScoped<ICurrentContextService, CurrentContextService>();

    #endregion

    #region Dependency Injection – Auth
    builder.Services.AddScoped<IBootstrapService, BootstrapService>();
    builder.Services.AddScoped<IAuthBO, AuthBO>();
    builder.Services.AddScoped<IPasswordEncryption, TripleDESEncryption>();
    builder.Services.AddScoped<IJwtService, JwtService>();
    builder.Services.AddScoped<IExternalAppService, ExternalAppService>();
    builder.Services.AddScoped<IInternalUserService, InternalUserService>();

    #endregion

    #region Dependency Injection – Patients

    builder.Services.AddScoped<IPatientBO, PatientBO>();
    builder.Services.AddScoped<IPatientService, PatientService>();

    #endregion

    #region Dependency Injection – Cases

    builder.Services.AddScoped<ICaseBO, CaseBO>();
    builder.Services.AddScoped<ICaseService, CaseService>();
    builder.Services.AddScoped<ICasePricingService, CasePricingService>();
    builder.Services.AddScoped<ICaseDiscountService, CaseDiscountService>();
    builder.Services.AddScoped<ICaseReferenceBehaviourService, CaseReferenceBehaviourService>();
    builder.Services.AddScoped<ICaseCoverageService, CaseCoverageService>();

    #endregion

    #region Dependency Injection – Tests

    builder.Services.AddScoped<TestBO>();
    builder.Services.AddScoped<ITestService, TestService>();
    builder.Services.AddScoped<ITestRateLookupService, TestRateLookupService>();

    builder.Services.AddScoped<TestManagementBO>();
    builder.Services.AddScoped<ITestDepartmentService, TestDepartmentService>();
    builder.Services.AddScoped<ITestGroupService, TestGroupService>();
    builder.Services.AddScoped<ITestNormalValueService, TestNormalValueService>();
    builder.Services.AddScoped<ITestParameterService, TestParameterService>();
    builder.Services.AddScoped<ITestProfileService, TestProfileService>();
    builder.Services.AddScoped<ITestTemplateService, TestTemplateService>();

    #endregion

    #region Dependency Injection – Management

    builder.Services.AddScoped<ICenterService, CenterService>();
    builder.Services.AddScoped<IReferenceService, ReferenceService>();
    builder.Services.AddScoped<IConsultantService, ConsultantService>();
    builder.Services.AddScoped<IKeyValueService, KeyValueService>();
    builder.Services.AddScoped<ICenterHierarchyService, CenterHierarchyService>();
    builder.Services.AddScoped<NPE.Core.Modules.Management.Shared.Services.IPatientTitleService, NPE.Infrastructure.Modules.Management.Shared.Services.PatientTitleService>();
    builder.Services.AddScoped<NPE.Core.Modules.Management.Shared.Services.ICountryService, NPE.Infrastructure.Modules.Management.Shared.Services.CountryService>();
    builder.Services.AddScoped<NPE.Core.Modules.Management.Shared.Services.ICityService, NPE.Infrastructure.Modules.Management.Shared.Services.CityService>();

    builder.Services.AddScoped<IValidator<CenterDTO>, CenterValidator>();
    builder.Services.AddScoped<IValidator<ReferenceDTO>, ReferenceValidator>();
    builder.Services.AddScoped<IValidator<ConsultantDto>, ConsultantValidator>();
    builder.Services.AddScoped<IValidator<NPE.Core.Modules.Management.Shared.DTOs.PatientTitleDTO>, NPE.Core.Modules.Management.Shared.Validation.PatientTitleValidator>();
    builder.Services.AddScoped<IValidator<NPE.Core.Modules.Management.Shared.DTOs.CountryDTO>, NPE.Core.Modules.Management.Shared.Validation.CountryValidator>();
    builder.Services.AddScoped<IValidator<NPE.Core.Modules.Management.Shared.DTOs.CityDTO>, NPE.Core.Modules.Management.Shared.Validation.CityValidator>();

    #endregion

    #region Dependency Injection – Result Entry

    builder.Services.AddScoped<ResultEntryBO>();
    builder.Services.AddScoped<IResultEntryService, ResultEntryService>();

    #endregion

    #region Dependency Injection – Test History

    builder.Services.AddScoped<TestHistoryBO>();
    builder.Services.AddScoped<ITestHistoryService, TestHistoryService>();

    #endregion

    #region Dependency Injection – iLock

    builder.Services.AddScoped<MenuBuilder>();
    builder.Services.AddScoped<IMenuService, MenuService>();
    builder.Services.AddScoped<IIlockService, ILockService>();
    builder.Services.AddScoped<IEncryptDecryptService, EncryptDecryptService>();

    #endregion

    #region Dependency Injection – Sample Processing

    builder.Services.AddScoped<SampleProcessingBO>();
    builder.Services.AddScoped<ISampleProcessingService, SampleProcessingService>();

    #endregion

    #region Dependency Injection – Batch Processing

    builder.Services.AddScoped<BatchProcessingBO>();
    builder.Services.AddScoped<IBatchProcessingService, BatchProcessingService>();

    #endregion

    #region Dependency Injection – Sign up

    builder.Services.AddScoped<ISignupBO, SignupBO>();
    builder.Services.AddScoped<ISignupService, SignupService>();
    builder.Services.AddScoped<ITenantProvisioningService, TenantProvisioningService>();

    #endregion

    #region Dependency Injection – Users

    builder.Services.AddScoped<IUserSettingsService, UserSettingsService>();
    builder.Services.AddScoped<IUserService, UserService>();
    builder.Services.AddScoped<IUserBO, UserBO>();

    #endregion

    #region Dependency Injection – Tenancy
    builder.Services.AddScoped<ITenantOwnershipResolver, TenantOwnershipResolver>();
    #endregion

    #region CORS

    builder.Services
        .AddCors(
            options =>
            {
                options.AddPolicy(
                    "AllowFrontend",
                    policy =>
                    {
                        policy
                            .WithOrigins(
                                "http://localhost:5173")
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });

    #endregion

    var app = builder.Build();

    #region Middleware Pipeline

    app.UseMiddleware<ExceptionMiddleware>();

    app.UseSwagger();

    app.UseSwaggerUI(
        c =>
        {
            c.SwaggerEndpoint(
                "/swagger/v1/swagger.json",
                "NPE API v1");
        });

    app.UseHttpsRedirection();

    app.UseCors(
        "AllowFrontend");

    app.UseAuthentication();

    app.UseAuthorization();

    app.MapControllers();

    #endregion

    app.Run();
}
catch (Exception ex)
{
    File.WriteAllText(
        "startup-error.txt",
        ex.ToString());

    throw;
}