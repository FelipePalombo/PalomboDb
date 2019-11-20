namespace AplicacaoBase
{
    public class AlunoDto
    {
        public int? Chave { get => null; private set => value = null; }
        public int? Codigo { get; set; }
        public string Nome { get; set; }
        public decimal? Nota { get; set; }
    }
}