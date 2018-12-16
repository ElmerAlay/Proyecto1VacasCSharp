using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using USQL.com.compi2.usac.tablaSimbolos;

namespace USQL.com.compi2.usac.patronInterprete
{
    class If : ASTNode
    {
        private ASTNode condition;
        private ArrayList body;
        private ArrayList elsebody;

        public If(ASTNode condition, ArrayList body, ArrayList elsebody) : base() 
        {
            this.condition = condition;
            this.body = body;
            this.elsebody = elsebody;
        }

        public Object execute(Entorno actual)
        {
            if (Convert.ToBoolean(condition.execute(actual)))
            {
                foreach (ASTNode n in body)
                {
                    n.execute(actual);
                }
            }
            else
            {
                foreach (ASTNode n in elsebody)
                {
                    n.execute(actual);
                }
            }
            return null;
        }
    }
}
