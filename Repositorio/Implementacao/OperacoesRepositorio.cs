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
        private string pathAlunos = @"..\Repositorio\Banco\alunos.json";
        public OperacoesRepositorio(ITransacaoRepositorio transacaoRepositorio)
        {
            _transacaoRepositorio = transacaoRepositorio;

            serializerSettings = new JsonSerializerSettings 
            { 
                Formatting = Formatting.Indented,
                TypeNameHandling = TypeNameHandling.Auto 
            };
        }

        public IEnumerable<AlunoDominio> Listar(OperacaoDominio operacao, TransacaoDominio transacao)
        {
            this.path = transacao.Path;
            
            List<AlunoDominio> alunos = this.ListarTudo(operacao, transacao).ToList();

            var filtros = operacao.Filtros;

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

        private IEnumerable<AlunoDominio> ListarTudo(OperacaoDominio operacao, TransacaoDominio transacao)
        {
            string jsonRepo = File.ReadAllText(pathAlunos);
            var alunos = !String.IsNullOrWhiteSpace(jsonRepo) ? JsonConvert.DeserializeObject<List<AlunoDominio>>(jsonRepo) : new List<AlunoDominio>();
            
            string json = File.ReadAllText(path);
            if(!String.IsNullOrWhiteSpace(json))
            {
                List<OperacaoDominio> operacoes = JsonConvert.DeserializeObject<List<OperacaoDominio>>(json);
                foreach(OperacaoDominio operacaoAjustes in operacoes)
                {
                    if(operacaoAjustes.Tipo == EnumTiposOperacoes.Insert.getInt())
                    {
                        var alunosNovos = operacaoAjustes.Alunos;
                        alunos = alunos.Concat(alunosNovos).ToList();                        
                    }
                    else if (operacaoAjustes.Tipo == EnumTiposOperacoes.Update.getInt())
                    {                        
                        foreach(AlunoDominio aluno in alunos)
                        {
                            bool atualizar = IsFiltroCompativel(aluno, operacaoAjustes.Filtros);
                            if (atualizar)
                            {
                                aluno.AtualizarCampoNecessario(operacaoAjustes.Alunos.FirstOrDefault());
                            }
                        }
                    }
                    else if (operacaoAjustes.Tipo == EnumTiposOperacoes.Delete.getInt())
                    {
                        List<AlunoDominio> filtradosDelete = new List<AlunoDominio>();
                        var filtrosAjustes = operacaoAjustes.Filtros;

                        if(filtrosAjustes.Count() > 0)
                        {                        
                            foreach(AlunoDominio aluno in alunos)
                            {
                                bool deletar = IsFiltroCompativel(aluno, filtrosAjustes);
                                if (!deletar)
                                {
                                    filtradosDelete.Add(aluno);
                                }                
                            }
                        }

                        alunos = new List<AlunoDominio>();
                        alunos = alunos.Concat(filtradosDelete).ToList();            
                    }
                }          
            }

            return alunos;
        }

        public IEnumerable<RegistroDominio> ListarRegistros()
        {
            string json = File.ReadAllText(@"..\Repositorio\Banco\alunos.json");
            List<AlunoDominio> alunos = JsonConvert.DeserializeObject<List<AlunoDominio>>(json);  

            List<RegistroDominio> registros = new List<RegistroDominio>(); 
            foreach(AlunoDominio aluno in alunos)
            {
                registros.Add(new RegistroDominio { Codigo = aluno.Codigo, Bloqueado = 'N'});
            }
            return registros;
        }

        public void Inserir(OperacaoDominio operacao, string pathOp) 
        {
            this.path = pathOp;

            string json = File.ReadAllText(path);
            var primeiroCadastro = String.IsNullOrEmpty(json);  

            var operacoes = new List<OperacaoDominio>(); 

            if(primeiroCadastro)
            {                
                operacoes.Add(operacao);
                EscreverOperacoes(operacoes);                    
            }
            else
            {                 
                operacoes = JsonConvert.DeserializeObject<List<OperacaoDominio>>(json); 
                operacoes.Add(operacao);

                if(operacoes.Count() < 1)
                    throw new FileLoadException("Arquivo lido incorretamente");

                EscreverOperacoes(operacoes);
            }
        }

        public void Atualizar(OperacaoDominio operacao, string pathOp)
        {
            this.path = pathOp;

            string json = File.ReadAllText(path);
            var primeiroCadastro = String.IsNullOrEmpty(json);  

            var operacoes = new List<OperacaoDominio>(); 

            if(primeiroCadastro)
            {                
                operacoes.Add(operacao);
                EscreverOperacoes(operacoes);                    
            }
            else
            {                 
                operacoes = JsonConvert.DeserializeObject<List<OperacaoDominio>>(json); 
                operacoes.Add(operacao);

                if(operacoes.Count() < 1)
                    throw new FileLoadException("Arquivo lido incorretamente");

                EscreverOperacoes(operacoes);
            }
        }

        public void Deletar(OperacaoDominio operacao, string pathOp)
        {
            this.path = pathOp;

            string json = File.ReadAllText(path);
            var primeiroCadastro = String.IsNullOrEmpty(json);  

            var operacoes = new List<OperacaoDominio>(); 

            if(primeiroCadastro)
            {                
                operacoes.Add(operacao);
                EscreverOperacoes(operacoes);                    
            }
            else
            {                 
                operacoes = JsonConvert.DeserializeObject<List<OperacaoDominio>>(json); 
                operacoes.Add(operacao);

                if(operacoes.Count() < 1)
                    throw new FileLoadException("Arquivo lido incorretamente");

                EscreverOperacoes(operacoes);
            }
        }

        private void EscreverOperacoes(IEnumerable<OperacaoDominio> operacoes)
        {
            var json = JsonConvert.SerializeObject(operacoes, serializerSettings);
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
