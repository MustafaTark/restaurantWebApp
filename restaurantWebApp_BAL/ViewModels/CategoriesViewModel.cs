using Microsoft.AspNetCore.Http;

namespace restaurantWebApp_BAL.ViewModels
{
    public class CategoriesViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public IFormFile? Image { get; set; }
    }
}
