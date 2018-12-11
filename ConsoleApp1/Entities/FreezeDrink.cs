using CocosSharp;

namespace ConsoleApp1.Entities
{
    public class FreezeDrink : Entities
    {

        public FreezeDrink() : base()
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