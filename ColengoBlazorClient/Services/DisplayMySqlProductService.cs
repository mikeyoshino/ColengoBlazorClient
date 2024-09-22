

using ColengoBlazorClient.Interfaces;
using ColengoBlazorClient.Responses;

namespace ColengoBlazorClient.Services
{
    public class DisplayMySqlProductService : IDisplayProductService
    {
        public Task<GetProductResponse> GetProducts(int page, int pageSize, string productTitle)
        {
            throw new NotImplementedException();
        }
    }
}
