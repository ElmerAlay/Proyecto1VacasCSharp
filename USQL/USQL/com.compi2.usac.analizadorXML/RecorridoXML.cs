using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Irony.Parsing;
using System.Windows.Forms;
using USQL.com.compi2.usac.controlArchivos;

namespace USQL.com.compi2.usac.analizadorXML
{
    class RecorridoXML
    {
        private static string result=""; 

        public static String valores()
        {
            return result;
        }

        public static String resultado(ParseTreeNode root)
        {
            //MessageBox.Show(expresion(root.ChildNodes.ElementAt(0)));
            result = expresion(root.ChildNodes.ElementAt(0));
            return result;       
        }

        private static String expresion(ParseTreeNode root)
        {
            switch (root.ChildNodes.Count)
            {
                case 1: //Nodos hojas
                    if (String.Compare(root.ToString(), "BDS") == 0 || String.Compare(root.ToString(), "REGISTROS") == 0 ||
                        String.Compare(root.ToString(), "REGISTRO") == 0 || String.Compare(root.ToString(), "CAMPOS") == 0 ||
                        String.Compare(root.ToString(), "ROWS") == 0 || String.Compare(root.ToString(), "PROCS") == 0 ||
                        String.Compare(root.ToString(), "OBJS") == 0)
                    {
                        return expresion(root.ChildNodes.ElementAt(0));
                    }
                        else if(String.Compare(root.ToString(), "ROW") == 0){
                        return "-" + Environment.NewLine + expresion(root.ChildNodes.ElementAt(0));
                    }
                    else if(String.Compare(root.ToString(), "ROW") == 0){
                        return "-" + Environment.NewLine + expresion(root.ChildNodes.ElementAt(0)) + Environment.NewLine;
                    }
                    else
                    {
                        if (String.Compare(root.ToString(), "PROCEDURE") == 0)
                        {
                            return "procedure:" + root.ChildNodes.ElementAt(0).ToString().Replace(" (cadenaNormal)", "") + Environment.NewLine;
                        }
                        else if (String.Compare(root.ToString(), "OBJECT") == 0)
                        {
                            return "object:" + root.ChildNodes.ElementAt(0).ToString().Replace(" (cadenaNormal)", "") + Environment.NewLine;
                        }
                        else
                        {
                            return root.ChildNodes.ElementAt(0).ToString().Replace(" (cadenaNormal)", "");
                        }
                    }
                case 2:
                    if (String.Compare(root.ToString(), "BDS") == 0 || String.Compare(root.ToString(), "REGISTROS") == 0 ||
                        String.Compare(root.ToString(), "CAMPOS") == 0 || String.Compare(root.ToString(), "ROWS") == 0 ||
                        String.Compare(root.ToString(), "PROCS") == 0 || String.Compare(root.ToString(), "OBJS") == 0)
                    {
                        return expresion(root.ChildNodes.ElementAt(0)) + expresion(root.ChildNodes.ElementAt(1));
                    }
                    else if (String.Compare(root.ToString(), "CAMPO") == 0)
                    {
                        String val = root.ChildNodes.ElementAt(0).ToString().Replace(" (Key symbol)", "");
                        val = val.Replace("<", "");
                        val = val.Replace(">", " ");

                        return val + root.ChildNodes.ElementAt(1).ToString().Replace(" (cadenaNormal)", "") + Environment.NewLine;
                    }
                    else if (String.Compare(root.ToString(), "OBJ") == 0)
                    { 
                        return root.ChildNodes.ElementAt(0).ToString().Replace(" (cadenaNormal)", "") + Environment.NewLine +
                            "campos: " + Environment.NewLine +
                            expresion(root.ChildNodes.ElementAt(1));
                    }
                    return "";
                case 3:
                    if (String.Compare(root.ToString(), "BD") == 0)
                    {
                        return
                            root.ChildNodes.ElementAt(0).ToString().Replace(" (cadenaNormal)", "") + "," +
                            root.ChildNodes.ElementAt(1).ToString().Replace(" (cadenaNormal)", "") + "," +
                            root.ChildNodes.ElementAt(2).ToString().Replace(" (cadenaNormal)", "") + Environment.NewLine;
                    }
                    else if (String.Compare(root.ToString(), "TABLE") == 0)
                    {
                        String val = "-" + Environment.NewLine + 
                            root.ChildNodes.ElementAt(0).ToString().Replace(" (cadenaNormal)", "") + Environment.NewLine +
                            root.ChildNodes.ElementAt(1).ToString().Replace(" (cadenaNormal)", "") + Environment.NewLine;
                        return val + expresion(root.ChildNodes.ElementAt(2)) + "-" + Environment.NewLine;

                    }
                    else if (String.Compare(root.ToString(), "CAMPO") == 0)
                    {
                        String val = root.ChildNodes.ElementAt(0).ToString().Replace(" (id)", "");

                        return val + " " + root.ChildNodes.ElementAt(1).ToString().Replace(" (cadenaNormal)", "") + Environment.NewLine;
                    }
                    else if (String.Compare(root.ToString(), "PROC") == 0)
                    {
                        return root.ChildNodes.ElementAt(0).ToString().Replace(" (cadenaNormal)", "") + Environment.NewLine +
                            "parametros: " + Environment.NewLine +
                            expresion(root.ChildNodes.ElementAt(1)) +
                            root.ChildNodes.ElementAt(2).ToString().Replace(" (cadenaNormal)", "") + Environment.NewLine;
                    }
                    
                    return "";
            }

            return "";
        }
    }
}
