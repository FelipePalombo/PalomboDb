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
        private JsonSerializerSettings serializerSettings;
        private readonly ITransacaoRepositorio _transacaoRepositorio;
        private string path;
        public OperacoesRepositorio(ITransacaoRepositorio transacaoRepositorio)
        {
            _transacaoRepositorio = transacaoRepositorio;

            serializerSettings = new JsonSerializerSettings 
            { 
                Formatting = Formatting.Indented,
                TypeNameHandling = TypeNameHandling.Auto 
            };
        }

        public IEnumerable<AlunoDominio> Listar(IEnumerable<FiltroDominio> filtros, int tid)
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

        private IEnumerable<AlunoDominio> Listar(IEnumerable<FiltroDominio> filtros)
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

        public void Inserir(IEnumerable<AlunoDominio> alunosNovos, int tid) 
        {
            var transacao = _transacaoRepositorio.ObterTransacaoPorTid(tid);
            this.path = transacao.Path;

            string json = File.ReadAllText(path);
            var primeiroCadastro = String.IsNullOrEmpty(json);   

            if(primeiroCadastro)
            {
                EscreverNovasOperacao(alunosNovos);                    
            }
            else
            {                 
                List<AlunoDominio> alunos = JsonConvert.DeserializeObject<List<AlunoDominio>>(json);        
                List<AlunoDominio> alunosNovosLista = alunosNovos.ToList();
                alunos.Concat(alunosNovosLista);

                if(alunos.Count() < 1)
                    throw new FileLoadException("Arquivo lido incorretamente");

                EscreverNovasOperacao(alunos);
            }
        }

        public void Atualizar(AlunoDominio alunoAtualizacao, IEnumerable<FiltroDominio> filtros, int tid)
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

            EscreverNovasOperacao(alunosFiltrados);
        }

        public void Deletar(IEnumerable<FiltroDominio> filtros, int tid)
        {
            List<AlunoDominio> alunosFiltrados = new List<AlunoDominio>();

            if(filtros.Count() > 0)
            {
                var alunos = Listar(new List<FiltroDominio>());
            
                foreach(AlunoDominio aluno in alunos)
                {
                    bool deletar = IsFiltroCompativel(aluno, filtros);
                    if (!deletar)
                    {
                        alunosFiltrados.Add(aluno);
                    }                
                }
            }            

            EscreverNovasOperacao(alunosFiltrados);
        }

        private void EscreverNovasOperacao(IEnumerable<AlunoDominio> alunos)
        {
            var json = JsonConvert.SerializeObject(alunos, serializerSettings);
            File.WriteAllText(path, json);
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
