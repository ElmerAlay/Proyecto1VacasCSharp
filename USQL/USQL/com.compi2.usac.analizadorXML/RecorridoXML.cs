using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Irony.Parsing;
using System.Windows.Forms;

namespace USQL.com.compi2.usac.analizadorXML
{
    class RecorridoXML
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
                    if (String.Compare(root.ToString(), "BDS") == 0)
                    {
                        return expresion(root.ChildNodes.ElementAt(0));
                    }
                    else
                    {
                        return root.ChildNodes.ElementAt(0).ToString().Replace(" (cadenaNormal)", "");
                    }
                case 2: //Nodo binario
                    if (String.Compare(root.ToString(), "BD") == 0)
                    {
                        return root.ChildNodes.ElementAt(0).ToString().Replace(" (cadenaNormal)", "") + "," +
                            root.ChildNodes.ElementAt(1).ToString().Replace(" (cadenaNormal)", "") + Environment.NewLine;
                    }
                    else if (String.Compare(root.ToString(), "BDS") == 0)
                    {
                        return expresion(root.ChildNodes.ElementAt(0)) + expresion(root.ChildNodes.ElementAt(1));
                    }
                    
                    return "";
            }

            return "";
        }
    }
}
