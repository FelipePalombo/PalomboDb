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
        private int tipoChaveTransacao = EnumTiposChaves.Transacao.getInt();
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

            ChaveDominio chave = _chaveRepositorio.ProximaChave(tipoChaveTransacao);
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

            CriaArquivoOperacoes(transacao.Path);

            return transacao.Tid;
        }

        public IEnumerable<TransacaoDominio> ListarTransacoes()
        {
            string json = File.ReadAllText(pathControl);
            return JsonConvert.DeserializeObject<List<TransacaoDominio>>(json);
        }

        public TransacaoDominio ObterTransacaoPorTid(int tid)
        {
            string json = File.ReadAllText(pathControl);
            var retorno = JsonConvert.DeserializeObject<List<TransacaoDominio>>(json);
            return retorno.Where(x => x.Tid == tid).FirstOrDefault();
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
            string path = transacoes.Where(x => x.Tid == tid).First().Path;

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

        private void CriaArquivoOperacoes (string path)
        {
            var json = ""; //JsonConvert.SerializeObject("", serializerSettings);
            File.WriteAllText(path, json);
        }
    }        
}
