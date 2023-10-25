using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using RESTbookStatusCodes.Repositories;
using RESTbookStatusCodes.Models;

namespace RESTbookStatusCodes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BooksRepository _manager = new BooksRepository();

        // GET: api/<BooksController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IEnumerable<Book> Get([FromQuery] string title, [FromQuery] string sort_by)
        {
            return _manager.GetAll(title, sort_by);
        }

        // GET api/<BooksController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Book> Get(int id)
        {
            Book book = _manager.GetById(id);
            if (book == null) return NotFound("No such book, id: " + id);
            return Ok(book);
        }

        // POST api/<BooksController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Book> Post([FromBody] Book value)
        {
            try
            {
                Book newBook = _manager.Add(value);
                string uri = Url.RouteUrl(RouteData.Values) + "/" + newBook.Id;
                return Created(uri, newBook);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<BooksController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Book> Put(int id, [FromBody] Book value)
        {
            try
            {
                Book updatedBook = _manager.Update(id, value);
                if (updatedBook == null) return NotFound("No such book, id: " + id);
                return Ok(updatedBook);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<BooksController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Book> Delete(int id)
        {
            Book deletedBook = _manager.Delete(id);
            if (deletedBook == null) return NotFound("No such book, id: " + id);
            return Ok(deletedBook);
        }
    }
}
