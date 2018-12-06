using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Irony.Ast;
using Irony.Parsing;

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

            return true;
        }
    }
}
