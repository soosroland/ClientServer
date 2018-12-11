using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using ConsoleApp1;
using cocos2d;

namespace ServerApplication
{
    class Program
    {
        private static int bufferSize = 2048;
        private static byte[] buffer = new byte[bufferSize];
        private static List<Socket> clientSockets = new List<Socket>();
        private static List<String> clientSocketsIPAddress = new List<String>();
        private static Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private static List<Match> matches = new List<Match>();
        private static Game game;
        private static MatchMaking matchMaking = new MatchMaking();
        private static GameContainer gameContainer = matchMaking.gameContainer;
        private static Socket socket;


        static void Main(string[] args)
        {
            Console.Title = "Server";
            SetupServer();

            //StartGame();
            Console.ReadLine();
            //var tcpClient = new TcpClient("192.168.1.103", 8889); // Emulatoraddress
                                                                           //var tcpClient = new TcpClient("10.0.2.15", 8889); // Emulator address
            //NetworkStream clientStream = tcpClient.GetStream();

            //byte[] outStream = System.Text.Encoding.ASCII.GetBytes("Rendben");
            //clientStream.Write(outStream, 0, outStream.Length);
            //clientStream.Flush();
            //response = current_socket.RemoteEndPoint.ToString();
            //byte[] data2 = Encoding.ASCII.GetBytes(response);
            //current_socket.Send(data2);
            //Console.WriteLine("Sent To Client: " + "192.168.0.171");

            /*Console.ReadLine();

            byte[] outStream2 = System.Text.Encoding.ASCII.GetBytes("Elment");
            clientStream.Write(outStream2, 0, outStream2.Length);
            clientStream.Flush();*/
            //response = current_socket.RemoteEndPoint.ToString();
            //byte[] data2 = Encoding.ASCII.GetBytes(response);
            //current_socket.Send(data2);
            //Console.WriteLine("Sent To Client: " + "192.168.0.171");

            Console.ReadKey();
            CloseAllSockets();
        }

        /*private static void StartGame(){
            string level_num = "1";
            int hp=5;
            string speed="normal";
            game = new Game(level_num, hp, speed);
        }*/

        private static void SetupServer()
        {
            Console.WriteLine("Setting Up Server 192.168.0.248 Please Wait"); //192.168.0.248 / 1.104 / 3.103
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
                //if (!clientSocketsIPAddress.Contains(socket.RemoteEndPoint.ToString().Split(':')[0]))
                //{
                    clientSockets.Add(socket);
                    clientSocketsIPAddress.Add(socket.RemoteEndPoint.ToString().Split(':')[0]);
                    Console.WriteLine("Client Connected");
                    Console.WriteLine(socket.RemoteEndPoint.ToString().Split(':')[0]);
                //}
                socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallBack), socket);
                serverSocket.BeginAccept(new AsyncCallback(CallBack), null);

            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

        private static void ReceiveCallBack(IAsyncResult e)
        {
            try
            {
                socket = (Socket)e.AsyncState;
                int received;
                try
                {
                    received = socket.EndReceive(e);
                }
                catch (SocketException)
                {
                    Console.WriteLine("Client forcefully disconnected");

                    int id = matchMaking.getIdForIp(socket.RemoteEndPoint.ToString().Split(':')[0]); // get match ID
                    if (id != -1)
                    {
                        byte[] outStream = System.Text.Encoding.ASCII.GetBytes("DisconnectGame");
                        int sajt = matchMaking.getIdForIp(socket.RemoteEndPoint.ToString().Split(':')[0]);
                        TcpClient tcpClient;
                        NetworkStream clientStream;
                        int max = matchMaking.GetPlayerNumber(sajt);
                        List<int> nums = new List<int>();
                        List<String> ips = new List<String>();
                        int match_id = matchMaking.getIdForIp(socket.RemoteEndPoint.ToString().Split(':')[0]);
                        for (int i = 0; i < max; i++)
                        {
                            nums.Add(matchMaking.getNumForIp(matchMaking.GetIps(match_id, i)));
                            ips.Add(matchMaking.GetIps(match_id, i));
                        }
                        for (int i = max; i > 0; i--)
                        {
                            Console.WriteLine("End the game for: " + sajt);
                            matchMaking.RemoveFromGame(id, nums[i-1]); // get Player ID in a match
                            String ip = ips[max - i];
                            matchMaking.RemovePlayer(ips[max - i]);
                            if (max-i != id) // A disconnecteltnek nem küldök
                            {
                                tcpClient = new TcpClient(ip, 8890);// matchMaking.GetIps(sajt, i), 8890);
                                clientStream = tcpClient.GetStream();
                                clientStream.Write(outStream, 0, outStream.Length);
                                clientStream.Flush();
                            }
                        }
                        matchMaking.RemoveGame(id);
                    }

                    clientSockets.Remove(socket);
                    clientSocketsIPAddress.Remove(socket.RemoteEndPoint.ToString().Split(':')[0]);
                    socket.Close();
                    return;
                }
                byte[] dataBuf = new byte[received];
                Array.Copy(buffer, dataBuf, received);

                string text = Encoding.ASCII.GetString(dataBuf);
                HandleIncomingEvent(text, socket.RemoteEndPoint.ToString().Split(':')[0]);
                Console.WriteLine("Client request: " + text);
                
                string response = string.Empty;

                if (text == string.Empty)
                {
                    /*foreach (var current_socket_ipaddress in clientSocketsIPAddress)
	                    {*/
                        //Console.WriteLine(current_socket.RemoteEndPoint.ToString().Split(':')[0]);
                        //String current_socket_ip_address = current_socket.RemoteEndPoint.ToString().Split(':')[0];
                        //var tcpClient = new TcpClient("10.0.2.15", 8889); // Emulator address


                        /*var tcpClient = new TcpClient(current_socket_ipaddress, 8890); // Emulator address
                        NetworkStream clientStream = tcpClient.GetStream();
                        
                        byte[] outStream = System.Text.Encoding.ASCII.GetBytes("Megy");
                        clientStream.Write(outStream, 0, outStream.Length);
                        clientStream.Flush();*/


                        //response = current_socket.RemoteEndPoint.ToString();
                        //byte[] data2 = Encoding.ASCII.GetBytes(response);
                        //current_socket.Send(data2);
                        //Console.WriteLine("Sent To Client: " + current_socket_ipaddress);
                        //serverSocket.BeginAccept(new AsyncCallback(CallBack), null);
                    /*}
                }
                else{*/
                    Console.WriteLine("Client forcefully disconnected else");
                    int id = matchMaking.getIdForIp(socket.RemoteEndPoint.ToString().Split(':')[0]); // get match ID
                    if (id != -1)
                    {
                        byte[] outStream = System.Text.Encoding.ASCII.GetBytes("DisconnectGame");
                        int sajt = matchMaking.getIdForIp(socket.RemoteEndPoint.ToString().Split(':')[0]);
                        TcpClient tcpClient;
                        NetworkStream clientStream;
                        if (matchMaking.GetPlayerNumber(sajt) != -1)
                        {
                            for (int i = 0; i < matchMaking.GetPlayerNumber(sajt); i++)
                            {
                                Console.WriteLine("End the game for: "+sajt);
                                matchMaking.RemoveFromGame(id, matchMaking.getNumForIp(socket.RemoteEndPoint.ToString().Split(':')[0])); // get Player ID in a match
                                matchMaking.RemovePlayer(socket.RemoteEndPoint.ToString().Split(':')[0]);
                                if (i != id) // A disconnecteltnek nem küldjük
                                {
                                    tcpClient = new TcpClient(matchMaking.GetIps(sajt, i), 8890);
                                    clientStream = tcpClient.GetStream();
                                    clientStream.Write(outStream, 0, outStream.Length);
                                    clientStream.Flush();
                                }
                            }
                            matchMaking.RemoveGame(id);
                        }
                        else
                        {
                            matchMaking.RemovePlayer(socket.RemoteEndPoint.ToString().Split(':')[0]);
                            /*tcpClient = new TcpClient(socket.RemoteEndPoint.ToString().Split(':')[0], 8890);
                            clientStream = tcpClient.GetStream();
                            clientStream.Write(outStream, 0, outStream.Length);
                            clientStream.Flush();*/
                        }
                    }



                    clientSockets.Remove(socket);
                    clientSocketsIPAddress.Remove(socket.RemoteEndPoint.ToString().Split(':')[0]);
                    Console.WriteLine(socket.RemoteEndPoint.ToString().Split(':')[0]);
                    socket.Close();
                    return;
                }
                socket.BeginReceive(buffer, 0, bufferSize, SocketFlags.None, ReceiveCallBack, socket);
            }
            catch (Exception ex) { Console.WriteLine(  ex ); }
        }

        private static void HandleIncomingEvent(String text, String ip)
        {
            String[] temp = text.Split(';');
            int sajt;
            try
            {
            /*using (TcpClient tcpClient = new TcpClient(ip, 8890))
            {*/
            TcpClient tcpClient;
            NetworkStream clientStream;
            byte[] outStream;
            String temptext;
                switch (temp[0])
                {
                    case "Multiplayer Request":
                        Console.WriteLine("Multiplayer Request");
                        break;
                    case "Matchmaking":
                        matchMaking.AddMatch(temp[1], temp[2], int.Parse(temp[3]), temp[4], temp[5], ip);
                        Console.WriteLine("Matchmaking");
                        break;
                    case "Cancel Matchmaking":
                        Console.WriteLine("Cancel Matchmaking");
                        break;
                    case "Right button Request":
                        Console.WriteLine("Right button request");
                        sajt = matchMaking.getIdForIp(ip); // sajt=id
                        matchMaking.Step_Right(sajt, ip);
                    temptext = "CharacterPosition";
                    for (int i = 0; i < matchMaking.GetPlayerNumber(sajt); i++)
                    {
                        temptext += ";";
                        temptext += matchMaking.GetCharacterPosition(sajt, matchMaking.GetIps(sajt, i));
                    }
                    outStream = System.Text.Encoding.ASCII.GetBytes(temptext);
                    for (int i = 0; i < matchMaking.GetPlayerNumber(sajt); i++)
                    {
                        tcpClient = new TcpClient(matchMaking.GetIps(sajt, i), 8890);
                        clientStream = tcpClient.GetStream();
                        clientStream.Write(outStream, 0, outStream.Length);
                        clientStream.Flush();
                    }
                    Console.WriteLine("Sent:" + System.Text.Encoding.ASCII.GetString(outStream));
                        break;
                    case "Left button Request":
                        Console.WriteLine("Left button request");
                        sajt = matchMaking.getIdForIp(ip);
                        matchMaking.Step_Left(sajt, ip);
                    temptext = "CharacterPosition";
                    for (int i = 0; i < matchMaking.GetPlayerNumber(sajt); i++)
                    {
                        temptext += ";";
                        temptext += matchMaking.GetCharacterPosition(sajt, matchMaking.GetIps(sajt, i));
                    }
                    outStream = System.Text.Encoding.ASCII.GetBytes(temptext);
                    for (int i = 0; i < matchMaking.GetPlayerNumber(sajt); i++)
                    {
                        tcpClient = new TcpClient(matchMaking.GetIps(sajt, i), 8890);
                        clientStream = tcpClient.GetStream();
                        clientStream.Write(outStream, 0, outStream.Length);
                        clientStream.Flush();
                    }
                    Console.WriteLine("Sent:" + System.Text.Encoding.ASCII.GetString(outStream));
                        break;
                    case "Down button Request":
                        Console.WriteLine("Down button request");
                        sajt = matchMaking.getIdForIp(ip);
                        matchMaking.Step_Down(sajt, ip);
                        Console.WriteLine(matchMaking.GetCharacterPosition(sajt, ip));
                    temptext = "CharacterPosition";
                    for (int i = 0; i < matchMaking.GetPlayerNumber(sajt); i++)
                    {
                        temptext += ";";
                        temptext += matchMaking.GetCharacterPosition(sajt, matchMaking.GetIps(sajt, i));
                    }
                    outStream = System.Text.Encoding.ASCII.GetBytes(temptext);
                    for (int i = 0; i < matchMaking.GetPlayerNumber(sajt); i++)
                    {
                        tcpClient = new TcpClient(matchMaking.GetIps(sajt, i), 8890);
                        clientStream = tcpClient.GetStream();
                        clientStream.Write(outStream, 0, outStream.Length);
                        clientStream.Flush();
                    }
                    Console.WriteLine("Sent:" + System.Text.Encoding.ASCII.GetString(outStream));
                        break;
                    case "Up button Request":
                        Console.WriteLine("Up button request");
                        sajt = matchMaking.getIdForIp(ip);
                        matchMaking.Step_Up(sajt, ip);
                        Console.WriteLine(matchMaking.GetCharacterPosition(sajt, ip));
                    temptext = "CharacterPosition";
                    for(int i = 0; i < matchMaking.GetPlayerNumber(sajt); i++)
                    {
                        temptext += ";";
                        temptext += matchMaking.GetCharacterPosition(sajt, matchMaking.GetIps(sajt, i));
                    }
                    outStream = System.Text.Encoding.ASCII.GetBytes(temptext);
                    for (int i = 0; i < matchMaking.GetPlayerNumber(sajt); i++)
                    {
                        tcpClient = new TcpClient(matchMaking.GetIps(sajt, i), 8890);
                        clientStream = tcpClient.GetStream();
                        clientStream.Write(outStream, 0, outStream.Length);
                        clientStream.Flush();
                    }

                    /*tcpClient = new TcpClient(ip, 8890);
                    clientStream = tcpClient.GetStream();
                    outStream = System.Text.Encoding.ASCII.GetBytes("CharacterPosition;" + matchMaking.GetCharacterPosition(sajt, ip));*/

                    Console.WriteLine("Sent:" + System.Text.Encoding.ASCII.GetString(outStream));
                        break;
                    case "Special button Request":
                        Console.WriteLine("Special button Request");
                        break;
                    case "Open":
                        sajt = matchMaking.getIdForIp(ip);
                        matchMaking.Open(sajt, int.Parse(temp[1]), int.Parse(temp[2]));
                        break;
                    case "Treasure":
                        sajt = matchMaking.getIdForIp(ip);
                        matchMaking.Treasure(sajt, int.Parse(temp[1]), int.Parse(temp[2]));
                        break;
                    case "MatchMakingCanceled":
                        matchMaking.CancelMatch(ip);
                        break;
                    default:
                        Console.WriteLine("DEFAULT :O");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Client forcefully disconnected catch"+ex);
            }            
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
    }
}