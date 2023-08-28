using FI.AtividadeEntrevista.BLL;
using FI.AtividadeEntrevista.DML;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using WebAtividadeEntrevista.Models;

namespace WebAtividadeEntrevista.Controllers
{
    public class BeneficiarioController : Controller
    {

        [HttpPost]
        public JsonResult ObterListaBeneficiarios()
        {
            List<BeneficiarioModel> beneficarios = Session["beneficiarios"] as List<BeneficiarioModel>;

            if (beneficarios == null)
            {
                beneficarios = new List<BeneficiarioModel>();
            }

            string beneficiariosJSON = JsonConvert.SerializeObject(beneficarios);
            return Json(beneficiariosJSON);
        }

        [HttpPost]
        public JsonResult Incluir(BeneficiarioModel model)
        {
            if(string.IsNullOrEmpty(model.Cpf) || string.IsNullOrEmpty(model.Nome))
            {
                Response.StatusCode = 400;
                return Json(string.Join(Environment.NewLine, "Dados inválidos"));
            }

            string cpf = Regex.Replace(model.Cpf, "[^0-9]", "");

            BoBeneficiario bo = new BoBeneficiario();

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

                if (model.IdCliente != null && bo.ExisteCPFCadastradoParfaOCliente(int.Parse(model.IdCliente), cpf))
                {
                    Response.StatusCode = 400;
                    return Json(string.Join(Environment.NewLine, "CPF ja cadastrado"));
                }
                else
                {
                    List<BeneficiarioModel> beneficarios = Session["beneficiarios"] as List<BeneficiarioModel>;

                    if (beneficarios == null)
                    {
                        beneficarios = new List<BeneficiarioModel>();
                    }

                    if (beneficarios.Where(x => x.Cpf == cpf).Any())
                    {
                        Response.StatusCode = 400;
                        return Json(string.Join(Environment.NewLine, "CPF ja esta na lista"));
                    }
                    else
                    {
                        beneficarios.Add(new BeneficiarioModel
                        {
                            Id = model.Id,
                            Cpf = cpf,
                            Nome = model.Nome,
                            IdCliente = model.IdCliente
                        });
                        Session["beneficiarios"] = beneficarios;
                    }

                    return Json("Adicionado");
                }
            }
        }

        [HttpPost]
        public JsonResult EditarBeneficiario(BeneficiarioModel beneficiario)
        {
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
                List<BeneficiarioModel> beneficarios = Session["beneficiarios"] as List<BeneficiarioModel>;

                foreach (var beneficiarioItem in beneficarios)
                {
                    if (beneficiarioItem.Id == beneficiario.Id || beneficiarioItem.Cpf == beneficiario.Cpf)
                    {
                        beneficiarioItem.Cpf = beneficiario.Cpf;
                        beneficiarioItem.Nome = beneficiario.Nome;
                    }
                }

                return Json("Editado");
            }
        }

        [HttpPost]
        public JsonResult ExcluirBeneficiario(BeneficiarioModel beneficiario)
        {
            List<BeneficiarioModel> beneficarios = Session["beneficiarios"] as List<BeneficiarioModel>;


            List<BeneficiarioModel> beneficariosExcluir = Session["beneficiariosExcluir"] as List<BeneficiarioModel>;
            if (beneficariosExcluir == null)
            {
                Session["beneficiariosExcluir"] = new List<BeneficiarioModel>();
                beneficariosExcluir = new List<BeneficiarioModel>();
            }

            beneficariosExcluir.Add(beneficiario);

            BeneficiarioModel beneficario = beneficarios.Where(x => x.Cpf == beneficiario.Cpf).FirstOrDefault();
            beneficarios.Remove(beneficario);

            Session["beneficiarios"] = beneficarios;
            Session["beneficiariosExcluir"] = beneficariosExcluir;

            return Json("Excluído");
        }
    }
}