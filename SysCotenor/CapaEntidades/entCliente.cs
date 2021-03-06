﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class entCliente
    {
        public int Cli_Id { get; set; }
        public entTipDoc TipDoc { get; set; }
        public String Cli_Codigo { get; set; }
        public String Cli_Nombre { get; set; }
        public String Cli_RazonSocial { get; set; }
        public entSegmento Segmento { get; set; }
        public String Cli_Numero_Documento { get; set; } 
        public DateTime Cli_FechaNacimiento { get; set; }
        public String Cli_LugarNacimiento { get; set; }
        public String Cli_Correo { get; set; }
        public String Cli_Telefono_Referencia { get; set; }
        public String Cli_Estado { get; set; }
        public DateTime Cli_FechaRegistro { get; set; }
        public String Cli_UsuarioRegistro { get; set; }
        public DateTime Cli_FechaModificacion { get; set; }
        public String Cli_UsuarioModificacion { get; set; }

        public List<entProducto> SVAS { get; set; }
        //es una lista porque son muchos SVAS

    }
}
