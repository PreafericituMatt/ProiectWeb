
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using ProiectWebData.Entities;
using ProiectWebData.Repositories.Interface;

namespace ProiectWebData.Repositories.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _dbContext;
        private readonly IConfiguration _configuration;

        public UserRepository(DataContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        public async Task<ServiceResponse<User>> Register(User user)
        {
            var existingUser = await UserExists(user.Email);

            if (existingUser)
            {
                return new ServiceResponse<User>()
                {
                    Message = "User already exists",
                    Success = false
                };
            }

            CreatePasswordHash(user);

            var data = await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();


            return new ServiceResponse<User>
            {
                Data = data.Entity
            };
        }

        public async Task<ServiceResponse<string>> Login(User user)
        {
            var response = new ServiceResponse<string>();

            var existingUser = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Email.ToLower().Equals(user.Email.ToLower()));

            if (existingUser == null)
            {
                response.Success = false;
                response.Message = "User or password are incorrect";
                return response;
            }
            if (!VerifyPasswordHash(existingUser, user.Password))
            {
                response.Success = false;
                response.Message = "User or password are incorrect";
                return response;
            }

            var token = CreateToken(existingUser);
            response.Data = token;

            return response;
        }

        public async Task<ServiceResponse<User>> GetUserByEmail(string userEmail)
        {
            var existingUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == userEmail);
            var response = new ServiceResponse<User>();

            if (existingUser == null)
            {
                response.Success = false;
                response.Message = "User does not exist!";
            }
            else
            {
                response.Data = existingUser;
            }
            return response;
        }
        
        public async Task<bool> UserExists(string email)
        {
            var result = await _dbContext.Users.AnyAsync(u => u.Email.ToLower() == email.ToLower());

            return result ? true : false;
        }

        private void CreatePasswordHash(User user)
        {
            using var hmac = new System.Security.Cryptography.HMACSHA512();
            user.PasswordSalt = hmac.Key;
            user.PasswordHash = hmac.ComputeHash(Encoding.UTF32.GetBytes(user.Password));
        }
        private bool VerifyPasswordHash(User user, string password)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(user.PasswordSalt))
            {
                var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF32.GetBytes(password));
                var valid = computeHash.SequenceEqual(user.PasswordHash);

                return valid;
            }
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.NameIdentifier,(user.Id).ToString()),
                new Claim(ClaimTypes.Email,user.Email)

            };

            SymmetricSecurityKey key = new(System.Text.Encoding.UTF32
                .GetBytes(_configuration.GetSection("JwtSettings:Key").Value));

            SigningCredentials credentials = new(key, SecurityAlgorithms.HmacSha512Signature);

            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(14),
                SigningCredentials = credentials
            };
            JwtSecurityTokenHandler tokenHandler = new();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}