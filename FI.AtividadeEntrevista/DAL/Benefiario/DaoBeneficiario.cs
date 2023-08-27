using System.Collections.Generic;
using System.Data;

namespace FI.AtividadeEntrevista.DAL.Clientes
{
    internal class DaoBeneficiario : AcessoDados
    {
        internal long Incluir(DML.Beneficiario benefiario)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("Cpf", benefiario.Cpf));
            parametros.Add(new System.Data.SqlClient.SqlParameter("Nome", benefiario.Nome));
            parametros.Add(new System.Data.SqlClient.SqlParameter("IdCliente", benefiario.IdCliente));

            DataSet ds = base.Consultar("FI_SP_IncBeneficiario", parametros);

            long ret = 0;
            if (ds.Tables[0].Rows.Count > 0)
                long.TryParse(ds.Tables[0].Rows[0][0].ToString(), out ret);
            return ret;
        }

        internal List<DML.Beneficiario> ListaBeneficiarioDoCliente(long id)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("clienteId", id));

            DataSet ds = base.Consultar("FI_SP_PesqListaBeneficiarios", parametros);

            List<DML.Beneficiario> beneficiarios = Converter(ds);

            return beneficiarios;
        }

        private List<DML.Beneficiario> Converter(DataSet ds)
        {
            List<DML.Beneficiario> listaBeneficiarios = new List<DML.Beneficiario>();
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    DML.Beneficiario beneficiario = new DML.Beneficiario();
                    beneficiario.Id = row.Field<long>("Id");
                    beneficiario.Nome = row.Field<string>("Nome");
                    beneficiario.Cpf = row.Field<string>("Cpf");
                    beneficiario.IdCliente = row.Field<long>("IdCliente");
                    listaBeneficiarios.Add(beneficiario);
                }
            }
            return listaBeneficiarios;
        }

        internal bool ExisteRegistroComIdECPF(long clientId, string cpf)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("Id", clientId));
            parametros.Add(new System.Data.SqlClient.SqlParameter("Cpf", cpf));

            DataSet ds = base.Consultar("FI_SP_VerificaCPFParaCliente", parametros);

            long ret = 0;
            if (ds.Tables[0].Rows.Count > 0)
                return true;
            else
                return false;
        }
    }
}
