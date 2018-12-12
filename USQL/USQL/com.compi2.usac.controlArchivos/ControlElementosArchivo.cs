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
        private String[] lineas;
        private String[] ruta;
        private int contador;

        public ControlElementosArchivo()
        {
            this.contador = 0;
        }

        public  String[] separarLineas(String cadena)
        {
            String[] valores = cadena.Split('\n');
            return valores;
        }

        public  String[] separarCadena(String cadena)
        {
            String[] valores = cadena.Split(',');
            return valores;
        }

        public void getDBs()
        {
            Nodo raiz = new Nodo("Maestro");
            raiz.setValor("maestro");
            raiz.setIdNodo(contador++);

            foreach (Object obj in getDB())
            {
                raiz.addHijos((Nodo)obj);
            }

            MessageBox.Show(raiz.getHijos().Count.ToString());
        }

        public ArrayList getDB()
        {
            ArrayList databases = new ArrayList();

            if (SintacticoXML.analizar(LecturaArchivos.leerMaestro()))
            {
                lineas = separarLineas(RecorridoXML.valores());
                for (int i = 0; i < lineas.Length - 1; i++)
                {
                    ruta = separarCadena(lineas[i]);

                    Nodo dataBase = new Nodo("base de datos");
                    dataBase.setValor(ruta[0]);
                    dataBase.setIdNodo(contador++);

                    foreach (Object obj in getTable(ruta[1]))
                    {
                        Nodo aux = new Nodo("tabla");
                        aux.setValor(obj);
                        aux.setIdNodo(contador++);
                        dataBase.addHijos(aux);
                    }

                    foreach (Object obj in getProcedures(ruta[1]))
                    {
                        Nodo aux = new Nodo("procedimiento");
                        aux.setValor(obj);
                        aux.setIdNodo(contador++);
                        dataBase.addHijos(aux);
                    }

                    foreach (Object obj in getObjects(ruta[1]))
                    {
                        Nodo aux = new Nodo("objeto");
                        aux.setValor(obj);
                        aux.setIdNodo(contador++);
                        dataBase.addHijos(aux);
                    }

                    databases.Add(dataBase);
                }

                return databases;
            }
            else
            {
                MessageBox.Show("Error al leer el archivo maestro de la base de datos");
                return null;
            }
        }

        private ArrayList getTable(String cadena)
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
                        String nombre = valores[1].Replace("\r", "");
                        String ruta = valores[2].Replace("\r", "");
                        String campos = "";

                        for (int j = 3; j < valores.Length - 1; j++)
                        {
                            campos += valores[j].Replace("\r", "") + ",";
                        }

                        Tabla table = new Tabla(nombre, ruta);
                        table.addRow(campos);
                        table.addRowAtEnd(getRows(ruta));
                        //table.printValues(table.getRowsTable());

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
        }

        private ArrayList getRows(String ruta)
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

        private ArrayList getProcedures(String ruta)
        {
            ArrayList procedures = new ArrayList();

            if (SintacticoXML.analizar(LecturaArchivos.leerDB(ruta)))
            {
                String resultado = RecorridoXML.valores();
                String[] tablas = resultado.Split('-');

                for (int i = 0; i < tablas.Length - 1; i++)
                {
                    if(!tablas[i].Contains("procedure") && !tablas[i].Contains("object")){
                        
                    }else{
                        String[] lineas = separarLineas(tablas[i]);

                        for (int j = 0; j < lineas.Length-1; j++)
                        {
                            lineas[j] = lineas[j].Replace("\r", "");
                            if (String.Compare(lineas[j], "") != 0)
                            {
                                if (lineas[j].Contains("procedure:"))
                                {
                                    String[] path = lineas[j].Split(':');
                                    procedures = getProcedure(path[1]);
                                }
                            }
                        }
                    }
                }
                return procedures;
            }
            else{
                MessageBox.Show("Error al parsear el archivo de la base de datos");
                return null;
            }
        }

        private ArrayList getProcedure(String ruta)
        {
            ArrayList procedures = new ArrayList();

            if (SintacticoXML.analizar(LecturaArchivos.leerDB(ruta)))
            {
                String resultado = RecorridoXML.valores();
                String[] procedimientos = resultado.Split('-');

                for (int i = 1; i < procedimientos.Length; i++)
                {
                    String[] valores = procedimientos[i].Split('°');
                    String nombreProc = valores[0].Replace("\r\n", "");

                    Procedimiento proc = new Procedimiento(nombreProc);

                    String[] param = separarLineas(valores[1]);
                    for (int j = 1; j < param.Length - 1; j++)
                    {
                        param[j] = param[j].Replace("\r", "");
                        proc.addParam(param[j]);
                    }

                    String[] inst = valores[2].Replace("\r\n", "").Split(';');
                    for (int j = 0; j < inst.Length - 1; j++)
                    {
                        proc.addInst(inst[j]);
                    }

                    procedures.Add(proc);
                    
                }

                return procedures;
            }
            else
            {
                MessageBox.Show("Error al parsear el archivo de los procedimientos");
                return null;
            }
        }

        private ArrayList getObjects(String ruta)
        {
            ArrayList objects = new ArrayList();

            if (SintacticoXML.analizar(LecturaArchivos.leerDB(ruta)))
            {
                String resultado = RecorridoXML.valores();
                String[] tablas = resultado.Split('-');

                for (int i = 0; i < tablas.Length - 1; i++)
                {
                    if (!tablas[i].Contains("procedure") && !tablas[i].Contains("object"))
                    {

                    }
                    else
                    {
                        String[] lineas = separarLineas(tablas[i]);

                        for (int j = 0; j < lineas.Length - 1; j++)
                        {
                            lineas[j] = lineas[j].Replace("\r", "");
                            if (String.Compare(lineas[j], "") != 0)
                            {
                                if (lineas[j].Contains("object:"))
                                {
                                    String[] path = lineas[j].Split(':');
                                    objects = getObject(path[1]);
                                }
                            }
                        }
                    }
                }
                return objects;
            }
            else
            {
                MessageBox.Show("Error al parsear el archivo de la base de datos");
                return null;
            }
        }

        private ArrayList getObject(String ruta)
        {
            ArrayList objects = new ArrayList();

            if (SintacticoXML.analizar(LecturaArchivos.leerDB(ruta)))
            {
                String resultado = RecorridoXML.valores();
                String[] objetos = resultado.Split('-');

                for (int i = 1; i < objetos.Length; i++)
                {
                    String[] valores = objetos[i].Split('°');
                    String nombreObj = valores[0].Replace("\r\n", "");

                    Objeto obj = new Objeto(nombreObj);

                    String[] param = separarLineas(valores[1]);
                    for (int j = 1; j < param.Length - 1; j++)
                    {
                        param[j] = param[j].Replace("\r", "");
                        obj.addCampo(param[j]);
                    }

                    objects.Add(obj);
                }

                return objects;
            }
            else
            {
                MessageBox.Show("Error al parsear el archivo de los procedimientos");
                return null;
            }
        }
    }
}
