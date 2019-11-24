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
    public class BloqueioRepositorio : IBloqueioRepositorio
    {
        private JsonSerializerSettings serializerSettings;
        private string path = @"..\Repositorio\Banco\bloqueios.json";
        private int tipoChaveBloqueios = EnumTiposChaves.Bloqueio.getInt();
        private readonly int tipoChaveBloqueio = EnumTiposChaves.Bloqueio.getInt();
        private readonly int tipoBloqueioExclusivo = EnumTiposBloqueios.Exclusivo.getInt();    
        private readonly int tipoBloqueioCompartilhado = EnumTiposBloqueios.Compartilhado.getInt();    
        private IChaveRepositorio _chaveRepositorio;
        public BloqueioRepositorio(IChaveRepositorio chaveRepositorio)
        {
            _chaveRepositorio = chaveRepositorio;

            serializerSettings = new JsonSerializerSettings 
            { 
                Formatting = Formatting.Indented,
                TypeNameHandling = TypeNameHandling.Auto 
            };
        }

        public bool IsBloqueado(int chave, int tid)
        {
            bool bloqueado = false;

            string json = File.ReadAllText(path);
            List<BloqueioDominio> bloqueios = JsonConvert.DeserializeObject<List<BloqueioDominio>>(json);
            
            if(bloqueios == null)
                return bloqueado;

            if(bloqueios.Count() > 0)
            {
                foreach(BloqueioDominio bloqueio in bloqueios)
                {
                    if(bloqueio.ChaveAluno == chave && bloqueio.Tid != tid)
                    {
                        bloqueado = true;
                    }
                }
            }

            return bloqueado;
        }

        public bool IsBloqueado(AlunoDominio aluno, int tipoBloqueio, int tid)
        {
            string json = File.ReadAllText(path);
            List<BloqueioDominio> bloqueios = JsonConvert.DeserializeObject<List<BloqueioDominio>>(json);
            bool bloqueado = false;

            if(bloqueios.Count() > 0)
            {
                foreach(BloqueioDominio bloqueio in bloqueios)
                {
                    if(bloqueio.ChaveAluno == aluno.Chave && bloqueio.Tid != tid)
                    {
                        if(tipoBloqueio != tipoBloqueioCompartilhado && bloqueio.Tipo != tipoBloqueioCompartilhado)
                        {
                            bloqueado = true;
                        }                        
                    }
                }
            }

            return bloqueado;
        }

        public void InserirBloqueio(BloqueioDominio bloqueio)
        {
            string json = File.ReadAllText(path);
            var primeiroCadastro = String.IsNullOrEmpty(json);  

            var bloqueios = new List<BloqueioDominio>(); 

            if(primeiroCadastro)
            {                
                bloqueios.Add(bloqueio);
                EscreverBloqueios(bloqueios);                    
            }
            else
            {                 
                bloqueios = JsonConvert.DeserializeObject<List<BloqueioDominio>>(json);

                if(bloqueios.Count() < 1)
                    throw new FileLoadException("Arquivo lido incorretamente");

                var bloqueioExiste = bloqueios.Exists(b => b.ChaveAluno == bloqueio.ChaveAluno && b.Tipo == bloqueio.Tipo);

                if(!bloqueioExiste) 
                    bloqueios.Add(bloqueio);               

                EscreverBloqueios(bloqueios);
            }
        }

        public void EncerrarBloqueio(int tid)
        {
            string json = File.ReadAllText(path);
            var bloqueios = JsonConvert.DeserializeObject<List<BloqueioDominio>>(json);
            var bloqueiosFiltrados = new List<BloqueioDominio>();

            if(bloqueios.Count() > 0)
            {                        
                foreach(BloqueioDominio bloqueio in bloqueios)
                {
                    bool deletar = bloqueio.Tid == tid;
                    if (!deletar)
                    {
                        bloqueiosFiltrados.Add(bloqueio);
                    }                
                }
            }

            EscreverBloqueios(bloqueiosFiltrados);
        }

        private void EscreverBloqueios(IEnumerable<BloqueioDominio> bloqueios)
        {
            var json = JsonConvert.SerializeObject(bloqueios, serializerSettings);
            File.WriteAllText(path, json);
        }
    }
}