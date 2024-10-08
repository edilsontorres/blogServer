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

        [HttpGet("page={skip},take={take}")]
        public async Task<ActionResult> ListAll([FromRoute] int skip, [FromRoute] int take)
        {
            var total = await _context.Posts.CountAsync();
            var posts = await _context.Posts.Skip(skip).Take(take).ToListAsync();
            return Ok(posts);
            
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Post>> ListPostById([FromRoute] int id)
        {
            var idPost = await _context.Posts.FindAsync(id);
            if (idPost != null)
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

            // var teste = HttpUtility.HtmlEncode(posts.Content);
            // posts.Content = teste;

            _context.Add(posts);
            await _context.SaveChangesAsync();
            return Ok(201);
        }

        [HttpGet("foto/{id}")]
        public async Task<ActionResult> Photo([FromRoute] int id)
        {
            var idPost = await _context.Posts.FindAsync(id);

            if (idPost != null && idPost.CoverImg != null)
            {
                var idPhotoByte = System.IO.File.ReadAllBytes(idPost.CoverImg);
                return File(idPhotoByte, "image/png");

            }

            return BadRequest("Algo deu errado");

        }

        [HttpPut("editarpostagem/{id}")]
        public async Task<ActionResult> ToEdit([FromRoute] int id, [FromForm] Post posts, [FromForm] IFormFile img)
        {
            var idPost = await _context.Posts.FindAsync(id);
            if (idPost != null)
            {

                if (img != null)
                {
                    var path = Path.Combine("ImgData", img.FileName);
                    using Stream stream = new FileStream(path, FileMode.Create);
                    img.CopyTo(stream);
                    posts.CoverImg = path;

                    idPost.Title = posts.Title;
                    idPost.Content = posts.Content;
                    idPost.Author = posts.Author;
                    idPost.CoverImg = posts.CoverImg;
                    idPost.LastDateUpdate = DateTime.Now;

                    _context.Posts.Update(idPost);
                    await _context.SaveChangesAsync();
                    return Ok("Dados atualizado com sucesso!");
                }

            }

            return BadRequest("Erro ao indexar postagem");


        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> RemovePost([FromRoute] int id)
        {
            var idPost = await _context.Posts.FindAsync(id);
            if (idPost != null)
            {
                _context.Posts.Remove(idPost);
                await _context.SaveChangesAsync();
                return Ok();
            }

            return BadRequest("Sem reultados para pesquisa");
        }


    }
}