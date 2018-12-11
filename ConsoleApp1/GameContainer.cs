using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class GameContainer
    {
        //List<Game> gameList = new List<Game>();
        public Dictionary<int, Game> games_Dictionary = new Dictionary<int, Game>();

        public GameContainer() { }

        public void AddGame(String level_num, int hp, String game_speed, int id, List<String> players)
        {
            Game game = new Game(level_num, hp, game_speed, id, players);
            //gameList.Add(game);
            games_Dictionary.Add(id, game);
        }

        public void RemoveGame(int id)
        {
            games_Dictionary.Remove(id);
        }

        public void Step_Right(int id, int num)
        {
            games_Dictionary[id].Step_Right(num);
        }

        public void Step_Left(int id, int num)
        {
            games_Dictionary[id].Step_Left(num);
        }

        public void Step_Down(int id, int num)
        {
            games_Dictionary[id].Step_Down(num);
        }

        public void Step_Up(int id, int num)
        {
            games_Dictionary[id].Step_Up(num);
        }

        public String GetCharacterPosition(int id, int num)
        {
            return games_Dictionary[id].GetCharacterPosition(num);
        }

        public void Open(int id, int x, int y)
        {
            games_Dictionary[id].Open(x, y);
        }

        public void Treasure(int id, int x, int y)
        {
            games_Dictionary[id].Treasure(x, y);
        }

        public int GetPlayerNumber(int id)
        {
            if (games_Dictionary.ContainsKey(id))
            {
                return games_Dictionary[id].GetPlayerNumber();
            }
            else
            {
                return -1;
            }
        }

        public String GetIps(int id, int player_id)
        {
            return games_Dictionary[id].GetIps(player_id);
        }

        public void RemoveFromGame(int id, int player_id)
        {
            games_Dictionary[id].RemoveFromGame(player_id);
        }
    }
}
