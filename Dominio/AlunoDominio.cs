using System;

namespace Dominio
{
    public class AlunoDominio
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public decimal Nota { get; set; }                        
    }

    public static class AlunoDominioExtension
    {
        public static object GetPropertyValue(this AlunoDominio aluno, string propriedade)
        {   
            var valor = aluno.GetType().GetProperty(propriedade).GetValue(aluno, null).ToString();
            return valor;
        }
    }
}
