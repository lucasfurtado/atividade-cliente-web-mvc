namespace FI.AtividadeEntrevista.DML
{
    /// <summary>
    /// Classe de beneficiario que representa o registo na tabela Beneficiarios do Banco de Dados
    /// </summary>
    public class Beneficiario
    {
        public long Id { get; set; }

        /// <summary>
        /// Nome
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// CPF
        /// </summary>
        public string Cpf { get; set; }

        /// <summary>
        /// IDCLIENTE
        /// </summary>
        public long IdCliente { get; set; }
    }
}