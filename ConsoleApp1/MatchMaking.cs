using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class MatchMaking
    {
        List<String> clients = new List<string>(); // Client IP-s
        List<Match> matchmaking_matches = new List<Match>();
        List<Match> running_matches = new List<Match>();
        Dictionary<String, int> matches_dictionary = new Dictionary<String, int>();
        public GameContainer gameContainer = new GameContainer();
        Dictionary<String, int> iptoplayernum = new Dictionary<String, int>();
        private bool added;
        int id = 0;


        public MatchMaking() { }

        public void AddMatch(String map_name, String matchtype, int maxplayernumber, String speed, String difficulty, String ip)
        {
            added = false;
            if (clients.Contains(ip))
            {
                clients.Remove(ip);
                foreach (Match match in matchmaking_matches)
                {
                    if (match.players.Contains(ip))
                    {
                        match.RemovePlayer(ip);
                    }
                }
                matches_dictionary.Remove(ip);
            }

            clients.Add(ip);

            for (int i = matchmaking_matches.Count-1; i >= 0; i--)
            {
                if (matchmaking_matches[i].match_type.Equals(matchtype))
                {
                    matchmaking_matches[i].AddPlayer(ip);
                    added = true;
                    if (matchmaking_matches[i].Full())
                    {
                        running_matches.Add(matchmaking_matches[i]);
                        for(int j = 0; j < matchmaking_matches[i].players.Count; j++)
                        {
                            matches_dictionary.Add(matchmaking_matches[i].players[j], id);
                        }
                        matchmaking_matches.Remove(matchmaking_matches[i]);

                        // TODO Notify clients


                        // Start Game
                        String level_num = "1";
                        int hp = 5;
                        String game_speed = "normal";
                        List<String> players = new List<string>();
                        for (int k = 0; k < running_matches[running_matches.Count - 1].players.Count; k++)
                        {
                            iptoplayernum.Add(running_matches[running_matches.Count - 1].players[k], k);
                        }
                        players = running_matches[running_matches.Count - 1].players;
                        gameContainer.AddGame(level_num, hp, game_speed,id++, players);
                    }
                }
            }

            if (!added)
            {
                //maxplayernumber = 1;
                matchmaking_matches.Add(new Match(map_name, matchtype, maxplayernumber, speed, difficulty));
                matchmaking_matches[matchmaking_matches.Count - 1].AddPlayer(ip);
                if(matchmaking_matches[matchmaking_matches.Count - 1].Full())
                {
                    running_matches.Add(matchmaking_matches[matchmaking_matches.Count - 1]);
                    for (int j = 0; j < matchmaking_matches[matchmaking_matches.Count - 1].players.Count; j++)
                    {
                        matches_dictionary.Add(matchmaking_matches[matchmaking_matches.Count - 1].players[j], id);
                    }
                    matchmaking_matches.Remove(matchmaking_matches[matchmaking_matches.Count - 1]);

                    // TODO Notify clients


                    // Start Game
                    String level_num = "1";
                    int hp = 5;
                    String game_speed = "normal";
                    List<String> players = new List<string>();
                    for(int i = 0; i < running_matches[running_matches.Count - 1].players.Count; i++)
                    {
                        iptoplayernum.Add(running_matches[running_matches.Count - 1].players[i], i);
                    }
                    players = running_matches[running_matches.Count - 1].players;
                    gameContainer.AddGame(level_num, hp, game_speed, id++, players);
                }
            }
        }

        public int getIdForIp(String ip)
        {
            if (matches_dictionary.ContainsKey(ip))
            {
                int temp = matches_dictionary[ip];
                return temp;
            }
            else
            {
                return -1;
            }
        }

        public int getNumForIp(String ip)
        {
            return iptoplayernum[ip];
        }

        public void Step_Right(int id, String ip)
        {
            gameContainer.Step_Right(id, getNumForIp(ip));
        }

        public void Step_Left(int id, String ip)
        {
            gameContainer.Step_Left(id, getNumForIp(ip));
        }

        public void Step_Down(int id, String ip)
        {
            gameContainer.Step_Down(id, getNumForIp(ip));
        }

        public void Step_Up(int id, String ip)
        {
            gameContainer.Step_Up(id, getNumForIp(ip));
        }

        public String GetCharacterPosition(int id, String ip)
        {
            return gameContainer.GetCharacterPosition(id, getNumForIp(ip));
        }

        public void Open(int id, int x, int y)
        {
            gameContainer.Open(id, x, y);
        }

        public void Treasure(int id, int x, int y)
        {
            gameContainer.Treasure(id, x, y);
        }

        public int GetPlayerNumber(int id)
        {
            return gameContainer.GetPlayerNumber(id);
        }

        public String GetIps(int id, int player_id)
        {
            return gameContainer.GetIps(id, player_id);
        }

        public void RemoveFromGame(int id, int player_id)
        {
            gameContainer.RemoveFromGame(id, player_id);
        }

        public void RemoveGame(int id)
        {
            gameContainer.RemoveGame(id);
        }

        public void RemovePlayer(String ip)
        {
            iptoplayernum.Remove(ip);
        }

        public void CancelMatch(String ip)
        {
            foreach (var match in matchmaking_matches)
            {
                if (match.players.Contains(ip))
                {
                    match.RemovePlayer(ip);
                }
            }
            
        }
    }
}
