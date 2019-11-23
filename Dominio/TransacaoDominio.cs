using System;

namespace Dominio
{
    public class TransacaoDominio
    {
        public int Tid { get; set; }
        public string Path { get => $"..\\Repositorio\\Banco\\Operacoes\\trans_{Tid}.json"; }
        public int Status { get; set; }
    }
}
