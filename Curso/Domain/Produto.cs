using curso_console.ValueObjects;

namespace curso_console.Domain
{
    public class Produto
    {
        public int Id { get; set; }
        public string CodigoBarras { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public TipoProduto TipoProduto { get; set; }
        public bool Ativo { get; set; }
    }
}