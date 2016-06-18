﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidades;
using CapaAccesoDatos;

namespace CapaNegocio
{
    public class negAsigncionLlamadas
    {
        #region Singleton
        private static readonly negAsigncionLlamadas _Instancia = new negAsigncionLlamadas();
        public static negAsigncionLlamadas Instancia
        {
            get
            {
                return negAsigncionLlamadas._Instancia;
            }
        }
        #endregion Singleton

        #region metodos

        public List<entAsigncionLlamadas> ListaClientesAsignadosXUsuario(Int32 UsuarioId) {
            try
            {
                return datAsigncionLlamadas.Instancia.ListaClientesAsignadosXUsuario(UsuarioId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion metodos
    }
}
