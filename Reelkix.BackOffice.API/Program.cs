using Reelkix.BackOffice.API.Extensions;
using Reelkix.BackOffice.API.Middlewares;
using Reelkix.BackOffice.Application.Manufacturers.Commands.CreateManufacturer;
using Reelkix.BackOffice.Application.Manufacturers.Commands.CreateManufacturer.Validators;
using Reelkix.BackOffice.Application.Manufacturers.Queries.GetAllManufacturers;
using Reelkix.BackOffice.Application.Manufacturers.Queries.GetManufacturerById;
using Reelkix.BackOffice.Application.Products.Commands.CreateProduct;
using Reelkix.BackOffice.Application.Products.Commands.CreateProduct.Validators;
using Reelkix.BackOffice.Application.Products.Queries.GetProductById;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var useInMemory = builder.Configuration.GetValue<bool>("UseInMemoryDbForIntegrationTests");
if (!useInMemory)
{
    builder.Services.AddSqlServerDbContext(builder.Configuration);
}


builder.Services.AddScoped<CreateProductHandler>();
builder.Services.AddScoped<GetProductByIdHandler>();
builder.Services.AddScoped<CreateProductCommandValidator>();

builder.Services.AddScoped<CreateManufacturerHandler>();
builder.Services.AddScoped<GetAllManufacturersHandler>();
builder.Services.AddScoped<GetManufacturerByIdHandler>();
builder.Services.AddScoped<CreateManufacturerCommandValidator>();


builder.Services.AddControllers(); // Endpoint routing is enabled by default in ASP.NET Core 6.0 and later

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalFrontend", builder =>
    {
        builder
            .WithOrigins("http://localhost:5173") // Vite default - TODO: Store to appsettings for Production when deployed
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials(); // if you use cookies or auth headers
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.MapScalarApiReference();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseCors("AllowLocalFrontend");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers(); // This maps the controller actions to endpoints

app.Run();

public partial class Program { }
