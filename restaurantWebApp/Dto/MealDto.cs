using restaurantWebApp.Models;

namespace restaurantWebApp.Dto
{
    public class MealDto
    {
       public int id { get; set; }
        public string? Name { get; set; }
        public double Price { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public int CategoryId { get; set; }
        public CategoryDto? Category { get; set; }
    }
}
