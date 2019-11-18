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
    public class TransacaoRepositorio : ITransacaoRepositorio
    {
        private JsonSerializerSettings serializerSettings;
        private string pathControl = @"..\Repositorio\Banco\trans_control.json";
        private int tipoTransacao = EnumTiposChaves.Transacao.getInt();
        private IChaveRepositorio _chaveRepositorio;
        public TransacaoRepositorio(IChaveRepositorio chaveRepositorio)
        {
            _chaveRepositorio = chaveRepositorio;
            serializerSettings = new JsonSerializerSettings 
            { 
                Formatting = Formatting.Indented,
                TypeNameHandling = TypeNameHandling.Auto 
            };
        }

        public int NovaTransacao()
        {
            string json = File.ReadAllText(pathControl);
            var primeiroCadastro = String.IsNullOrEmpty(json);

            ChaveDominio chave = _chaveRepositorio.ProximaChave(tipoTransacao);
            List<TransacaoDominio> transacoes;
            TransacaoDominio transacao = new TransacaoDominio { Tid = chave.ProximaChave, Status = 0 };

            if(primeiroCadastro)
            {
                transacoes = new List<TransacaoDominio>();
                transacoes.Add(transacao);
                EscreverNovasTransacoes(transacoes);
            }
            else
            {
                transacoes = JsonConvert.DeserializeObject<List<TransacaoDominio>>(json);
                transacoes.Add(transacao);
                EscreverNovasTransacoes(transacoes);
            }

            CriaArquivoProcessos(transacao.Path);

            return transacao.Tid;
        }

        public IEnumerable<TransacaoDominio> ListarTransacoes()
        {
            string json = File.ReadAllText(pathControl);
            return JsonConvert.DeserializeObject<List<TransacaoDominio>>(json);
        }

        public void CommitTransacao(int tid)
        {
            var transacoes = ListarTransacoes();
            List<TransacaoDominio> transacoesAtualizadas = new List<TransacaoDominio>();

            foreach(TransacaoDominio transacao in transacoes)
            {
                if(transacao.Tid == tid)
                {
                    transacao.Status = 1;
                }

                transacoesAtualizadas.Add(transacao);
            }

            EscreverNovasTransacoes(transacoesAtualizadas);
        }

        public void RollbackTransacao(int tid)
        {
            var transacoes = ListarTransacoes();
            List<TransacaoDominio> transacoesAtualizadas = new List<TransacaoDominio>();
            string path = transacoesAtualizadas.Where(x => x.Tid == tid).First().Path;

            foreach(TransacaoDominio transacao in transacoes)
            {
                if(transacao.Tid != tid)
                {
                    transacoesAtualizadas.Add(transacao);
                }               
            }

            File.Delete(path);
            EscreverNovasTransacoes(transacoesAtualizadas);
        }             

        private void EscreverNovasTransacoes(IEnumerable<TransacaoDominio> transacoes)
        {
            var json = JsonConvert.SerializeObject(transacoes, serializerSettings);
            File.WriteAllText(pathControl, json);
        }

        private void CriaArquivoProcessos (string path)
        {
            List<TransacaoDominio> transacoes = new List<TransacaoDominio>();
            var json = JsonConvert.SerializeObject(transacoes, serializerSettings);
            File.WriteAllText(path, json);
        }
    }        
}
