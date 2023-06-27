using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")] //localhost:4200/api/users[controller indicated the name of the present]
    public class UsersController
    {
        private readonly DataContext _context;

        //creating and assigning field context

        public UsersController(DataContext context)//instantiating datacontext with context
        {
            _context = context;
        }
        [HttpGet]
        public async Task <ActionResult<IEnumerable<AppUser>>> GetUsers() //ActionResult-Helps in getting HTTP responses. IEnumerable-Since we return list so we use IEnumerable
        {
            var users= await _context.Users.ToListAsync();
            return users;
        }
        [HttpGet("{id}")]
        public async Task <ActionResult <AppUser>> GetUser(int id)
        {
            return await _context.Users.FindAsync(id);
        }
    }
}