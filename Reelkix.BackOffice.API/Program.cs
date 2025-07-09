using Reelkix.BackOffice.API.Extensions;
using Reelkix.BackOffice.API.Middlewares;
using Reelkix.BackOffice.Application.Manufacturers.Commands.CreateManufacturer;
using Reelkix.BackOffice.Application.Manufacturers.Commands.CreateManufacturer.Validators;
using Reelkix.BackOffice.Application.Manufacturers.Queries.GetAllManufacturers;
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
builder.Services.AddScoped<CreateManufacturerCommandValidator>();


builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.MapScalarApiReference();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }
