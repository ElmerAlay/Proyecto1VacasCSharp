using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using USQL.com.compi2.usac.analizador;

namespace USQL
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btn_analizar_Click(object sender, EventArgs e)
        {
            bool resultado = SintacticoUSQL.analizar(rtb_entrada.Text);
            if (resultado)
            {
                rtb_consola.Text = "Correcto";
            }
            else
            {
                rtb_consola.Text = "Incorrecto";
            }
        }
    }
}
