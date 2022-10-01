using restaurantWebApp.Dto;
using restaurantWebApp.Models;

namespace restaurantWebApp.ViewModels
{
    public class MealsViewModel
    {

        public int Id { get; set; }
        public string? Name { get; set; }
        public double Price { get; set; }
        public string? Description { get; set; }
        public int CategoryId { get; set; }
        public CategoryDto? Category { get; set; }
        public IFormFile? Image { get; set; }
    }
}
