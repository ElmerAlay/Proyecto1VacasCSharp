using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USQL.com.compi2.usac.patronInterprete
{
    class MenorIgual : ASTNode
    {
        private ASTNode operand1;
        private ASTNode operand2;

        public MenorIgual(ASTNode operand1, ASTNode operand2) : base()
        {
            this.operand1 = operand1;
            this.operand2 = operand2;
        }

        public Object execute()
        {
            return Convert.ToDouble(operand1.execute()) <= Convert.ToDouble(operand2.execute());
        }
    }
}
