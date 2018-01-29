using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Entity;
using DAL.Generic;

namespace DAL.Persistence
{
    public class EmployeeDAL : GenericDal<Employee, Guid>
    {
        #region Construtor

        public EmployeeDAL()
        {

        }

        #endregion

        #region Metodos

        public static EmployeeDAL GeneratesEmployeeDAL()
        {
            return new EmployeeDAL();
        }

        //retorna todo o conteudo de funcionario otimizado de 10 em 10, é gerado um full query para gerar o relatorio
        public List<Employee> FindAllPage(int? page, int? pageSize)
        {
            try
            {
                List<Employee> list_A = new List<Employee>();

                int total = 0;
                int skip = 0;
                int pass = 0;
                bool truth = false;

                if (page != null)
                {
                    if (!truth)
                    {
                        skip = Convert.ToInt32(pageSize) * (Convert.ToInt32(page) - 1);
                        pass = Convert.ToInt32(pageSize) * (Convert.ToInt32(page) - 1);
                    }

                    list_A = Con.Employee.OrderBy(e => e.Name).Skip(skip).Take(Convert.ToInt32(pageSize)).ToList();

                    if (list_A.Count < pageSize)
                    {
                        page = 1;
                        truth = true;
                        skip = 0;
                    }
                    else
                        truth = false;

                    total = total + Con.Employee.Count();
                    if (truth && pass > total)
                        skip = pass - total;
                    if (skip < 0)
                        skip = 0;
                }
                else
                {
                    list_A = Con.Employee.OrderBy(e => e.Name).ToList();
                }

                return list_A;
            }
            catch
            {
                throw;
            }
        }

        //retorna o conteudo de funcionario com a pesquisa pelo nome otimizado de 10 em 10
        public List<Employee> FindPage(string employeeName, int? page, int? pageSize)
        {
            try
            {
                List<Employee> list_A = new List<Employee>();

                int total = 0;
                int skip = 0;
                int pass = 0;
                bool truth = false;

                if (page != null)
                {
                    if (!truth)
                    {
                        skip = Convert.ToInt32(pageSize) * (Convert.ToInt32(page) - 1);
                        pass = Convert.ToInt32(pageSize) * (Convert.ToInt32(page) - 1);
                    }

                    list_A = Con.Employee.Where(e => e.Name.Contains(employeeName)).OrderBy(e => e.Name).Skip(skip).Take(Convert.ToInt32(pageSize)).ToList();

                    if (list_A.Count < pageSize)
                    {
                        page = 1;
                        truth = true;
                        skip = 0;
                    }
                    else
                        truth = false;

                    total = total + Con.Employee.Where(e => e.Name.Contains(employeeName)).Count();
                    if (truth && pass > total)
                        skip = pass - total;
                    if (skip < 0)
                        skip = 0;
                }

                return list_A;
            }
            catch
            {
                throw;
            }
        }

        //retorna a quantidade de registros que contenham o termo pesquisado
        public int FindCount(string employeeName)
        {
            try
            {
                return Con.Employee.Where(e => e.Name.Contains(employeeName)).Count();
            }
            catch
            {
                throw;
            }
        }

        //verifica se o registro existe que contenham o termo pesquisado
        public bool ExistEmployee(string employeeName)
        {
            try
            {
                return Con.Employee.Where(e => e.Name.Contains(employeeName)).Count() > 0;
            }
            catch
            {
                throw;
            }
        }

        //verifica se o registro existe que contenham o termo pesquisado
        public bool ExistEmail(string email)
        {
            try
            {
                return Con.Employee.Where(e => e.Email == email).Count() > 0; ;
            }
            catch
            {
                throw;
            }
        }

        //faz o update do registro
        public void Update(Employee employee)
        {
            try
            {
                Employee change = new Employee();

                change = Con.Employee.Where(e => e.Id == employee.Id).FirstOrDefault();

                if (change != null)
                {
                    change.Name = employee.Name;
                    change.Email = employee.Email;
                    change.Genre = employee.Genre;
                    change.Birth = employee.Birth;
                    change.Role = employee.Role;

                    Con.SaveChanges();
                }
                else
                {
                    throw new Exception("Registro não encontrado!");
                }
            }
            catch
            {

                throw;
            }
        }

        #endregion

    }
}
