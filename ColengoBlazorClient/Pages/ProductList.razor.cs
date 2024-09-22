using ColengoBlazorClient.Interfaces;
using ColengoBlazorClient.Responses;
using Microsoft.AspNetCore.Components;

namespace ColengoBlazorClient.Pages
{
    public partial class ProductList
    {
        [Inject] public IDisplayProductService? DisplayProductService { get; set; }
        public GetProductResponse? GetProductResponse { get; set; }

        protected override async Task OnInitializedAsync()
        {
            if(DisplayProductService != null)
            {
                var response = await DisplayProductService.GetProducts(1, 10, "");
                if (response.Products != null)
                    GetProductResponse = response;
            }

        }
        public int MyProperty { get; set; }

    }
}
