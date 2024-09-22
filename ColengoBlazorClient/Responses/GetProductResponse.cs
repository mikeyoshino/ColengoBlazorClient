
using ColengoBlazorClient.Dto;

namespace ColengoBlazorClient.Responses
{
    public class GetProductResponse
    {
        public int TotalPages { get; set; }
        public int TotalItems { get; set; }
        public int PageSize { get; set; }
        public List<ProductDto>? Products { get; set; }
    }
}
