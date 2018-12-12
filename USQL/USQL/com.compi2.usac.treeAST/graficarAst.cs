using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USQL.com.compi2.usac.treeAST
{
    class graficarAst
    {
        public static void generarImagen(Node raiz)
        {
            String grafoDot = controlDot.getDot(raiz);

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
