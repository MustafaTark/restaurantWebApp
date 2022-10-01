using restaurantWebApp.Models;

namespace restaurantWebApp.ViewModels
{
    public class HomeViewModel
    {
        public ICollection<Category>? Categories { get; set; }
        public ICollection<Meal>? Meals { get; set; }
    }
}
