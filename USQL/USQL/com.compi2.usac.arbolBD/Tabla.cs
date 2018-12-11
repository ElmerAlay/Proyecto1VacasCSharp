using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace USQL.com.compi2.usac.arbolBD
{
    class Tabla
    {
        private String nameTable;
        private String pathTable;
        private ArrayList rowsTable;

        public Tabla(String nameTable, String pathTable)
        {
            this.nameTable = nameTable;
            this.pathTable = pathTable;
            this.rowsTable = new ArrayList();
        }

        public void addRow(String value)
        {
            this.rowsTable.Add(value);
        }

        public void addRowAtEnd(ArrayList newRows)
        {
            this.rowsTable.AddRange(newRows);
        }

        public void setNameTable(String nameTable)
        {
            this.nameTable = nameTable;
        }

        public String getNameTable()
        {
            return this.nameTable;
        }

        public void setPathTable(String pathTable)
        {
            this.pathTable = pathTable;
        }

        public String getPathTable()
        {
            return this.pathTable;
        }

        public void setRowsTabla(ArrayList rowsTable)
        {
            this.rowsTable = rowsTable;
        }

        public ArrayList getRowsTable()
        {
            return this.rowsTable;
        }

        public void printValues(ArrayList myList)
        {
            foreach (Object obj in myList)
                MessageBox.Show(obj + Environment.NewLine);
        }
    }
}
