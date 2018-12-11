using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Match
    {
        public String map_name;
        public String match_type;
        public int max_player_number;
        public int curr_player_number;
        public String speed;
        public String difficulty;
        public List<String> players = new List<string>();

        public Match() { }

        public Match(String m_n, String m_t, int m_p_n, String s, String diff)
        {
            map_name = m_n;
            match_type = m_t;
            max_player_number = m_p_n;
            speed = s;
            difficulty = diff;
        }

        public void AddPlayer(String ip)
        {
            if (!this.Full())
            {
                curr_player_number++;
                players.Add(ip);
            }
        }

        public void RemovePlayer(String ip)
        {
            if ((players.Count != 0) && (players.Contains(ip)))
            {
                curr_player_number--;
                players.Remove(ip);
            }
        }

        public Boolean Full()
        {
            return curr_player_number == max_player_number ? true : false;
        }
    }
}
