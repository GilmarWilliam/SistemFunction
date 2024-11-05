using FI.AtividadeEntrevista.BLL;
using WebAtividadeEntrevista.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FI.AtividadeEntrevista.DML;

namespace WebAtividadeEntrevista.Controllers
{
    public class BeneficiariosController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Incluir()
        {
            return View();
        }

        [HttpPost]
        public JsonResult VerificarExistenciaCPF(string CPF)
        {
            BoBeneficiarios bo = new BoBeneficiarios();
            return bo.VerificarExistencia(CPF) ? Json("CPF ja existente no banco de dados") : Json("");
        }

        [HttpPost]
        public JsonResult Incluir(BeneficiariosModel model)
        {
            BoBeneficiarios bo = new BoBeneficiarios();
            
            if (!this.ModelState.IsValid)
            {
                List<string> erros = (from item in ModelState.Values
                                      from error in item.Errors
                                      select error.ErrorMessage).ToList();

                Response.StatusCode = 400;
                return Json(string.Join(Environment.NewLine, erros));
            }
            else
            {
                
                model.Id = bo.Incluir(new Beneficiarios()
                {      
                    CPF = model.CPF,
                    Nome = model.Nome,
                    IdCliente = model.IdCliente
                });

           
                return Json("Cadastro efetuado com sucesso");
            }
        }

        [HttpPost]
        public JsonResult Alterar(BeneficiariosModel model)
        {
            BoBeneficiarios bo = new BoBeneficiarios();
       
            if (!this.ModelState.IsValid)
            {
                List<string> erros = (from item in ModelState.Values
                                      from error in item.Errors
                                      select error.ErrorMessage).ToList();

                Response.StatusCode = 400;
                return Json(string.Join(Environment.NewLine, erros));
            }
            else
            {
                bo.Alterar(new Beneficiarios()
                {
                    CPF = model.CPF,
                    Nome = model.Nome,
                    IdCliente = model.IdCliente
                });
                               
                return Json("Cadastro alterado com sucesso");
            }
        }

        [HttpGet]
        public ActionResult Alterar(long id)
        {
            BoBeneficiarios bo = new BoBeneficiarios();
            Beneficiarios beneficiarios = bo.Consultar(id);
            Models.BeneficiariosModel model = null;

            if (beneficiarios != null)
            {
                model = new BeneficiariosModel()
                {
                    CPF = model.CPF,
                    Nome = model.Nome,
                    IdCliente = model.IdCliente
                };
            }

            return View(model);
        }

        [HttpPost]
        public JsonResult Excluir(long id)
        {
            BoBeneficiarios bo = new BoBeneficiarios();
            bo.Excluir(id);
            return Json("Cadastro excluído com sucesso");
        }

        //[HttpPost]
        //public JsonResult BeneficiariosList(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        //{
        //    try
        //    {
        //        int qtd = 0;
        //        string campo = string.Empty;
        //        string crescente = string.Empty;
        //        string[] array = jtSorting.Split(' ');

        //        if (array.Length > 0)
        //            campo = array[0];

        //        if (array.Length > 1)
        //            crescente = array[1];

        //        List<Beneficiarios> beneficiarios = new BoBeneficiarios().Pesquisa(jtStartIndex, jtPageSize, campo, crescente.Equals("ASC", StringComparison.InvariantCultureIgnoreCase), out qtd);

        //        //Return result to jTable
        //        return Json(new { Result = "OK", Records = beneficiarios, TotalRecordCount = qtd });
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { Result = "ERROR", Message = ex.Message });
        //    }
        //}
    }
}