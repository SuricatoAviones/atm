using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace atm_v2
{
    public partial class Form1 : Form
    {
        private TcpClient client; // Cliente TCP para mantener la conexi�n
        private NetworkStream stream; // Flujo para enviar/recibir datos
        private Thread receiveThread; // Hilo para recibir mensajes del servidor

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string serverIP = "192.168.101.2"; // Cambiar seg�n sea necesario
            int port = 1234; // Cambiar seg�n sea necesario

            try
            {
                AddLog("Intentando conectar al servidor...");
                client = new TcpClient(serverIP, port);
                stream = client.GetStream();

                labelStatus.Text = "Estado: Conectado";
                labelStatus.ForeColor = System.Drawing.Color.Green;

                AddLog("Conexi�n establecida con el servidor.");

                // Iniciar el hilo para recibir mensajes del servidor
                receiveThread = new Thread(ReceiveMessages);
                receiveThread.IsBackground = true; // Hilo en segundo plano
                receiveThread.Start();

                // Mostrar controles relacionados con el env�o de mensajes
                textBoxMessage.Visible = true;
                buttonSend.Visible = true;
                buttonDisconnect.Visible = true; // Mostrar el bot�n de desconexi�n
                buttonAprobado.Visible = true; // Mostrar el bot�n de Aprobado
                buttonRechazado.Visible = true; // Mostrar el bot�n de Rechazado
            }
            catch (Exception ex)
            {
                labelStatus.Text = "Estado: Desconectado";
                labelStatus.ForeColor = System.Drawing.Color.Red;

                AddLog("Error al conectar con el servidor: " + ex.Message);
                MessageBox.Show($"Error al conectar con el servidor: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void ReceiveMessages()
        {
            while (client.Connected)
            {
                try
                {
                    byte[] lengthBuffer = new byte[2];

                    // Leer los primeros 2 bytes para obtener la longitud del mensaje
                    int bytesRead = await stream.ReadAsync(lengthBuffer, 0, 2);
                    if (bytesRead < 2)
                    {
                        Invoke((MethodInvoker)delegate
                        {
                            AddLog("Error: No se recibieron los bytes de longitud completos.");
                        });
                        break;
                    }

                    // Asegurar que los bytes de longitud est�n en formato Little-Endian
                    if (BitConverter.IsLittleEndian)
                    {
                        Array.Reverse(lengthBuffer);
                    }

                    // Convertir los primeros 2 bytes a un n�mero entero (longitud esperada)
                    short messageLength = BitConverter.ToInt16(lengthBuffer, 0);

                    // Crear un buffer del tama�o adecuado para el mensaje
                    byte[] buffer = new byte[messageLength];

                    // Leer el mensaje completo basado en la longitud recibida
                    bytesRead = await LeerTotalAsync(stream, buffer, messageLength);
                    if (bytesRead < messageLength)
                    {
                        Invoke((MethodInvoker)delegate
                        {
                            AddLog("Error: No se recibi� el mensaje completo.");
                        });
                        break;
                    }

                    // Convertir el mensaje de bytes a string UTF-8
                    string receivedMessage = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                    // Mostrar el mensaje recibido en el log
                    Invoke((MethodInvoker)delegate
                    {
                        AddLog($"Mensaje recibido: {receivedMessage}");
                    });
                }
                catch (Exception ex)
                {
                    if (!this.IsDisposed)
                    {
                        Invoke((MethodInvoker)delegate
                        {
                            AddLog("Error: No se recibieron los bytes de longitud completos.");
                        });
                    }

                    break;
                }
            }
        }

        // Funci�n auxiliar para leer el mensaje completo asegurando que todos los bytes sean recibidos
        async Task<int> LeerTotalAsync(NetworkStream stream, byte[] buffer, int length)
        {
            int totalRead = 0;
            while (totalRead < length)
            {
                int read = await stream.ReadAsync(buffer, totalRead, length - totalRead);
                if (read == 0) break; // La conexi�n se cerr�
                totalRead += read;
            }
            return totalRead;
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            EnviarMensaje(textBoxMessage.Text);
        }

        private void buttonAprobado_Click(object sender, EventArgs e)
        {
            // Construir el mensaje de aprobado con el car�cter ASCII 28
            char ascii28 = (char)28;
            string message = $"{ascii28}22{ascii28}099{ascii28}X{ascii28}99";

            EnviarMensaje(message);
        }

        private void buttonRechazado_Click(object sender, EventArgs e)
        {
            // Construir el mensaje de rechazado con el car�cter ASCII 28
            char ascii28 = (char)28;
            string message = $"{ascii28}22{ascii28}099{ascii28}X{ascii28}A";

            EnviarMensaje(message);;
        }

        private void EnviarMensaje(string messageToSend)
        {
            if (client == null || !client.Connected)
            {
                AddLog("No est�s conectado al servidor.");
                return;
            }

            try
            {
                // Convertir el n�mero de longitud a 2 bytes en formato Little-Endian
                byte[] lengthBytes = BitConverter.GetBytes((short)messageToSend.Length);

                // Convertir el mensaje a bytes UTF-8
                byte[] messageBytes = Encoding.UTF8.GetBytes(messageToSend);

                // Crear el paquete final (longitud + mensaje)
                byte[] finalMessage = new byte[lengthBytes.Length + messageBytes.Length];
                Array.Copy(lengthBytes, 0, finalMessage, 0, lengthBytes.Length);
                Array.Copy(messageBytes, 0, finalMessage, lengthBytes.Length, messageBytes.Length);

                // Enviar al sistema central
                stream.Write(finalMessage, 0, finalMessage.Length);

                AddLog("Mensaje enviado correctamente.");
                textBoxMessage.Clear();
            }
            catch (Exception ex)
            {
                AddLog("Error al enviar el mensaje: " + ex.Message);
            }
        }

        private void buttonDisconnect_Click(object sender, EventArgs e)
        {
            try
            {
                if (client != null && client.Connected)
                {
                    stream.Close();
                    client.Close();

                    labelStatus.Text = "Estado: Desconectado";
                    labelStatus.ForeColor = System.Drawing.Color.Red;

                    AddLog("Desconectado del servidor.");

                    // Ocultar controles relacionados con el env�o de mensajes
                    textBoxMessage.Visible = false;
                    buttonSend.Visible = false;
                    buttonDisconnect.Visible = false; // Ocultar el bot�n de desconexi�n
                    buttonAprobado.Visible = false; // Ocultar el bot�n de Aprobado
                    buttonRechazado.Visible = false; // Ocultar el bot�n de Rechazado
                }
            }
            catch (Exception ex)
            {
                AddLog("Error al desconectar: " + ex.Message);
            }
        }

        // Conversi�n de un n�mero a su array de bits
        private byte[] ConvertNumberToBitArray(short number)
        {
            return BitConverter.GetBytes(number); // Convierte el n�mero a un array de bytes
        }

        private void AddLog(string message)
        {
            if (listBoxLogs.InvokeRequired)
            {
                listBoxLogs.Invoke(new Action<string>(AddLog), message);
            }
            else
            {
                listBoxLogs.Items.Add($"{DateTime.Now:HH:mm:ss} - {message}");
                listBoxLogs.TopIndex = listBoxLogs.Items.Count - 1; // Scroll autom�tico
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // Liberar recursos al cerrar el formulario
            stream?.Close();
            client?.Close();
            base.OnFormClosing(e);
        }

        private void labelStatus_Click(object sender, EventArgs e)
        {

        }
    }
}

