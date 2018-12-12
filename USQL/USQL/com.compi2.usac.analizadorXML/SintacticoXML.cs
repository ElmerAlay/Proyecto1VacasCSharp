using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Irony.Ast;
using Irony.Parsing;
using System.Windows.Forms;
using USQL.com.compi2.usac.controlDot;
using System.IO;

namespace USQL.com.compi2.usac.analizadorXML
{
    class SintacticoXML : Grammar
    {
        public static bool analizar(String cadena)
        {
            GramaticaXML gramatica = new GramaticaXML();
            LanguageData lenguaje = new LanguageData(gramatica);
            Parser parser = new Parser(lenguaje);
            ParseTree arbol = parser.Parse(cadena);
            ParseTreeNode raiz = arbol.Root;

            if (raiz == null)
            {
                return false;
            }

            //generarImagen(raiz);
            //MessageBox.Show("Imagen generada");
            RecorridoXML.resultado(raiz);

            return true;
        }

        private static void generarImagen(ParseTreeNode raiz)
        {
            String grafoDot = ControlDot.getDot(raiz);

            TextWriter file = new StreamWriter("astXML.dot");
            file.WriteLine(grafoDot);
            file.Close();

            TextWriter bat = new StreamWriter("file.bat");
            bat.WriteLine("@echo off");
            bat.WriteLine("cd C:\\Program Files\\Java\\jre1.8.0_161\\bin\\javaw.exe");
            bat.WriteLine("dot -Tpng astXML.dot -o astXML.png");
            bat.Close();

            System.Diagnostics.Process.Start("file.bat");
        }
    }
}
