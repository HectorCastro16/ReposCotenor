﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class entPedido
    {
        public int Ped_Id { get; set; }
        public String Ped_Codigo { get; set; }
        public entCliente Cliente { get; set; }
        public entUsuario Usuario { get; set; }
        public entEstado Estado { get; set; }
        public entAccionComercial AccionComercial { get; set; }
        public String Ped_Dir_Inst { get; set; }
        public Double Ped_Total { get; set; }
        public DateTime Ped_FechaRegistro { get; set; }
        public String Ped_UsuarioRegistro { get; set; }
        public DateTime Ped_FechaModificacion { get; set; }
        public String Ped_UsuarioModificacion { get; set; }

    }
}
