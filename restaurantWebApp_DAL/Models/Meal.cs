using Microsoft.EntityFrameworkCore;

namespace restaurantWebApp_DAL.Models
{
    [Index(nameof(Name))]
    public class Meal
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public double Price { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<Cart> Carts { get; set; }
        public Meal()
        {
            Orders = new List<Order>();
            Carts = new List<Cart>();
        }
    }
}
