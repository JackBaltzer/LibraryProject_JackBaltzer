using LibraryProject.API.Authorization;
using LibraryProject.API.Database.Entities;
using LibraryProject.API.DTOs.Requests;
using LibraryProject.API.DTOs.Responses;
using LibraryProject.API.Helpers;
using LibraryProject.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryProject.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;


        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate(AuthenticateRequest login)
        {
            try
            {
                var response = await _userService.Authenticate(login);
                if(response == null)
                {
                    return Unauthorized();
                }
                return Ok(response);
            }
            catch(Exception ex)
            {
                return Problem(ex.Message);
            }

        }

        [Authorize(Role.Admin)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAll();
            return Ok(users);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            // only admins can access other user records
            var currentUser = (UserResponse)HttpContext.Items["User"];
            if (id != currentUser.Id && currentUser.Role != Role.Admin)
                return Unauthorized(new { message = "Unauthorized" });

            var user = await _userService.GetById(id);
            return Ok(user);
        }
    }
}
