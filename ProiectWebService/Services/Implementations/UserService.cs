
using AutoMapper;


using System.Net.Mail;
using System.Net;
using ProiectWebData.Repositories.Interface;
using ProiectWebService;
using ProiectWebService.Dtos;
using ProiectWebData.Entities;
using ProiectWebService.Services.Interfaces;

namespace ProiectWeb.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<UserDto>> Register(UserDto user)
        {
            ServiceResponse<UserDto> response = new();

            var result = await _userRepository.Register(_mapper.Map<User>(user));

            if (result.Success)
            {
                response.Data = _mapper.Map<User, UserDto>(result.Data);
            }
            else
            {
                response.Success = false;
                response.Message = result.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<AuthResponseDto>> Login(AuthDto user)
        {
            var response = new ServiceResponse<AuthResponseDto>();
            var result = await _userRepository.Login(_mapper.Map<AuthDto, User>(user));

            if (result.Success)
            {
                var auth = new AuthResponseDto()
                {
                    Email = user.Email,
                    Token = result.Data
                };

                response.Data = auth;
            }
            else
            {
                response.Success = false;
                response.Message = result.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<UserDto>> ForgotPassword(string email)
        {
            var response = new ServiceResponse<UserDto>();
            var result = await _userRepository.GetUserByEmail(email);

            if (result.Success)
            {
                response.Message = "An email has been sent to: " + email;

                var data = result.Data;

                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("itptrainingprogram@gmail.com", "zhaffsjzazgaolkz"),
                    EnableSsl = true,
                };

                smtpClient.Send("itptrainingprogram@gmail.com", email, "Your forgotten password", "Your password is: " + data.Password);
            }
            else
            {
                response.Success = false;
                response.Message = result.Message;
            }

            return response;
        }     
    }
}

