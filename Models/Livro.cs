namespace PIExemploDb.Models
{
    public class Livro
    {
        public int ID { get; set; }
        public string Titulo { get; set; }

        public int GeneroId { get; set; }
        public Genero Genero { get; set; }
    }
}
