using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Irony.Parsing;
using System.Windows.Forms;


namespace USQL.com.compi2.usac.analizador
{
    class RecorridoUSQL
    {
        public static String cadena="";
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
