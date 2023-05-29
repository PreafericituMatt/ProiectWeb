


using ProiectWebService.Dtos;

namespace ProiectWebService.Services.Interfaces
{
    public interface IUserService
    {
        Task<ServiceResponse<UserDto>> Register(UserDto user);

        Task<ServiceResponse<AuthResponseDto>> Login (AuthDto user);

        Task<ServiceResponse<UserDto>> ForgotPassword(string email);      
    }
}
