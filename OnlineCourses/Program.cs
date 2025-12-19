using Microsoft.EntityFrameworkCore;
using OnlineCourses.Data;

var builder = WebApplication.CreateBuilder(args); 

builder.Services.AddOpenApi();
builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
); 

var app = builder.Build();
 

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

