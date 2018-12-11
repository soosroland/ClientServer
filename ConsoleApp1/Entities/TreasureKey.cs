using CocosSharp;

namespace ConsoleApp1.Entities
{
    class TreasureKey : Entities
    {

        public TreasureKey() : base()
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