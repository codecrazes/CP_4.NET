namespace BibliotecaMongo.DTOs
{
    public class AutorDTO
    {
        public required string Nome { get; set; }

        public required string Nacionalidade { get; set; }
    }
    public class LivroDTO
    {
        public required string Titulo { get; set; }

        public required int AnoPublicacao { get; set; }

        public List<AutorDTO> Autores { get; set; } = new List<AutorDTO>();
    }
}