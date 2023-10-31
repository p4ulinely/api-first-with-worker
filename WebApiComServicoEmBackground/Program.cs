using Service;
using WebApiComServicoEmBackground;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHostedService<MeuWorker>();

builder.Services.AddHttpClient("Github", c => {
    c.BaseAddress = new Uri("https://api.github.com/");
});

builder.Services.AddHttpClient("ApiVagas", c => {
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
