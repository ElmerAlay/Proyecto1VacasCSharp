using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using USQL.com.compi2.usac.analizadorXML;
using USQL.com.compi2.usac.arbolBD;
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
            if (SintacticoXML.analizar(LecturaArchivos.leerMaestro()))
            {
                lineas = ControlElementosArchivo.separarLineas(RecorridoXML.valores());
                for (int i = 0; i < lineas.Length - 1; i++)
                {
                    ruta = ControlElementosArchivo.separarCadena(lineas[i]);

                    if (String.Compare(ruta[0], db) == 0 && String.Compare(ruta[2].Replace("\r", ""), user) == 0)
                    {
                        MessageBox.Show("Si existe!!!");
                        
                        MessageBox.Show(getTable(ruta[1]).Count.ToString());
                    }
                        
                }
            }
            else
            {
                return "Error en el archivo";
            }
            return "";
        }

        private static ArrayList getTable(String cadena)
        {
            ArrayList tables = new ArrayList();

            if (SintacticoXML.analizar(LecturaArchivos.leerDB(cadena)))
            {
                String resultado = RecorridoXML.valores();
                String[] tablas = resultado.Split('-');

                for (int i = 0; i < tablas.Length - 1; i++)
                {
                    if (!tablas[i].Contains("procedure") && !tablas[i].Contains("object"))
                    {
                        String[] valores = tablas[i].Split('\n');
                        String nombre = valores[1].Replace("\r","");
                        String ruta = valores[2].Replace("\r", "");
                        String campos = ""; 

                        for (int j = 3; j < valores.Length - 1; j++)
                        {
                            campos += valores[j].Replace("\r", "") + ",";
                        }

                        Tabla table = new Tabla(nombre, ruta);
                        table.addRow(campos);
                        table.addRowAtEnd(getRows(ruta));
                        table.printValues(table.getRowsTable());

                        tables.Add(table);
                    }
                }

                return tables;
            }
            else
            {
                MessageBox.Show("Error al parsear el archivo de la base de datos");
                return null;
            }

            return null;
        }

        private static ArrayList getRows(String ruta)
        {
            ArrayList result = new ArrayList();

            if (SintacticoXML.analizar(LecturaArchivos.leerDB(ruta)))
            {
                String resultado = RecorridoXML.valores();
                String[] rows = resultado.Split('-');

                for (int i = 1; i < rows.Length; i++)
                {
                    String[] valores = rows[i].Split('\n');
                    String row ="";
                    String final = "";

                    for (int j = 1; j < valores.Length - 1; j++)
                    {
                        row = valores[j].Replace("\r", "");
                        String[] aux = row.Split(' ');
                        
                        for(int k=1;k<aux.Length;k++){
                            final += aux[k];
                        }
                        final += ",";
                    }
                    final += Environment.NewLine;

                    result.Add(final);
                }

                return result;
            }
            else
            {
                MessageBox.Show("Error al leer el xml de la tabla");
                return null;
            }
        }
    }
}
