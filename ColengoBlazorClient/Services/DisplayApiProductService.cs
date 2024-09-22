using ColengoBlazorClient.Interfaces;
using ColengoBlazorClient.Responses;
using System.Text.Json;

namespace ColengoBlazorClient.Services 
{
    public class DisplayApiProductService : IDisplayProductService
    {
        private readonly HttpClient _httpClient;

        public DisplayApiProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<GetProductResponse> GetProducts(int page, int pageSize, string productTitle)
        { 

            var getProductResponse = new GetProductResponse();
            var requestUrl = $"https://localhost:7193/api/product/get-products?page={page}&pageSize={pageSize}";

            var response = await _httpClient.GetAsync(requestUrl);
            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<GetProductResponse>(jsonResponse, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });


                getProductResponse.Products = result.Products;
                getProductResponse.TotalPages = result.TotalPages;

                return getProductResponse;
            }
            return null;
        }
    }
}
