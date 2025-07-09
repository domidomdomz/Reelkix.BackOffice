using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Reelkix.BackOffice.Application.Products.Commands.CreateProduct;
using Reelkix.BackOffice.Application.Products.DTOs;
using Reelkix.BackOffice.Persistence.Data;

namespace Reelkix.BackOffice.IntegrationTests
{
    public class ProductTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public ProductTests(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();

            // Reset DB between tests
            using var scope = factory.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

        }

        [Fact]
        public async Task Can_Create_And_Retrieve_Product()
        {
            // Arrange
            var command = new CreateProductCommand
            {
                Name = "Test Shoe",
                Description = "A test shoe",
                CostPrice = 100,
                SellingPrice = 150,
                ImageUrls = new List<string> { "https://example.com/image.jpg" }
            };

            // Act
            var createResponse = await _client.PostAsJsonAsync("/api/products", command);
            createResponse.EnsureSuccessStatusCode();

            var location = createResponse.Headers.Location;
            var getResponse = await _client.GetAsync(location!);
            var product = await getResponse.Content.ReadFromJsonAsync<ProductDto>();

            // Assert
            product.Should().NotBeNull();
            product!.Name.Should().Be("Test Shoe");
            product.ImageUrls.Should().Contain("https://example.com/image.jpg");
        }

        [Fact]
        public async Task Cannot_Create_Product_WhenNameIsMissing()
        {
            // Arrange
            var command = new CreateProductCommand
            {
                Name = "", // Invalid: empty name
                Description = "No Name",
                CostPrice = 100,
                SellingPrice = 150,
                ImageUrls = new List<string> { "https://example.com/image.jpg" }
            };
            // Act
            var response = await _client.PostAsJsonAsync("/api/products", command);
            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
            var content = await response.Content.ReadAsStringAsync();
            content.Should().Contain("Product name is required");
        }

        [Fact]
        public async Task Cannot_Create_Product_WhenCostPriceIsNegative()
        {
            var command = new CreateProductCommand
            {
                Name = "Undervalued Shoe",
                Description = "Bad pricing",
                CostPrice = -1, // Invalid
                SellingPrice = 50,
                ImageUrls = new List<string> { "https://example.com/image.jpg" }
            };

            var response = await _client.PostAsJsonAsync("/api/products", command);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var content = await response.Content.ReadAsStringAsync();
            content.Should().Contain("Cost price cannot be negative");
        }

        [Fact]
        public async Task Cannot_Create_Product_WhenSellingPriceIsLessThanCostPrice()
        {
            var command = new CreateProductCommand
            {
                Name = "Undervalued Shoe",
                Description = "Bad pricing",
                CostPrice = 100,
                SellingPrice = 50, // Invalid
                ImageUrls = new List<string> { "https://example.com/image.jpg" }
            };

            var response = await _client.PostAsJsonAsync("/api/products", command);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var content = await response.Content.ReadAsStringAsync();
            content.Should().Contain("Selling price must be greater than or equal to cost price");
        }
    }
}
