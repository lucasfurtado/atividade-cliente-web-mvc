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
    }
}
