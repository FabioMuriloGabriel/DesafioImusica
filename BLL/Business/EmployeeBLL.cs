using System;
using System.Collections.Generic;
using System.Text;
using DAL.DTO;
using DAL.Entity;
using DAL.Persistence;
using System.Text.RegularExpressions;

namespace BLL.Business
{
    public class EmployeeBLL 
    {
        #region Atributos

        private EmployeeDAL employeeDAL;
        private DependentBLL dependentBLL;
        private RoleDAL roleDAL;

        #endregion

        #region Construtor

        public EmployeeBLL()
        {
            this.employeeDAL = EmployeeDAL.GeneratesEmployeeDAL();
            this.dependentBLL = DependentBLL.GeneratesDependentBLL();
            this.roleDAL = RoleDAL.GeneratesRoleDAL();
        }
       
        #endregion

        #region Metodos

        public static EmployeeBLL GeneratesEmployeeBLL()
        {
            return new EmployeeBLL();
        }

        //metodo contem a regra de negocio para insercao do funcionario, e se existir, seus dependentes
        public void RegisterEmployee(RegisterEmployeeDTO registerEmployeeDTO)
        {
            try
            {
                StringBuilder concat = new StringBuilder();
             
                bool update = false;
                Employee employee = new Employee();
                if (Convert.ToString(registerEmployeeDTO.Id) != "00000000-0000-0000-0000-000000000000")
                {
                    employee = employeeDAL.FindById(registerEmployeeDTO.Id);
                    update = true;
                }

                if (registerEmployeeDTO.Name == null || registerEmployeeDTO.Name == string.Empty)
                    concat.AppendLine("Por favor, informe o campo Nome! ");
                else if (registerEmployeeDTO.Name.Length > 100)
                    concat.AppendLine("Desculpe, o campo Nome pode ter no máximo 100 caracteres! ");

                if (registerEmployeeDTO.Email != null && registerEmployeeDTO.Email != string.Empty)
                {
                    registerEmployeeDTO.Email = registerEmployeeDTO.Email.Trim();
                    
                    if (registerEmployeeDTO.Email.Length > 100)
                        concat.AppendLine("Desculpe, o campo Email pode ter no máximo 100 caracteres! ");
                    else if (update == false && employeeDAL.ExistEmail(registerEmployeeDTO.Email))
                        concat.AppendLine("Desculpe, o Email infomardo já esta cadastrado para outro funcionário! ");
                    else if (update == true && employeeDAL.ExistEmail(registerEmployeeDTO.Email) && registerEmployeeDTO.Email != employee.Email)
                            concat.AppendLine("Desculpe, o Email infomardo já esta cadastrado para outro funcionário! ");
                }

                if (registerEmployeeDTO.Genre == null || registerEmployeeDTO.Genre == string.Empty)
                    concat.AppendLine("Por favor, informe o campo Gênero! ");
                else if (registerEmployeeDTO.Genre != "M" && registerEmployeeDTO.Genre != "F")
                    concat.AppendLine("Por favor, não altere o html do campo Gênero! ");

                if (registerEmployeeDTO.Birth != null)
                {
                    if (registerEmployeeDTO.Birth.Length > 10)
                        concat.AppendLine("Desculpe, o campo Nascimento pode ter no máximo 10 caracteres");
                    else if (Convert.ToDateTime(DateTime.Now.ToShortDateString()).Year - Convert.ToDateTime(registerEmployeeDTO.Birth).Year <= 16)
                        concat.AppendLine("Desculpe, o campo Nascimento deve conter uma data que some mais ou seja igual a 16 anos! ");
                }

                if (registerEmployeeDTO.RoleId == 0)
                    concat.AppendLine("Por favor, informe o campo Cargo! ");
                else if (!roleDAL.Exist(registerEmployeeDTO.RoleId))
                    concat.AppendLine("Por favor, não altere o html do campo Cargo! ");

                if (registerEmployeeDTO.DependentName != null && registerEmployeeDTO.DependentName.Length > 0)
                {
                    for (int i = 0; i < registerEmployeeDTO.DependentName.Length; i++)
                    {
                        if (registerEmployeeDTO.DependentName[i].Length > 100)
                        {
                            concat.AppendLine("Desculpe, o campo Nome Dependente pode ter no máximo 100 caracteres na " + (i + 1) + "° inserção");

                            if (registerEmployeeDTO.RemoveDependent[i] == null || registerEmployeeDTO.RemoveDependent[i] == string.Empty)
                                concat.AppendLine("Por favor, informe o campo Excluir Dependente! ");
                            else if (registerEmployeeDTO.RemoveDependent[i] != "S" && registerEmployeeDTO.RemoveDependent[i] != "N")
                                concat.AppendLine("Por favor, não altere o html do campo Excluir Dependente! ");
                        }
                    }
                }

                if (concat.Length > 0)
                    throw new Exception(concat.ToString());

                employee = new Employee();

                if (update == false)
                    employee.Id = Guid.NewGuid();
                else
                    employee.Id = registerEmployeeDTO.Id;

                employee.Name = registerEmployeeDTO.Name;
                employee.Email = registerEmployeeDTO.Email;

                if (registerEmployeeDTO.Genre == "M")
                    employee.Genre = true;
                else if (registerEmployeeDTO.Genre == "F")
                    employee.Genre = false;

                if (registerEmployeeDTO.Birth != null)
                    employee.Birth = Convert.ToDateTime(registerEmployeeDTO.Birth);

                employee.Role = registerEmployeeDTO.RoleId;

                if (update == false)
                {
                    employee = employeeDAL.InsertReturn(employee);

                    if (registerEmployeeDTO.DependentName != null && registerEmployeeDTO.DependentName.Length > 0)
                        dependentBLL.RegisterDependent(employee.Id, registerEmployeeDTO.DependentName, registerEmployeeDTO.RemoveDependent);
                }
                else
                {
                    employeeDAL.Update(employee);

                    List<Dependent> dependentList = new List<Dependent>();

                    dependentList = dependentBLL.ReturnDependent(employee.Id);

                    if (dependentList.Count == 0 && registerEmployeeDTO.DependentName != null && registerEmployeeDTO.DependentName.Length > 0)
                        dependentBLL.RegisterDependent(employee.Id, registerEmployeeDTO.DependentName, registerEmployeeDTO.RemoveDependent);
                    else if (dependentList.Count > 0 && registerEmployeeDTO.DependentName != null && registerEmployeeDTO.DependentName.Length > 0)
                    {
                        dependentBLL.DeleteAllDependent(employee.Id);

                        dependentBLL.RegisterDependent(employee.Id, registerEmployeeDTO.DependentName, registerEmployeeDTO.RemoveDependent);
                    }
                } 
            }
            catch
            {
                throw;
            }
        }

        //metodo retorna o nome do funcionario, busca realiza pelo id
        public string ReturnEmployeeName(Guid employeeId)
        {
            try
            {
                
                Employee employee = new Employee();

                employee = employeeDAL.FindById(employeeId);
                
                return employee.Name;
            }
            catch
            {

                throw;
            }
        }

        //metodo remove o funcionario, e se existir, seus dependentes
        public void DeleteEmployee(Guid employeeId)
        {
            try
            {
                dependentBLL.DeleteAllDependent(employeeId);

                Employee employee = new Employee();

                employee = employeeDAL.FindById(employeeId);
                employeeDAL.Delete(employee);
            }
            catch 
            {

                throw;
            }
        }

        //metodo verifica se o funcionario pesquisado existe, busca realizada pelo nome
        public bool ExistEmployee(string employeeName)
        {
            try
            {
                Employee employee = new Employee();

                return employeeDAL.ExistEmployee(employeeName);
            }
            catch
            {

                throw;
            }
        }

        //metodo devolve o conteudo que sera exibido ja em formato DTO, mais tarde, pela view correspondente
        public List<ConsultEmployeeDTO> EmployeeList(string employeeName, int? page, int? pageSize)
        {
            try
            {
                List<ConsultEmployeeDTO> employeeListDTO = new List<ConsultEmployeeDTO>();

                List<Employee> employeeList = new List<Employee>();

                if (employeeName == string.Empty)
                    employeeList = employeeDAL.FindAllPage(page, pageSize);
                else
                    employeeList = employeeDAL.FindPage(employeeName, page, pageSize);

                if (employeeList.Count == 0)
                {
                    ConsultEmployeeDTO consultEmployeeDTO = new ConsultEmployeeDTO();

                    consultEmployeeDTO.Count = 0;

                    employeeListDTO.Add(consultEmployeeDTO);

                    return employeeListDTO;
                }

                int count = 0;

                foreach (var item in employeeList)
                {
                    ConsultEmployeeDTO consultEmployeeDTO = new ConsultEmployeeDTO();

                    consultEmployeeDTO.EmployeeId = item.Id;
                    consultEmployeeDTO.Name = item.Name;

                    if (item.Email == null || item.Email == string.Empty)
                        consultEmployeeDTO.Email = "Não há";
                    else
                    consultEmployeeDTO.Email = item.Email;

                    if (item.Genre == true)
                        consultEmployeeDTO.Genre = "Masculino";
                    else if (item.Genre == false)
                        consultEmployeeDTO.Genre = "Feminino";

                    if (item.Birth == null)
                    {
                        consultEmployeeDTO.Birth = "Não há";
                    }
                    else
                    {
                        string birth = Convert.ToString(item.Birth).Replace(" 00:00:00", "");
                        consultEmployeeDTO.Birth = birth;
                    }

                    Role role = new Role();
                    role = roleDAL.FindById(item.Role);

                    consultEmployeeDTO.Role = role.Name;

                    if (!dependentBLL.ExistDependent(item.Id))
                        consultEmployeeDTO.DependentName = "Não há";
                    else
                        consultEmployeeDTO.DependentName = dependentBLL.ReturnDependentName(item.Id);

                    if (count == 0)
                    {
                        if (employeeName == string.Empty)
                        {
                            count = employeeDAL.Count();
                            consultEmployeeDTO.NamePesq = "Não há";
                        }
                        else
                        {
                            count = employeeDAL.FindCount(employeeName);
                            consultEmployeeDTO.NamePesq = employeeName;
                        }
                    }
                    consultEmployeeDTO.Count = count;

                    employeeListDTO.Add(consultEmployeeDTO);

                }

                List<ConsultEmployeeDTO> employeeListDTOPag = new List<ConsultEmployeeDTO>();
                if (page != null)
                {
                    ConsultEmployeeDTO consultEmployeeDTONull = new ConsultEmployeeDTO();

                    int passou = (Convert.ToInt32(page) - 1) * Convert.ToInt32(pageSize);

                    for (int i = 0; i < passou; i++)
                    {
                        employeeListDTOPag.Add(consultEmployeeDTONull);
                    }

                    employeeListDTOPag.AddRange(employeeListDTO);

                    int cont = employeeListDTO[0].Count - employeeListDTOPag.Count;

                    for (int i = 0; i < cont; i++)
                    {
                        employeeListDTOPag.Add(consultEmployeeDTONull);
                    }
                    return employeeListDTOPag;
                }
                else
                    return employeeListDTO;

            }
            catch (Exception)
            {

                throw;
            }
        }

        //metodo retorna o DTO para edicao
        public RegisterEmployeeDTO ReturnEmployeeDTO(Guid employeeId)
        {
            try
            {
                RegisterEmployeeDTO registerEmployeeDTO = new RegisterEmployeeDTO();

                Employee employee = new Employee();

                employee = employeeDAL.FindById(employeeId);

                registerEmployeeDTO.Id = employee.Id;
                registerEmployeeDTO.Name = employee.Name;
                registerEmployeeDTO.Email = employee.Email;

                if (employee.Genre == true)
                    registerEmployeeDTO.Genre = "M";
                else if (employee.Genre == false)
                    registerEmployeeDTO.Genre = "F";

                registerEmployeeDTO.Birth = Convert.ToString(employee.Birth).Replace(" 00:00:00","");

                registerEmployeeDTO.RoleId = employee.Role;

                List<Dependent> dependentList = new List<Dependent>();

                dependentList = dependentBLL.ReturnDependent(employeeId);

                if (dependentList.Count > 0)
                {
                    registerEmployeeDTO.DependentName = new string[dependentList.Count];
                    registerEmployeeDTO.DependentNameId = new short[dependentList.Count];
                    for (int i = 0; i < dependentList.Count; i++)
                    {
                        registerEmployeeDTO.DependentName[i] = dependentList[i].Name;
                        registerEmployeeDTO.DependentNameId[i] = dependentList[i].Id;
                    }
                }

                return registerEmployeeDTO;
            }
            catch
            {
                throw;
            }
        }

        #endregion
    }
}
