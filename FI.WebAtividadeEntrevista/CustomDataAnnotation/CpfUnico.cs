using FI.AtividadeEntrevista.BLL;
using System.ComponentModel.DataAnnotations;
using WebAtividadeEntrevista.Models;

namespace WebAtividadeEntrevista.CustomDataAnnotation
{
    public class CpfUnico : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is null) new ValidationResult("CPF não pode ser nulo");

            BoCliente boCliente = new BoCliente();

            var cliente = validationContext.ObjectInstance as ClienteModel;

            if(cliente.Id > 0)
            {
                if (boCliente.VerificarValidadeCPF(cliente.Cpf))
                {
                    if (boCliente.VerificaSeCpfEDoUsuario(cliente.Id, cliente.Cpf)) return ValidationResult.Success;
                    else
                    {
                        if (boCliente.VerificarExistencia(cliente.Cpf)) return new ValidationResult("CPF já existente na base de dados");
                        else return ValidationResult.Success;
                    }
                }
                else return new ValidationResult("CPF inválido");
            }
            else
            {
                return boCliente.VerificarExistencia(value.ToString()) == true ? new ValidationResult("CPF já existente na base de dados") : ValidationResult.Success;
            }
        }
    }
}