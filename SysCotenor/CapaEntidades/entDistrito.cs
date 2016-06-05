using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
  public  class entDistrito
    {
        public int idDist { get; set; }
        public String distrito { get; set; }
        public entProvincia provincia { get; set; }
    }
}
