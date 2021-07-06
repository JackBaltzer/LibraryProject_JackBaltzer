﻿using LibraryProject.API.DTOs.Responses;
using LibraryProject.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                List<AuthorResponse> Authors = await _authorService.GetAllAuthors();

                if (Authors == null)
                {
                    return Problem("Got no data, not even an empty list, this is unexpected");
                }

                if (Authors.Count == 0)
                {
                    return NoContent();
                }

                return Ok(Authors);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

    }
}
