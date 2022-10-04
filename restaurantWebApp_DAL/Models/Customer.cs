using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace restaurantWebApp_DAL.Models
{
    public class Customer:IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public double? Wallet { get; set; }
        public LinkedList<Order>? OrderList { get; set; }
        [ForeignKey("cartId")]
        public int CartId { get; set; }
        public Cart? Cart { get; set; }
        public Customer()
        {
            OrderList=new LinkedList<Order>();
        }
    }
}
