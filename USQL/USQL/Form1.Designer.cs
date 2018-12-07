namespace USQL
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.rtb_entrada = new System.Windows.Forms.RichTextBox();
            this.rtb_consola = new System.Windows.Forms.RichTextBox();
            this.btn_analizar = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.escucha = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // rtb_entrada
            // 
            this.rtb_entrada.Location = new System.Drawing.Point(13, 13);
            this.rtb_entrada.Name = "rtb_entrada";
            this.rtb_entrada.Size = new System.Drawing.Size(257, 96);
            this.rtb_entrada.TabIndex = 0;
            this.rtb_entrada.Text = "";
            // 
            // rtb_consola
            // 
            this.rtb_consola.Location = new System.Drawing.Point(13, 145);
            this.rtb_consola.Name = "rtb_consola";
            this.rtb_consola.Size = new System.Drawing.Size(257, 96);
            this.rtb_consola.TabIndex = 1;
            this.rtb_consola.Text = "";
            // 
            // btn_analizar
            // 
            this.btn_analizar.Location = new System.Drawing.Point(13, 116);
            this.btn_analizar.Name = "btn_analizar";
            this.btn_analizar.Size = new System.Drawing.Size(257, 23);
            this.btn_analizar.TabIndex = 2;
            this.btn_analizar.Text = "analizar";
            this.btn_analizar.UseVisualStyleBackColor = true;
            this.btn_analizar.Click += new System.EventHandler(this.btn_analizar_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // escucha
            // 
            this.escucha.AutoSize = true;
            this.escucha.Location = new System.Drawing.Point(277, 13);
            this.escucha.Name = "escucha";
            this.escucha.Size = new System.Drawing.Size(89, 21);
            this.escucha.TabIndex = 3;
            this.escucha.Text = "Escuchar";
            this.escucha.UseVisualStyleBackColor = true;
            this.escucha.CheckedChanged += new System.EventHandler(this.escucha_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(362, 253);
            this.Controls.Add(this.escucha);
            this.Controls.Add(this.btn_analizar);
            this.Controls.Add(this.rtb_consola);
            this.Controls.Add(this.rtb_entrada);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtb_entrada;
        private System.Windows.Forms.RichTextBox rtb_consola;
        private System.Windows.Forms.Button btn_analizar;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.CheckBox escucha;
    }
}

