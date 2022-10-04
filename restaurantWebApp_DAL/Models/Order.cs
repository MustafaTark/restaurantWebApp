using System.ComponentModel.DataAnnotations.Schema;

namespace restaurantWebApp_DAL.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public double Cost { get; set; }
        public LinkedList<Meal>? Meals { get; set; }
       public int Quntity { get; set; }
        public Order()
        {
            Meals = new LinkedList<Meal>();
        }
    }
}
