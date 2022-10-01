using restaurantWebApp.Models;

namespace restaurantWebApp.Dto
{
    public class OrderDto
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public double Cost { get; set; }
        public LinkedList<MealDto>? Meals { get; set; }
        public OrderDto()
        {
            Meals = new LinkedList<MealDto>();
        }
    }
}
