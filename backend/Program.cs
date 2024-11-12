using backend.Data;
using backend.Models;
using backend.Repository;
using backend.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BackendContext>();
BackendContext db = new();
db.Database.EnsureDeleted();
db.Database.EnsureCreated();


builder.Services.AddControllers();

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
    .AddEntityFrameworkStores<BackendContext>()
    .AddApiEndpoints();

builder.Services.AddAuthentication()
    .AddCookie(IdentityConstants.ApplicationScheme, opt => {
        opt.LoginPath = string.Empty;
        opt.Events.OnRedirectToLogin = context => 
        {
            context.Response.StatusCode = 401;
            return Task.CompletedTask;
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

//app.MapIdentityApi<IdentityUser>();

app.UseAuthentication();
app.UseAuthentication();

app.Run();