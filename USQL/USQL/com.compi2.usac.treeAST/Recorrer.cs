using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using USQL.com.compi2.usac.patronInterprete;
using System.Collections;
using USQL.com.compi2.usac.tablaSimbolos;

namespace USQL.com.compi2.usac.treeAST
{
    class Recorrer
    {
        public static Entorno global = new Entorno(null);

        public void resultado(Node root){
            //root.setValor(tree(root).execute(global).ToString());
            //MessageBox.Show("El resultado es: " + root.getValor());
            //MessageBox.Show("El resultado es: " + root.getValor());
            variablesSSL(root);
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
                    else if (((Node)root.getHijos()[0]).getEtiqueta().Contains(" (variable)"))
                    {
                        root.setValor(((Node)root.getHijos()[0]).getEtiqueta().Replace(" (variable)", ""));
                        
                        return new VarRef(root.getValor().ToString());
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
                    else if (((Node)root.getHijos()[0]).getEtiqueta().Contains(" (fecha)"))
                    {
                        String fecha = ((Node)root.getHijos()[0]).getEtiqueta().Replace(" (fecha)", "");
                        String[] values = fecha.Split('-');
                        DateTime date = new DateTime(Convert.ToInt32(values[2]), Convert.ToInt32(values[1]), Convert.ToInt32(values[0]));
                        root.setValor(date.ToShortDateString());
                        return new Constante(root.getValor());
                    }
                    else if (((Node)root.getHijos()[0]).getEtiqueta().Contains(" (fechahora)"))
                    {
                        String fecha = ((Node)root.getHijos()[0]).getEtiqueta().Replace(" (datetime)", "");
                        String[] values = fecha.Split('-');
                        DateTime aux = DateTime.Parse(fecha);
                        
                        root.setValor(aux.ToShortDateString());
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

        public static String variablesSSL(Node root)
        {
            switch (root.getHijos().Count)
            {
                case 1:
                    if (root.getEtiqueta().Equals("LVAR"))
                    {
                        root.setValor(((Node)root.getHijos()[0]).getEtiqueta());
                        return root.getValor().ToString();
                    }
                    else if (root.getEtiqueta().Equals("TIPO"))
                    {
                        root.setValor(((Node)root.getHijos()[0]).getEtiqueta());
                        return root.getValor().ToString();
                    }
                    else if (root.getEtiqueta().Equals("S"))
                    {
                        return variablesSSL((Node)root.getHijos()[0]);
                    }
                    return "";
                case 2:
                    if (root.getEtiqueta().Equals("LVAR"))
                    {
                        root.setValor(variablesSSL(((Node)root.getHijos()[0])) + "," + ((Node)root.getHijos()[1]).getEtiqueta());
                        return root.getValor().ToString();
                    }
                    else if (root.getEtiqueta().Equals("DEC"))
                    {
                            String[] variables = variablesSSL(((Node)root.getHijos()[0])).Split(',');
                            String tipo =  variablesSSL(((Node)root.getHijos()[1]));

                            for (int i = 0; i < variables.Length; i++)
                            {
                                Simbolo sim = new Simbolo(variables[i], new object(), tipo);
                                VarDec vd = new VarDec(variables[i], sim);
                                vd.execute(global);
                            }
                        
                        return "";
                    }
                    return "";
                case 3:
                    if (root.getEtiqueta().Equals("DEC"))
                    {
                        String[] variables = variablesSSL(((Node)root.getHijos()[0])).Split(',');
                        String tipo = variablesSSL(((Node)root.getHijos()[1]));

                        for (int i = 0; i < variables.Length; i++)
                        {
                            Simbolo sim = new Simbolo(variables[i], new object(), tipo);
                            VarDec vd = new VarDec(variables[i], sim);
                            vd.execute(global);

                            VarAsign va = new VarAsign(variables[i], tree((Node)root.getHijos()[2]));
                            va.execute(global);
                        }

                        return "";
                    }
                    else
                    {
                        MessageBox.Show("Error!");
                    }
                    return "";
            }

            return "";
        }
        
    }
}
