using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USQL.com.compi2.usac.treeAST
{
    class controlDot
    {
        private static int contador;
        private static String grafo;

        public static String getDot(Node raiz)
        {
            grafo = "digraph G{";
            grafo += "node[shape=\"box\"];\n";
            grafo += "nodo0[label=\"" + escapar(raiz.getEtiqueta()) + "\"];\n";
            contador = 1;
            recorrerAST("nodo0", raiz);
            grafo += "}";

            return grafo;
        }

        private static void recorrerAST(String padre, Node hijos)
        {
            foreach (Node hijo in hijos.getHijos())
            {
                String nombreHijo = "nodo" + contador.ToString();

                grafo += nombreHijo + "[label=\"" + escapar(hijo.getEtiqueta()) + "\"];\n";
                grafo += padre + "->" + nombreHijo + ";\n";

                contador++;

                recorrerAST(nombreHijo, hijo);
            }
        }

        private static String escapar(String cadena)
        {
            cadena = cadena.Replace("\\", "\\\\");
            cadena = cadena.Replace("\"", "\\\"");

            return cadena;
        }
    }
}
