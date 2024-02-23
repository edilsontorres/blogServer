using Microsoft.AspNetCore.Mvc;
using blog_BackEnd.Data;
using Microsoft.EntityFrameworkCore;
using blog_BackEnd.Entites;

namespace blog.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostagensController : ControllerBase
    {
        private readonly DataContext _context;
        public PostagensController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> ListAll()
        {
            var posts = await _context.Posts.ToListAsync();
            return Ok(posts);
        }

        [HttpPost("novapostagem")]
        public async Task<ActionResult<Post>> NewPost([FromBody] Post posts)
        {
            _context.Add(posts);
            await _context.SaveChangesAsync();
            return Ok(201);
        }

        
    }
}