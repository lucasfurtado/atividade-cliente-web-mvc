using FI.AtividadeEntrevista.DAL.Clientes;

namespace FI.AtividadeEntrevista.BLL
{
    public class BoBeneficiario
    {
        public long Incluir(DML.Beneficiario beneficiario)
        {
            DaoBeneficiario cli = new DaoBeneficiario();
            return cli.Incluir(beneficiario);
        }
    }
}
