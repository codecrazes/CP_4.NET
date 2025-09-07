using Microsoft.AspNetCore.Mvc;
using BibliotecaMongo.Data;
using BibliotecaMongo.Models;
using BibliotecaMongo.DTOs;
using MongoDB.Driver;

namespace BibliotecaMongo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LivrosController : ControllerBase
    {
        private readonly MongoDbContext _context;

        public LivrosController(MongoDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Livro>> Get(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 5,
            [FromQuery] string sortBy = "Titulo",
            [FromQuery] string sortOrder = "asc")
        {
            var sort = sortOrder.ToLower() == "desc"
                ? Builders<Livro>.Sort.Descending(sortBy)
                : Builders<Livro>.Sort.Ascending(sortBy);

            var livros = _context.Livros
                .Find(l => true)
                .Sort(sort)
                .Skip((page - 1) * pageSize)
                .Limit(pageSize)
                .ToList();

            return Ok(livros);
        }

        [HttpGet("{id:length(24)}", Name = "GetLivroById")]
        public ActionResult<Livro> GetById(string id)
        {
            var livro = _context.Livros.Find(l => l.Id == id).FirstOrDefault();
            if (livro == null) return NotFound(new { message = "Livro não encontrado" });
            return Ok(livro);
        }

        [HttpGet("search")]
        public ActionResult<IEnumerable<Livro>> Search([FromQuery] string? titulo, [FromQuery] int? ano)
        {
            var filter = Builders<Livro>.Filter.Empty;

            if (!string.IsNullOrEmpty(titulo))
                filter &= Builders<Livro>.Filter.Regex(l => l.Titulo, new MongoDB.Bson.BsonRegularExpression(titulo, "i"));

            if (ano.HasValue)
                filter &= Builders<Livro>.Filter.Eq(l => l.AnoPublicacao, ano.Value);

            var livros = _context.Livros.Find(filter).ToList();
            return Ok(livros);
        }

        [HttpGet("autor/{nome}")]
        public ActionResult<IEnumerable<Livro>> GetByAutor(string nome)
        {
            var livros = _context.Livros
                .Find(l => l.Autor.Nome.ToLower().Contains(nome.ToLower()))
                .ToList();

            if (!livros.Any()) return NotFound(new { message = "Nenhum livro encontrado para este autor" });

            return Ok(livros);
        }

        [HttpPost]
        public ActionResult<Livro> Create([FromBody] LivroDTO dto)
        {
            if (dto == null || string.IsNullOrEmpty(dto.Titulo))
                return BadRequest(new { message = "Título do livro é obrigatório" });

            var livro = new Livro
            {
                Titulo = dto.Titulo,
                AnoPublicacao = dto.AnoPublicacao,
                Autor = new Autor
                {
                    Nome = dto.Autor.Nome,
                    Nacionalidade = dto.Autor.Nacionalidade
                }
            };

            _context.Livros.InsertOne(livro);

            return CreatedAtRoute("GetLivroById", new { id = livro.Id }, livro);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, [FromBody] LivroDTO dto)
        {
            var livro = _context.Livros.Find(l => l.Id == id).FirstOrDefault();
            if (livro == null) return NotFound(new { message = "Livro não encontrado" });

            livro.Titulo = dto.Titulo;
            livro.AnoPublicacao = dto.AnoPublicacao;
            livro.Autor = new Autor
            {
                Nome = dto.Autor.Nome,
                Nacionalidade = dto.Autor.Nacionalidade
            };

            _context.Livros.ReplaceOne(l => l.Id == id, livro);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var result = _context.Livros.DeleteOne(l => l.Id == id);
            if (result.DeletedCount == 0) return NotFound(new { message = "Livro não encontrado" });

            return NoContent();
        }
    }
}
