using Mails.API.Data;
using Mails.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Mails.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TopicsController : Controller
    {
        private readonly MailsDbContext topicDbContext;

        public TopicsController(MailsDbContext topicDbContext)
        {
            this.topicDbContext = topicDbContext;
        }
        // get all topics
        [HttpGet]
        public async Task<IActionResult> GetAllTopics()
        {
            var topics = await topicDbContext.Topic.ToListAsync();
            return Ok(topics);
        }

        // get topic
        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetTopic")]
        public async Task<IActionResult> GetTopic([FromRoute] Guid id)
        {
            var topic = await topicDbContext.Topic.FirstOrDefaultAsync(x => x.Id == id);
            if (topic != null)
            {
                return Ok(topic);
            }
            return NotFound("Topic not found");
        }

        // add topic
        [HttpPost]
        public async Task<IActionResult> AddTopic([FromBody] Topics topic)
        {
            topic.Id = Guid.NewGuid();
            await topicDbContext.Topic.AddAsync(topic);
            await topicDbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTopic), new { id = topic.Id }, topic);
        }
    }
}
