using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace CapaEntidades
{
    public class entTipDoc
    {
        public int td_id { get; set; }
        public String td_nombre { get; set; }

        public String td_Descripcion { get; set; }
    }
}
