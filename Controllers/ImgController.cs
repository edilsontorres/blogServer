using Microsoft.AspNetCore.Mvc;

namespace blog_BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImgController : Controller
    {
       [HttpPost("newimage")]
       public IActionResult Imagens([FromForm] IFormFile img)
       {
            var path = Path.Combine("ImgData", img.FileName);

            using Stream stream = new FileStream(path, FileMode.Create);
            if(img!= null)
            {
                img.CopyTo(stream);
            }
            

            return Ok(img);
       }
    }
}