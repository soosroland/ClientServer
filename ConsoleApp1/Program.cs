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
        private static GoldRushMatchMaking goldRushMatchMaking = new GoldRushMatchMaking();
        private static GameContainer gameContainer = matchMaking.gameContainer;
        private static Socket socket;


        static void Main(string[] args)
        {
            Console.Title = "Server";
            SetupServer();
            Console.ReadLine();

            Console.ReadKey();
            CloseAllSockets();
        }

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
                        if (matchMaking.GetPlayerNumber(sajt) != -1)
                        {
                            for (int i = 0; i < matchMaking.GetPlayerNumber(sajt); i++)
                            {
                                Console.WriteLine("End the game for: " + sajt);
                                matchMaking.RemoveFromGame(id, matchMaking.getNumForIp(socket.RemoteEndPoint.ToString().Split(':')[0]), socket.RemoteEndPoint.ToString().Split(':')[0]); // get Player ID in a match
                                matchMaking.RemovePlayer(socket.RemoteEndPoint.ToString().Split(':')[0]);
                                if (i != id) // A disconnecteltnek nem küldjük
                                {
                                    tcpClient = new TcpClient(matchMaking.GetIps(sajt, i), 8890);
                                    clientStream = tcpClient.GetStream();
                                    clientStream.Write(outStream, 0, outStream.Length);
                                    clientStream.Flush();
                                }
                            }
                            matchMaking.RemoveGame(id, socket.RemoteEndPoint.ToString().Split(':')[0]);
                        }
                        else
                        {
                            matchMaking.RemovePlayer(socket.RemoteEndPoint.ToString().Split(':')[0]);
                        }
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
                                matchMaking.RemoveFromGame(id, matchMaking.getNumForIp(socket.RemoteEndPoint.ToString().Split(':')[0]), socket.RemoteEndPoint.ToString().Split(':')[0]); // get Player ID in a match
                                matchMaking.RemovePlayer(socket.RemoteEndPoint.ToString().Split(':')[0]);
                                if (i != id) // A disconnecteltnek nem küldjük
                                {
                                    tcpClient = new TcpClient(matchMaking.GetIps(sajt, i), 8890);
                                    clientStream = tcpClient.GetStream();
                                    clientStream.Write(outStream, 0, outStream.Length);
                                    clientStream.Flush();
                                }
                            }
                            matchMaking.RemoveGame(id, socket.RemoteEndPoint.ToString().Split(':')[0]);
                        }
                        else
                        {
                            matchMaking.RemovePlayer(socket.RemoteEndPoint.ToString().Split(':')[0]);
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
                        Console.WriteLine("Matchmaking"+ip);
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
                    case "GRight button Request":
                        Console.WriteLine("Right button request");
                        sajt = goldRushMatchMaking.getIdForIp(ip); // sajt=id
                        goldRushMatchMaking.Step_Right(sajt, ip);
                        temptext = "CharacterPosition";
                        for (int i = 0; i < goldRushMatchMaking.GetPlayerNumber(sajt); i++)
                        {
                            temptext += ";";
                            temptext += goldRushMatchMaking.GetCharacterPosition(sajt, goldRushMatchMaking.GetIps(sajt, i));
                        }
                        outStream = System.Text.Encoding.ASCII.GetBytes(temptext);
                        for (int i = 0; i < goldRushMatchMaking.GetPlayerNumber(sajt); i++)
                        {
                            tcpClient = new TcpClient(goldRushMatchMaking.GetIps(sajt, i), 8891);
                            clientStream = tcpClient.GetStream();
                            clientStream.Write(outStream, 0, outStream.Length);
                            clientStream.Flush();
                        }
                        Console.WriteLine("Sent:" + System.Text.Encoding.ASCII.GetString(outStream));
                        break;
                    case "GLeft button Request":
                        Console.WriteLine("Left button request");
                        sajt = goldRushMatchMaking.getIdForIp(ip);
                        goldRushMatchMaking.Step_Left(sajt, ip);
                        temptext = "CharacterPosition";
                        for (int i = 0; i < goldRushMatchMaking.GetPlayerNumber(sajt); i++)
                        {
                            temptext += ";";
                            temptext += goldRushMatchMaking.GetCharacterPosition(sajt, goldRushMatchMaking.GetIps(sajt, i));
                        }
                        outStream = System.Text.Encoding.ASCII.GetBytes(temptext);
                        for (int i = 0; i < goldRushMatchMaking.GetPlayerNumber(sajt); i++)
                        {
                            tcpClient = new TcpClient(goldRushMatchMaking.GetIps(sajt, i), 8891);
                            clientStream = tcpClient.GetStream();
                            clientStream.Write(outStream, 0, outStream.Length);
                            clientStream.Flush();
                        }
                        Console.WriteLine("Sent:" + System.Text.Encoding.ASCII.GetString(outStream));
                        break;
                    case "GDown button Request":
                        Console.WriteLine("Down button request");
                        sajt = goldRushMatchMaking.getIdForIp(ip);
                        goldRushMatchMaking.Step_Down(sajt, ip);
                        Console.WriteLine(goldRushMatchMaking.GetCharacterPosition(sajt, ip));
                        temptext = "CharacterPosition";
                        for (int i = 0; i < goldRushMatchMaking.GetPlayerNumber(sajt); i++)
                        {
                            temptext += ";";
                            temptext += goldRushMatchMaking.GetCharacterPosition(sajt, goldRushMatchMaking.GetIps(sajt, i));
                        }
                        outStream = System.Text.Encoding.ASCII.GetBytes(temptext);
                        for (int i = 0; i < goldRushMatchMaking.GetPlayerNumber(sajt); i++)
                        {
                            tcpClient = new TcpClient(goldRushMatchMaking.GetIps(sajt, i), 8891);
                            clientStream = tcpClient.GetStream();
                            clientStream.Write(outStream, 0, outStream.Length);
                            clientStream.Flush();
                        }
                        Console.WriteLine("Sent:" + System.Text.Encoding.ASCII.GetString(outStream));
                        break;
                    case "GUp button Request":
                        Console.WriteLine("Up button request");
                        sajt = goldRushMatchMaking.getIdForIp(ip);
                        goldRushMatchMaking.Step_Up(sajt, ip);
                        Console.WriteLine(goldRushMatchMaking.GetCharacterPosition(sajt, ip));
                        temptext = "CharacterPosition";
                        for (int i = 0; i < goldRushMatchMaking.GetPlayerNumber(sajt); i++)
                        {
                            temptext += ";";
                            temptext += goldRushMatchMaking.GetCharacterPosition(sajt, goldRushMatchMaking.GetIps(sajt, i));
                        }
                        outStream = System.Text.Encoding.ASCII.GetBytes(temptext);
                        for (int i = 0; i < goldRushMatchMaking.GetPlayerNumber(sajt); i++)
                        {
                            tcpClient = new TcpClient(goldRushMatchMaking.GetIps(sajt, i), 8891);
                            clientStream = tcpClient.GetStream();
                            clientStream.Write(outStream, 0, outStream.Length);
                            clientStream.Flush();
                        }
                        
                        Console.WriteLine("Sent:" + System.Text.Encoding.ASCII.GetString(outStream));
                        break;
                    case "Special button Request":
                        Console.WriteLine("Special button Request");
                        break;
                    case "Open":
                        sajt = matchMaking.getIdForIp(ip);
                        matchMaking.Open(sajt, int.Parse(temp[1]), int.Parse(temp[2]));
                        break;
                    case "GOpen":
                        sajt = goldRushMatchMaking.getIdForIp(ip);
                        goldRushMatchMaking.Open(sajt, int.Parse(temp[1]), int.Parse(temp[2]));
                        break;
                    case "Treasure":
                        sajt = matchMaking.getIdForIp(ip);
                        matchMaking.Treasure(sajt, int.Parse(temp[1]), int.Parse(temp[2]));
                        break;
                    case "MatchMakingCanceled":
                        matchMaking.CancelMatch(ip);
                        break;
                    case "Leave":
                        int id = matchMaking.getIdForIp(socket.RemoteEndPoint.ToString().Split(':')[0]); // get match ID
                        if (id != -1)
                        {
                            outStream = System.Text.Encoding.ASCII.GetBytes("DisconnectGame");
                            sajt = matchMaking.getIdForIp(socket.RemoteEndPoint.ToString().Split(':')[0]);
                            if (matchMaking.GetPlayerNumber(sajt) != -1)
                            {
                                for (int i = 0; i < matchMaking.GetPlayerNumber(sajt); i++)
                                {
                                    Console.WriteLine("End the game for: " + sajt);
                                    matchMaking.RemoveFromGame(id, matchMaking.getNumForIp(socket.RemoteEndPoint.ToString().Split(':')[0]), socket.RemoteEndPoint.ToString().Split(':')[0]); // get Player ID in a match
                                    matchMaking.RemovePlayer(socket.RemoteEndPoint.ToString().Split(':')[0]);
                                    if (i != id) // A disconnecteltnek nem küldjük
                                    {
                                        tcpClient = new TcpClient(matchMaking.GetIps(sajt, i), 8890);
                                        clientStream = tcpClient.GetStream();
                                        clientStream.Write(outStream, 0, outStream.Length);
                                        clientStream.Flush();
                                    }
                                }
                                matchMaking.RemoveGame(id, socket.RemoteEndPoint.ToString().Split(':')[0]);
                            }
                            else
                            {
                                matchMaking.RemovePlayer(socket.RemoteEndPoint.ToString().Split(':')[0]);
                            }
                        }
                        break;
                    case "GLeave":
                        id = goldRushMatchMaking.getIdForIp(socket.RemoteEndPoint.ToString().Split(':')[0]); // get match ID
                        if (id != -1)
                        {
                            outStream = System.Text.Encoding.ASCII.GetBytes("DisconnectGame");
                            sajt = goldRushMatchMaking.getIdForIp(socket.RemoteEndPoint.ToString().Split(':')[0]);
                            if (goldRushMatchMaking.GetPlayerNumber(sajt) != -1)
                            {
                                for (int i = 0; i < goldRushMatchMaking.GetPlayerNumber(sajt); i++)
                                {
                                    Console.WriteLine("End the game for: " + sajt);
                                    goldRushMatchMaking.RemoveFromGame(id, goldRushMatchMaking.getNumForIp(socket.RemoteEndPoint.ToString().Split(':')[0]), socket.RemoteEndPoint.ToString().Split(':')[0]); // get Player ID in a match
                                    goldRushMatchMaking.RemovePlayer(socket.RemoteEndPoint.ToString().Split(':')[0]);
                                    if (i != id) // A disconnecteltnek nem küldjük
                                    {
                                        tcpClient = new TcpClient(goldRushMatchMaking.GetIps(sajt, i), 8891);
                                        clientStream = tcpClient.GetStream();
                                        clientStream.Write(outStream, 0, outStream.Length);
                                        clientStream.Flush();
                                    }
                                }
                                goldRushMatchMaking.RemoveGame(id, socket.RemoteEndPoint.ToString().Split(':')[0]);
                            }
                            else
                            {
                                goldRushMatchMaking.RemovePlayer(socket.RemoteEndPoint.ToString().Split(':')[0]);
                            }
                        }
                        break;
                    case "GoldRushMatchmaking":
                        goldRushMatchMaking.AddMatch(temp[1], ip);
                        break;
                    case "GCoin":
                        id = goldRushMatchMaking.getIdForIp(socket.RemoteEndPoint.ToString().Split(':')[0]); // get match ID
                        if (id != -1)
                        {
                            outStream = System.Text.Encoding.ASCII.GetBytes("DeleteCoin;" + temp[1] + ";" + temp[2]);
                            sajt = goldRushMatchMaking.getIdForIp(socket.RemoteEndPoint.ToString().Split(':')[0]);
                            if (goldRushMatchMaking.GetPlayerNumber(sajt) != -1)
                            {
                                for (int i = 0; i < goldRushMatchMaking.GetPlayerNumber(sajt); i++)
                                {
                                    tcpClient = new TcpClient(goldRushMatchMaking.GetIps(sajt, i), 8891);
                                    clientStream = tcpClient.GetStream();
                                    clientStream.Write(outStream, 0, outStream.Length);
                                    clientStream.Flush();
                                }
                                /*for (int i = 0; i < goldRushMatchMaking.GetPlayerNumber(sajt); i++)
                                {
                                    if (i != id) // Annak aki felszedte a Coint nem küldjük
                                    {
                                        tcpClient = new TcpClient(goldRushMatchMaking.GetIps(sajt, i), 8891);
                                        clientStream = tcpClient.GetStream();
                                        clientStream.Write(outStream, 0, outStream.Length);
                                        clientStream.Flush();
                                    }
                                }*/
                            }
                        }
                        Console.WriteLine("Coin: " + int.Parse(temp[1])+ ";" +int.Parse(temp[2]));
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