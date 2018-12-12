using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Windows.Forms;

namespace USQL.com.compi2.usac.arbolBD
{
    class Procedimiento
    {
        private String name;
        private String path;
        private ArrayList listaParam;
        private ArrayList listaInst;

        public Procedimiento(String name)
        {
            this.name = name;
            this.path = null;
            this.listaParam = new ArrayList();
            this.listaInst = new ArrayList();
        }

        public void addParam(String param)
        {
            this.listaParam.Add(param);
        }

        public void addParamAtEnd(ArrayList newParams)
        {
            this.listaParam.AddRange(newParams);
        }

        public void addInst(String inst)
        {
            this.listaInst.Add(inst);
        }

        public void addInstAtEnd(ArrayList newInsts)
        {
            this.listaInst.AddRange(newInsts);
        }

        public void setName(String name)
        {
            this.name = name;
        }

        public String getName()
        {
            return this.name;
        }

        public void setPath(String path)
        {
            this.path = path;
        }

        public String getPath()
        {
            return this.path;
        }

        public void setListaParam(ArrayList listaParam)
        {
            this.listaParam = listaParam;
        }

        public ArrayList getListaParam()
        {
            return this.listaParam;
        }

        public void setListaInst(ArrayList listaInst)
        {
            this.listaInst = listaInst;
        }

        public ArrayList getListaInst()
        {
            return this.listaInst;
        }

        public void printParams(ArrayList myList)
        {
            foreach (Object obj in myList)
                MessageBox.Show(obj.ToString());
        }

        public void printInsts(ArrayList myList)
        {
            foreach (Object obj in myList)
                MessageBox.Show(obj.ToString());
        }
    }
}
