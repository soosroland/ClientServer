using CocosSharp;

namespace ConsoleApp1.Entities
{
    public class Coin : Entities
    {

        public Coin() : base()
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