using System;

namespace Dominio
{
    public class RegistroDominio
    {
        private string tipoBloqueio;
        public int? Codigo { get; set; }
        public char Bloqueado { get; set; }
        public string TipoBloqueio { get => tipoBloqueio; set => tipoBloqueio = string.IsNullOrWhiteSpace(value) ? "Nenhum" : value; }
    }
}
