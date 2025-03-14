using blog_BackEnd.Data;
using blog_BackEnd.Entites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;



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
        public async Task<ActionResult> Register([FromForm]User user, [FromForm]IFormFile? avatar)
        {
            if(user != null && avatar != null)
            {
                var path = Path.Combine("Avatar", avatar.FileName);
                using Stream stream = new FileStream(path, FileMode.Create);
                avatar.CopyTo(stream);
                user.Avatar = path;
                
                _context.Add(user);
                await _context.SaveChangesAsync();

                return Created();
            }

            

            return BadRequest("Não foi possivel criar o usário");
        }

        //[Authorize]
        [HttpPost("name")]
        public async Task<ActionResult<User>> GetUser([FromBody]User name)
        {
            var userRepository = await _context.Users.FirstOrDefaultAsync(u => u.NameUser == name.NameUser);

            if(userRepository != null)
            {
                var avatar = userRepository.Avatar;
                var fileUrl = $"{Request.Scheme}://{Request.Host}/{avatar}";
                return Ok(new 
                { 
                    link = fileUrl,
                    userName = userRepository.NameUser 

                });
            }

            return BadRequest("Erro na requisição");
        }
      
    }
}