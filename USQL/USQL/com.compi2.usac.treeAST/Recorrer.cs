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
            root.setValor(tree(root).execute().ToString());
            MessageBox.Show("El resultado es: " + root.getValor());
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
                    else if (((Node)root.getHijos()[0]).getEtiqueta().Contains(" (flotante)"))
                    {
                        root.setValor(Convert.ToDouble(((Node)root.getHijos()[0]).getEtiqueta().Replace(" (flotante)", "")));
                        return new Constante(root.getValor());
                    }
                    else if (((Node)root.getHijos()[0]).getEtiqueta().Contains(" (Keyword)"))
                    {
                        String valor = ((Node)root.getHijos()[0]).getEtiqueta().Replace(" (Keyword)", "");
                        switch (valor){
                            case "true":
                                root.setValor(true);
                                return new Constante(root.getValor());
                            case "false":
                                root.setValor(false);
                                return new Constante(root.getValor());
                            default:
                                return null;
                        }
                    }
                    else if (((Node)root.getHijos()[0]).getEtiqueta().Contains(" (date)"))
                    {
                        String fecha = ((Node)root.getHijos()[0]).getEtiqueta().Replace(" (date)", "");
                        String[] values = fecha.Split('-');
                        DateTime date = new DateTime(Convert.ToInt32(values[2]), Convert.ToInt32(values[1]), Convert.ToInt32(values[0]));
                        root.setValor(date.ToShortDateString());
                        return new Constante(root.getValor());
                    }
                    else if (((Node)root.getHijos()[0]).getEtiqueta().Contains(" (datetime)"))
                    {
                        String fecha = ((Node)root.getHijos()[0]).getEtiqueta().Replace(" (datetime)", "");
                        String[] values = fecha.Split('-');
                        DateTime date = new DateTime(Convert.ToInt32(values[2]), Convert.ToInt32(values[1]), Convert.ToInt32(values[0]));
                        root.setValor(date.ToShortDateString());
                        return new Constante(root.getValor());
                    }
                    else
                    {
                        return tree((Node)root.getHijos()[0]);
                    }
                case 2:
                    if (root.getEtiqueta().Equals("E"))
                    {
                        switch(((Node)root.getHijos()[0]).getEtiqueta()){
                            case "-": //-E
                                return new Mult(tree((Node)root.getHijos()[1]), new Constante(-1));
                            case "!": //!E
                                return new NOT(tree((Node)root.getHijos()[1]));
                        }
                            
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
                        case ">": //E-E
                            return new MayorQue(tree((Node)root.getHijos()[0]), tree((Node)root.getHijos()[2]));
                        case "<": //E-E
                            return new MenorQue(tree((Node)root.getHijos()[0]), tree((Node)root.getHijos()[2]));
                        case ">=": //E-E
                            return new MayorIgual(tree((Node)root.getHijos()[0]), tree((Node)root.getHijos()[2]));
                        case "<=": //E-E
                            return new MenorIgual(tree((Node)root.getHijos()[0]), tree((Node)root.getHijos()[2]));
                        case "==": //E-E
                            return new Igualdad(tree((Node)root.getHijos()[0]), tree((Node)root.getHijos()[2]));
                        case "!=": //E-E
                            return new NotEquals(tree((Node)root.getHijos()[0]), tree((Node)root.getHijos()[2]));
                        case "&&": //E-E
                            return new AND(tree((Node)root.getHijos()[0]), tree((Node)root.getHijos()[2]));
                        case "||": //E-E
                            return new OR(tree((Node)root.getHijos()[0]), tree((Node)root.getHijos()[2]));
                        default: //(E)
                            return tree((Node)root.getHijos()[0]);
                    }
                default:
                    return null;
            }
        }
    }
}
