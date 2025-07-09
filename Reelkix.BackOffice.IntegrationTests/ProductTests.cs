using System.Net.Http.Json;
using FluentAssertions;
using Reelkix.BackOffice.Application.Products.Commands.CreateProduct;
using Reelkix.BackOffice.Application.Products.DTOs;

namespace Reelkix.BackOffice.IntegrationTests
{
    public class ProductTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public ProductTests(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
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

    }
}
