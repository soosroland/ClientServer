using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CocosSharp;


namespace ConsoleApp1.Entities
{
    public abstract class Entities
    {
        public int PositionX;
        public int PositionY;

        public Entities() { }

        public abstract void MoveX(int x);

        public abstract void MoveY(int y);
    }
}
