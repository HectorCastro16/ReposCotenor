using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class entDetallePedido
    {
       public String DetPed_Id { get; set; }
       public entPedido Pedido{ get; set; }
       public entProducto Producto { get; set; }
       public Double DetPed_Pre_Pro { get; set; }
       public int DetPed_Cantidad { get; set; }

    }
}
