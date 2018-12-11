using CocosSharp;

namespace ConsoleApp1.Entities
{
    public class DoorKey : Entities
    {
        public DoorKey() : base()
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