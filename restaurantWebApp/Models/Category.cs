using Microsoft.EntityFrameworkCore;

namespace restaurantWebApp.Models
{
    [Index(nameof(Name))]
    public class Category
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public ICollection<Meal>? Meals { get; set; }
        public string? Image { get; set; }
        public string? Description { get; set; }
        public Category()
        {
            Meals = new HashSet<Meal>();
        }
    }
}
