using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Timers;

namespace USQL.com.compi2.usac.socket
{
    class Servidor
    {
        private TcpListener servidor; //servidor de escucha
        private IPAddress ip;       //ip del servidor
        private int port;               //puerto por el que realiza la escucha
        private TcpClient cliente;  //cliente conectado(aceptamos la solicitud y asignamos el tcpclient)
        private NetworkStream leer_escribir = null; //metodo para enviar y recibir informacion desde cliente

        private Byte[] bytes;   // donde almacenamos lo recibido o lo que vamos a enviar
        private String cadena; //almacenamos la info antes de codificar o bien despues para tratarla
        private int max_connect; //maximas conexiones permitidas
        private int conectados; //usuarios conectados
        private IntPtr[] idclientes; //array de identificadores de clientes
        private TcpClient[] tcpclientes; //array de tcpclient de los clientes
        private int posicion;

        Timer secuencia_lecturas;

        public Servidor(){
            servidor = null;
            ip = null;
            port = 0;
            cliente = null;
            leer_escribir = null;

            cadena = "";
            max_connect = 10;
            conectados = 0;
            idclientes = new IntPtr[max_connect];
            tcpclientes = new TcpClient[max_connect];
            posicion = 0;

            secuencia_lecturas = new Timer();
            secuencia_lecturas.Enabled = true;
        }

        public void Conectar()
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
                    if (servidor.Pending() == true) {
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
                            //mandar_usuarios();

                            secuencia_lecturas.Interval = 1000;
                            secuencia_lecturas.Start();

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
            catch(Exception ex){
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
            for (j = 0; j < posicion; j++)
            {
                cadena += "U.[ID] - " + idclientes[j].ToString() + "\n";
            }

            for (i = 0; i < posicion; i++)
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

        public void secuencia_lecturas_Tick(System.Object sender, System.EventArgs e)
        {
            int i, j;
            
            for (i = 0; i < posicion-1; i++){
                leer_escribir = tcpclientes[i].GetStream();

                if (leer_escribir.DataAvailable == true)
                {
                    bytes = new byte[cliente.ReceiveBufferSize];
                    leer_escribir.Read(bytes, 0, bytes.Length);

                    if (Encoding.ASCII.GetString(bytes).Equals("hola"))
                        cadena = "El mensaje se ha recibido sin errores\n";
                    else
                        cadena = Encoding.ASCII.GetString(bytes) + "\n";
                    for (j = 0; j < posicion - 1; j++)
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
