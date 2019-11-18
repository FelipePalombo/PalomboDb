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
    public class ChaveRepositorio : IChaveRepositorio
    {
        private string path = @"..\Repositorio\Banco\chaves.json";        
        public ChaveDominio ProximaChave(int tipoChave)
        {
            List<ChaveDominio> chaves = this.ListarChaves();
            List<ChaveDominio> chavesAtualizadas = new List<ChaveDominio>();
            var chaveFiltro = chaves.Where(chave => chave.Tipo == tipoChave).First();
            
            ChaveDominio chaveRetorno = new ChaveDominio 
            {
                Tipo = chaveFiltro.Tipo, 
                ProximaChave = chaveFiltro.ProximaChave,
                ChaveAtual = chaveFiltro.ChaveAtual
            };

            foreach(ChaveDominio chave in chaves)
            {
                if (chave.Tipo == tipoChave)
                {
                    chave.ProximaChave++;
                    chave.ChaveAtual++;
                }

                chavesAtualizadas.Add(chave);
            }

            EscreverNovasChaves(chavesAtualizadas);
            
            return chaveRetorno;
        }

        public List<ChaveDominio> ListarChaves ()
        {
            string json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<List<ChaveDominio>>(json);            
        }

        private void EscreverNovasChaves(IEnumerable<ChaveDominio> chaves)
        {
            using (StreamWriter file = File.CreateText(path))
            using (JsonTextWriter writer = new JsonTextWriter(file))
            {
                writer.Formatting = Formatting.Indented;

                writer.WriteStartArray();
                foreach(ChaveDominio chave in chaves)
                {
                    PrepararChave(writer, chave);
                }                                            
                writer.WriteEndArray();   
            }
        }

        private void PrepararChave(JsonTextWriter writer, ChaveDominio chave)
        {
            writer.WriteStartObject();
            writer.WritePropertyName("tipo");
            writer.WriteValue(chave.Tipo);
            writer.WritePropertyName("proximaChave");
            writer.WriteValue(chave.ProximaChave);                
            writer.WritePropertyName("chaveAtual");
            writer.WriteValue(chave.ChaveAtual);
            writer.WriteEndObject();
        }
    }        
}
