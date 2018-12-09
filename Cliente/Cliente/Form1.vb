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

                Dim array_size As Integer = 0

                array_size = leer_escribir.Read(bytes, 0, bytes.Length)
                Array.Resize(bytes, array_size)
                Dim r As String = Encoding.Default.GetString(bytes)

                'leer_escribir.Read(bytes, 0, bytes.Length)
                'cadena = Encoding.ASCII.GetString(bytes, 0, bytes.Length)

                cadena = r

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

            cadena = "usql "
            cadena &= txtenviar.Text
            bytes = Encoding.ASCII.GetBytes(cadena)
            leer_escribir.Write(bytes, 0, bytes.Length)

            cadena = ""
            bytes = Nothing

        End If
    End Sub

    Private Sub btn_reporte_Click(sender As Object, e As EventArgs) Handles btn_reporte.Click
        If rtb_reporte.Text <> "" Then

            cadena = "report "
            cadena &= rtb_reporte.Text
            bytes = Encoding.ASCII.GetBytes(cadena)
            leer_escribir.Write(bytes, 0, bytes.Length)

            cadena = ""
            bytes = Nothing

        End If
    End Sub

    Private Sub btn_login_Click(sender As Object, e As EventArgs) Handles btn_login.Click
        If rtb_login.Text <> "" Then

            cadena = "login "
            cadena &= rtb_login.Text
            bytes = Encoding.ASCII.GetBytes(cadena)
            leer_escribir.Write(bytes, 0, bytes.Length)

            cadena = ""
            bytes = Nothing

        End If
    End Sub
End Class
