using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace atm_v2
{
    public partial class Form1 : Form
    {
        private TcpClient client; // Cliente TCP para mantener la conexión
        private NetworkStream stream; // Flujo para enviar/recibir datos
        private Thread receiveThread; // Hilo para recibir mensajes del servidor
        private bool autoAprobado = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string serverIP = "192.168.1.107"; // Cambiar según sea necesario
            int port = 1234; // Cambiar según sea necesario

            try
            {
                AddLog("Intentando conectar al servidor...");
                client = new TcpClient(serverIP, port);
                stream = client.GetStream();

                labelStatus.Text = "Estado: Conectado";
                labelStatus.ForeColor = System.Drawing.Color.Green;

                AddLog("Conexión establecida con el servidor.");

                // Iniciar el hilo para recibir mensajes del servidor
                receiveThread = new Thread(ReceiveMessages);
                receiveThread.IsBackground = true; // Hilo en segundo plano
                receiveThread.Start();

                // Mostrar controles relacionados con el envío de mensajes
                textBoxMessage.Visible = true;
                buttonSend.Visible = true;
                buttonDisconnect.Visible = true; // Mostrar el botón de desconexión
                buttonAprobado.Visible = true; // Mostrar el botón de Aprobado
                buttonRechazado.Visible = true; // Mostrar el botón de Rechazado
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

                    // Leer los primeros 2 bytes (longitud del mensaje)
                    int bytesRead = await LeerTotalAsync(stream, lengthBuffer, 2);
                    if (bytesRead < 2)
                    {
                        if (!this.IsDisposed)
                        {
                            Invoke((MethodInvoker)delegate
                            {
                                AddLog("Error: No se recibieron los bytes de longitud completos. Bytes leídos: " + bytesRead);
                            });
                        }
                        break;
                    }

                    // Mostrar los bytes de longitud recibidos
                    byte[] receivedBytes = lengthBuffer.ToArray();
                    Array.Reverse(receivedBytes); // Convertir de Little-Endian a Big-Endian
                    short messageLength = BitConverter.ToInt16(receivedBytes, 0);

                    // Log de los bytes recibidos (longitud)
                    if (!this.IsDisposed)
                    {
                        Invoke((MethodInvoker)delegate
                        {
                            AddLog($"Bytes recibidos (longitud): [{string.Join(", ", receivedBytes)}]");
                            AddLog($"Longitud del mensaje esperada: {messageLength}");
                        });
                    }

                    // Leer el mensaje completo basado en la longitud recibida
                    byte[] buffer = new byte[messageLength];
                    bytesRead = await LeerTotalAsync(stream, buffer, messageLength);
                    if (bytesRead < messageLength)
                    {
                        if (!this.IsDisposed)
                        {
                            Invoke((MethodInvoker)delegate
                            {
                                AddLog($"Error: No se recibió el mensaje completo. Bytes esperados: {messageLength}, Bytes recibidos: {bytesRead}");
                            });
                        }
                        break;
                    }

                    // Log de los bytes recibidos (mensaje)
                    if (!this.IsDisposed)
                    {
                        Invoke((MethodInvoker)delegate
                        {
                            AddLog($"Bytes recibidos (mensaje): [{string.Join(", ", buffer)}]");
                        });
                    }

                    // Convertir mensaje recibido en string
                    string receivedMessage = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                    // Mostrar mensaje en el log
                    if (!this.IsDisposed)
                    {
                        Invoke((MethodInvoker)delegate
                        {
                            AddLog($"Mensaje recibido: {receivedMessage}");

                            // Si el CheckBox está activado, responder automáticamente con Aprobado
                            if (autoAprobado)
                            {
                                EnviarMensajeAprobado();
                            }
                        });
                    }
                }
                catch (Exception ex)
                {
                    if (!this.IsDisposed)
                    {
                        Invoke((MethodInvoker)delegate
                        {
                            AddLog("Error en la recepción del mensaje: " + ex.Message);
                        });
                    }
                    break;
                }
            }
        }

        // Función auxiliar para leer el mensaje completo asegurando que todos los bytes sean recibidos
        async Task<int> LeerTotalAsync(NetworkStream stream, byte[] buffer, int length)
        {
            int totalRead = 0;
            while (totalRead < length)
            {
                int read = await stream.ReadAsync(buffer, totalRead, length - totalRead);
                if (read == 0) break; // La conexión se cerró
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
            // Construir el mensaje de aprobado con el carácter ASCII 28
            char ascii28 = (char)28;
            string message = $"{ascii28}22{ascii28}099{ascii28}X{ascii28}99";

            EnviarMensaje(message);
        }

        private void buttonRechazado_Click(object sender, EventArgs e)
        {
            // Construir el mensaje de rechazado con el carácter ASCII 28
            char ascii28 = (char)28;
            string message = $"{ascii28}22{ascii28}099{ascii28}X{ascii28}A";

            EnviarMensaje(message);
        }

        private async void EnviarMensaje(string mensaje)
        {
            if (client == null || !client.Connected)
            {
                AddLog("No estás conectado al servidor.");
                return;
            }

            try
            {
                // Convertir la longitud del mensaje a 2 bytes en formato Little-Endian
                byte[] lengthBytes = BitConverter.GetBytes((short)mensaje.Length);
                if (BitConverter.IsLittleEndian)
                {
                    Array.Reverse(lengthBytes); // Asegurar que se envíe en Little-Endian
                }

                // Convertir el mensaje a bytes UTF-8
                byte[] mensajeBytes = Encoding.UTF8.GetBytes(mensaje);

                // Crear el paquete final (longitud + mensaje)
                byte[] finalMessage = new byte[lengthBytes.Length + mensajeBytes.Length];
                Array.Copy(lengthBytes, 0, finalMessage, 0, lengthBytes.Length);
                Array.Copy(mensajeBytes, 0, finalMessage, lengthBytes.Length, mensajeBytes.Length);

                // Log de los bytes enviados
                AddLog($"Bytes enviados (longitud): [{string.Join(", ", lengthBytes)}]");
                AddLog($"Bytes enviados (mensaje): [{string.Join(", ", mensajeBytes)}]");

                // Enviar el mensaje completo al cajero
                await stream.WriteAsync(finalMessage, 0, finalMessage.Length);

                AddLog($"Mensaje enviado: {mensaje}");
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

                    // Ocultar controles relacionados con el envío de mensajes
                    textBoxMessage.Visible = false;
                    buttonSend.Visible = false;
                    buttonDisconnect.Visible = false; // Ocultar el botón de desconexión
                    buttonAprobado.Visible = false; // Ocultar el botón de Aprobado
                    buttonRechazado.Visible = false; // Ocultar el botón de Rechazado
                }
            }
            catch (Exception ex)
            {
                AddLog("Error al desconectar: " + ex.Message);
            }
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
                listBoxLogs.TopIndex = listBoxLogs.Items.Count - 1; // Scroll automático
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

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            autoAprobado = checkBox1.Checked;
            AddLog($"Auto-respuesta de aprobación: {(autoAprobado ? "Activada" : "Desactivada")}");
        }

        private void EnviarMensajeAprobado()
        {
            char ascii28 = (char)28;
            string message = $"{ascii28}22{ascii28}099{ascii28}X{ascii28}99";
            EnviarMensaje(message);
            AddLog("Respuesta automática: Aprobado enviado.");
        }
    }
}

