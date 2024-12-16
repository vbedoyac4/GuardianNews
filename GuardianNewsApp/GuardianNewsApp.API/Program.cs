using GuardianNewsApp.Application.Interfaces;
using GuardianNewsApp.Application.Services;
using GuardianNewsApp.Domain.Interfaces;
using GuardianNewsApp.Infrastructure.Data; 
using GuardianNewsApp.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))));

builder.Services.AddHttpClient(); 
builder.Services.AddAutoMapper(typeof(Program)); 
builder.Services.AddControllers();

builder.Services.AddScoped<INewsRepository, NewsRepository>(); 
builder.Services.AddScoped<ISearchParamRepository, SearchParamRepository>(); 
builder.Services.AddScoped<IGuardianApiService, GuardianApiService>(); 
builder.Services.AddScoped<NewsService>(); 
builder.Services.AddScoped<SearchParamService>(); 

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.WithOrigins("http://localhost:8080")
        .AllowAnyHeader()
        .AllowAnyMethod());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.UseCors("AllowSpecificOrigin");

app.MapControllers();

app.Run();
