using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USQL.com.compi2.usac.tablaSimbolos
{
    class Simbolo
    {
        private String id;
        private Object valor;
        private String tipo;

        public Simbolo(String id, Object valor, String tipo)
        {
            this.id = id;
            this.valor = valor;
            this.tipo = tipo;
        }

        public String getId()
        {
            return id;
        }

        public void setId(String id)
        {
            this.id = id;
        }

        public Object getValor()
        {
            return valor;
        }

        public void setValor(Object valor)
        {
            this.valor = valor;
        }

        public String getTipo()
        {
            return this.tipo;
        }

        public void setTipo(String tipo)
        {
            this.tipo = tipo;
        }
    }
}
