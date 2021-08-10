using LibraryProject.API.Authorization;
using LibraryProject.API.Database.Entities;
using LibraryProject.API.DTOs.Requests;
using LibraryProject.API.DTOs.Responses;
using LibraryProject.API.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryProject.API.Services
{
    public interface IUserService
    {
        Task<AuthenticateResponse> Authenticate(AuthenticateRequest login);
        Task<User> GetById(int userId);
        void Register(RegisterUser newUser);
        void Update(int userId, UpdateUser updateUser);

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

        public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest login)
        {

            User user = await _userRepository.GetByEmail(login.Email);
            if(user == null)
            {
                return null;
            }

            if(user.Password == login.Password)
            {
                AuthenticateResponse response = new AuthenticateResponse
                {
                    Id = user.Id,
                    Email = user.Email,
                    Username = user.Username,
                    JwtToken = _jwtUtils.GenerateToken(user)
                };
                return response;
            }

            return null;
        }

        public void Register(RegisterUser newUser)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetById(int userId)
        {
            return await _userRepository.GetById(userId);
        }

        public void Update(int userId, UpdateUser updateUser)
        {
            throw new NotImplementedException();
        }
    }
}
