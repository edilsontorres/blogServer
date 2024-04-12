using Microsoft.AspNetCore.Mvc;
using blog_BackEnd.Data;
using Microsoft.EntityFrameworkCore;
using blog_BackEnd.Entites;
using System;
using System.Web;
using System.IO;



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

        [HttpGet("{id}")]
        public async Task<ActionResult<Post>> ListPostById([FromRoute]int id)
        {
            var idPost = await _context.Posts.FindAsync(id);
            if(idPost != null)
            {
                return Ok(idPost);
            }

            return BadRequest("Sem reultados para pesquisa");
        }

        [HttpPost("novapostagem")]
        public async Task<ActionResult<Post>> NewPost([FromForm] Post posts, [FromForm] IFormFile? img)
        {
            if (img != null)
            {
                var path = Path.Combine("ImgData", img.FileName);
                using Stream stream = new FileStream(path, FileMode.Create);
                img.CopyTo(stream);
                posts.CoverImg = path;   
            }
            
            var teste = HttpUtility.HtmlEncode(posts.Content);
            posts.Content = teste;

            _context.Add(posts);
            await _context.SaveChangesAsync();
            return Ok(201);
        }

        [HttpPut("editarpostagem/{id}")]
        public async Task<ActionResult> ToEdit([FromRoute]int id, [FromBody] Post posts)
        {
            var idPost = await _context.Posts.FindAsync(id);
            if(idPost != null)
            {
                idPost.Title = posts.Title;
                idPost.Content = posts.Content;
                idPost.Author = posts.Author;

                _context.Posts.Update(idPost);
                await _context.SaveChangesAsync();
                return Ok("Dados atualizado com sucesso!");
            }

            return BadRequest("Erro ao indexar postagem");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> RemovePost([FromRoute]int id)
        {
            var idPost = await _context.Posts.FindAsync(id);
            if(idPost != null)
            {
                _context.Posts.Remove(idPost);
                await _context.SaveChangesAsync();
                return Ok();
            }

            return BadRequest("Sem reultados para pesquisa");
        }

        
    }
}