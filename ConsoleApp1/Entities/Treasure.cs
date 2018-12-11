using System;
using CocosSharp;

namespace ConsoleApp1.Entities
{
    public class Treasure : Entities
    {
        bool opened;

        public Treasure(String str) : base()
        {
            opened = false;
        }

        public bool IsOpen()
        {
            return opened;
        }

        public void Open()
        {
            opened = true;
        }

        public void Close()
        {
            opened = false;
        }

        public override void MoveX(int x)
        {
            this.PositionX += x;
            /*sprite_opened.PositionX += x;
            sprite_closed.PositionX += x;*/
            /*sprite2.PositionX += x;
            sprite3.PositionX += x;
            sprite4.PositionX += x;*/
        }

        public override void MoveY(int y)
        {
            this.PositionY += y;
            /*sprite_opened.PositionY += y;
            sprite_closed.PositionY += y;*/
            /*sprite2.PositionY += y;
            sprite3.PositionY += y;
            sprite4.PositionY += y;*/
        }

        public void Interact()
        {
            if (IsOpen())
            {
                Close();
            }
            else
            {
                Open();
            }
        }
    }
}