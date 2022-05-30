using Mails.API.Data;
using Mails.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Mails.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly MailsDbContext userDbContext;

        public UsersController(MailsDbContext userDbContext)
        {
            this.userDbContext = userDbContext;
        }
        // get all users
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await userDbContext.User.ToListAsync();
            return Ok(users);
        }

        // get user
        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetUser")]
        public async Task<IActionResult> GetUser([FromRoute] Guid id)
        {
            var user = await userDbContext.User.FirstOrDefaultAsync(x => x.Id == id);
            if (user != null)
            {
                return Ok(user);
            }
            return NotFound("User not found");
        }

        // add user
        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] Users user)
        {
            user.Id = Guid.NewGuid();
            await userDbContext.User.AddAsync(user);
            await userDbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }
    }
}

