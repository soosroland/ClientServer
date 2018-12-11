using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SecondClient
{
    class Program
    {
        static NetworkStream serverStream;
        private static int bufferSize = 2048;
        private static byte[] buffer = new byte[bufferSize];
        private static List<Socket> clientSockets = new List<Socket>();
        static Socket serverSocket1 = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        static Socket serverSocket2 = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private static TcpClient tcpClient;
        static void Main(string[] args)
        {
            Console.WriteLine("Setting Up Server Plz Wait");
            serverSocket1.Bind(new IPEndPoint(IPAddress.Any, 8890));
            serverSocket1.Listen(10);
            serverSocket1.BeginAccept(new AsyncCallback(CallBack), null);
            serverSocket2.Bind(new IPEndPoint(IPAddress.Any, 8889));
            serverSocket2.Listen(10);
            serverSocket2.BeginAccept(new AsyncCallback(CallBack2), null);
            Console.WriteLine("Server Made");

            Console.ReadKey();
            var tcpClient = new TcpClient("192.168.0.248", 8888); // Emulator server address
            //var tcpClient = new TcpClient("192.168.3.102", 8888); // Emulator server address
            //var tcpClient = new TcpClient("10.0.2.2", 8888); // Emulator server address
            serverStream = tcpClient.GetStream();

            byte[] outStream = System.Text.Encoding.ASCII.GetBytes("Matchmaking;temp[1];temp[2];2;temp[4];temp[5]");
            serverStream.Write(outStream, 0, outStream.Length);
            serverStream.Flush();
            Console.ReadKey();

            outStream = System.Text.Encoding.ASCII.GetBytes("Up button Request");
            serverStream.Write(outStream, 0, outStream.Length);
            serverStream.Flush();
            Console.ReadKey();
        }


        static void CallBack(IAsyncResult e)
        {
            try
            {
                Socket socket = serverSocket1.EndAccept(e);
                clientSockets.Add(socket);
                System.Diagnostics.Debug.WriteLine("Client connected");
                socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallBack), socket);
                serverSocket1.BeginAccept(new AsyncCallback(CallBack), null);

            }
            catch (Exception ex) { System.Diagnostics.Debug.WriteLine(ex + " Errooooor"); }
        }

        private static void ReceiveCallBack(IAsyncResult e)
        {
            try
            {
                Socket socket = (Socket)e.AsyncState;
                int received;
                try
                {
                    received = socket.EndReceive(e);
                }
                catch (SocketException)
                {
                    System.Diagnostics.Debug.WriteLine("Server forcefully disconnected");
                    socket.Close();
                    clientSockets.Remove(socket);
                    return;
                }
                byte[] dataBuf = new byte[received];
                Array.Copy(buffer, dataBuf, received);
                String text = System.Text.Encoding.ASCII.GetString(dataBuf);
                Console.WriteLine("Server request: Multi: " + text);
                socket.BeginReceive(buffer, 0, bufferSize, SocketFlags.None, ReceiveCallBack, socket);
            }
            catch (Exception ex) { String s = ex.Message; }
        }


        static void CallBack2(IAsyncResult e)
        {
            try
            {
                Socket socket = serverSocket1.EndAccept(e);
                clientSockets.Add(socket);
                System.Diagnostics.Debug.WriteLine("Client connected");
                socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallBack), socket);
                serverSocket1.BeginAccept(new AsyncCallback(CallBack), null);

            }
            catch (Exception ex) { System.Diagnostics.Debug.WriteLine(ex + " Errooooor"); }
        }

        private static void ReceiveCallBack2(IAsyncResult e)
        {
            try
            {
                Socket socket = (Socket)e.AsyncState;
                int received;
                try
                {
                    received = socket.EndReceive(e);
                }
                catch (SocketException)
                {
                    Console.WriteLine("Server forcefully disconnected");
                    socket.Close();
                    clientSockets.Remove(socket);
                    return;
                }
                byte[] dataBuf = new byte[received];
                Array.Copy(buffer, dataBuf, received);

                string text = System.Text.Encoding.ASCII.GetString(dataBuf);
                HandleIncomingEvent(text, socket.RemoteEndPoint.ToString().Split(':')[0]);
                Console.WriteLine("Server request: " + text);
                socket.BeginReceive(buffer, 0, bufferSize, SocketFlags.None, ReceiveCallBack2, socket);
            }
            catch (Exception ex) { Console.WriteLine(ex); }
        }

        protected static void HandleIncomingEvent(String text, String ip)
        {
            String[] temp = text.Split(';');
            switch (temp[0])
            {
                case "StartGame":
                    Console.WriteLine("START");
                    break;
                default:
                    break;
            }
        }
    }
}
