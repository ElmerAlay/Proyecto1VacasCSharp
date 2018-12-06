using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Irony.Ast;
using Irony.Parsing;
using WINGRAPHVIZLib;
using System.Drawing;
using System.IO;
using USQL.com.compi2.usac.controlDot;
using System.Windows.Forms;

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

            generarImagen(raiz);
            MessageBox.Show("Imagen generada");

            return true;
        }

        private static void generarImagen(ParseTreeNode raiz)
        {
            String grafoDot = ControlDot.getDot(raiz);
            MessageBox.Show(grafoDot);
            try
            {
                WINGRAPHVIZLib.DOT dot = new WINGRAPHVIZLib.DOT();
                WINGRAPHVIZLib.BinaryImage img = dot.ToPNG(grafoDot);
                byte[] imageBytes = Convert.FromBase64String(img.ToBase64String());
                MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
                ms.Write(imageBytes, 0, imageBytes.Length);
                Image imagen = Image.FromStream(ms, true);
                img.Save("AST.png");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }
    }
}
