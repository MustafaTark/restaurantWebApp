using restaurantWebApp_DAL.Models;

namespace restaurantWebApp_BAL.ViewModels
{
    public class HomeViewModel
    {
        public ICollection<Category>? Categories { get; set; }
        public ICollection<Meal>? Meals { get; set; }
    }
}
