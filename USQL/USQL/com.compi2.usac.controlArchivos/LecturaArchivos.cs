using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;
using System.Windows.Forms;

namespace USQL.com.compi2.usac.controlArchivos
{
    class LecturaArchivos
    {
        private static readonly String ruta = "C:\\Users\\junio\\OneDrive\\Documents\\Universidad\\Cursos\\Compiladores2\\Proyecto1_Vacas\\Proyecto1VacasCSharp\\USQL\\USQL\\bin\\Debug";

        public static String leerMaestro()
        {
            StreamReader archivo = new StreamReader(ruta + "\\DBS\\master.usac");
            
            string sLine = "";
            ArrayList arrText = new ArrayList();

            while (sLine != null)
            {
                sLine = archivo.ReadLine();
                if (sLine != null)
                    arrText.Add(sLine);
            }
            archivo.Close();

            String respuesta = "";
            foreach (string sOutput in arrText)
                respuesta += sOutput + Environment.NewLine;

            return respuesta;
        }

        public static String leerDB(String rutaAbsoluta)
        {
            StreamReader archivo = new StreamReader( ruta + rutaAbsoluta);
            string sLine = "";
            ArrayList arrText = new ArrayList();

            while (sLine != null)
            {
                sLine = archivo.ReadLine();
                if (sLine != null)
                    arrText.Add(sLine);
            }
            archivo.Close();

            String respuesta = "";
            foreach (string sOutput in arrText)
            {
                //MessageBox.Show(sOutput);
                respuesta += sOutput + Environment.NewLine;
            }
                
            return respuesta;
        }
    }
}
