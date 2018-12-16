using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using USQL.com.compi2.usac.tablaSimbolos;

namespace USQL.com.compi2.usac.patronInterprete
{
    class OR : ASTNode
    {
        private ASTNode operand1;
        private ASTNode operand2;

        public OR(ASTNode operand1, ASTNode operand2) : base()
        {
            this.operand1 = operand1;
            this.operand2 = operand2;
        }

        public Object execute(Entorno actual)
        {
            return Convert.ToBoolean(operand1.execute(actual)) || Convert.ToBoolean(operand2.execute(actual));
        }
    }
}
