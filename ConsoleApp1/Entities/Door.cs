using System;
using CocosSharp;

namespace ConsoleApp1.Entities
{
    public class Door : Entities
    {
        public int number { get; set; }
        public String type { get; set; }
        public String part { get; set; }
        public bool open { get; set; }

        String opened;
        String closed;

        public Door(String s, String o, String p) : base()
        {
            type = s;
            part = p;
            if (o == "true")
            {
                open = true;
            }
            else
            {
                open = false;
            }
            opened = s + "_open_";
            closed = s + "_closed_";
            opened += p;
            closed += p;
            if (open)
            {
                //this.AddChild(sprite_opened);
            }
            else
            {
                //this.AddChild(sprite_closed);
            }
        }
        public override void MoveX(int x)
        {
            this.PositionX += x;
        }

        public override void MoveY(int y)
        {
            this.PositionY += y;
        }

        public void Open()
        {
        }
    }
}