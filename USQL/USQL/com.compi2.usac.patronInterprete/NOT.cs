using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using USQL.com.compi2.usac.tablaSimbolos;

namespace USQL.com.compi2.usac.patronInterprete
{
    class NOT : ASTNode
    {
        private ASTNode operand1;

        public NOT(ASTNode operand1) : base()
        {
            this.operand1 = operand1;
        }

        public Object execute(Entorno actual)
        {
            return !Convert.ToBoolean(operand1.execute(actual));
        }
    }
}
