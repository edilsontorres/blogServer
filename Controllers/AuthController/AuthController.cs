using System.Threading.Tasks;
using blog_BackEnd.Data;
using blog_BackEnd.Entites;
using blogServer.Service.JwtService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace blogServer.Controllers.AuthController
{
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly JwtService _jwtService;
        private readonly DataContext _context;

        public AuthController(JwtService jwtService, DataContext context)
        {
            _jwtService = jwtService;
            _context = context;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] User user)
        {
           
            var userRepository = await _context.Users
            .FirstOrDefaultAsync(u => u.NameUser == user.NameUser);

            if(userRepository != null)
            {
                var token = _jwtService.GenerateToken(userRepository);
                return Ok(new { Token = token });
            }
            return Unauthorized("Credenciais inválidas.");
            
        }

    }
}