using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using USQL.com.compi2.usac.tablaSimbolos;

namespace USQL.com.compi2.usac.patronInterprete
{
    class VarRef : ASTNode
    {
        private String name;

        public VarRef(String name)
            : base()
        {
            this.name = name;
        }

        public Object execute(Entorno actual)
        {
            Simbolo s = actual.get(name);

            return s.getValor();
        }
    }
}
