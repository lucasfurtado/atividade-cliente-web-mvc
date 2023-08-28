using FI.AtividadeEntrevista.DAL.Clientes;
using System.Collections.Generic;

namespace FI.AtividadeEntrevista.BLL
{
    public class BoBeneficiario
    {
        public long Incluir(DML.Beneficiario beneficiario)
        {
            DaoBeneficiario cli = new DaoBeneficiario();
            return cli.Incluir(beneficiario);
        }

        public List<DML.Beneficiario> BuscaBeneficiariosDoCliente(long id)
        {
            DaoBeneficiario cli = new DaoBeneficiario();
            return cli.ListaBeneficiarioDoCliente(id);
        }

        public bool ExisteCPFCadastradoParfaOCliente(long idCliente, string cpf)
        {
            DaoBeneficiario cli = new DaoBeneficiario();
            return cli.ExisteRegistroComIdECPF(idCliente, cpf);
        }

        public void Editar(DML.Beneficiario beneficiario)
        {
            DaoBeneficiario cli = new DaoBeneficiario();
            cli.Editar(beneficiario);
        }

        public void Excluir(DML.Beneficiario beneficiario)
        {
            DaoBeneficiario cli = new DaoBeneficiario();
            cli.Excluir(beneficiario);
        }
    }
}
