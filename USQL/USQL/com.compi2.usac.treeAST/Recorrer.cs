using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using USQL.com.compi2.usac.patronInterprete;
using System.Collections;

namespace USQL.com.compi2.usac.treeAST
{
    class Recorrer
    {
        public static void resultado(Node root){
            MessageBox.Show("El resultado es: " + tree(root).execute().ToString());
        }

        public static ASTNode tree(Node root)
        {
            switch (root.getHijos().Count)
            {
                case 1: //Nodos hojas
                    if(((Node)root.getHijos()[0]).getEtiqueta().Contains(" (numero)")){
                        root.setValor(Convert.ToInt32(((Node)root.getHijos()[0]).getEtiqueta().Replace(" (numero)", "")));
                        return new Constante(root.getValor());
                    }
                    else if (root.getEtiqueta().Equals("SENT") || root.getEtiqueta().Equals("SENTS"))
                    {
                        return tree((Node)root.getHijos()[0]);
                    }
                    else if (root.getEtiqueta().Equals("COND"))
                    {
                        root.setValor(Convert.ToBoolean(((Node)root.getHijos()[0]).getEtiqueta().Replace(" (Keyword)", "")));
                        return new Constante(root.getValor());
                    }
                    else
                    {
                        return tree((Node)root.getHijos()[0]);
                    }
                case 2:
                    if (root.getEtiqueta().Equals("IF"))
                    {
                        return new If(tree((Node)root.getHijos()[0]), (ArrayList)tree((Node)root.getHijos()[1]),null);
                    }
                    return null;
                case 3: //Nodo binario
                    switch (((Node)root.getHijos()[1]).getEtiqueta())
                    {
                        case "+": //E+E
                            return new Addition(tree((Node)root.getHijos()[0]), tree((Node)root.getHijos()[2]));
                        case "-": //E-E
                            return new Subtraction(tree((Node)root.getHijos()[0]), tree((Node)root.getHijos()[2]));
                        case "*": //E*E
                            return new Mult(tree((Node)root.getHijos()[0]), tree((Node)root.getHijos()[2]));
                        case "/": //E-E
                            return new div(tree((Node)root.getHijos()[0]), tree((Node)root.getHijos()[2]));
                        default: //(E)
                            return tree((Node)root.getHijos()[0]);
                    }
                default:
                    return null;
            }
        }
    }
}
