using System;
using System.Collections.Generic;
using System.Web.Mvc;
using DAL.Persistence;
using DAL.Entity;
using System.ComponentModel.DataAnnotations;

namespace DAL.DTO
{
    public class RegisterEmployeeDTO 
    {
        #region Atributos

        private RoleDAL roleDAL;

        public Guid Id { get; set; }

        [Display(Name = "Nome *")]
        [Required(ErrorMessage = "Por favor, informe o campo Nome")]
        [MaxLength(100, ErrorMessage = "Desculpe, o campo Nome pode ter no máximo 100 caracteres")]
        public string Name { get; set; }

        [Display(Name = "Email")]
        [MaxLength(100, ErrorMessage = "Desculpe, o campo Email pode ter no máximo 100 caracteres")]
        [RegularExpression("^[a-z0-9][-a-z0-9.!#$%&'*+-=?^_`{|}~\\/]+@([-a-z0-9]+\\.)+[a-z]{2,5}$", ErrorMessage = "Desculpe, formato de Email digitado não é válido")]
        public string Email { get; set; }

        [Display(Name = "Gênero *")]
        [Required(ErrorMessage = "Por favor, informe o campo Gênero")]
        public string Genre { get; set; }

        [Display(Name = "Nascimento")]
        [MaxLength(10, ErrorMessage = "Desculpe, o campo Nascimento pode ter no máximo 10 caracteres")]
        [RegularExpression("^([0]?[0-9]|[12][0-9]|[3][01])[./-]([0]?[1-9]|[1][0-2])[./-]([0-9]{4}|[0-9]{2})$", ErrorMessage = "Desculpe, formato de Nascimento digitado não é válido")]
        public string Birth { get; set; }

        [Display(Name = "Cargo *")]
        [Required(ErrorMessage = "Por favor, informe o campo Cargo")]
        public short RoleId { get; set; }

        [Display(Name = "Nome Dependente *")]
        [MaxLength(100, ErrorMessage = "Desculpe, o campo Nome Dependente pode ter no máximo 100 caracteres")]
        public string[] DependentName { get; set; }

        public short[] DependentNameId { get; set; }

        [Display(Name = "Excluir Dependente *")]
        public string[] RemoveDependent { get; set; }

        #endregion

        #region Construtores

        public RegisterEmployeeDTO()
        {
            this.roleDAL = RoleDAL.GeneratesRoleDAL();
        }

        #endregion

        #region Metodos

        public static RegisterEmployeeDTO GeneratesRegisterEmployeeDTO()
        {
            return new RegisterEmployeeDTO();
        }
        
        #endregion

        #region DropDownLists

        public List<SelectListItem> Role//Popula o dropdownlist  
        {
            get
            {
                List<SelectListItem> roleList = new List<SelectListItem>();

                try
                {
                    using (roleDAL)
                    {
                        foreach (Role role in roleDAL.FindAll())
                        {
                            SelectListItem item = new SelectListItem();

                            item.Value = role.Id.ToString();
                            item.Text = role.Name.ToString();

                            roleList.Add(item);
                        }
                    }
                }
                catch
                {
                    throw;
                }
                return roleList;
            }
        }

        #endregion
    }

    public class ConsultEmployeeDTO
    {
        #region Atributos

        public string NamePesq { get; set; }

        public int ReturnID { get; set; }

        public int Count { get; set; }

        public Guid EmployeeId { get; set; }

        [Display(Name = "Nome")]
        public string Name { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Gênero")]
        public string Genre { get; set; }

        [Display(Name = "Nascimento")]
        public string Birth { get; set; }

        [Display(Name = "Cargo")]
        public string Role { get; set; }

        [Display(Name = "Nome Dependente")]
        public string DependentName { get; set; }


        #endregion

        #region Construtores

        public ConsultEmployeeDTO()
        {

        }

        #endregion

        #region Metodos

        public static ConsultEmployeeDTO GeneratesConsultEmployeeDTO()
        {
            return new ConsultEmployeeDTO();
        }

        #endregion
    }
}
