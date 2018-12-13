using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USQL.com.compi2.usac.patronInterprete
{
    class Constante : ASTNode
    {
        private Object valor;

        public Constante(Object valor)
            : base()
        {
            this.valor = valor;
        }

        public Object execute()
        {
            return valor;
        }

    }
}
