using LibraryProject.API.Database;
using LibraryProject.API.Database.Entities;
using LibraryProject.API.DTOs.Requests;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryProject.API.Repositories
{
    public interface IUserRepository
    {
        Task<User> Create(User user);
        Task<User> GetByEmail(string email);
        Task<User> GetById(int userId);
        Task<User> Update(int userId, User user);
    }

    public class UserRepository : IUserRepository
    {
        private readonly LibraryProjectContext _context;

        public UserRepository(LibraryProjectContext context)
        {
            _context = context;
        }

        public async Task<User> Create(User user)
        {
            if(_context.User.Any(u => u.Email == user.Email))
            {
                throw new Exception("Email " + user.Email + " is not available");
            }

            if (_context.User.Any(u => u.Username == user.Username))
            {
                throw new Exception("Username " + user.Username+ " is not available");
            }

            _context.User.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<User> GetById(int userId)
        {
            return await _context.User
                .FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<User> GetByEmail(string Email)
        {
            return await _context.User
                .FirstOrDefaultAsync(u => u.Email == Email);
        }


        public async Task<User> Update(int userId, User user)
        {
            User updateUser = await _context.User
                .FirstOrDefaultAsync(u => u.Id == userId);

            if(updateUser != null)
            {
                if(_context.User.Any(u => u.Id != userId && u.Email == user.Email))
                {
                    throw new Exception("Email " + user.Email + " is not available");
                }

                if (_context.User.Any(u => u.Id != userId && u.Username == user.Username))
                {
                    throw new Exception("Username " + user.Username + " is not available");
                }

                updateUser.Email = user.Email;
                updateUser.Username = user.Username;

                if(user.Password != null)
                {
                    updateUser.Password = user.Password;
                }
            }

            return updateUser;
        }
    }
}
