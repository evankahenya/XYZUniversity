using Microsoft.EntityFrameworkCore;
using XYZUniversity.API.Data;
using XYZUniversity.API.Mappings;
using XYZUniversity.API.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure the DbContext for the application
builder.Services.AddDbContext<XYZUniversityDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("XYZUniversityConnectionString")));

// Register the Student repository
builder.Services.AddScoped<IStudentRepository, SQLStudentRepository>();

// Register AutoMapper profiles
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

var app = builder.Build();

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
