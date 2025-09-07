namespace BibliotecaMongo.DTOs
{
    public class AutorDTO
    {
        public required string Nome { get; set; }
        public required string Nacionalidade { get; set; }
    }

    public class LivroDTO
    {
        public string Titulo { get; set; }
        public int AnoPublicacao { get; set; }
        public AutorDTO Autor { get; set; }
    }
}
