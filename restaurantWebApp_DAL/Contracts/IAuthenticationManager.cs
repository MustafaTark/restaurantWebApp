using restaurantWebApp_DAL.Dto;

namespace restaurantWebApp_DAL.Contracts
{
    public interface IAuthenticationManager
    {
        Task<bool> ValidateUser(UserForAuthenticationDto userForAuth);
        Task<string> CreateToken();
    }
}
