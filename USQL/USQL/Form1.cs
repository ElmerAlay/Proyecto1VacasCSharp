using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using USQL.com.compi2.usac.analizador;
using USQL.com.compi2.usac.analizadorPlyCS;
using USQL.com.compi2.usac.analizadorXML;
using USQL.com.compi2.usac.controlArchivos;

namespace USQL
{
    public partial class Form1 : Form
    {
        #region Variables
        private TcpListener servidor = null; //servidor de escucha
        private IPAddress ip = null;       //ip del servidor
        private int port = 0;               //puerto por el que realiza la escucha
        private TcpClient cliente = null;  //cliente conectado(aceptamos la solicitud y asignamos el tcpclient)
        private NetworkStream leer_escribir = null; //metodo para enviar y recibir informacion desde cliente

        private Byte[] bytes;// = new Byte[10000];   // donde almacenamos lo recibido o lo que vamos a enviar
        private String cadena = ""; //almacenamos la info antes de codificar o bien despues para tratarla
        private static int max_connect = 100; //maximas conexiones permitidas
        private int conectados = 0; //usuarios conectados
        private IntPtr[] idclientes = new IntPtr[max_connect]; //array de identificadores de clientes
        private TcpClient[] tcpclientes= new TcpClient[max_connect]; //array de tcpclient de los clientes
        private int posicion = 0;
        #endregion

        public Form1()
        {
            InitializeComponent();
        }

        private void btn_analizar_Click(object sender, EventArgs e)
        {
            bool resultado = SintacticoUSQL.analizar(rtb_entrada.Text);
            //bool resultado = SintacticoXML.analizar();

            //ControlElementosArchivo cs = new ControlElementosArchivo();
            //cs.getDBs();

            if (resultado)
            {
                rtb_consola.Text = "Correcto";
            }
            else
            {
                rtb_consola.Text = "Incorrecto";
            }
        }

        private void escucha_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                //    indicamos ip y puerto
                ip = IPAddress.Parse("127.0.0.1");
                port = 6000;

                //    asignamos un nuevo tcplistener a servidor
                servidor = new TcpListener(ip, port);
                servidor.Start(); //inicializamos el modo escucha

                while (true)
                {
                    //        preguntamos si existen solicitudes pendientes
                    //y posteriormente se acepta y luego se controla si se mantiene
                    if (servidor.Pending() == true)
                    {
                        cliente = servidor.AcceptTcpClient();

                        if (conectados < max_connect)
                        {
                            conectados++;

                            //guardamos info del cliente
                            idclientes[posicion] = Marshal.StringToHGlobalUni(cliente.Client.Handle.ToString());

                            //usuarios.Text &= "********************************************" & vbCrLf
                            Console.WriteLine("********************************************");
                            //usuarios.Text &= "[ID] " & idclientes(posicion).ToString & "     " & "[DT] " & Date.Now & vbCrLf
                            Console.WriteLine("[ID] " + idclientes[posicion].ToString() + "     " + "[DT] " + DateTime.Now);

                            tcpclientes[posicion] = cliente;
                            mandar_usuarios();

                            timer1.Interval = 1000;
                            timer1.Start();

                            posicion++;
                        }
                        else
                        {
                            noconnect(cliente);
                        }
                    }

                    //esperamos cualquier escritura en el servidor 
                    System.Windows.Forms.Application.DoEvents();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void noconnect(TcpClient cliente)
        {
            cadena = "Servidor Saturado\n";
            bytes = Encoding.ASCII.GetBytes(cadena);
            leer_escribir = cliente.GetStream();
            leer_escribir.Write(bytes, 0, bytes.Length);

            leer_escribir.Flush();

            leer_escribir = null;
            bytes = null;
            cadena = "";

            cliente.Close();
        }

        private void mandar_usuarios()
        {
            int i, j;

            //'almacenar lista usuarios en cadena
            for (j = 0; j <= posicion; j++)
            {
                cadena += "U.[ID] - " + idclientes[j].ToString() + "\n";
            }

            for (i = 0; i <= posicion; i++)
            {
                //meter en cadena todos los id
                leer_escribir = tcpclientes[i].GetStream();
                bytes = Encoding.ASCII.GetBytes(cadena);
                leer_escribir.Write(bytes, 0, bytes.Length);
            }


            cadena = "";
            bytes = null;
            leer_escribir = null;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int i, j;

            for (i = 0; i <= posicion - 1; i++)
            {
                leer_escribir = tcpclientes[i].GetStream();

                if (leer_escribir.DataAvailable == true)
                {
                    bytes = new byte[10000];
                    int array_size = 0;

                    array_size = leer_escribir.Read(bytes, 0, bytes.Length);
                    Array.Resize(ref bytes, array_size);
                    String r = Encoding.Default.GetString(bytes);

                    if (r.Contains("report "))
                    {
                        r = r.Replace("report ", "");
                        cadena = "[" +
	                                "paquete: reporte," +
	                                "datos:" + "\"" + r + "\"" +
                                 "]";

                    }
                    else if (r.Contains("login "))
                    {
                        r = r.Replace("login ", "");
                        cadena = "[" +
                                    "validar: 1500," +
                                    "datos:" + "\"" + r + "\"" +
                                "]";
                    }
                    else if (r.Contains("logout "))
                    {
                        r = r.Replace("logout "," ");
                        cadena = "cadena finalizada";
                    }
                    else
                    {
                        r = r.Replace("usql ", "");
                        cadena = "[" + 
                                    "paquete: usql," + 
                                    "datos:" +
                                    "[" +
                                        "[ \"" + r + "\"]" +
                                    "]" +
                                "]";
                    }

                    for (j = 0; j <= posicion - 1; j++)
                    {
                        leer_escribir = tcpclientes[j].GetStream();
                        bytes = Encoding.ASCII.GetBytes(cadena);
                        leer_escribir.Write(bytes, 0, bytes.Length);
                    }

                    cadena = "";
                    bytes = null;
                    leer_escribir = null;
                }
            }
        }
    }
}
