using AutoMapper;
using restaurantWebApp.Dto;
using restaurantWebApp.Models;

namespace restaurantWebApp
{
    public class MappingProfile :Profile
    {
        public MappingProfile()
        {
            CreateMap<MealDto, Meal>().ReverseMap();
            CreateMap<CategoryDto, Category>().ReverseMap();
            CreateMap<UserForRegistrationDto, Customer>();
        }
    }
}
