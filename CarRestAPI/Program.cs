using CarRestAPI;
using CarRestAPI.Repositories;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowAll",
                              policy =>
                              {
                                  policy.WithOrigins("http://hejmeddig.dk")
                                  .AllowAnyMethod()
                                  .AllowAnyHeader();
                              });
});

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<CarsRepository>(new CarsRepository());

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

// Configure the HTTP request pipeline.

app.UseCors("AllowAll");


app.UseAuthorization();

app.MapControllers();

app.Run();
