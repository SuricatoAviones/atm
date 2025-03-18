namespace atm_v2
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.textBoxMessage = new System.Windows.Forms.TextBox();
            this.labelStatus = new System.Windows.Forms.Label();
            this.listBoxLogs = new System.Windows.Forms.ListBox();
            this.buttonSend = new System.Windows.Forms.Button();
            this.buttonDisconnect = new System.Windows.Forms.Button();
            this.buttonAprobado = new System.Windows.Forms.Button();
            this.buttonRechazado = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(683, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(105, 37);
            this.button1.TabIndex = 0;
            this.button1.Text = "Conectar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBoxMessage
            // 
            this.textBoxMessage.Location = new System.Drawing.Point(572, 89);
            this.textBoxMessage.Name = "textBoxMessage";
            this.textBoxMessage.PlaceholderText = "Escribe tu mensaje aquí...";
            this.textBoxMessage.Size = new System.Drawing.Size(216, 23);
            this.textBoxMessage.TabIndex = 1;
            this.textBoxMessage.Visible = false;
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.ForeColor = System.Drawing.Color.Red;
            this.labelStatus.Location = new System.Drawing.Point(12, 415);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(123, 15);
            this.labelStatus.TabIndex = 2;
            this.labelStatus.Text = "Estado: Desconectado";
            this.labelStatus.Click += new System.EventHandler(this.labelStatus_Click);
            // 
            // listBoxLogs
            // 
            this.listBoxLogs.FormattingEnabled = true;
            this.listBoxLogs.ItemHeight = 15;
            this.listBoxLogs.Location = new System.Drawing.Point(12, 12);
            this.listBoxLogs.Name = "listBoxLogs";
            this.listBoxLogs.Size = new System.Drawing.Size(554, 394);
            this.listBoxLogs.TabIndex = 3;
            // 
            // buttonSend
            // 
            this.buttonSend.Location = new System.Drawing.Point(698, 118);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(90, 27);
            this.buttonSend.TabIndex = 4;
            this.buttonSend.Text = "Enviar";
            this.buttonSend.UseVisualStyleBackColor = true;
            this.buttonSend.Visible = false;
            this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
            // 
            // buttonDisconnect
            // 
            this.buttonDisconnect.Location = new System.Drawing.Point(572, 12);
            this.buttonDisconnect.Name = "buttonDisconnect";
            this.buttonDisconnect.Size = new System.Drawing.Size(105, 37);
            this.buttonDisconnect.TabIndex = 5;
            this.buttonDisconnect.Text = "Desconectar";
            this.buttonDisconnect.UseVisualStyleBackColor = true;
            this.buttonDisconnect.Click += new System.EventHandler(this.buttonDisconnect_Click);
            // 
            // buttonAprobado
            // 
            this.buttonAprobado.Location = new System.Drawing.Point(572, 151);
            this.buttonAprobado.Name = "buttonAprobado";
            this.buttonAprobado.Size = new System.Drawing.Size(105, 37);
            this.buttonAprobado.TabIndex = 6;
            this.buttonAprobado.Text = "Aprobado";
            this.buttonAprobado.UseVisualStyleBackColor = true;
            this.buttonAprobado.Visible = false;
            this.buttonAprobado.Click += new System.EventHandler(this.buttonAprobado_Click);
            // 
            // buttonRechazado
            // 
            this.buttonRechazado.Location = new System.Drawing.Point(683, 151);
            this.buttonRechazado.Name = "buttonRechazado";
            this.buttonRechazado.Size = new System.Drawing.Size(105, 37);
            this.buttonRechazado.TabIndex = 7;
            this.buttonRechazado.Text = "Rechazado";
            this.buttonRechazado.UseVisualStyleBackColor = true;
            this.buttonRechazado.Visible = false;
            this.buttonRechazado.Click += new System.EventHandler(this.buttonRechazado_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.buttonRechazado);
            this.Controls.Add(this.buttonAprobado);
            this.Controls.Add(this.buttonDisconnect);
            this.Controls.Add(this.buttonSend);
            this.Controls.Add(this.listBoxLogs);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.textBoxMessage);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Cliente TCP";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBoxMessage;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.ListBox listBoxLogs;
        private System.Windows.Forms.Button buttonSend;
        private System.Windows.Forms.Button buttonDisconnect;
        private System.Windows.Forms.Button buttonAprobado;
        private System.Windows.Forms.Button buttonRechazado;
    }
}

