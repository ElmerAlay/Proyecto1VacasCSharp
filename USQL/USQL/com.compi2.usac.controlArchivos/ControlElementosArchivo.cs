using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using USQL.com.compi2.usac.analizadorXML;
using System.Windows.Forms;
using System.IO;
using System.Collections;

namespace USQL.com.compi2.usac.controlArchivos
{
    class ControlElementosArchivo
    {
        private static String[] lineas;
        private static String[] ruta;

        public static String[] separarLineas(String cadena)
        {
            String[] valores = cadena.Split('\n');
            return valores;
        }

        public static String[] separarCadena(String cadena)
        {
            String[] valores = cadena.Split(',');
            return valores;
        }

        public static String getDB(String db, String user)
        {
            bool resultado = SintacticoXML.analizar(LecturaArchivos.leerMaestro());

            if (resultado)
            {
                lineas = ControlElementosArchivo.separarLineas(RecorridoXML.valores());
                for (int i = 0; i < lineas.Length - 1; i++)
                {
                    ruta = ControlElementosArchivo.separarCadena(lineas[i]);

                    //MessageBox.Show(ruta[0] + "=" + db + " " + ruta[2].Replace("\r","") + "=" + user);
                    if (String.Compare(ruta[0], db) == 0 && String.Compare(ruta[2].Replace("\r", ""), user) == 0)
                    {
                        MessageBox.Show("Si existe!!!");
                        getTable(ruta[1]);
                    }
                        
                }
            }
            else
            {
                return "Error en el archivo";
            }
            return "";
        }

        private static String getTable(String cadena)
        {
            if (SintacticoXML.analizar(LecturaArchivos.leerDB(cadena)))
            {
                String resultado = RecorridoXML.valores();
                String[] tablas = resultado.Split('-');

                for (int i = 0; i < tablas.Length - 1; i++)
                {
                    if (!tablas[i].Contains("path"))
                    {
                        String[] valores = tablas[i].Split('\n');
                        String nombre = valores[1].Replace("\r","");
                        String ruta = valores[2].Replace("\r", "");
                        String campos = ""; 

                        for (int j = 3; j < valores.Length - 1; j++)
                        {
                            campos += valores[j].Replace("\r", "") + ";";
                        }
                           MessageBox.Show(nombre + Environment.NewLine + ruta + Environment.NewLine + campos);
                    }
                }
            }
            return "";
        }
    }
}
