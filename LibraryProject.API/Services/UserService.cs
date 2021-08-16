using LibraryProject.API.Authorization;
using LibraryProject.API.Database.Entities;
using LibraryProject.API.DTOs.Requests;
using LibraryProject.API.DTOs.Responses;
using LibraryProject.API.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryProject.API.Services
{
    public interface IUserService
    {
        Task<List<UserResponse>> GetAll();
        Task<UserResponse> GetById(int userId);
        Task<LoginResponse> Authenticate(LoginRequest login);
        Task<UserResponse> Register(RegisterUser newUser);
        Task<UserResponse> Update(int userId, UpdateUser updateUser);
    }

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtUtils _jwtUtils;

        public UserService(IUserRepository userRepository, IJwtUtils jwtUtils)
        {
            _userRepository = userRepository;
            _jwtUtils = jwtUtils;
        }

        public async Task<List<UserResponse>> GetAll()
        {
            List<User> users = await _userRepository.GetAll();

            return users == null ? null : users.Select(u => new UserResponse
            {
                Id = u.Id,
                Email = u.Email,
                Role = u.Role,
                Username = u.Username
            }).ToList();
        }

        public async Task<UserResponse> GetById(int userId)
        {
            User user = await _userRepository.GetById(userId);
            return userResponse(user);
        }

        public async Task<LoginResponse> Authenticate(LoginRequest login)
        {

            User user = await _userRepository.GetByEmail(login.Email);
            if (user == null)
            {
                return null;
            }

            if (user.Password == login.Password)
            {
                LoginResponse response = new LoginResponse
                {
                    Id = user.Id,
                    Email = user.Email,
                    Username = user.Username,
                    Role = user.Role,
                    Token = _jwtUtils.GenerateJwtToken(user)
                };
                return response;
            }

            return null;
        }

        public async Task<UserResponse> Register(RegisterUser newUser)
        {
            User user = new User
            {
                Email = newUser.Email,
                Username = newUser.Username,
                Password = newUser.Password,
                Role = Helpers.Role.User // force all users created through Register, to Role.User
            };

            user = await _userRepository.Create(user);

            return userResponse(user);
        }

        public async Task<UserResponse> Update(int userId, UpdateUser updateUser)
        {
            User user = new User
            {
                Email = updateUser.Email,
                Username = updateUser.Username,
                Password = updateUser.Password,
                Role = updateUser.Role
            };

            user = await _userRepository.Update(userId, user);

            return userResponse(user);
        }

        private UserResponse userResponse(User user)
        {
            return user == null ? null : new UserResponse
            {
                Id = user.Id,
                Email = user.Email,
                Username = user.Username,
                Role = user.Role
            };
        }
    }
}
