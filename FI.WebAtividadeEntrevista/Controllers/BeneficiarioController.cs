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
                if(bo.ExisteCPFCadastradoParfaOCliente(model.IdCliente, cpf))
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

                    if(beneficarios.Where(x => x.Cpf == cpf).Any())
                    {
                        Response.StatusCode = 400;
                        return Json(string.Join(Environment.NewLine, "CPF ja esta na lista"));
                    }
                    else
                    {
                        beneficarios.Add(new BeneficiarioModel
                        {
                            Cpf = cpf,
                            Nome = model.Nome,
                            IdCliente = model.IdCliente
                        });
                        Session["beneficiarios"] = beneficarios;
                    }
                
                //bo.Incluir(new Beneficiario
                //{
                //    Cpf = Regex.Replace(model.Cpf, "[^0-9]", ""),
                //    Nome = model.Nome,
                //    IdCliente = model.IdCliente
                //});

                    return Json("Adicionado");
                }
            }

        }
    }
}