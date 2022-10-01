using System.ComponentModel.DataAnnotations.Schema;

namespace restaurantWebApp.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public LinkedList<Meal>? Meals { get; set; }
        public double? Cost { get; set; }
        [ForeignKey("CustomerId")]
        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }
        public Cart()
        {
            Meals = new LinkedList<Meal>();
        }
    }
}
