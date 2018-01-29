using System.Collections.Generic;

namespace DAL.Interface
{
    public interface IGenericDal<T, K>
        where T : class
        //T é um Generico que representará a entidade (Classe)
        where K : struct
        //K é um Generico que representará o tipo da chave primária
    {
        //assinaturas dos metodos
        void Insert(T obj);
        T InsertReturn(T obj);
        void Delete(T obj);
        List<T> FindAll();
        T FindById(K key);
        int Count();
        bool Exist(K key);
    }
}
