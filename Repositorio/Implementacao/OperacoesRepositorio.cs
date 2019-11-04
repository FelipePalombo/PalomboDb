using System.Collections;
using Repositorio.Interface;
using System.Collections.Generic;
using Dominio;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;

namespace Repositorio.Implementacao
{
    public class OperacoesRepositorio : IOperacoesRepositorio
    {
        private string path = @"..\Repositorio\Banco\alunos.json";
        public IEnumerable<AlunoDominio> Listar(IEnumerable<FiltroDominio> filtros)
        {
            string json = File.ReadAllText(path);
            List<AlunoDominio> alunos = JsonConvert.DeserializeObject<List<AlunoDominio>>(json); 

            if (filtros.Count() > 0)
            {
                List<AlunoDominio> alunosFiltrados = null;
                foreach(FiltroDominio filtro in filtros)
                {
                    alunosFiltrados = alunos.Where(x => x.GetPropertyValue(filtro.Propriedade).Equals(filtro.Valor)).ToList();
                }

                return alunosFiltrados;
            }
            else
            {
                return alunos;
            }
        }

        public void Inserir(AlunoDominio alunoNovo) 
        {
            string json = File.ReadAllText(path);
            var primeiroCadastro = String.IsNullOrEmpty(json);   

            if(primeiroCadastro)
            {
                using (StreamWriter file = File.CreateText(path))
                using (JsonTextWriter writer = new JsonTextWriter(file))
                {
                    writer.Formatting = Formatting.Indented;

                    writer.WriteStartArray();                    
                    EscreverNovoAluno(writer, alunoNovo);
                    writer.WriteEndArray();   
                }                    
            }
            else
            {                    
                List<AlunoDominio> alunos = JsonConvert.DeserializeObject<List<AlunoDominio>>(json);                
                alunos.Add(alunoNovo);

                using (StreamWriter file = File.CreateText(path))
                using (JsonTextWriter writer = new JsonTextWriter(file))
                {
                    writer.Formatting = Formatting.Indented;

                    writer.WriteStartArray();
                    foreach(AlunoDominio aluno in alunos)
                    {
                        EscreverNovoAluno(writer, aluno);
                    }                                            
                    writer.WriteEndArray();   
                }
            }
        }

        public void Update(AlunoDominio aluno, IEnumerable<FiltroDominio> filtros)
        {

        }

        public void Delete(IEnumerable<FiltroDominio> filtros)
        {

        }

        private void EscreverNovoAluno(JsonTextWriter writer, AlunoDominio aluno)
        {
            writer.WriteStartObject();
            writer.WritePropertyName("codigo");
            writer.WriteValue(aluno.Codigo);
            writer.WritePropertyName("nome");
            writer.WriteValue(aluno.Nome);                
            writer.WritePropertyName("nota");
            writer.WriteValue(aluno.Nota);
            writer.WriteEndObject();
        }
    }
}
