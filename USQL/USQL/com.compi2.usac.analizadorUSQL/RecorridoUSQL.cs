using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Irony.Parsing;
using System.Windows.Forms;
using USQL.com.compi2.usac.treeAST;


namespace USQL.com.compi2.usac.analizador
{
    class RecorridoUSQL
    {
        public static String cadena="";

        public static void resultado(ParseTreeNode root)
        {
            //MessageBox.Show("Respuesta " + tree(root.ChildNodes.ElementAt(0)));
        }

        public static void arbolAST(ParseTreeNode root)
        {
            graficarAst.generarImagen(tree(root));
            Recorrer.variablesSSL(tree(root));

            MessageBox.Show(Recorrer.global.get("@var2").getValor().ToString());
        }

        public static Node tree(ParseTreeNode root)
        {
            switch (root.ChildNodes.Count)
            {
                case 1: //Nodos hojas
                    if (String.Compare(root.ToString(), "S") == 0)
                    {
                        Node raiz = new Node("S");
                        raiz.addHijos(tree(root.ChildNodes.ElementAt(0)));
                        return raiz;
                    }
                    else if (String.Compare(root.ToString(), "SENTS") == 0)
                    {
                        Node aux = new Node("SENTS");

                        aux.addHijos(tree(root.ChildNodes.ElementAt(0)));
                        return aux;
                    }
                    else if (String.Compare(root.ToString(), "SENT") == 0)
                    {
                        Node aux;
                        if (root.ChildNodes.ElementAt(0).ToString().Contains("detener"))
                        {
                            aux = new Node("SENT");
                            aux.addHijos(new Node(root.ChildNodes.ElementAt(0).ToString().Replace(" (Keyword)","")));
                        }
                        else if (root.ChildNodes.ElementAt(0).ToString().Contains("retornar"))
                        {
                            aux = new Node("SENT");
                            aux.addHijos(new Node(root.ChildNodes.ElementAt(0).ToString().Replace(" (Keyword)","")));
                        }
                        else
                        {
                            aux = new Node("SENT");
                            aux.addHijos(tree(root.ChildNodes.ElementAt(0)));
                        }

                        return aux;
                    }
                    else if (root.ToString().Equals("LVAR"))
                    {
                        Node var = new Node("LVAR");
                        var.addHijos(new Node(root.ChildNodes.ElementAt(0).ToString().Replace(" (variable)","")));

                        return var;
                    }
                    else if (root.ToString().Equals("TIPO"))
                    {
                        Node var = new Node("TIPO");
                        var.addHijos(new Node(root.ChildNodes.ElementAt(0).ToString().Replace(" (Keyword)", "")));

                        return var;
                    }
                    else
                    {
                        Node aux = new Node("E");
                        Node hoja = new Node(root.ChildNodes.ElementAt(0).ToString());

                        aux.addHijos(hoja);
                        return aux;
                    }
                case 2:
                    if (String.Compare(root.ToString(), "SENTS") == 0)
                    {
                        Node aux = new Node("SENTS");

                        aux.addHijos(tree(root.ChildNodes.ElementAt(0)));
                        aux.addHijos(tree(root.ChildNodes.ElementAt(1)));
                        return aux;
                    }
                    else if (String.Compare(root.ToString(), "E") == 0)
                    {
                        switch (root.ChildNodes.ElementAt(0).ToString().Replace(" (Key symbol)",""))
                        {
                            case "-": //-E
                                Node aux = new Node("E");

                                aux.addHijos(new Node("-"));
                                aux.addHijos(tree(root.ChildNodes.ElementAt(1)));
                                return aux;
                            case "!": //!E
                                Node aux1 = new Node("E");

                                aux1.addHijos(new Node("!"));
                                aux1.addHijos(tree(root.ChildNodes.ElementAt(1)));
                                return aux1;
                        }

                        
                    }
                    return null;
                case 3: //Nodo binario
                    if (root.ToString().Equals("E"))
                    {
                        String operador = root.ChildNodes.ElementAt(1).ToString().Replace(" (Key symbol)", "");
                        switch (operador)
                        {
                            case "+": //E + E
                                Node suma = new Node("E");
                                //suma.setIdNodo(contador++);
                                suma.addHijos(tree(root.ChildNodes.ElementAt(0)));
                                suma.addHijos(new Node("+"));
                                suma.addHijos(tree(root.ChildNodes.ElementAt(2)));

                                return suma;
                            case "-": //E - E
                                Node resta = new Node("E");
                                resta.addHijos(tree(root.ChildNodes.ElementAt(0)));
                                resta.addHijos(new Node("-"));
                                resta.addHijos(tree(root.ChildNodes.ElementAt(2)));

                                return resta;
                            case "*": //E * E
                                Node mult = new Node("E");
                                mult.addHijos(tree(root.ChildNodes.ElementAt(0)));
                                mult.addHijos(new Node("*"));
                                mult.addHijos(tree(root.ChildNodes.ElementAt(2)));

                                return mult;
                            case "/": //E / E
                                Node div = new Node("E");
                                div.addHijos(tree(root.ChildNodes.ElementAt(0)));
                                div.addHijos(new Node("/"));
                                div.addHijos(tree(root.ChildNodes.ElementAt(2)));

                                return div;
                            case ">": //E > E
                                Node gt = new Node("E");
                                gt.addHijos(tree(root.ChildNodes.ElementAt(0)));
                                gt.addHijos(new Node(">"));
                                gt.addHijos(tree(root.ChildNodes.ElementAt(2)));

                                return gt;
                            case "<": //E < E
                                Node lt = new Node("E");
                                lt.addHijos(tree(root.ChildNodes.ElementAt(0)));
                                lt.addHijos(new Node("<"));
                                lt.addHijos(tree(root.ChildNodes.ElementAt(2)));

                                return lt;
                            case ">=": //E >= E
                                Node gte = new Node("E");
                                gte.addHijos(tree(root.ChildNodes.ElementAt(0)));
                                gte.addHijos(new Node(">="));
                                gte.addHijos(tree(root.ChildNodes.ElementAt(2)));

                                return gte;
                            case "<=": //E <= E
                                Node lte = new Node("E");
                                lte.addHijos(tree(root.ChildNodes.ElementAt(0)));
                                lte.addHijos(new Node("<="));
                                lte.addHijos(tree(root.ChildNodes.ElementAt(2)));

                                return lte;
                            case "==": //E == E
                                Node eq = new Node("E");
                                eq.addHijos(tree(root.ChildNodes.ElementAt(0)));
                                eq.addHijos(new Node("=="));
                                eq.addHijos(tree(root.ChildNodes.ElementAt(2)));

                                return eq;
                            case "!=": //E != E
                                Node neq = new Node("E");
                                neq.addHijos(tree(root.ChildNodes.ElementAt(0)));
                                neq.addHijos(new Node("!="));
                                neq.addHijos(tree(root.ChildNodes.ElementAt(2)));

                                return neq;
                            case "&&": //E && E
                                Node and = new Node("E");
                                and.addHijos(tree(root.ChildNodes.ElementAt(0)));
                                and.addHijos(new Node("&&"));
                                and.addHijos(tree(root.ChildNodes.ElementAt(2)));

                                return and;
                            case "||": //E || E
                                Node or = new Node("E");
                                or.addHijos(tree(root.ChildNodes.ElementAt(0)));
                                or.addHijos(new Node("||"));
                                or.addHijos(tree(root.ChildNodes.ElementAt(2)));

                                return or;
                            default: //(E)
                                Node par = new Node("E");
                                par.addHijos(tree(root.ChildNodes.ElementAt(1)));

                                return par;
                        }
                    }
                    else if (root.ToString().Equals("LVAR"))
                    {
                        Node var = new Node("LVAR");
                        var.addHijos(tree(root.ChildNodes.ElementAt(0)));
                        var.addHijos(new Node(root.ChildNodes.ElementAt(2).ToString().Replace(" (variable)", "")));

                        return var;
                    }
                    return null; 
                case 4:
                    if (root.ToString().Equals("DEC"))
                    {
                        Node dec = new Node("DEC");
                        dec.addHijos(tree(root.ChildNodes.ElementAt(1)));
                        dec.addHijos(tree(root.ChildNodes.ElementAt(2)));
                        
                        return dec;
                    }
                    return null;
                case 6:
                    if (root.ToString().Equals("DEC"))
                    {
                        Node dec = new Node("DEC");
                        dec.addHijos(tree(root.ChildNodes.ElementAt(1)));
                        dec.addHijos(tree(root.ChildNodes.ElementAt(2)));
                        dec.addHijos(tree(root.ChildNodes.ElementAt(4)));
                        
                        return dec;
                    }
                    return null;
                case 7:
                    if (root.ToString().Equals("IF"))
                    {
                        Node aux = new Node("SI");
                        aux.addHijos(tree(root.ChildNodes.ElementAt(2)));
                        aux.addHijos(tree(root.ChildNodes.ElementAt(5)));

                        return aux;
                    }
                    else if (root.ToString().Equals("WHILE"))
                    {
                        Node aux = new Node("mientras");
                        aux.addHijos(tree(root.ChildNodes.ElementAt(2)));
                        aux.addHijos(tree(root.ChildNodes.ElementAt(5)));

                        return aux;
                    }
                    return null;
                case 11:
                    if (root.ToString().Equals("IF"))
                    {
                        Node aux = new Node("SI");
                        aux.addHijos(tree(root.ChildNodes.ElementAt(2)));
                        aux.addHijos(tree(root.ChildNodes.ElementAt(5)));
                        aux.addHijos(tree(root.ChildNodes.ElementAt(9)));

                        return aux;
                    }
                    return null;
            }

            return null;
        }

        private static double expresion(ParseTreeNode root)
        {
            switch (root.ChildNodes.Count)
            {
                case 1: //Nodos hojas
                    String[] numero = root.ChildNodes.ElementAt(0).ToString().Split(' ');
                    if (numero[1].Equals("(cadenaNormal)") || numero[1].Equals("(id)"))
                    {
                        cadena = cadena + numero[0] + " ";
                        return 0.0;
                    }
                    else
                    {
                        return Convert.ToDouble(numero[0]);
                    }

                case 3: //Nodo binario
                    switch (root.ChildNodes.ElementAt(1).ToString().Substring(0, 1))
                    {
                        case "+": //E+E
                            if (expresion(root.ChildNodes.ElementAt(0)) == 0.0 || expresion(root.ChildNodes.ElementAt(2))==0.0){
                                return 0.0;
                            }
                            return expresion(root.ChildNodes.ElementAt(0)) + expresion(root.ChildNodes.ElementAt(2));
                        case "-": //E-E
                            if (expresion(root.ChildNodes.ElementAt(0)) == 0.0 || expresion(root.ChildNodes.ElementAt(2)) == 0.0)
                            {
                                return 0.0;
                            }
                            return expresion(root.ChildNodes.ElementAt(0)) - expresion(root.ChildNodes.ElementAt(2));
                        case "*": //E+E
                            if (expresion(root.ChildNodes.ElementAt(0)) == 0.0 || expresion(root.ChildNodes.ElementAt(2)) == 0.0)
                            {
                                return 0.0;
                            }
                            return expresion(root.ChildNodes.ElementAt(0)) * expresion(root.ChildNodes.ElementAt(2));
                        case "/": //E-E
                            if (expresion(root.ChildNodes.ElementAt(0)) == 0.0 || expresion(root.ChildNodes.ElementAt(2)) == 0.0)
                            {
                                return 0.0;
                            }
                            return expresion(root.ChildNodes.ElementAt(0)) / expresion(root.ChildNodes.ElementAt(2));
                        default: //(E)
                            if (expresion(root.ChildNodes.ElementAt(0)) == 0.0)
                            {
                                return 0.0;
                            }
                            return expresion(root.ChildNodes.ElementAt(1));
                    }
            }

            return 0.0;
        }

    }
}
