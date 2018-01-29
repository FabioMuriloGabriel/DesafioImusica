using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Entity;
using DAL.Generic;

namespace DAL.Persistence
{
    public class DependentDAL : GenericDal<Dependent, short>
    {
        #region Construtor

        public DependentDAL()
        {
        
        }

        #endregion

        #region Metodos

        public static DependentDAL GeneratesDependentDAL()
        {
            return new DependentDAL();
        }

        //retorna todo o conteudo dos dependentes associado a um funcionario
        public List<Dependent> FindAllDependent(Guid employeeId)
        {
            try
            {
                return Con.Dependent.Where(d => d.Employee == employeeId).ToList();
            }
            catch 
            {
                throw;
            }

        }

        //verifica se o registro existe que contenham o termo pesquisado
        public bool Exist(Guid employeeId)
        {
            try
            {
                return Con.Dependent.Where(d => d.Employee == employeeId).Count() > 0;
            }
            catch
            {
                throw;
            }

        }
        #endregion

    }
}
