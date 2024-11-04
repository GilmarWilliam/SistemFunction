using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FI.AtividadeEntrevista.BLL
{
    public class BoBeneficiarios
    {
        /// <summary>
        /// Inclui um novo Beneficiario
        /// </summary>
        /// <param name="beneficiario">Objeto de beneficiario</param>
        public long Incluir(DML.Beneficiarios beneficiario)
        {
            DAL.DaoBeneficiarios cli = new DAL.DaoBeneficiarios();
            return cli.Incluir(beneficiario);
        }

        /// <summary>
        /// Altera um beneficiario
        /// </summary>
        /// <param name="beneficiario">Objeto de beneficiario</param>
        public void Alterar(DML.Beneficiarios beneficiario)
        {
            DAL.DaoBeneficiarios cli = new DAL.DaoBeneficiarios();
            cli.Alterar(beneficiario);
        }

        /// <summary>
        /// Consulta o beneficiario pelo id
        /// </summary>
        /// <param name="id">id do beneficiario</param>
        /// <returns></returns>
        public DML.Beneficiarios Consultar(long id)
        {
            DAL.DaoBeneficiarios cli = new DAL.DaoBeneficiarios();
            return cli.Consultar(id);
        }

        /// <summary>
        /// Excluir o cliente pelo id
        /// </summary>
        /// <param name="id">id do cliente</param>
        /// <returns></returns>
        public void Excluir(long id)
        {
            DAL.DaoBeneficiarios cli = new DAL.DaoBeneficiarios();
            cli.Excluir(id);
        }

        /// <summary>
        /// Lista os clientes
        /// </summary>
        public List<DML.Beneficiarios> Listar()
        {
            DAL.DaoBeneficiarios cli = new DAL.DaoBeneficiarios();
            return cli.Listar();
        }

        /// <summary>
        /// Lista os clientes
        /// </summary>
        public List<DML.Beneficiarios> ListarTodosPorCliente(long clientId)
        {
            DAL.DaoBeneficiarios cli = new DAL.DaoBeneficiarios();
            return cli.ListarTodosPorCliente(clientId);
        }

        /// <summary>
        /// Lista os beneficiarios
        /// </summary>
        //public List<DML.Beneficiarios> Pesquisa(int iniciarEm, int quantidade, string campoOrdenacao, bool crescente, out int qtd)
        //{
        //    DAL.DaoBeneficiarios cli = new DAL.DaoBeneficiarios();
        //    return cli.Pesquisa(iniciarEm,  quantidade, campoOrdenacao, crescente, out qtd);
        //}

        /// <summary>
        /// VerificaExistencia
        /// </summary>
        /// <param name="CPF"></param>
        /// <returns></returns>
        public bool VerificarExistencia(string CPF)
        {
            DAL.DaoBeneficiarios cli = new DAL.DaoBeneficiarios();
            return cli.VerificarExistencia(CPF);
        }
    }
}
