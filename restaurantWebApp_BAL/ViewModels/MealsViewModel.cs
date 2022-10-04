using Microsoft.AspNetCore.Http;
using restaurantWebApp_DAL.Dto;
using restaurantWebApp_DAL.Models;

namespace restaurantWebApp_BAL.ViewModels
{
    public class MealsViewModel
    {

        public int Id { get; set; }
        public string? Name { get; set; }
        public double Price { get; set; }
        public string? Description { get; set; }
        public int CategoryId { get; set; }
        public CategoryDto? Category { get; set; }
        public IFormFile? Image { get; set; }
    }
}
