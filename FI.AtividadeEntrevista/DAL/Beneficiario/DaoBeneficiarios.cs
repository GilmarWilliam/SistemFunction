using FI.AtividadeEntrevista.DML;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace FI.AtividadeEntrevista.DAL
{
    /// <summary>
    /// Classe de acesso a dados dos beneficiarios
    /// </summary>
    internal class DaoBeneficiarios : AcessoDados
    {
        /// <summary>
        /// Inclui um novo beneficiario
        /// </summary>
        /// <param name="beneficiarios">Objeto de beneficiarios</param>
        internal long Incluir(DML.Beneficiarios beneficiarios)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("CPF", beneficiarios.CPF));
            parametros.Add(new System.Data.SqlClient.SqlParameter("Nome", beneficiarios.Nome));
            parametros.Add(new System.Data.SqlClient.SqlParameter("IdCliente", beneficiarios.IdCliente));

            DataSet ds = base.Consultar("FI_SP_IncBeneficiarios", parametros);
            long ret = 0;
            if (ds.Tables[0].Rows.Count > 0)
                long.TryParse(ds.Tables[0].Rows[0][0].ToString(), out ret);
            return ret;
        }

        /// <summary>
        /// consulta um beneficiario
        /// </summary>
        /// <param name="beneficiarios">Objeto de beneficiarios</param>
        internal DML.Beneficiarios Consultar(long Id)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("Id", Id));

            DataSet ds = base.Consultar("FI_SP_ConsBeneficiarios", parametros);
            List<DML.Beneficiarios> cli = Converter(ds);

            return cli.FirstOrDefault();
        }

        internal bool VerificarExistencia(string CPF)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("CPF", CPF));

            DataSet ds = base.Consultar("FI_SP_VerificaBeneficiarios", parametros);

            return ds.Tables[0].Rows.Count > 0;
        }

        /// <summary>
        /// Lista todos os Beneficiarios
        /// </summary>
        internal List<DML.Beneficiarios> Listar()
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("Id", 0));

            DataSet ds = base.Consultar("FI_SP_ConsBeneficiarios", parametros);
            List<DML.Beneficiarios> cli = Converter(ds);

            return cli;
        }

        /// <summary>
        /// Lista todos os Beneficiarios
        /// </summary>
        internal List<DML.Beneficiarios> ListarTodosPorCliente(long clientId)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("IdCliente", clientId));

            DataSet ds = base.Consultar("FI_SP_ConsBeneficiariosPorCliente", parametros);
            List<DML.Beneficiarios> cli = Converter(ds);

            return cli;
        }

        /// <summary>
        /// Inclui um novo Beneficiario
        /// </summary>
        /// <param name="beneficiarios">Objeto de Beneficiario</param>
        internal void Alterar(DML.Beneficiarios beneficiarios)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("CPF", beneficiarios.CPF));
            parametros.Add(new System.Data.SqlClient.SqlParameter("Nome", beneficiarios.Nome));
            parametros.Add(new System.Data.SqlClient.SqlParameter("IdCliente", beneficiarios.IdCliente));
            parametros.Add(new System.Data.SqlClient.SqlParameter("Id", beneficiarios.Id));

            base.Executar("FI_SP_AltBeneficiarios", parametros);
        }

        /// <summary>
        /// Excluir Cliente
        /// </summary>
        /// <param name="cliente">Objeto de cliente</param>
        internal void Excluir(long Id)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("Id", Id));

            base.Executar("FI_SP_DelBeneficiario", parametros);
        }

        private List<DML.Beneficiarios> Converter(DataSet ds)
        {
            List<DML.Beneficiarios> lista = new List<DML.Beneficiarios>();
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    DML.Beneficiarios cli = new DML.Beneficiarios();
                    cli.Id = row.Field<long>("Id");
                    cli.CPF = row.Field<string>("CPF");
                    cli.Nome = row.Field<string>("Nome");
                    cli.IdCliente = row.Field<long>("IdCliente");
                    lista.Add(cli);
                }
            }

            return lista;
        }
    }
}
