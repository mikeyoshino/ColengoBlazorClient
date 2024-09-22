using ColengoBlazorClient.Responses;

namespace ColengoBlazorClient.Interfaces
{
    public interface IDisplayProductService
    {
        Task<GetProductResponse> GetProducts(int page, int pageSize, string productTitle);
    }
}
