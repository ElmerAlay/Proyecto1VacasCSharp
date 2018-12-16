using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using USQL.com.compi2.usac.tablaSimbolos;

namespace USQL.com.compi2.usac.patronInterprete
{
    class VarDec : ASTNode
    {
        private String name;
        private Simbolo sim;

        public VarDec(String name, Simbolo sim) : base()
        {
            this.name = name;
            this.sim = sim;
        }

        public Object execute(Entorno actual)
        {
            actual.put(name, sim);
            return null;
        }
    }
}
