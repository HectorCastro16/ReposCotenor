using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace CapaAccesoDatos
{
    public class Conexion
    {
        #region Singleton
        private static readonly Conexion _Instancia = new Conexion();
        public static Conexion Instancia
        {
            get
            {
                return Conexion._Instancia;
            }
        }
        #endregion Singleton

        #region metodos
        public SqlConnection Conectar()
        {
            SqlConnection cn = new SqlConnection();
            //cn.ConnectionString = "Data Source=.; Initial Catalog=DB_A052FB_BDCotenor-23;User ID=sa; Password=123456";
            //cn.ConnectionString = "Data Source=BDCotenor.mssql.somee.com; Initial Catalog=BDCotenor;User ID=Cotenor_SQLLogin_1; Password=shodwaudo5";
            cn.ConnectionString = "Data Source=SQL5027.Smarterasp.net;Initial Catalog=DB_A052FB_BDCotenor;User Id=DB_A052FB_BDCotenor_admin;Password=bdcotenor123";
            return cn;

        }
        #endregion metodos

    }
}
