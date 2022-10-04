using restaurantWebApp_DAL.Models;

namespace restaurantWebApp_DAL.Dto
{
    public class CartDto
    {
        public int Id { get; set; }
        public LinkedList<Meal>? Meals { get; set; }
        public double? Cost { get; set; }
     
        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }
        public CartDto()
        {
            Meals = new LinkedList<Meal>();
        }
    }
}
