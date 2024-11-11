using backend.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BackendContext>();
BackendContext db = new();
db.Database.EnsureDeleted();
db.Database.EnsureCreated();


builder.Services.AddControllers();

builder.Services.AddAutoMapper(typeof(Program));


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();