using ColengoBlazorClient.Dto;
using ColengoBlazorClient.Interfaces;
using ColengoBlazorClient.Responses;

namespace ColengoBlazorClient.Services
{
    public class MockProductService : IDisplayProductService
    {

        public async Task<GetProductResponse> GetProducts(int page, int pageSize, string productTitle)
        {
            // Mock 1000 product data
            var mockProducts = GenerateMockProducts(1000);

            // Filter by productTitle if provided
            if (!string.IsNullOrEmpty(productTitle))
            {
                mockProducts = mockProducts
                    .Where(p => p.Title.Contains(productTitle, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            // Pagination logic
            var totalItems = mockProducts.Count;
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
            var paginatedProducts = mockProducts
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            // Prepare the response
            var response = new GetProductResponse
            {
                TotalPages = totalPages,
                TotalItems = totalItems,
                PageSize = pageSize,
                Products = paginatedProducts
            };

            return await Task.FromResult(response);
        }

        // Mock data generation
        private List<ProductDto> GenerateMockProducts(int count)
        {
            var products = new List<ProductDto>();
            var random = new Random();

            for (int i = 1; i <= count; i++)
            {
                products.Add(new ProductDto
                {
                    ProductId = i,
                    Name = $"Product {i}",
                    Title = $"Title {i}",
                    ThumbnailImage = $"https://example.com/images/product{i}.jpg",
                    Price = new PriceDto {  Amount = random.Next(500, 1000), Currency = "EUR"},
                    OriginalPrice = new PriceDto {  Amount = random.Next(500, 1000) },
                    Brand = new BrandDto { Name = $"Brand {random.Next(1, 10)}" },
                    Sold = random.Next(1, 100),
                    AllowMultipleConfigs = random.Next(0, 2) == 1,
                    Url = $"https://example.com/product/{i}",
                    Created = DateTime.Now.AddDays(-random.Next(0, 365)),
                    OverallCampaignEndDate = random.Next(0, 2) == 1 ? DateTime.Now.AddDays(random.Next(1, 100)) : null,
                    ReviewScore = random.NextDouble() * 5,
                    ReviewCount = random.Next(0, 1000),
                    Has3DAssets = random.Next(0, 2) == 1,
                    FullPriceBeforeOverallDiscount = new PriceDto {  Amount = random.Next(100, 1000) },
                    PossibleDiscountPrice = new PriceDto {  Amount = random.Next(10, 500) },
                    Categories = new List<CategoryDto> { new CategoryDto { Name = $"Category {random.Next(1, 5)}" } },
                    Tags = new List<TagDto> { new TagDto { Name = $"Tag {random.Next(1, 10)}" } },
                });
            }

            return products;
        }
    }
}
