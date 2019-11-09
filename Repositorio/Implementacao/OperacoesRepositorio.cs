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

        public IEnumerable<RegistroDominio> ListarRegistros()
        {
            var alunos = Listar(new List<FiltroDominio>());
            List<RegistroDominio> registros = new List<RegistroDominio>(); 
            foreach(AlunoDominio aluno in alunos)
            {
                registros.Add(new RegistroDominio { Codigo = aluno.Codigo, Bloqueado = 'N'});
            }
            return registros;
        }

        public void Inserir(IEnumerable<AlunoDominio> alunosNovos) 
        {
            string json = File.ReadAllText(path);
            var primeiroCadastro = String.IsNullOrEmpty(json);   

            if(primeiroCadastro)
            {
                EscreverNovosAlunos(alunosNovos);                    
            }
            else
            {                    
                List<AlunoDominio> alunos = JsonConvert.DeserializeObject<List<AlunoDominio>>(json);        
                alunos.Concat(alunosNovos);

                EscreverNovosAlunos(alunos);
            }
        }

        public void Atualizar(AlunoDominio alunoAtualizacao, IEnumerable<FiltroDominio> filtros)
        {
            var alunos = Listar(new List<FiltroDominio>());
            List<AlunoDominio> alunosFiltrados = new List<AlunoDominio>();
            foreach(AlunoDominio aluno in alunos)
            {
                bool atualizar = IsFiltroCompativel(aluno, filtros);
                if (atualizar)
                {
                    aluno.AtualizarCampoNecessario(alunoAtualizacao);
                }
                alunosFiltrados.Add(aluno);
            }

            EscreverNovosAlunos(alunosFiltrados);
        }

        public void Deletar(IEnumerable<FiltroDominio> filtros)
        {
            var alunos = Listar(new List<FiltroDominio>());
            List<AlunoDominio> alunosFiltrados = new List<AlunoDominio>();
            foreach(AlunoDominio aluno in alunos)
            {
                bool deletar = IsFiltroCompativel(aluno, filtros);
                if (!deletar)
                {
                    alunosFiltrados.Add(aluno);
                }                
            }

            EscreverNovosAlunos(alunosFiltrados);
        }

        private void PrepararNovoAluno(JsonTextWriter writer, AlunoDominio aluno)
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

        private void EscreverNovosAlunos(IEnumerable<AlunoDominio> alunos)
        {
            using (StreamWriter file = File.CreateText(path))
            using (JsonTextWriter writer = new JsonTextWriter(file))
            {
                writer.Formatting = Formatting.Indented;

                writer.WriteStartArray();
                foreach(AlunoDominio aluno in alunos)
                {
                    PrepararNovoAluno(writer, aluno);
                }                                            
                writer.WriteEndArray();   
            }
        }

        private bool IsFiltroCompativel(AlunoDominio aluno, IEnumerable<FiltroDominio> filtros)
        {
            bool compativel = true;
            foreach(FiltroDominio filtro in filtros)
            {
                compativel = aluno.GetPropertyValue(filtro.Propriedade).Equals(filtro.Valor);
                if (compativel == false) 
                    return false;
            }
            return compativel;
        }
    }
}
