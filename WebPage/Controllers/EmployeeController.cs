using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL.Interface;
using BLL.Business;
using DAL.DTO;
using PagedList;
using OfficeOpenXml;
using System.Drawing;
using OfficeOpenXml.Style;
using System.IO;

namespace WebPage.Controllers
{
    [RoutePrefix("employee")]
    public class EmployeeController : Controller
    {
        #region Atributos

        private EmployeeBLL employeeBLL;

        #endregion

        #region Construtores

        public EmployeeController()
        {
            employeeBLL = EmployeeBLL.GeneratesEmployeeBLL();
        }


        #endregion

        #region Metodos

        //retorna o contador para a criacao dos fields de dependentes
        [Route("returncont")]
        public ActionResult ReturnCont()
        {
            int cont = Convert.ToInt32(Session["cont"]);
            cont++;
            Session["cont"] = cont;
            return Content(cont.ToString());
        }

        //retorna a lista de dependentes associados a um funcionario
        [Route("returndependentname/{employeeId:Guid}")]
        public ActionResult returnDependentName(Guid employeeId)
        {
            string dependentList = string.Empty;
            try
            {
                DependentBLL DependentBLL = new DependentBLL();
                dependentList = DependentBLL.ReturnDependentName(employeeId);
            }
            catch
            {
                throw;
            }
            return Content(dependentList);
        }

        //retorna o nome do funcionario
        [Route("returnemployeename/{employeeId:Guid}")]
        public ActionResult returnEmployeeName(Guid employeeId)
        {
            string employeeName = string.Empty;
            try
            {
                employeeName = employeeBLL.ReturnEmployeeName(employeeId);
            }
            catch
            {
                throw;
            }
            return Content(employeeName);
        }
        
        #endregion

        #region Lista

        private ExcelPackage GereneratesAllReport()//configura o excel 
        {
            ExcelPackage arquivoExcel = new ExcelPackage();
            try
            {
                List<ConsultEmployeeDTO> employeeList = new List<ConsultEmployeeDTO>();

                employeeList = employeeBLL.EmployeeList(string.Empty, null, null);

                arquivoExcel = new ExcelPackage();

                // CRIANDO (ADD) uma planilha neste arquivo e obtendo a referência para meu código operá-la.
                ExcelWorksheet planilha = arquivoExcel.Workbook.Worksheets.Add("Aba 1");

                // (operações para gerar o arquivo)
                // ESCREVENDO O CABECALHO
                // Uma forma de escrever: informando endereco da celula
                //planilha.Cells["A1"].Value = "Histórico de Entregas Realizadas";
                // Outra forma de escrever: informando line e column da celula
                // lines e columns das células começam no 1
                int line = 1;
                int column = 1;
                int lineInicioTabela; // Servirá para marcar o início da tabela, pra depois aplicar as bordas no final

                planilha.Cells.Style.Font.Name = "Arial";      // Fonte Arial no documento inteiro
                planilha.Cells.Style.Font.Size = 11;           // Aplicando tamanho 11 no documento inteiro

                // Escrevendo cabeçalho principal
                planilha.Cells[line, column, line, column + 10].Merge = true;  // Mesclando células
                planilha.Cells[line, column].Style.Font.Bold = true;           // Deixando negrito
                planilha.Cells[line, column].Style.Font.Size = 16;             // Aplicando tamanho 18 na fonte
                planilha.Cells[line, column].Style.Font.Name = "Calibri";      // Fonte Calibri
                planilha.Cells[line, column].Style.Font.Color.SetColor(Color.FromArgb(204, 0, 0)); // Cor da fonte. Você pode usar RGB ou uma pronta, assim: Color.White
                planilha.Cells[line++, column].Value = DateTime.Now + " Desafio iMúsica";
                line++;

                planilha.Cells[line, column, line, column + 1].Merge = true;  // Mesclando células
                planilha.Cells[line, column].Style.Font.Bold = true;           // Deixando negrito
                planilha.Cells[line++, column].Value = "Quantidade de itens: " + employeeList.Count;
                line++;

                // Escrevendo cabecalho tabela
                lineInicioTabela = line;  // O cabecalho faz parte da tabela ;)
                planilha.Cells[line, column, line, column + 5].Style.Fill.PatternType = ExcelFillStyle.Solid;     // preenchimento de fundo sólido
                Color colour = ColorTranslator.FromHtml("#D9EDF7");
                planilha.Cells[line, column, line, column + 5].Style.Fill.BackgroundColor.SetColor(colour);   // Cor do preenchimento de fundo
                planilha.Cells[line, column, line, column + 5].Style.Font.Bold = true;                            // Deixando negrito o cabecalho da tabela
                planilha.Cells[line, column, line, column + 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;   // Centralizando o alinemento
                planilha.Cells[line, column++].Value = "Nome";
                planilha.Cells[line, column++].Value = "Email";
                planilha.Cells[line, column++].Value = "Gênero";
                planilha.Cells[line, column++].Value = "Nascimento";
                planilha.Cells[line, column++].Value = "Cargo";
                planilha.Cells[line, column++].Value = "Dependentes";

                line++;

                // Escrevendo tabela
                foreach (var item in employeeList)
                {
                    column = 1;
                    planilha.Cells[line, column++].Value = item.Name;
                    planilha.Cells[line, column++].Value = item.Email;
                    planilha.Cells[line, column++].Value = item.Role;
                    planilha.Cells[line, column++].Value = item.Birth;
                    planilha.Cells[line, column++].Value = item.Role;
                    planilha.Cells[line, column++].Value = item.DependentName;

                    line++;
                }

                // Colocando as bordas da tabela
                column = 1;
                planilha.Cells[lineInicioTabela, column, line - 1, column + 5].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                planilha.Cells[lineInicioTabela, column, line - 1, column + 5].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                planilha.Cells[lineInicioTabela, column, line - 1, column + 5].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                planilha.Cells[lineInicioTabela, column, line - 1, column + 5].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                // Auto ajustando a largura das células da tabela
                planilha.Cells[lineInicioTabela, column, line - 1, column + 5].AutoFitColumns();

            }
            catch
            {
                throw;
            }
            return arquivoExcel;
        }

        [HttpPost]
        [Route("gereneratesallreportaction")]
        public ActionResult GereneratesAllReportAction()//rota para inicio da criacao do arquivo 
        {
            try
            {
                // Gerando minha planilha e recebendo-a
                using (ExcelPackage arquivoExcel = GereneratesAllReport())
                {
                    var stream = new MemoryStream();
                    arquivoExcel.SaveAs(stream);

                    string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    DateTime date = DateTime.Now;

                    string original = Convert.ToString(date);

                    string[] fields = original.Split('/');

                    string newLine = String.Join("_", fields);

                    fields = newLine.Split(':');
                    newLine = String.Join("_", fields);

                    fields = newLine.Split(' ');
                    newLine = String.Join("_", fields);

                    string fileName = "Relatorio_iMusica_todos_funcionarios_" + newLine + ".xlsx";

                    stream.Position = 0;
                    return File(stream, contentType, fileName);
                }
            }
            catch (Exception e)
            {
                TempData["mensagemErro"] = e.Message;
                return RedirectToAction("listemployee", "employee");
            }
        }

        [Route("listemployee/{page:int?}")]
        public ActionResult ListEmployee(int? page)//retorna a lista de funcionarios
        {
            int itemsPerPage = 10;
            int pageNumber = page ?? 1;

            List<ConsultEmployeeDTO> employeeList = new List<ConsultEmployeeDTO>();

            try
            {
                employeeList = employeeBLL.EmployeeList(string.Empty, pageNumber, itemsPerPage);
            }
            catch (Exception e)
            {
                TempData["mensagemErro"] = e.Message;
            }
            return View(employeeList.ToPagedList(pageNumber, itemsPerPage));
        }

        [Route("deleteemployee/{employeeId:Guid}")]
        public JsonResult DeleteEmployee(Guid employeeId)//remove por completo o funcionario, incluindo seus dependentes
        {
            try
            {
                employeeBLL.DeleteEmployee(employeeId);

                TempData["mensagemSucesso"] = "Registro removido com sucesso!";
            }
            catch (Exception e)
            {
                TempData["mensagemErro"] = e.Message;
            }
            return Json("");
        }

        #endregion

        #region Pesquisa

        [Route("findnameemployee/{employeeName}/{page:int?}")]
        public ActionResult FindNameEmployee(string employeeName, int? page)//monta a pagina de pesquisa por nome do funcionario, utilizando "like"
        {
            int itemsPerPage = 10;
            int pageNumber = page ?? 1;

            List<ConsultEmployeeDTO> employeeList = new List<ConsultEmployeeDTO>();

            try
            {
                employeeName = HttpUtility.UrlDecode(employeeName, System.Text.Encoding.GetEncoding("ISO-8859-1"));


                employeeList = employeeBLL.EmployeeList(employeeName, pageNumber, itemsPerPage);
            }
            catch (Exception e)
            {
                TempData["mensagemErro"] = e.Message;
            }
            return View(employeeList.ToPagedList(pageNumber, itemsPerPage));
        }

        [Route("findexistnameemployee/{employeeName}")]
        public JsonResult FindExistNameEmployee(string employeeName)//verifica se existe o funcionario, essa chamada é realizada via ajax no arquivo "script/site/site.js"
        {
            try
            {
                employeeName = HttpUtility.UrlDecode(employeeName, System.Text.Encoding.GetEncoding("ISO-8859-1"));

                bool existEmployee = employeeBLL.ExistEmployee(employeeName);

                if (existEmployee)
                {
                    TempData["mensagemSucesso"] = "Registro encontrado com sucesso!";
                    return Json(existEmployee);
                }
                else
                {
                    throw new Exception("Registro não encontrado! ");
                }
            }
            catch (Exception e)
            {
                TempData["mensagemErro"] = e.Message;
            }
            return Json("");
        }

        #endregion

        #region Registro

        [Route("register/{employeeId:Guid?}")]
        public ActionResult Register(Guid? employeeId)//registra e edita o funcionario, se ja existir o id, será uma edicao
        {
            var model = new RegisterEmployeeDTO();
            try
            {
                if (employeeId == null)
                    Session["cont"] = -1;
                else
                {
                    model = employeeBLL.ReturnEmployeeDTO((Guid)employeeId);

                    if (model.DependentNameId != null)
                        Session["cont"] = model.DependentNameId.Length - 1;
                    else
                        Session["cont"] = -1;
                }
            }
            catch (Exception e)
            {
                TempData["mensagemErro"] = e.Message;
            }
            return View(model);

        }

        [HttpPost]
        [Route("registeremployee")]
        public ActionResult RegisterEmployee(RegisterEmployeeDTO model)//submete os dados do funcionario
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool update = false;
                    if (Convert.ToString(model.Id) != "00000000-0000-0000-0000-000000000000")
                        update = true;

                    employeeBLL.RegisterEmployee(model);

                    ModelState.Clear();

                    if (update == false)
                        TempData["mensagemSucesso"] = "Registro cadastrado com sucesso!";
                    else
                        TempData["mensagemSucesso"] = "Registro atualizado com sucesso!";

                    return RedirectToAction("listemployee", "employee");
                }
            }
            catch (Exception e)
            {
                TempData["mensagemErro"] = e.Message;
            }
            return View("register", new RegisterEmployeeDTO());
        }

        #endregion
    }
}