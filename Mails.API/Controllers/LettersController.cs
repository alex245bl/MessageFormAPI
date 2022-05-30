using Mails.API.Data;
using Mails.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Mails.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LettersController : Controller
    {
        private readonly MailsDbContext mailDbContext;

        public LettersController(MailsDbContext mailDbContext)
        {
            this.mailDbContext = mailDbContext;
        }
        //////////////////////////////////////////////////////////////////////////////////
        // get all letters
        [HttpGet]
        public async Task<IActionResult> GetAllLetters()
        {
            var letter = await mailDbContext.Letter.ToListAsync();
            return Ok(letter);
        }

        //////////////////////////////////////////////////////////////////////////////////

        // get message
        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetLetter")]
        public async Task<IActionResult> GetLetter([FromRoute] Guid id)
        {

            var letter = await mailDbContext.Letter.FirstOrDefaultAsync(x => x.Id == id);
            if (letter != null)
            {
                return Ok(letter);
            }

            return NotFound("Letter not found");
        }



        // add letter
        [HttpPost]
        public async Task<IActionResult> AddLetter([FromBody] Letters letter)
        {
            letter.Id = Guid.NewGuid();
            await mailDbContext.Letter.AddAsync(letter);
            await mailDbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetLetter), new { id = letter.Id }, letter);
        }



        // update letter
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateLetter([FromRoute] Guid id, [FromBody] Letters message)
        {
            var existingMessage = await mailDbContext.Letter.FirstOrDefaultAsync(x => x.Id == id);
            if (existingMessage != null)
            {
                existingMessage.Message = message.Message;
                existingMessage.Topic = message.Topic;
                await mailDbContext.SaveChangesAsync();
                return Ok(existingMessage);


            }

            return NotFound("Letter not found");
        }


        // update letter
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteLetter([FromRoute] Guid id)
        {
            var existingLetter = await mailDbContext.Letter.FirstOrDefaultAsync(x => x.Id == id);
            if (existingLetter != null)
            {
                mailDbContext.Remove(existingLetter);
                await mailDbContext.SaveChangesAsync();
                return Ok(existingLetter);
            }

            return NotFound("Letter not found");
        }
    }
}