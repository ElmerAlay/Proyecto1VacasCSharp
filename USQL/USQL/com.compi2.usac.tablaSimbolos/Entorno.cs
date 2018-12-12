using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USQL.com.compi2.usac.tablaSimbolos
{
    class Entorno
    {
        private Hashtable tabla;
        protected Entorno ant;

        public Entorno(Entorno padre)
        {
            tabla = new Hashtable();
            ant = padre;
        }

        public void put(String s, Simbolo sim)
        {
            if (!exists(s))
                tabla.Add(s, sim);
        }

        public Simbolo get(String s)
        {
            for (Entorno e = this; e != null; e = e.ant)
            {
                //Simbolo encontro = (Simbolo)(e.tabla.(s));
                //if (encontro != null)
                //    return encontro;
            }
            return null;
        }

        public bool exists(String s)
        {
            for (Entorno e = this; e != null; e = e.ant)
            {
                //Simbolo encontro = (Simbolo)(e.tabla.(s));
                //if (encontro != null)
                //    return true;
            }

            return false;
        }
    }
}
