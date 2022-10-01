namespace restaurantWebApp.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public double Cost { get; set; }
        public LinkedList<Meal>? Meals { get; set; }
        public Order()
        {
            Meals = new LinkedList<Meal>();
        }
    }
}
