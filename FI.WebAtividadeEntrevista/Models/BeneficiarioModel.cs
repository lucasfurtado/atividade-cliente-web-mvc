using System.ComponentModel.DataAnnotations;
using WebAtividadeEntrevista.CustomDataAnnotation;

namespace WebAtividadeEntrevista.Models
{
    /// <summary>
    /// Classe de Modelo do Beneficiario
    /// </summary>
    public class BeneficiarioModel
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// CPF
        /// </summary>
        [Required]
        [ValidarCpf]
        public string Cpf { get; set; }

        /// <summary>
        /// Nome
        /// </summary>
        [Required]
        public string Nome { get; set; }

        public string IdCliente { get; set; }
    }
}