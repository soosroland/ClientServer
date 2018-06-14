using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientApplication
{
    public partial class Form1 : Form
    {
        System.Net.Sockets.TcpClient clientSocket = new System.Net.Sockets.TcpClient();
        
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            msg("Client Started");
            clientSocket.ReceiveBufferSize = 1024;
            clientSocket.Connect("127.0.0.1", 8888);
            label1.Text = "Client Socket Program - Server Connected ...";
        }

        public void msg(string mesg)
        {
            textBox1.Text = textBox1.Text + Environment.NewLine + " >> " + mesg;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            /*if (clientSocket.GetStream().CanRead)
            {
                byte[] myReadBuffer = new byte[1024];
                StringBuilder myCompleteMessage = new StringBuilder();
                int numberOfBytesRead = 0;

                // Incoming message may be larger than the buffer size.
                do
                {
                    numberOfBytesRead = clientSocket.GetStream().Read(myReadBuffer, 0, myReadBuffer.Length);

                    myCompleteMessage.AppendFormat("{0}", Encoding.ASCII.GetString(myReadBuffer, 0, numberOfBytesRead));

                }
                while (clientSocket.GetStream().DataAvailable);

                // Print out the received message to the console.
                Console.WriteLine("You received the following message : " +
                                             myCompleteMessage);
            }
            else
            {
                Console.WriteLine("Sorry.  You cannot read from this NetworkStream.");
            }*/
            
            NetworkStream serverStream = clientSocket.GetStream();
            byte[] outStream = System.Text.Encoding.ASCII.GetBytes(textBox2.Text);// + "$");
            serverStream.Write(outStream, 0, outStream.Length);
            serverStream.Flush();

            byte[] inStream = new byte[10025];
            serverStream.Read(inStream, 0, (int)clientSocket.ReceiveBufferSize);
            string returndata = System.Text.Encoding.ASCII.GetString(inStream);
            msg(returndata);
            textBox2.Text = "";
            textBox2.Focus();
        }
    }
}
