namespace AplicacaoBase
{
    public class AlunoDto
    {
        private int? chave;
        public int? Chave { get => chave; set => chave = value; }
        public int? Codigo { get; set; }
        public string Nome { get; set; }
        public decimal? Nota { get; set; }
    }
}