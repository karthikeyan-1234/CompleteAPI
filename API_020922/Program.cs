using API_020922.Contexts;
using API_020922.Repositories;
using API_020922.Services;
using Microsoft.EntityFrameworkCore;
using API_020922.Profiles;
using API_020922.Caching;
using Serilog;
using Microsoft.AspNetCore.Cors.Infrastructure;
using API_020922.Hubs;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.

var logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).Enrich
    .FromLogContext()
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IAppDBContext, AppDBContext>();
builder.Services.AddDbContext<AppDBContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("AppDB")));
builder.Services.AddScoped(typeof(IGenericRepo<>), typeof(GenericRepo<>));
builder.Services.AddScoped<IExpenseService, ExpenseService>();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddLocalization(opt => { opt.ResourcesPath = "Resources"; });

builder.Services.AddScoped<ICacheManager, CacheManager>();
builder.Services.AddStackExchangeRedisCache(opt => opt.Configuration = configuration.GetConnectionString("Redis"));

builder.Services.AddSignalR(optns =>
{
    optns.EnableDetailedErrors = true;
    optns.KeepAliveInterval = TimeSpan.FromHours(2);
});


CorsPolicyBuilder cbuilder = new CorsPolicyBuilder().AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
CorsPolicy policy = cbuilder.Build();

builder.Services.AddCors(opt => {
    opt.AddPolicy("MyCors", policy);
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var supportedCultures = new[] { "en-US", "fr-FR", "ta-IN" };
var localizationOptions = new RequestLocalizationOptions().SetDefaultCulture(supportedCultures[0])
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);

app.UseRequestLocalization(localizationOptions);

app.UseAuthorization();

app.MapControllers();

app.UseCors("MyCors");

app.MapHub<InformHub>("/informHub");

app.Run();
