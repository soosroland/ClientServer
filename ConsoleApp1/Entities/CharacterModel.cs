using System;
using CocosSharp;

namespace ConsoleApp1.Entities
{
    public class CharacterModel : Entities
    {
        bool shielded = false;

        public CharacterModel(String str, String part) : base()
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
        public void Shield()
        {
            shielded = true;
        }
        public void UnShield()
        {
            shielded = false;
        }
        public void ChangeSprite()
        {
            if (shielded)
            {
            }
            else
            {
            }
        }
    }
}