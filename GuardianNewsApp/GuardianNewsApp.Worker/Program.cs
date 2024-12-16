using Hangfire;
using GuardianNewsApp.Application.Interfaces;
using GuardianNewsApp.Application.Services;
using GuardianNewsApp.Domain.Interfaces;
using GuardianNewsApp.Infrastructure.Data;
using GuardianNewsApp.Infrastructure.Repositories;
using GuardianNewsApp.Worker.Jobs;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Hangfire.MySql;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    )
);

builder.Services.AddHangfire(configuration => configuration
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UseStorage(new MySqlStorage(
        builder.Configuration.GetConnectionString("HangfireConnection"),
        new MySqlStorageOptions
        {
            QueuePollInterval = TimeSpan.FromSeconds(15)
        }
    ))
);

builder.Services.AddHangfireServer();

builder.Services.AddHttpClient(); 
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<INewsRepository, NewsRepository>();
builder.Services.AddScoped<ISearchParamRepository, SearchParamRepository>();
builder.Services.AddScoped<IGuardianApiService, GuardianApiService>();
builder.Services.AddScoped<NewsUpsertJob>();

var app = builder.Build();

app.UseHangfireDashboard();

app.UseHangfireServer();

RecurringJob.AddOrUpdate<NewsUpsertJob>(
    "news-upsert-job",
    job => job.UpsertNewsDataAsync(),
    Cron.MinuteInterval(5)
) ;

app.Run();