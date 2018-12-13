using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Irony.Ast;
using Irony.Parsing;
using System.Drawing;
using System.IO;
using USQL.com.compi2.usac.controlDot;
using USQL.com.compi2.usac.treeAST;
using System.Windows.Forms;
using System.Diagnostics;

namespace USQL.com.compi2.usac.analizador
{
    class SintacticoUSQL : Grammar
    {
        public static bool analizar(String cadena)
        {
            GramaticaUSQL gramatica = new GramaticaUSQL();
            LanguageData lenguaje = new LanguageData(gramatica);
            Parser parser = new Parser(lenguaje);
            ParseTree arbol = parser.Parse(cadena);
            ParseTreeNode raiz = arbol.Root;

            if (raiz == null)
            {
                return false;
            }

            RecorridoUSQL.arbolAST(raiz);
            //RecorridoUSQL.resultado(raiz);
            //generarImagen(raiz);
            return true;
        }

        private static void generarImagen(ParseTreeNode raiz)
        {
            String grafoDot = ControlDot.getDot(raiz);

            TextWriter file = new StreamWriter("ast.dot");
            file.WriteLine(grafoDot);
            file.Close();

            TextWriter bat = new StreamWriter("file.bat");
            bat.WriteLine("@echo off");
            bat.WriteLine("cd C:\\Program Files\\Java\\jre1.8.0_161\\bin\\javaw.exe");
            bat.WriteLine("dot -Tpng ast.dot -o ast.png");
            bat.Close();

            System.Diagnostics.Process.Start("file.bat");
        }
    }
}
