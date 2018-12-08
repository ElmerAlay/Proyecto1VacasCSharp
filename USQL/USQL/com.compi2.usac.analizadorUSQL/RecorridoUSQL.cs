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
        public static void resultado(ParseTreeNode root)
        {
            MessageBox.Show("Respuesta " + expresion(root.ChildNodes.ElementAt(0)));
        }

        private static String expresion(ParseTreeNode root)
        {
            switch (root.ChildNodes.Count)
            {
                case 1: //Nodos hojas
                    String[] numero = root.ChildNodes.ElementAt(0).ToString().Split(' ');
                    return numero[0];
                case 3: //Nodo binario
                    switch (root.ChildNodes.ElementAt(1).ToString().Substring(0, 1))
                    {
                        case "+": //E+E
                            return expresion(root.ChildNodes.ElementAt(0)) + expresion(root.ChildNodes.ElementAt(2));
                        case "-": //E-E
                            return expresion(root.ChildNodes.ElementAt(0)) + expresion(root.ChildNodes.ElementAt(2));
                        default: //(E)
                            return expresion(root.ChildNodes.ElementAt(1));
                    }
            }

            return "";
        }

    }
}
