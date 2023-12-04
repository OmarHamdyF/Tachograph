using Infrastructure.Data.Contexts;
using Infrastructure.Shared;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
DependencyContainer.RegisterServices(builder.Services);

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var loggerFactory = app.Services.GetService<ILoggerFactory>();
    loggerFactory.AddFile(builder.Configuration["Logging:LogFilePath"].ToString());

    //var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
    //dbInitializer.Initialize();//to update our data base from pending migrations
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
