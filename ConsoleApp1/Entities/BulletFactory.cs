﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace ConsoleApp1.Entities
{
    public class BulletFactory
    {
        static Lazy<BulletFactory> self =
            new Lazy<BulletFactory>(() => new BulletFactory());

        // simple singleton implementation
        public static BulletFactory Self
        {
            get
            {
                return self.Value;
            }
        }

        public event Action<Bullet> BulletCreated;

        private BulletFactory()
        {

        }

        public Bullet CreateNew()
        {
            Bullet newBullet = new Bullet();

            if (BulletCreated != null)
            {
                BulletCreated(newBullet);
            }

            return newBullet;
        }
    }
}