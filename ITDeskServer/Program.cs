using ITDeskServer.Context;
using ITDeskServer.Middleware;
using ITDeskServer.Models;
using ITDeskServer.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(configure =>
{
    configure.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod();
    });
});

#region Dependency Injection
builder.Services.AddScoped<JwtService>();
#endregion

#region Authentication
builder.Services.AddAuthentication().AddJwtBearer(options =>
{
    options.TokenValidationParameters = new()
    {
        ValidateIssuer = true, //token� g�nderen ki�i bilgisi
        ValidateAudience = true, //token� kullanacak site yada ki�i bilgisi
        ValidateIssuerSigningKey = true, //token�n g�venlik anahtar� �retmesini sa�layan g�venlik s�zc���
        ValidateLifetime = true, //token�n ya�am s�resini kontrol etmek istiyor musun 
        ValidIssuer = builder.Configuration.GetSection("Jwt:Issuer").Value,
        ValidAudience = builder.Configuration.GetSection("Jwt:Audience").Value,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Jwt:SecretKey").Value ?? ""))
    };
});
#endregion

#region DbContext ve Identity
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer("Data Source=LAPTOP-5ETPORJT;Initial Catalog=ITDeskDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
});

builder.Services.AddIdentity<AppUser, AppRole>(opt =>
{
    opt.Password.RequiredLength = 6;
    opt.SignIn.RequireConfirmedEmail = true;
    opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    opt.Lockout.MaxFailedAccessAttempts = 2;
    opt.Lockout.AllowedForNewUsers = true;

})
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();
#endregion

#region Presentation
builder.Services.AddControllers().AddOData(options => options.EnableQueryFeatures());
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(setup =>
{
    var jwtSecuritySheme = new OpenApiSecurityScheme
    {
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        Description = "Put **_ONLY_** yourt JWT Bearer token on textbox below!",

        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    setup.AddSecurityDefinition(jwtSecuritySheme.Reference.Id, jwtSecuritySheme);

    setup.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {jwtSecuritySheme, Array.Empty<string>() }
    });
});
#endregion

#region Middlewares
var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

ExtensionsMiddleware.AutoMigration(app);
ExtensionsMiddleware.CreateFirstUser(app);
ExtensionsMiddleware.CreateRoles(app);
ExtensionsMiddleware.CreateUserRole(app);

app.Run();
#endregion


