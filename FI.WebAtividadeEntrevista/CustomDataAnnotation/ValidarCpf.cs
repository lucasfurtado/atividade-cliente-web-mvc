using FI.AtividadeEntrevista.BLL;
using System;
using System.ComponentModel.DataAnnotations;

namespace WebAtividadeEntrevista.CustomDataAnnotation
{
    public class ValidarCpf : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            try
            {
                BoCliente boCliente = new BoCliente();

                if (value == null)
                {
                    return new ValidationResult("Valor do CPF é inválido");
                }

                string cpf = value.ToString();

                if (boCliente.VerificarValidadeCPF(cpf))
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult("CPF inválido");
                }
            }
            catch (Exception ex)
            {
                return new ValidationResult("Erro ao validar CPF");
            }
        }

        private bool ValorCPFInvalido(string cpf)
        {
            switch (cpf)
            {
                case "11111111111":
                    return true;
                case "00000000000":
                    return true;
                case "2222222222":
                    return true;
                case "33333333333":
                    return true;
                case "44444444444":
                    return true;
                case "55555555555":
                    return true;
                case "66666666666":
                    return true;
                case "77777777777":
                    return true;
                case "88888888888":
                    return true;
                case "99999999999":
                    return true;
            }
            return false;
        }
    }
}