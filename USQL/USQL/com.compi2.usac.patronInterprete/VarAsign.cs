using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using USQL.com.compi2.usac.tablaSimbolos;

namespace USQL.com.compi2.usac.patronInterprete
{
    class VarAsign : ASTNode
    {
        private String name;
        private ASTNode valor;

        public VarAsign(String name, ASTNode valor) :base()
        {
            this.name = name;
            this.valor = valor;
        }

        public Object execute(Entorno actual)
        {
            Simbolo s = actual.get(name);
            s.setValor(valor.execute(actual));

            actual.getHashTable()[name] = s;
            return null;
        }
    }
}
