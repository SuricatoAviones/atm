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
            button1 = new Button();
            textBoxMessage = new TextBox();
            labelStatus = new Label();
            listBoxLogs = new ListBox();
            buttonSend = new Button();
            buttonDisconnect = new Button();
            buttonAprobado = new Button();
            buttonRechazado = new Button();
            checkBox1 = new CheckBox();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(683, 12);
            button1.Name = "button1";
            button1.Size = new Size(105, 37);
            button1.TabIndex = 0;
            button1.Text = "Conectar";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // textBoxMessage
            // 
            textBoxMessage.Location = new Point(572, 89);
            textBoxMessage.Name = "textBoxMessage";
            textBoxMessage.PlaceholderText = "Escribe tu mensaje aquí...";
            textBoxMessage.Size = new Size(216, 23);
            textBoxMessage.TabIndex = 1;
            textBoxMessage.Visible = false;
            // 
            // labelStatus
            // 
            labelStatus.AutoSize = true;
            labelStatus.ForeColor = Color.Red;
            labelStatus.Location = new Point(12, 415);
            labelStatus.Name = "labelStatus";
            labelStatus.Size = new Size(123, 15);
            labelStatus.TabIndex = 2;
            labelStatus.Text = "Estado: Desconectado";
            labelStatus.Click += labelStatus_Click;
            // 
            // listBoxLogs
            // 
            listBoxLogs.FormattingEnabled = true;
            listBoxLogs.ItemHeight = 15;
            listBoxLogs.Location = new Point(12, 12);
            listBoxLogs.Name = "listBoxLogs";
            listBoxLogs.Size = new Size(554, 394);
            listBoxLogs.TabIndex = 3;
            // 
            // buttonSend
            // 
            buttonSend.Location = new Point(698, 118);
            buttonSend.Name = "buttonSend";
            buttonSend.Size = new Size(90, 27);
            buttonSend.TabIndex = 4;
            buttonSend.Text = "Enviar";
            buttonSend.UseVisualStyleBackColor = true;
            buttonSend.Visible = false;
            buttonSend.Click += buttonSend_Click;
            // 
            // buttonDisconnect
            // 
            buttonDisconnect.Location = new Point(572, 12);
            buttonDisconnect.Name = "buttonDisconnect";
            buttonDisconnect.Size = new Size(105, 37);
            buttonDisconnect.TabIndex = 5;
            buttonDisconnect.Text = "Desconectar";
            buttonDisconnect.UseVisualStyleBackColor = true;
            buttonDisconnect.Click += buttonDisconnect_Click;
            // 
            // buttonAprobado
            // 
            buttonAprobado.Location = new Point(572, 151);
            buttonAprobado.Name = "buttonAprobado";
            buttonAprobado.Size = new Size(105, 37);
            buttonAprobado.TabIndex = 6;
            buttonAprobado.Text = "Aprobado";
            buttonAprobado.UseVisualStyleBackColor = true;
            buttonAprobado.Visible = false;
            buttonAprobado.Click += buttonAprobado_Click;
            // 
            // buttonRechazado
            // 
            buttonRechazado.Location = new Point(683, 151);
            buttonRechazado.Name = "buttonRechazado";
            buttonRechazado.Size = new Size(105, 37);
            buttonRechazado.TabIndex = 7;
            buttonRechazado.Text = "Rechazado";
            buttonRechazado.UseVisualStyleBackColor = true;
            buttonRechazado.Visible = false;
            buttonRechazado.Click += buttonRechazado_Click;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(578, 214);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(82, 19);
            checkBox1.TabIndex = 8;
            checkBox1.Text = "checkBox1";
            checkBox1.UseVisualStyleBackColor = true;
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(checkBox1);
            Controls.Add(buttonRechazado);
            Controls.Add(buttonAprobado);
            Controls.Add(buttonDisconnect);
            Controls.Add(buttonSend);
            Controls.Add(listBoxLogs);
            Controls.Add(labelStatus);
            Controls.Add(textBoxMessage);
            Controls.Add(button1);
            Name = "Form1";
            Text = "Cliente TCP";
            ResumeLayout(false);
            PerformLayout();
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
        private CheckBox checkBox1;
    }
}

