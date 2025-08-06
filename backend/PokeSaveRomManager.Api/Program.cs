using Asp.Versioning;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PokeSaveRomManager.Api.Abilities;
using PokeSaveRomManager.Api.Auth0;
using PokeSaveRomManager.Api.Auth0.Interfaces;
using PokeSaveRomManager.Api.Categories;
using PokeSaveRomManager.Api.Games;
using PokeSaveRomManager.Api.Items;
using PokeSaveRomManager.Api.Moves;
using PokeSaveRomManager.Api.Natures;
using PokeSaveRomManager.Api.Pokemons;
using PokeSaveRomManager.Api.Roms;
using PokeSaveRomManager.Api.Saves;
using PokeSaveRomManager.Api.Shared.Middleware;
using PokeSaveRomManager.Api.Shared.Policies;
using PokeSaveRomManager.Api.Stats;
using PokeSaveRomManager.Api.Types;
using PokeSaveRomManager.Api.Users;
using PokeSaveRomManager.Data;

var builder = WebApplication.CreateBuilder(args);

// Add Auth0 authentication configuration
var auth0Domain = builder.Configuration["Auth0:Domain"];
var auth0Audience = builder.Configuration["Auth0:Audience"];

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.Authority = $"https://{auth0Domain}";
    options.Audience = auth0Audience;

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = $"https://{auth0Domain}",
        ValidateAudience = true,
        ValidAudience = auth0Audience,
        ValidateLifetime = true
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(PermissionPolicies.GamesPolicy, policy =>
        policy.RequireClaim("permissions", "manage:games"));
    options.AddPolicy(PermissionPolicies.TypesPolicy, policy =>
        policy.RequireClaim("permissions", "manage:types"));
    options.AddPolicy(PermissionPolicies.StatsPolicy, policy =>
        policy.RequireClaim("permissions", "manage:stats"));
    options.AddPolicy(PermissionPolicies.NaturesPolicy, policy =>
        policy.RequireClaim("permissions", "manage:natures"));
    options.AddPolicy(PermissionPolicies.CategoriesPolicy, policy =>
        policy.RequireClaim("permissions", "manage:categories"));
    options.AddPolicy(PermissionPolicies.AbilitiesPolicy, policy =>
        policy.RequireClaim("permissions", "manage:abilities"));
    options.AddPolicy(PermissionPolicies.ItemsPolicy, policy =>
        policy.RequireClaim("permissions", "manage:items"));
    options.AddPolicy(PermissionPolicies.MovesPolicy, policy =>
        policy.RequireClaim("permissions", "manage:moves"));
    options.AddPolicy(PermissionPolicies.PokemonPolicy, policy =>
        policy.RequireClaim("permissions", "manage:pokemon"));
    options.AddPolicy(PermissionPolicies.SavesPolicy, policy =>
        policy.RequireClaim("permissions", "manage:saves"));
    options.AddPolicy(PermissionPolicies.RomPolicy, policy =>
        policy.RequireClaim("permissions", "manage:roms"));
});

// Register user services
builder.Services.AddUserServices();
// Register game services
builder.Services.AddgGameServices();
// Register type services
builder.Services.AddgTypeServices();
// Register stat services
builder.Services.AddStatServices();
// Register nature services
builder.Services.AddNatureServices();
// Register category services
builder.Services.AddCategoryServices();
// Register ability services
builder.Services.AddAbilityServices();
// Register item services
builder.Services.AddItemServices();
// Register move services
builder.Services.AddMoveServices();
// Register pokemon services
builder.Services.AddPokemonServices();
// Register save services
builder.Services.AddSaveServices();
// Register rom services
builder.Services.AddRomsServices();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
// Add Swagger generation
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Pokemon API", Version = "v1" });

    // Configura JWT Bearer token per Swagger UI
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Inserisci il token JWT come: Bearer {token}",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
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
            new string[] {}
        }
    });
});

// Configure database connection
builder.Services.AddDbContext<PokemonDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.WebHost.ConfigureKestrel(options =>
{
    options.Limits.MaxRequestBodySize = 4 * 1024 * 1024;
    options.ListenAnyIP(80); // Only listen on HTTP port
});

// Add HttpContextAccessor to access HttpContext in services
builder.Services.AddHttpContextAccessor();

builder.Services.AddHttpClient<IAuth0ApiClient, Auth0ApiClient>();

// Add API versioning
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
    options.ApiVersionReader = new UrlSegmentApiVersionReader();
}).AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(policy => 
    policy.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()
);

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider("/app"),
    RequestPath = "/roms",
    ServeUnknownFileTypes = true,
    DefaultContentType = "application/octet-stream"
});
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider("/app"),
    RequestPath = "/saves",
    ServeUnknownFileTypes = true,
    DefaultContentType = "application/octet-stream"
});

//app.UseHttpsRedirection();

app.MapControllers();

app.Run();