using restaurantWebApp_DAL.Models;

namespace restaurantWebApp_DAL.Dto
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public ICollection<Meal>? Meals { get; set; }
        public string? Image { get; set; }
        public string? Description { get; set; }
        public CategoryDto()
        {
            Meals = new HashSet<Meal>();
        }
    }
}
