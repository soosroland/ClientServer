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
        private static List<Socket> clientSockets1 = new List<Socket>();
        private static List<Socket> clientSockets2 = new List<Socket>();
        private static List<Socket> clientSockets3 = new List<Socket>();
        static Socket serverSocket1 = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        static Socket serverSocket2 = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        static Socket serverSocket3 = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        static Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private static List<Socket> clientSockets = new List<Socket>();
        private static TcpClient tcpClient;
        static void Main(string[] args)
        {
            Console.WriteLine("Setting Up Server Plz Wait");
            /*serverSocket1.Bind(new IPEndPoint(IPAddress.Any, 8889));
            serverSocket1.Listen(10);
            serverSocket1.BeginAccept(new AsyncCallback(CallBack), null);*/
            serverSocket2.Bind(new IPEndPoint(IPAddress.Any, 8890));
            serverSocket2.Listen(10);
            serverSocket2.BeginAccept(new AsyncCallback(CallBack2), null);
            serverSocket3.Bind(new IPEndPoint(IPAddress.Any, 8889));
            serverSocket3.Listen(10);
            serverSocket3.BeginAccept(new AsyncCallback(CallBack3), null);
            Console.WriteLine("Server Made");

            System.Diagnostics.Debug.WriteLine("Setting Up Server for 8891 Plz Wait");
            serverSocket.Bind(new IPEndPoint(IPAddress.Any, 8891));
            serverSocket.Listen(10);
            serverSocket.BeginAccept(new AsyncCallback(CallBack), null);
            System.Diagnostics.Debug.WriteLine("Server Made");

            Console.ReadKey();
            var tcpClient = new TcpClient("192.168.0.248", 8888); // Emulator server address
            //var tcpClient = new TcpClient("192.168.3.102", 8888); // Emulator server address
            //var tcpClient = new TcpClient("10.0.2.2", 8888); // Emulator server address
            serverStream = tcpClient.GetStream();

            //byte[] outStream = System.Text.Encoding.ASCII.GetBytes("Matchmaking;Level 1;normal;2;normal;normal");
            byte[] outStream = System.Text.Encoding.ASCII.GetBytes("GoldRushMatchmaking;Level 1");
            serverStream.Write(outStream, 0, outStream.Length);
            serverStream.Flush();
            Console.ReadKey();
            outStream = System.Text.Encoding.ASCII.GetBytes("GLeft button Request");
            serverStream.Write(outStream, 0, outStream.Length);
            serverStream.Flush();
            Console.ReadKey();
            outStream = System.Text.Encoding.ASCII.GetBytes("GLeft button Request");
            serverStream.Write(outStream, 0, outStream.Length);
            serverStream.Flush();
            Console.ReadKey();
            outStream = System.Text.Encoding.ASCII.GetBytes("GLeft button Request");
            serverStream.Write(outStream, 0, outStream.Length);
            serverStream.Flush();
            Console.ReadKey();
            outStream = System.Text.Encoding.ASCII.GetBytes("GRight button Request");
            serverStream.Write(outStream, 0, outStream.Length);
            serverStream.Flush();

            Console.ReadKey();
        }

        static void CallBack(IAsyncResult e)
        {
            try
            {
                Socket socket = serverSocket.EndAccept(e);
                clientSockets.Add(socket);
                System.Diagnostics.Debug.WriteLine("Client connected");
                socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallBack), socket);
                serverSocket.BeginAccept(new AsyncCallback(CallBack), null);

            }
            catch (Exception ex) { System.Diagnostics.Debug.WriteLine(ex + " Errooooor"); }
        }

        static void ReceiveCallBack(IAsyncResult e)
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

        static void CallBack3(IAsyncResult e)
        {
            try
            {
                Socket socket = serverSocket3.EndAccept(e);
                clientSockets3.Add(socket);
                System.Diagnostics.Debug.WriteLine("Client connected");
                socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallBack3), socket);
                serverSocket1.BeginAccept(new AsyncCallback(CallBack3), null);

            }
            catch (Exception ex) { System.Diagnostics.Debug.WriteLine(ex + " Errooooor"); }
        }

        private static void ReceiveCallBack3(IAsyncResult e)
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
                    clientSockets3.Remove(socket);
                    return;
                }
                byte[] dataBuf = new byte[received];
                Array.Copy(buffer, dataBuf, received);
                String text = System.Text.Encoding.ASCII.GetString(dataBuf);
                Console.WriteLine("Server request: 3: " + text);
                socket.BeginReceive(buffer, 0, bufferSize, SocketFlags.None, ReceiveCallBack3, socket);
            }
            catch (Exception ex) { String s = ex.Message; }
        }

        /*static void CallBack(IAsyncResult e)
        {
            try
            {
                Socket socket = serverSocket1.EndAccept(e);
                clientSockets1.Add(socket);
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
                    clientSockets1.Remove(socket);
                    return;
                }
                byte[] dataBuf = new byte[received];
                Array.Copy(buffer, dataBuf, received);
                String text = System.Text.Encoding.ASCII.GetString(dataBuf);
                Console.WriteLine("Server request: 1: " + text);
                socket.BeginReceive(buffer, 0, bufferSize, SocketFlags.None, ReceiveCallBack, socket);
            }
            catch (Exception ex) { String s = ex.Message; }
        }*/


        static void CallBack2(IAsyncResult e)
        {
            try
            {
                Socket socket = serverSocket2.EndAccept(e);
                clientSockets2.Add(socket);
                System.Diagnostics.Debug.WriteLine("Client connected");
                socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallBack2), socket);
                serverSocket1.BeginAccept(new AsyncCallback(CallBack2), null);

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
                    clientSockets2.Remove(socket);
                    return;
                }
                byte[] dataBuf = new byte[received];
                Array.Copy(buffer, dataBuf, received);

                string text = System.Text.Encoding.ASCII.GetString(dataBuf);
                HandleIncomingEvent(text, socket.RemoteEndPoint.ToString().Split(':')[0]);
                Console.WriteLine("Server request: 2:" + text);
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
