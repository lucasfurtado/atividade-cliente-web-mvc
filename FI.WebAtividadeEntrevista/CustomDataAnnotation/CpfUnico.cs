using FI.AtividadeEntrevista.BLL;
using System.ComponentModel.DataAnnotations;

namespace WebAtividadeEntrevista.CustomDataAnnotation
{
    public class CpfUnico : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if(value is null)
                new ValidationResult("CPF inválido");

            BoCliente boCliente = new BoCliente();

            return boCliente.VerificarExistencia(value.ToString()) == true ? new ValidationResult("CPF já existente na base de dados") : ValidationResult.Success;
        }
    }
}