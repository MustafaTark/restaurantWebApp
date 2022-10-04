using restaurantWebApp_DAL.Dto;
using restaurantWebApp_DAL.Models;

namespace restaurantWebApp_DAL.Dto
{
    public class MealDto
    {
       public int id { get; set; }
        public string? Name { get; set; }
        public double Price { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public int Quntity { get; set; }
        public int CategoryId { get; set; }
        public CategoryDto? Category { get; set; }
    }
}
