using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using CapaEntidades;

namespace CapaAccesoDatos
{
    public class datTipDoc
    {
        #region Singleton

        private static readonly datTipDoc _Instancia = new datTipDoc();
        public static datTipDoc Instancia
        {
            get
            {
                return datTipDoc._Instancia;
            }
        }

        #endregion Singleton

        #region metodos



        #endregion metodos
    }
}
