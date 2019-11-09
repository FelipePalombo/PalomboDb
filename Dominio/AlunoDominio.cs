using System;

namespace Dominio
{
    public class AlunoDominio
    {
        public int? Codigo { get; set; }
        public string Nome { get; set; }
        public decimal? Nota { get; set; } 

        public void AtualizarCampoNecessario(AlunoDominio novoAluno)
        {
            if (novoAluno.Codigo != null && this.Codigo != novoAluno.Codigo)
            {
                this.Codigo = novoAluno.Codigo;
            }

            if (!string.IsNullOrWhiteSpace(novoAluno.Nome) && !this.Nome.Equals(novoAluno.Nome))
            {
                this.Nome = novoAluno.Nome;
            }

            if (novoAluno.Nota != null && this.Nota != novoAluno.Nota)
            {
                this.Nota = novoAluno.Nota;
            }
        }                       
    }

    public static class AlunoDominioExtension
    {
        public static object GetPropertyValue(this AlunoDominio aluno, string propriedade)
        {   
            var type = aluno.GetType();
            var property = type.GetProperty(propriedade); 
            var valor = property?.GetValue(aluno, null);
            return valor?.ToString();
        }        
    }
}
