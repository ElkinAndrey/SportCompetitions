using SportCompetitionsAPI.Controllers.Middlewares;
using SportCompetitionsAPI.Service;
using SportCompetitionsAPI.Service.Abstractions;
using SportCompetitionsAPI.Service.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<SqlQueries>();
builder.Services.AddTransient<ISportService, ADOSportService>();
builder.Services.AddTransient<IPersonService, ADOPersonService>();

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

app.UseCors(options =>
    options.WithOrigins("http://localhost:3000") //  Кому можно получать данные с сервера
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials()); // Разрешить отправку куки

app.UseTimeDelayMiddleware();

app.Run();
