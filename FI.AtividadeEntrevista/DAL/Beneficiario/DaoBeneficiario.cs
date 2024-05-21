using FI.AtividadeEntrevista.DML;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace FI.AtividadeEntrevista.DAL
{
    /// <summary>
    /// Classe de acesso a dados de Beneficiario
    /// </summary>
    internal class DaoBeneficiario : AcessoDados
    {
        /// <summary>
        /// Inclui um novo cliente
        /// </summary>
        /// <param name="beneficiarios">Objeto de cliente</param>
        internal void Incluir(List<DML.Beneficiario> beneficiarios)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();
            foreach(var beneficiario in beneficiarios)
            {
                parametros.Add(new System.Data.SqlClient.SqlParameter("Nome", beneficiario.Nome));
                parametros.Add(new System.Data.SqlClient.SqlParameter("Cpf", beneficiario.Cpf));
                parametros.Add(new System.Data.SqlClient.SqlParameter("IdCliente", beneficiario.IdCliente));

                DataSet ds = base.Consultar("FI_SP_IncBeneficiario", parametros);                
            }           
        }

        /// <summary>
        /// Inclui um novo cliente
        /// </summary>
        /// <param name="cliente">Objeto de cliente</param>
        internal IEnumerable<DML.Beneficiario> Consultar(long idCliente)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("IdCliente",idCliente));

            DataSet ds = base.Consultar("FI_SP_ConsBeneficiariosCliente", parametros);
            List<DML.Beneficiario> ben = Converter(ds);

            return ben;
        }

        internal bool VerificarExistencia(string CPF, long IDCLIENTE)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("CPF", CPF));
            parametros.Add(new System.Data.SqlClient.SqlParameter("IDCLIENTE", IDCLIENTE));

            DataSet ds = base.Consultar("FI_SP_VerificaBeneficiariosCliente", parametros);

            return ds.Tables[0].Rows.Count > 0;
        }

        internal List<Beneficiario> Pesquisa(int iniciarEm, int quantidade, string campoOrdenacao, bool crescente, out int qtd)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("iniciarEm", iniciarEm));
            parametros.Add(new System.Data.SqlClient.SqlParameter("quantidade", quantidade));
            parametros.Add(new System.Data.SqlClient.SqlParameter("campoOrdenacao", campoOrdenacao));
            parametros.Add(new System.Data.SqlClient.SqlParameter("crescente", crescente));

            DataSet ds = base.Consultar("FI_SP_PesqCliente", parametros);
            List<DML.Beneficiario> cli = Converter(ds);

            int iQtd = 0;

            if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                int.TryParse(ds.Tables[1].Rows[0][0].ToString(), out iQtd);

            qtd = iQtd;

            return cli;
        }

        /// <summary>
        /// Lista todos os clientes
        /// </summary>
        internal List<DML.Beneficiario> Listar()
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("Id", 0));

            DataSet ds = base.Consultar("FI_SP_ConsBeneficiariosCliente", parametros);
            List<DML.Beneficiario> ben = Converter(ds);

            return ben;
        }

        /// <summary>
        /// Inclui um novo cliente
        /// </summary>
        /// <param name="beneficiario">Objeto de cliente</param>
        internal void Alterar(DML.Beneficiario beneficiario)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("Nome", beneficiario.Nome));           
            parametros.Add(new System.Data.SqlClient.SqlParameter("Cpf", beneficiario.Cpf));
            parametros.Add(new System.Data.SqlClient.SqlParameter("ID", beneficiario.Id));
            parametros.Add(new System.Data.SqlClient.SqlParameter("IdCliente", beneficiario.IdCliente));

            base.Executar("FI_SP_AltBenef", parametros);
        }


        /// <summary>
        /// Excluir Cliente
        /// </summary>
        /// <param name="cliente">Objeto de cliente</param>
        internal void Excluir(long id, long idCliente)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("Id", id));
            parametros.Add(new System.Data.SqlClient.SqlParameter("IdCliente", idCliente));

            base.Executar("FI_SP_DelBeneficiarioCliente", parametros);
        }

        private List<DML.Beneficiario> Converter(DataSet ds)
        {
            List<DML.Beneficiario> lista = new List<DML.Beneficiario>();
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    DML.Beneficiario ben = new DML.Beneficiario();
                    ben.Id = row.Field<long>("Id");                 
                    ben.Nome = row.Field<string>("Nome");
                    ben.Cpf = row.Field<string>("Cpf");
                    ben.IdCliente = row.Field<long>("IdCliente");
                    lista.Add(ben);
                }
            }

            return lista;
        }
    }
}
