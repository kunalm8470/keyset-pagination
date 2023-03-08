using Api.Interfaces;
using Api.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddNewtonsoftJson();

builder.Services.AddCors(options =>
{
    options.AddPolicy("Dev_Environment_Policy", builder =>
    {
        builder
        .AllowAnyOrigin() // Access-Control-Allow-Origin: *
        .AllowAnyHeader() // Access-Control-Allow-Headers: *
        .AllowAnyMethod(); // For allowing unsafe requests (any request which is not HTTP GET or HTTP POST) Access-Control-Allow-Method: *
    });
});

builder.Services.AddSingleton<IRandomPostService, RandomPostService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseCors("Dev_Environment_Policy");
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
