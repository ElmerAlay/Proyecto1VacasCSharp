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
        public static Node root = null;
        public static int contador = 0;

        public static void resultado(ParseTreeNode root)
        {
            if (expresion(root.ChildNodes.ElementAt(0)) != 0.0)
            {
                MessageBox.Show("Respuesta " + expresion(root.ChildNodes.ElementAt(0)));
                cadena = "";

            }
            else
            {
                MessageBox.Show("Error de tipo: " + Environment.NewLine + " No se pudieron operar los números con las sigientes cadenas: " + cadena);
                cadena = "";
            }
        }

        public static void arbolAST(ParseTreeNode root)
        {
            graficarAst.generarImagen(tree(root));
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
                    else
                    {
                        Node hoja = new Node(root.ChildNodes.ElementAt(0).ToString());
                        return hoja;
                    }
                case 3: //Nodo binario
                    switch (root.ChildNodes.ElementAt(1).ToString().Substring(0, 1))
                    {
                        case "+": //E+E
                            Node suma = new Node("E");
                            //suma.setIdNodo(contador++);
                            suma.addHijos(tree(root.ChildNodes.ElementAt(0)));
                            suma.addHijos(new Node("+"));
                            suma.addHijos(tree(root.ChildNodes.ElementAt(2)));

                            return suma;
                        case "-": //E-E
                            Node resta = new Node("E");
                            resta.addHijos(tree(root.ChildNodes.ElementAt(0)));
                            resta.addHijos(new Node("-"));
                            resta.addHijos(tree(root.ChildNodes.ElementAt(2)));

                            return resta;
                        case "*": //E+E
                            Node mult = new Node("E");
                            mult.addHijos(tree(root.ChildNodes.ElementAt(0)));
                            mult.addHijos(new Node("*"));
                            mult.addHijos(tree(root.ChildNodes.ElementAt(2)));

                            return mult;
                        case "/": //E-E
                            Node div = new Node("E");
                            div.addHijos(tree(root.ChildNodes.ElementAt(0)));
                            div.addHijos(new Node("/"));
                            div.addHijos(tree(root.ChildNodes.ElementAt(2)));

                            return div;
                        default: //(E)
                            Node par = new Node("E");
                            par.addHijos(tree(root.ChildNodes.ElementAt(1)));

                            return par;
                    }
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
