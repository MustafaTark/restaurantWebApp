using AutoMapper;
using restaurantWebApp_DAL.Dto;
using restaurantWebApp_DAL.Models;

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
