using Microsoft.EntityFrameworkCore;
using Reelkix.BackOffice.Application.Products.Commands.CreateProduct;
using Reelkix.BackOffice.Application.Products.Commands.CreateProduct.Validators;
using Reelkix.BackOffice.Application.Products.Queries.CreateProductById;
using Reelkix.BackOffice.Persistence.Data;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<CreateProductHandler>();
builder.Services.AddScoped<GetProductByIdHandler>();
builder.Services.AddScoped<CreateProductCommandValidator>();


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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
