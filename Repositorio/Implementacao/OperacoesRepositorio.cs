using System.Collections;
using Repositorio.Interface;
using System.Collections.Generic;
using Dominio;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace Repositorio.Implementacao
{
    public class OperacoesRepositorio : IOperacoesRepositorio
    {
        private string path = @"..\Repositorio\Banco\alunos.json";
        public AlunoDominio Listar(IEnumerable<FiltroDominio> filtros)
        {
            return new AlunoDominio();
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
                    escreverNovoAluno(writer, alunoNovo);
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
                        escreverNovoAluno(writer, alunoNovo);
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

        private void escreverNovoAluno(JsonTextWriter writer, AlunoDominio aluno)
        {
            writer.WriteStartObject();
            writer.WritePropertyName("codigo");
            writer.WriteValue(aluno.Codigo);
            writer.WritePropertyName("nome");
            writer.WriteValue(aluno.Nome);                
            writer.WritePropertyName("nota");
            writer.WriteValue(aluno.Nota);
            // writer.WriteEnd();
            writer.WriteEndObject();
        }
    }
}
