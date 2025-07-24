using Microsoft.Extensions.FileProviders;
using Reelkix.BackOffice.API.Extensions;
using Reelkix.BackOffice.API.Middlewares;
using Reelkix.BackOffice.Application.Manufacturers.Commands.CreateManufacturer;
using Reelkix.BackOffice.Application.Manufacturers.Commands.CreateManufacturer.Validators;
using Reelkix.BackOffice.Application.Manufacturers.Commands.UpdateManufacturer;
using Reelkix.BackOffice.Application.Manufacturers.Commands.UpdateManufacturer.Validators;
using Reelkix.BackOffice.Application.Manufacturers.Queries.GetAllManufacturers;
using Reelkix.BackOffice.Application.Manufacturers.Queries.GetManufacturerById;
using Reelkix.BackOffice.Application.ProductImages.Commands.DeleteProductImage;
using Reelkix.BackOffice.Application.ProductImages.Commands.UploadProductImage;
using Reelkix.BackOffice.Application.Products.Commands.CreateDraftProduct;
using Reelkix.BackOffice.Application.Products.Commands.CreateDraftProduct.Validators;
using Reelkix.BackOffice.Application.Products.Commands.CreateProduct;
using Reelkix.BackOffice.Application.Products.Commands.CreateProduct.Validators;
using Reelkix.BackOffice.Application.Products.Commands.DeleteDraftProduct;
using Reelkix.BackOffice.Application.Products.Commands.UpdateDraftProduct;
using Reelkix.BackOffice.Application.Products.Commands.UpdateProduct;
using Reelkix.BackOffice.Application.Products.Queries.GetAllProducts;
using Reelkix.BackOffice.Application.Products.Queries.GetDraftProductById;
using Reelkix.BackOffice.Application.Products.Queries.GetProductById;
using Reelkix.BackOffice.Infrastructure.DependencyInjection;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var useInMemory = builder.Configuration.GetValue<bool>("UseInMemoryDbForIntegrationTests");
if (!useInMemory)
{
    builder.Services.AddSqlServerDbContext(builder.Configuration);
}

builder.Services.AddScoped<CreateProductHandler>();
builder.Services.AddScoped<CreateDraftProductHandler>();
builder.Services.AddScoped<UpdateDraftProductHandler>();
builder.Services.AddScoped<UpdateProductHandler>();
builder.Services.AddScoped<GetProductByIdHandler>();
builder.Services.AddScoped<GetDraftProductByIdHandler>();
builder.Services.AddScoped<GetAllProductsHandler>();
builder.Services.AddScoped<CreateProductCommandValidator>();
builder.Services.AddScoped<CreateDraftProductCommandValidator>();
builder.Services.AddScoped<DeleteDraftProductHandler>();

builder.Services.AddScoped<UploadProductImageHandler>();
builder.Services.AddScoped<DeleteProductImageHandler>();

builder.Services.AddScoped<CreateManufacturerHandler>();
builder.Services.AddScoped<CreateManufacturerCommandValidator>();
builder.Services.AddScoped<GetAllManufacturersHandler>();
builder.Services.AddScoped<GetManufacturerByIdHandler>();
builder.Services.AddScoped<UpdateManufacturerHandler>();
builder.Services.AddScoped<UpdateManufacturerCommandValidator>();

builder.Services.AddInfrastructure(builder.Configuration, builder.Environment);

builder.Services.AddControllers(); // Endpoint routing is enabled by default in ASP.NET Core 6.0 and later

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


var corsOrigin = builder.Configuration.GetSection("CorsOrigins").Get<string[]>() ?? [];
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalFrontend", builder =>
    {
        builder
            .WithOrigins(corsOrigin)
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials(); // if you use cookies or auth headers
    });
});

try
{
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

    app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(
            Path.Combine(Directory.GetCurrentDirectory(), "UploadedFiles")),
        RequestPath = "/static",
        ServeUnknownFileTypes = true, // Allow serving files with unknown types
        DefaultContentType = "application/octet-stream" // Default content type for unknown files
    });

    app.UseAuthorization();

    app.MapControllers(); // This maps the controller actions to endpoints

    app.Run();
}
catch (Exception ex)
{
    // Log critical startup failure
    Console.WriteLine("Startup failed: " + ex.Message);
    throw;
}


public partial class Program { }
