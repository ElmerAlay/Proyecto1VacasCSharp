<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.conversacion = New System.Windows.Forms.RichTextBox()
        Me.secuencia_lecturas = New System.Windows.Forms.Timer(Me.components)
        Me.usuarios = New System.Windows.Forms.RichTextBox()
        Me.btnenviar = New System.Windows.Forms.Button()
        Me.txtenviar = New System.Windows.Forms.RichTextBox()
        Me.conectar = New System.Windows.Forms.CheckBox()
        Me.rtb_reporte = New System.Windows.Forms.RichTextBox()
        Me.rtb_login = New System.Windows.Forms.RichTextBox()
        Me.btn_reporte = New System.Windows.Forms.Button()
        Me.btn_login = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'conversacion
        '
        Me.conversacion.Location = New System.Drawing.Point(13, 39)
        Me.conversacion.Name = "conversacion"
        Me.conversacion.Size = New System.Drawing.Size(257, 173)
        Me.conversacion.TabIndex = 0
        Me.conversacion.Text = ""
        '
        'secuencia_lecturas
        '
        '
        'usuarios
        '
        Me.usuarios.Location = New System.Drawing.Point(276, 39)
        Me.usuarios.Name = "usuarios"
        Me.usuarios.Size = New System.Drawing.Size(256, 173)
        Me.usuarios.TabIndex = 1
        Me.usuarios.Text = ""
        '
        'btnenviar
        '
        Me.btnenviar.Location = New System.Drawing.Point(457, 218)
        Me.btnenviar.Name = "btnenviar"
        Me.btnenviar.Size = New System.Drawing.Size(75, 23)
        Me.btnenviar.TabIndex = 2
        Me.btnenviar.Text = "Enviar"
        Me.btnenviar.UseVisualStyleBackColor = True
        '
        'txtenviar
        '
        Me.txtenviar.Location = New System.Drawing.Point(12, 218)
        Me.txtenviar.Name = "txtenviar"
        Me.txtenviar.Size = New System.Drawing.Size(439, 54)
        Me.txtenviar.TabIndex = 3
        Me.txtenviar.Text = ""
        '
        'conectar
        '
        Me.conectar.AutoSize = True
        Me.conectar.Location = New System.Drawing.Point(12, 12)
        Me.conectar.Name = "conectar"
        Me.conectar.Size = New System.Drawing.Size(95, 21)
        Me.conectar.TabIndex = 4
        Me.conectar.Text = "Conectar?"
        Me.conectar.UseVisualStyleBackColor = True
        '
        'rtb_reporte
        '
        Me.rtb_reporte.Location = New System.Drawing.Point(13, 279)
        Me.rtb_reporte.Name = "rtb_reporte"
        Me.rtb_reporte.Size = New System.Drawing.Size(438, 84)
        Me.rtb_reporte.TabIndex = 5
        Me.rtb_reporte.Text = ""
        '
        'rtb_login
        '
        Me.rtb_login.Location = New System.Drawing.Point(13, 369)
        Me.rtb_login.Name = "rtb_login"
        Me.rtb_login.Size = New System.Drawing.Size(438, 70)
        Me.rtb_login.TabIndex = 6
        Me.rtb_login.Text = ""
        '
        'btn_reporte
        '
        Me.btn_reporte.Location = New System.Drawing.Point(457, 339)
        Me.btn_reporte.Name = "btn_reporte"
        Me.btn_reporte.Size = New System.Drawing.Size(75, 23)
        Me.btn_reporte.TabIndex = 7
        Me.btn_reporte.Text = "Reporte"
        Me.btn_reporte.UseVisualStyleBackColor = True
        '
        'btn_login
        '
        Me.btn_login.Location = New System.Drawing.Point(458, 415)
        Me.btn_login.Name = "btn_login"
        Me.btn_login.Size = New System.Drawing.Size(75, 23)
        Me.btn_login.TabIndex = 8
        Me.btn_login.Text = "Login"
        Me.btn_login.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(542, 451)
        Me.Controls.Add(Me.btn_login)
        Me.Controls.Add(Me.btn_reporte)
        Me.Controls.Add(Me.rtb_login)
        Me.Controls.Add(Me.rtb_reporte)
        Me.Controls.Add(Me.conectar)
        Me.Controls.Add(Me.txtenviar)
        Me.Controls.Add(Me.btnenviar)
        Me.Controls.Add(Me.usuarios)
        Me.Controls.Add(Me.conversacion)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents conversacion As System.Windows.Forms.RichTextBox
    Friend WithEvents secuencia_lecturas As System.Windows.Forms.Timer
    Friend WithEvents usuarios As System.Windows.Forms.RichTextBox
    Friend WithEvents btnenviar As System.Windows.Forms.Button
    Friend WithEvents txtenviar As System.Windows.Forms.RichTextBox
    Friend WithEvents conectar As System.Windows.Forms.CheckBox
    Friend WithEvents rtb_reporte As System.Windows.Forms.RichTextBox
    Friend WithEvents rtb_login As System.Windows.Forms.RichTextBox
    Friend WithEvents btn_reporte As System.Windows.Forms.Button
    Friend WithEvents btn_login As System.Windows.Forms.Button

End Class
