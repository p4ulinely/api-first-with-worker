using Infra;
using Microsoft.Azure.Cosmos;
using Service;
using WebApiComServicoEmBackground;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHostedService<MeuWorker>();

// builder.Services.AddScoped<IDbConnectionFactory, SqlDbConnectionFactory>();

builder.Services.AddScoped(sp =>
    {
        var configuration = sp.GetService<IConfiguration>();

        return new CosmosClient(configuration["AzureCosmosDB:ConnectionString"]);
    });

builder.Services.AddHttpClient("Github", c =>
    {
        c.BaseAddress = new Uri("https://api.github.com/");
    });

builder.Services.AddHttpClient("ApiVagas", c =>
    {
        c.BaseAddress = new Uri("https://apibr.com/");
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.UseMiddleware<GlobalErrorHandlerMiddleware>();

app.MapControllers();

app.Run();
