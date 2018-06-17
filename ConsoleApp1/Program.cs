using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace ServerApplication
{
    class Program
    {
        private static int bufferSize = 2048;
        private static byte[] buffer = new byte[bufferSize];
        private static List<Socket> clientSockets = new List<Socket>();
        private static Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);


        static void Main(string[] args)
        {
            Console.Title = "Server";
            SetupServer();
            Console.ReadKey();
            CloseAllSockets();
        }

        private static void SetupServer()
        {
            Console.WriteLine("Settings Up Server Plz Wait");
            serverSocket.Bind(new IPEndPoint(IPAddress.Any, 8888));
            serverSocket.Listen(10);
            serverSocket.BeginAccept(new AsyncCallback(CallBack), null);
            Console.WriteLine("Server Made");
        }

        private static void CallBack(IAsyncResult e)
        {
            try
            {
                Socket socket = serverSocket.EndAccept(e);
                clientSockets.Add(socket);
                Console.WriteLine("Client Connected");
                socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallBack), socket);
                serverSocket.BeginAccept(new AsyncCallback(CallBack), null);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
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
                    Console.WriteLine("Client forcefully disconnected");
                    socket.Close();
                    clientSockets.Remove(socket);
                    return;
                }
                byte[] dataBuf = new byte[received];
                Array.Copy(buffer, dataBuf, received);

                string text = Encoding.ASCII.GetString(dataBuf);
                Console.WriteLine("Client request: " + text);

                string response = string.Empty;


                try
                {
                    if (text.ToLower() == "what time is it?")
                    {
                        response = DateTime.Now.ToString();
                    }
                    else if (text.ToLower() == "whats your name?")
                    {
                        response = "Tyler Gregorcyk";
                    }
                    else
                    {
                        response = "Invaled";
                    }
                }
                catch (Exception et) { Console.WriteLine(et.Message); socket.Close(); clientSockets.Remove(socket); }


                if (response == string.Empty)
                {
                    response = "Invaled";
                }

                if (response != string.Empty)
                {
                    /*if(response == "Invaled")
                    {*/
                        foreach (var current_socket in clientSockets)
	                    {
                            response = current_socket.RemoteEndPoint.ToString();
                            byte[] data2 = Encoding.ASCII.GetBytes(response);
                            current_socket.Send(data2);
                            Console.WriteLine("Sent To Client: " + response);
                            //serverSocket.BeginAccept(new AsyncCallback(CallBack), null);
	                    }
                    /*}
                    else{*/
                        
                        //byte[] data = Encoding.ASCII.GetBytes(response);
                        //socket.Send(data);
                        serverSocket.BeginAccept(new AsyncCallback(CallBack), null);
                    //}
                }

                //conn.Close();
                socket.BeginReceive(buffer, 0, bufferSize, SocketFlags.None, ReceiveCallBack, socket);
            }
            catch (Exception) { }
        }

        private static void CloseAllSockets()
        {
            foreach (Socket socket in clientSockets)
            {
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }

            serverSocket.Close();
        }

        /*var serverSocket = new TcpListener(IPAddress.Any, 8888);
        //TcpListener serverSocket = new TcpListener(8888);
        int requestCount = 0;
        TcpClient clientSocket = default(TcpClient);
        serverSocket.Start();
        Console.WriteLine(" >> Server Started");
        clientSocket = serverSocket.AcceptTcpClient();
        Console.WriteLine(" >> Accept connection from client");
        requestCount = 0;

        while ((true))
        {
            try
            {
                clientSocket.ReceiveBufferSize = 2048;
                requestCount = requestCount + 1;
                NetworkStream networkStream = clientSocket.GetStream();
                byte[] bytesFrom = new byte[10025];
                networkStream.Read(bytesFrom, 0, (int)clientSocket.ReceiveBufferSize);
                string dataFromClient = System.Text.Encoding.ASCII.GetString(bytesFrom);
                dataFromClient = dataFromClient.Substring(0, dataFromClient.IndexOf("$"));
                Console.WriteLine(" >> Data from client - " + dataFromClient);
                string serverResponse = "Last Message from client - " + dataFromClient;
                Byte[] sendBytes = Encoding.ASCII.GetBytes(serverResponse);
                networkStream.Write(sendBytes, 0, sendBytes.Length);
                networkStream.Flush();
                Console.WriteLine(" >> " + serverResponse);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        clientSocket.Close();
        serverSocket.Stop();
        Console.WriteLine(" >> exit");
        Console.ReadLine();*/
    }
}