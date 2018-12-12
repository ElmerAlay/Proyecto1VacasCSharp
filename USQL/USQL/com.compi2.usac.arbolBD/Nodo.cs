using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace USQL.com.compi2.usac.arbolBD
{
    class Nodo
    {
        private String etiqueta;
        private ArrayList hijos;
        private Object valor;
        private int idNodo;

        public Nodo(String etiqueta) {
            this.etiqueta = etiqueta;
            this.hijos = new ArrayList();
            this.valor = null;
            this.idNodo = 0;
        }

        public void addHijos(Nodo hijo)
        {
            hijos.Add(hijo);
        }

        public String getEtiqueta()
        {
            return this.etiqueta;
        }

        public void setEtiqueta(String etiqueta)
        {
            this.etiqueta = etiqueta;
        }

        public ArrayList getHijos()
        {
            return this.hijos;
        }

        public void setHijos(ArrayList hijos)
        {
            this.hijos = hijos;
        }

        public Object getValor()
        {
            return this.valor;
        }

        public void setValor(Object valor)
        {
            this.valor = valor;
        }

        public int getIdNodo()
        {
            return this.idNodo;
        }

        public void setIdNodo(int idNodo)
        {
            this.idNodo = idNodo;
        }
    }
}
