using restaurantWebApp_DAL.Dto;
using restaurantWebApp_DAL.Models;

namespace restaurantWebApp_DAL.Dto
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
