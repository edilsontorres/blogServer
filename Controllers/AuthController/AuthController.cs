using blogServer.Service.JwtService;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace blogServer.Controllers.AuthController
{
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly JwtService _jwtService;

        public AuthController(JwtService jwtService)
        {
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            // Simulação de usuário válido (substituir por lógica real)
            // if (request.Username == "admin" && request.Password == "123456")
            // {
            //     var token = _jwtService.GenerateToken("1"); // ID do usuário
            //     return Ok(new { Token = token });
            // }

            return Unauthorized("Credenciais inválidas.");
        }

    }
}