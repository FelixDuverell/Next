using System.Security.Claims;
using System.Text.Json.Serialization;
using backend.Data;
using backend.Models;
using backend.Repository;
using backend.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BackendContext>();
BackendContext db = new();
db.Database.EnsureDeleted();
db.Database.EnsureCreated();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.WithOrigins(["http://localhost:3000", "http://localhost:5285"]); 
            policy.WithHeaders("content-type");
            policy.AllowCredentials();
        }); 
});



builder.Services.AddControllers()
.AddJsonOptions(opt => {
    opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddScoped<IUserService, UserService>();


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(config => {

    config.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Kanban API",
        Version = "1.0",
        Contact = new OpenApiContact
        {
            Name = "Felix",
            Url = new Uri("https://felixduverell.github.io/")
        },
    });
});

builder.Services.AddIdentityCore<AppUser>(opt => 
{
    opt.Password.RequiredLength = 2;
})  
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<BackendContext>()
    .AddApiEndpoints();

builder.Services.AddAuthentication()
    .AddCookie(IdentityConstants.ApplicationScheme, opt => {
        opt.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        opt.Cookie.SameSite = SameSiteMode.None;
        opt.LoginPath = string.Empty;
        opt.Events.OnRedirectToLogin = context => 
        {
            context.Response.StatusCode = 401;
            return Task.CompletedTask;
        };
        opt.AccessDeniedPath = string.Empty;
        opt.Events.OnRedirectToAccessDenied = context => 
        {
            context.Response.StatusCode = 403;  
            return Task.CompletedTask; 
        };
    });

builder.Services.AddAuthorization(opt =>
{

    opt.AddPolicy("SelfOwnedResourceByQueryParam", policy => {

        policy.RequireAssertion(context => {

            string userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "unknown";
            var queryParam = new HttpContextAccessor()?.HttpContext?.Request.Query.FirstOrDefault(query => query.Key == "userid");

            return queryParam != null && queryParam?.Value.ToString() == userId;
        });
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.MapControllers();

//app.MapIdentityApi<IdentityUser>();

app.UseAuthentication();
app.UseAuthorization();

app.Run();

// policybaserad_auktorisering kolla på igen