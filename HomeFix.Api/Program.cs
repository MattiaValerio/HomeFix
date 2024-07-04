using HomeFix.Api.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var conf = builder.Configuration;
builder.Services.AddDbContext<DataContext>(options =>
    options.UseNpgsql(conf.GetConnectionString("DefaultConnection")));


builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors();

var app = builder.Build();

// initial migration
DatabaseManagementService.MigrateDatabase(app);

app.UseSwagger();
app.UseSwaggerUI();


app.UseCors(b =>
    b.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();