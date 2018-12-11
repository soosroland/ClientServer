using System;
using CocosSharp;

namespace ConsoleApp1.Entities
{
    public class Wall : Entities
    {

        public Wall(String s) : base()
        {

        }
        public Wall() : base()
        {

        }
        public override void MoveX(int x)
        {
            this.PositionX += x;
        }

        public override void MoveY(int y)
        {
            this.PositionY += y;
        }
    }
}