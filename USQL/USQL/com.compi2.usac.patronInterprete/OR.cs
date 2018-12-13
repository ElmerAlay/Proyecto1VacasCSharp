using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public Object execute()
        {
            return Convert.ToBoolean(operand1.execute()) || Convert.ToBoolean(operand2.execute());
        }
    }
}
