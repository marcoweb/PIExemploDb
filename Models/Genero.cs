using System.Collections.Generic;

namespace PIExemploDb.Models
{
    public class Genero
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public List<Livro> Livros { get; set; }
    }
}
