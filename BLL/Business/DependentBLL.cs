using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DTO;
using DAL.Entity;
using DAL.Persistence;

namespace BLL.Business
{
    public class DependentBLL
    {
        #region Atributos

        private DependentDAL dependentDAL;

        #endregion

        #region Construtor

        public DependentBLL()
        {
            dependentDAL = DependentDAL.GeneratesDependentDAL();
        }

        #endregion

        #region Metodos

        public static DependentBLL GeneratesDependentBLL()
        {
            return new DependentBLL();
        }

        //metodo registra o dependente associado ao funcionario
        public void RegisterDependent(Guid employeeId, string[] dependentName, string[] removeDependent)
        {
            try
            {
                StringBuilder concat = new StringBuilder();

                if (employeeId == null)
                    concat.AppendLine("Desculpe, ocorreu um erro no cadastro do dependente! ");

                if (dependentName != null && dependentName.Length > 0)
                {
                    for (int i = 0; i < dependentName.Length; i++)
                    {
                        if (dependentName[i].Length > 100)
                            concat.AppendLine("Desculpe, o campo Nome Dependente pode ter no máximo 100 caracteres na " + (i + 1) + "° inserção");

                        if (removeDependent[i] == null || removeDependent[i] == string.Empty)
                            concat.AppendLine("Por favor, informe o campo Excluir Dependente! ");
                        else if (removeDependent[i] != "S" && removeDependent[i] != "N")
                            concat.AppendLine("Por favor, não altere o html do campo Excluir Dependente! ");
                    }
                }

                if (concat.Length > 0)
                    throw new Exception(concat.ToString());

                for (int i = 0; i < dependentName.Length; i++)
                {
                    if (removeDependent[i] == "N")
                    {
                        Dependent dependent = new Dependent();

                        dependent.Employee = employeeId;
                        dependent.Name = dependentName[i];

                        dependentDAL.Insert(dependent);
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        //metodo retorna os nomes do dependentes associados ao funcionario
        public string ReturnDependentName(Guid employeeId)
        {
            try
            {
                List<Dependent> dependentList = new List<Dependent>();

                dependentList = dependentDAL.FindAllDependent(employeeId);

                StringBuilder concat = new StringBuilder();

                if (dependentList != null)
                {
                    for (int i = 0; i < dependentList.Count; i++)
                    {
                        concat.AppendLine(dependentList[i].Name + "; ");
                    }
                }
                else
                {
                    throw new Exception("Não há dependentes! ");
                }
                
                return concat.ToString();
            }
            catch
            {
                throw;
            }
        }

        //metodo remove todos dependentes associados ao funcionario
        public void DeleteAllDependent(Guid employeeId)
        {
            try
            {
                List<Dependent> dependentList = new List<Dependent>();

                dependentList = dependentDAL.FindAllDependent(employeeId);

                if (dependentList.Count > 0)
                {
                    for (int i = 0; i < dependentList.Count; i++)
                    {
                        dependentDAL.Delete(dependentList[i]);
                    }
                }
            }
            catch
            {

                throw;
            }
        }

        //metodo verifica se existe algum dependente associado ao funcionario
        public bool ExistDependent(Guid employeeId)
        {
            try
            {
                return dependentDAL.Exist(employeeId);
            }
            catch
            {
                throw;
            }
        }

        //metodo retorna a list de objetos "dependentes" associado ao funcionario
        public List<Dependent> ReturnDependent(Guid employeeId)
        {
            try
            {
                return dependentDAL.FindAllDependent(employeeId);
            }
            catch 
            {
                throw;
            }
        }

        #endregion
    }
}
