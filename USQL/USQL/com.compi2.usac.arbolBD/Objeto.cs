using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Windows.Forms;

namespace USQL.com.compi2.usac.arbolBD
{
    class Objeto
    {
        private String nombre;
        private ArrayList campos;

        public Objeto(String nombre){
            this.nombre = nombre;
            this.campos = new ArrayList();
        }

        public void addCampo(String value)
        {
            this.campos.Add(value);
        }

        public void addRowAtEnd(ArrayList newValues)
        {
            this.campos.AddRange(newValues);
        }

        public void setNombre(String nombre)
        {
            this.nombre = nombre;
        }

        public String getNombre()
        {
            return this.nombre;
        }

        public void setCampos(ArrayList campos)
        {
            this.campos = campos;
        }

        public ArrayList getCampos()
        {
            return this.campos;
        }

        public void printValues()
        {
            foreach (Object obj in this.campos)
                MessageBox.Show(obj + Environment.NewLine);
        }
    }
}
