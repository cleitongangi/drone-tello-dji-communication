namespace DroneTelloCommunication
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.btnConectar = new System.Windows.Forms.Button();
            this.txtComando = new System.Windows.Forms.TextBox();
            this.txtResultado = new System.Windows.Forms.TextBox();
            this.btnImportarArquivo = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(165, 39);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(124, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Send Command";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnConectar
            // 
            this.btnConectar.Location = new System.Drawing.Point(12, 12);
            this.btnConectar.Name = "btnConectar";
            this.btnConectar.Size = new System.Drawing.Size(75, 23);
            this.btnConectar.TabIndex = 1;
            this.btnConectar.Text = "Conect";
            this.btnConectar.UseVisualStyleBackColor = true;
            this.btnConectar.Click += new System.EventHandler(this.btnConectar_Click);
            // 
            // txtComando
            // 
            this.txtComando.Location = new System.Drawing.Point(12, 41);
            this.txtComando.Name = "txtComando";
            this.txtComando.Size = new System.Drawing.Size(147, 20);
            this.txtComando.TabIndex = 2;
            // 
            // txtResultado
            // 
            this.txtResultado.Location = new System.Drawing.Point(12, 97);
            this.txtResultado.Multiline = true;
            this.txtResultado.Name = "txtResultado";
            this.txtResultado.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtResultado.Size = new System.Drawing.Size(277, 438);
            this.txtResultado.TabIndex = 3;
            // 
            // btnImportarArquivo
            // 
            this.btnImportarArquivo.Location = new System.Drawing.Point(12, 68);
            this.btnImportarArquivo.Name = "btnImportarArquivo";
            this.btnImportarArquivo.Size = new System.Drawing.Size(196, 23);
            this.btnImportarArquivo.TabIndex = 4;
            this.btnImportarArquivo.Text = "Process commands from file";
            this.btnImportarArquivo.UseVisualStyleBackColor = true;
            this.btnImportarArquivo.Click += new System.EventHandler(this.btnImportarArquivo_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(301, 547);
            this.Controls.Add(this.btnImportarArquivo);
            this.Controls.Add(this.txtResultado);
            this.Controls.Add(this.txtComando);
            this.Controls.Add(this.btnConectar);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "DJI Tello Communication";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnConectar;
        private System.Windows.Forms.TextBox txtComando;
        private System.Windows.Forms.TextBox txtResultado;
        private System.Windows.Forms.Button btnImportarArquivo;
    }
}

