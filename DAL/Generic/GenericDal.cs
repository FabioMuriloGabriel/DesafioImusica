using DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Interface;

namespace DAL.Generic
{
    public abstract class GenericDal<T, K> : IGenericDal<T, K>, IDisposable
    where T : class
    where K : struct
    {
        //Atributo para a Classe de conexao
        protected Connection Con; 

        public void Dispose()//Fechar conexao
        {
            Con.Dispose();
        }

        public GenericDal()//Abrir conexao
        {
            //inicializar o atributo da Classe Conexao
            Con = new Connection(); 
        }

        //insere o objeto de maneira generica e retorna o mesmo
        public T InsertReturn(T obj)
        {
            try
            {
                var t = Con.Set<T>().Add(obj);
                Con.SaveChanges();
                return t;
            }
            catch
            {
                throw;
            }
        }

        //insere o objeto de maneira generica 
        public void Insert(T obj)
        {
            try
            {
                Con.Set<T>().Add(obj);
                Con.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        //remove o objeto de maneira generica
        public void Delete(T obj)
        {
            try
            {
                Con.Set<T>().Remove(obj);
                Con.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        //procura todo o conteudo de maneira generica de uma entidade 
        public List<T> FindAll()
        {
            try
            {
                return Con.Set<T>().ToList();
            }
            catch
            {
                throw;
            }
        }

        //procura de maneira generica determinado objeto utilizando como chave o id
        public T FindById(K key)
        {
            try
            {
                return Con.Set<T>().Find(key);
            }
            catch
            {
                throw;
            }
        }

        //retorna de maneira generica a quantidade de objetos da entidade
        public int Count()
        {
            try
            {
                return Con.Set<T>().Count();
            }
            catch 
            {
                throw;
            }
        }

        //verifica de maneira generica se o registro existe que contenham o termo pesquisado
        public bool Exist(K key)
        {
            try
            {
                var t = Con.Set<T>().Find(key);
                if (t != null)
                    return true;
                else
                    return false;
            }
            catch
            {
                throw;
            }
        }
    }
}
