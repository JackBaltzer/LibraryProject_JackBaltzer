using LibraryProject.API.Authorization;
using LibraryProject.API.DTOs.Requests;
using LibraryProject.API.DTOs.Responses;
using LibraryProject.API.Helpers;
using LibraryProject.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace LibraryProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                List<BookResponse> Books = await _bookService.GetAllBooks();

                if (Books == null)
                {
                    return Problem("Got no data, not even an empty list, this is unexpected");
                }

                if (Books.Count == 0)
                {
                    return NoContent();
                }

                return Ok(Books);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("{bookId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById([FromRoute] int bookId)
        {
            try
            {
                BookResponse book = await _bookService.GetById(bookId);

                if (book == null)
                {
                    return NotFound();
                }

                return Ok(book);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        //[Authorize(Role.Admin)]
        [HttpPost, DisableRequestSizeLimit]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] NewBook newBook)
        {
            try
            {
                if(Request.Form.Files != null && Request.Form.Files.Count > 0)
                {
                    //var file = Request.Form.Files[0];
                    var formCollection = await Request.ReadFormAsync();
                    var file = formCollection.Files.First();

                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    // where to save physical image file
                    var folderName = Path.Combine("../", "../","LibraryProject.Client", "src", "app", "assets","images");
                    // OS safe pointer to folder
                    var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                    var fullPath = Path.Combine(pathToSave, fileName);
                    // the actual name we want to save in the db
                    var dbPath = Path.Combine(folderName, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                        newBook.Image = dbPath;
                    }

                }
                BookResponse book = await _bookService.Create(newBook);

                if (book == null)
                {
                    return Problem("Book was not created, something went wrong");
                }

                return Ok(book);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [Authorize(Role.Admin)]
        [HttpPut("{bookId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromRoute] int bookId, [FromBody] UpdateBook updateBook)
        {
            try
            {
                BookResponse book = await _bookService.Update(bookId, updateBook);

                if (book == null)
                {
                    return Problem("Book was not updated, something went wrong");
                }

                return Ok(book);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [Authorize(Role.Admin)]
        [HttpDelete("{bookId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromRoute] int bookId)
        {
            try
            {
                bool result = await _bookService.Delete(bookId);

                if (!result)
                {
                    return Problem("Book was not deleted, something went wrong");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
