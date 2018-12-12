using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace USQL.com.compi2.usac.treeAST
{
    class Node
    {
        private String etiqueta;
        private ArrayList hijos;
        private Object valor;
        private static int idNodo=0;

        public Node(String etiqueta) {
            this.etiqueta = etiqueta;
            this.hijos = new ArrayList();
            this.valor = null;
            idNodo++;
        }

        public void addHijos(Node hijo)
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
            return idNodo;
        }

        //public void setIdNodo(int idNodo)
        //{
        //    idNodo = idNodo;
        //}
    }
}
