using ColengoBlazorClient.Interfaces;
using ColengoBlazorClient.Responses;
using Microsoft.AspNetCore.Components;

namespace ColengoBlazorClient.Pages
{
    public partial class ProductList
    {
        [Inject] public IDisplayProductService? DisplayProductService { get; set; }
        public GetProductResponse? GetProductResponse { get; set; }

        private List<string> paginationPages = new List<string>();
        private int currentPage = 1;

        private string searchTerm = "";

        protected override async Task OnInitializedAsync()
        {
            

            if (DisplayProductService != null)
            {
                var response = await DisplayProductService.GetProducts(currentPage, 10, "");
                if (response.Products != null)
                {
                    GetProductResponse = response;
                    paginationPages = GetPaginationPages(currentPage, GetProductResponse.TotalPages);
                }


            }
        }

        private List<string> GetPaginationPages(int currentPage, int totalPages)
        {
            var pages = new List<string>();

            if (totalPages > 0) pages.Add("1");
            if (totalPages > 1 && currentPage > 4) pages.Add("...");

            for (int i = Math.Max(2, currentPage - 2); i <= Math.Min(totalPages - 1, currentPage + 2); i++)
            {
                pages.Add(i.ToString());
            }

            if (totalPages > 1 && currentPage + 2 < totalPages - 1) pages.Add("...");
            if (totalPages > 1) pages.Add(totalPages.ToString());

            return pages;
        }

        private async Task NavigateToPage(int page)
        {
            if (page >= 1 && page <= GetProductResponse.TotalPages)
            {
                currentPage = page;
                paginationPages = GetPaginationPages(currentPage, GetProductResponse.TotalPages);

                // Fetch products for the selected page
                if (DisplayProductService != null)
                {
                    var response = await DisplayProductService.GetProducts(currentPage, 10, "");
                    if (response.Products != null)
                        GetProductResponse = response;
                }
            }
        }

        private async Task SortByNameAscending()
        {
            GetProductResponse.Products = GetProductResponse.Products.OrderBy(p => p.Title).ToList();
        }

        private async Task SortByNameDescending()
        {
            GetProductResponse.Products = GetProductResponse.Products.OrderByDescending(p => p.Title).ToList();
        }

        private async Task SearchProducts(ChangeEventArgs e)
        {
            searchTerm = e.Value.ToString();
            if (DisplayProductService != null)
            {
                var response = await DisplayProductService.GetProducts(currentPage, 10, searchTerm);
                if (response.Products != null)
                    GetProductResponse = response;
            }
        }


    }
}
