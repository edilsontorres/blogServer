using blog_BackEnd.Data;
using blog_BackEnd.Entites;
using Microsoft.AspNetCore.Mvc;



namespace blogServer.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
       
        private readonly DataContext _context;
        public UserController(DataContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult> Register([FromBody]User user)
        {
            if(user != null)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();

                return Ok(201);
            }
            return BadRequest("Não foi possivel criar o usário");
        }
      
    }
}