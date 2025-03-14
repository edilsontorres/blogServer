using Microsoft.AspNetCore.Mvc;
using blog_BackEnd.Data;
using Microsoft.EntityFrameworkCore;
using blog_BackEnd.Entites;
using blog_BackEnd.Service;
using Microsoft.AspNetCore.Authorization;



namespace blog.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly Slug _slug;
        public PostsController(DataContext context, Slug slug)
        {
            _context = context;
            _slug = slug;
        }

        [HttpGet("list")]
        public async Task<ActionResult<Post>> List([FromQuery] int page = 1, [FromQuery] int size = 6)
        {
            var total = await _context.Posts.CountAsync();
            var data = await _context.Posts
            .OrderByDescending(p => p.CreatedAt)
            .Skip((page - 1) * size)
            .Take(size)
            .ToListAsync();

            var response = new
            {
                TotalPosts = total,
                Page = page,
                PageSize = size,
                TotalPages = (int)Math.Ceiling(total / (double)size),
                Data = data
            };

            return Ok(response);
        }

        [HttpGet("{slug}")]
        public async Task<ActionResult<Post>> ListPostById([FromRoute] string slug)
        {
            var idPost = await _context.Posts.FirstOrDefaultAsync(x => x.Slug == slug);
            if (idPost != null)
            {
                return Ok(idPost);
            }

            return BadRequest("Sem reultados para pesquisa");
        }

        [Authorize]
        [HttpPost("novopost")]
        public async Task<ActionResult<Post>> NewPost([FromForm] Post posts, [FromForm] IFormFile? img)
        {
            if (img != null && posts.Title != null && posts.Author != null && posts.Content != null)
            {
                var path = Path.Combine("ImgData", img.FileName);
                using Stream stream = new FileStream(path, FileMode.Create);
                img.CopyTo(stream);
                posts.CoverImg = path;
                var slugUnique = _slug.GerarIdentificadorUnico(posts.Title);
                var finalTitle = posts.Title.Replace(" ", "-");
                var lowerSlug = finalTitle.ToLower();
                var finalSlug = $"{lowerSlug}-{slugUnique}";
                posts.Slug = finalSlug;

                _context.Add(posts);
                await _context.SaveChangesAsync();
                return Created();
            }

            return BadRequest();
        }

        [HttpGet("foto/{id}")]
        public async Task<ActionResult> Photo([FromRoute] int id)
        {
            var idPost = await _context.Posts.FindAsync(id);

            if (idPost != null && idPost.CoverImg != null)
            {
                var file = idPost.CoverImg;
                var fileUrl = $"{Request.Scheme}://{Request.Host}/{file}";
                return Ok(new { link = fileUrl });

            }

            return BadRequest("Algo deu errado");
        }

        [HttpPost("editor")]
        public async Task<ActionResult> PhotoEditorSave(IFormFile? file)
        {

            if (file != null)
            {
                var path = Path.Combine("ImgDataEditor", file.FileName);
                using Stream stream = new FileStream(path, FileMode.Create);
                await file.CopyToAsync(stream);
                var fileUrl = $"{Request.Scheme}://{Request.Host}/ImgDataEditor/{file.FileName}";
                return Ok(new { link = fileUrl });
            }

            return BadRequest("deu erro");
        }

        [Authorize]
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

        [Authorize]
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