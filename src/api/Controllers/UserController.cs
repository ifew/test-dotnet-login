using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using api.Models;

namespace api.Controllers
{
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly UserContext _context;

        public UserController(UserContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Create([FromBody] User item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            _context.Users.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetUser", new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] User item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            var user = _context.Users.FirstOrDefault(t => t.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            user.Displayname = item.Displayname;

            _context.Users.Update(user);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }
    
}