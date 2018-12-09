Imports System
Imports System.IO
Imports System.Net
Imports System.Net.Sockets
Imports System.Text
Imports Microsoft.VisualBasic

Public Class Form1
    Public cliente As TcpClient
    Public bytes() As Byte = Nothing
    Public leer_escribir As NetworkStream
    Public cadena As String

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub conectar_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles conectar.CheckedChanged
        cliente = New TcpClient
        Try
            cliente.Connect(IPAddress.Parse("127.0.0.1"), 6000)
            System.Windows.Forms.Application.DoEvents()
        Catch ex As Exception
            conversacion.Text = "IMPOSIBLE CONECTAR CON SERVIDOR"
        End Try

        If cliente.Connected = True Then
            leer_escribir = cliente.GetStream
            secuencia_lecturas.Interval = 1000
            secuencia_lecturas.Start()
        End If

    End Sub

    Private Sub secuencia_lecturas_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles secuencia_lecturas.Tick
        If cliente.Connected = True Then
            If leer_escribir.DataAvailable = True Then
                ReDim bytes(cliente.ReceiveBufferSize)
                'ReDim bytes(10000)
                leer_escribir.Read(bytes, 0, bytes.Length)
                'leer_escribir.Read(bytes, 0, 10000)
                cadena = Encoding.ASCII.GetString(bytes, 0, bytes.Length)

                If cadena(0) = "U" And cadena(1) = "." Then
                    usuarios.Text = cadena
                Else
                    conversacion.Text &= cadena
                    MyParser.Setup()
                    MyParser.Parse(New StringReader(cadena))
                    MsgBox(Datos.datos)
                End If
            End If

            bytes = Nothing
            cadena = ""
        Else
            conversacion.Text &= "PERDIDA LA CONEXION CON SERVIDOR"
            conversacion.SendToBack()
        End If
        
    End Sub

    Private Sub btnenviar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnenviar.Click
        If txtenviar.Text <> "" Then

            cadena = txtenviar.Text
            bytes = Encoding.ASCII.GetBytes(cadena)
            leer_escribir.Write(bytes, 0, bytes.Length)

            cadena = ""
            bytes = Nothing

        End If
    End Sub
End Class
