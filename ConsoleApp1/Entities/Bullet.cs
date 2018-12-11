using CocosSharp;

namespace ConsoleApp1.Entities
{
    public class Bullet : Entities
    {

        public float VelocityX
        {
            get;
            set;
        }

        public float VelocityY
        {
            get;
            set;
        }

        public Bullet() : base()
        {
            //this.Schedule(ApplyVelocity, 0.041f);
        }

        void ApplyVelocity(float time)
        {
            //PositionX += VelocityX * time;
            //PositionY += VelocityY * time;
        }

        public override void MoveX(int x)
        {
            PositionX += x;
        }

        public override void MoveY(int y)
        {
            PositionY += y;
        }
    }
}