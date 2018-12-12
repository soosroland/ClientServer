using CocosDenshion;
using CocosSharp;
using System;
using System.Collections.Generic;
using System.Text;
using ConsoleApp1.Entities;
using System.Threading;
using Microsoft.Xna.Framework;
using System.Drawing;
using TiledSharp;
using System.Net.Sockets;

namespace ConsoleApp1
{
    public class GoldRushGame : CCLayer
    {
        Treasure treasure;
        Cannon cannon;
        Wall wall;
        Door door;
        DoorKey doorkey;
        TreasureKey treasurekey;
        FreezeDrink freezedrink;
        ImmortalityDrink immortalitydrink;
        Coin coin;
        Finish finish;
        CharacterModel character;
        CharacterModel character_enemy;
        //List<ConsoleApp1.Entities.Entities> entities = new List<ConsoleApp1.Entities.Entities>();
        List<CharacterModel> charmodel_List = new List<CharacterModel>();
        List<CharacterModel> charmodel_enemy_List = new List<CharacterModel>();
        List<Treasure> treasure_List = new List<Treasure>();
        List<Cannon> shooter_List = new List<Cannon>();
        List<Bullet> bullet_List;
        List<Wall> wall_List = new List<Wall>();
        List<Wall> shootingWall_List = new List<Wall>();
        List<Door> door_List = new List<Door>();
        List<DoorKey> doorkey_List = new List<DoorKey>();
        List<TreasureKey> treasurekey_List = new List<TreasureKey>();
        List<FreezeDrink> freezedrink_List = new List<FreezeDrink>();
        List<ImmortalityDrink> immortalitydrink_List = new List<ImmortalityDrink>();
        List<int> charmodel_id1_List = new List<int>();
        List<int> charmodel_id2_List = new List<int>();
        List<int> charmodel_id3_List = new List<int>();
        List<int> charmodel_id4_List = new List<int>();
        List<int> charmodelenemy_id1_List = new List<int>();
        List<int> charmodelenemy_id2_List = new List<int>();
        List<int> charmodelenemy_id3_List = new List<int>();
        List<int> charmodelenemy_id4_List = new List<int>();
        List<int> shooter_up_List = new List<int>();
        List<int> shooter_down_List = new List<int>();
        List<int> shooter_right_List = new List<int>();
        List<int> shooter_left_List = new List<int>();
        List<int> shooter_id_List = new List<int>();
        List<int> treasure_id_List = new List<int>();
        List<int> wall_id_List = new List<int>();
        List<int> door_id_List = new List<int>();
        List<int> doorkey_id_List = new List<int>();
        List<int> finish_id_List = new List<int>();
        List<int> coin_id_List = new List<int>();
        List<int> treasurekey_id_List = new List<int>();
        List<int> freezedrink_id_List = new List<int>();
        List<int> immortalitydrink_id_List = new List<int>();
        List<int> door_upDooropenedpart1_id_List = new List<int>();
        List<int> door_upDooropenedpart2_id_List = new List<int>();
        List<int> door_upDoorclosedpart1_id_List = new List<int>();
        List<int> door_upDoorclosedpart2_id_List = new List<int>();
        List<int> door_rightDooropenedpart1_id_List = new List<int>();
        List<int> door_rightDooropenedpart2_id_List = new List<int>();
        List<int> door_rightDoorclosedpart1_id_List = new List<int>();
        List<int> door_rightDoorclosedpart2_id_List = new List<int>();
        List<int> door_leftDooropenedpart1_id_List = new List<int>();
        List<int> door_leftDooropenedpart2_id_List = new List<int>();
        List<int> door_leftDoorclosedpart1_id_List = new List<int>();
        List<int> door_leftDoorclosedpart2_id_List = new List<int>();
        List<int> door_downDooropenedpart1_id_List = new List<int>();
        List<int> door_downDooropenedpart2_id_List = new List<int>();
        List<int> door_downDooropenedpart3_id_List = new List<int>();
        List<int> door_downDooropenedpart4_id_List = new List<int>();
        List<int> door_downDoorclosedpart1_id_List = new List<int>();
        List<int> door_downDoorclosedpart2_id_List = new List<int>();
        List<int> door_downDoorclosedpart3_id_List = new List<int>();
        List<int> door_downDoorclosedpart4_id_List = new List<int>();
        List<Coin> coin_List = new List<Coin>();
        List<Finish> finish_List = new List<Finish>();
        System.Drawing.Point character_place;
        CCLabel time_label;
        CCLabel door_key_label;
        CCLabel treasure_key_label;
        CCLabel health_point_label;
        CCLabel coin_count_label;
        //CCPoint character_enemy_place;
        const float timeToTake = 0.5f; // in seconds
        //int rrow;
        //int ccolumn;
        static bool[,] walls;
        static String[,] treasures;
        bool[,] doors;
        bool[,] finishes;
        List<int[]> char_pos = new List<int[]>();
        /*int[] char_pos = new int[2];
        int[] char_pos_enemy1 = new int[2];*/
        System.Drawing.Point char_diff;
        String _level;
        //MainLayer _mainLayer;
        DateTime date_start;
        int numberOfColumns;
        int numberOfRows;
        int door_key_count = 0;
        int treasure_key_count = 0;
        DateTime refresh_time;
        bool refreshed = true;
        TimeSpan temp;
        int starting_health_point;
        //int starting_coin_count;
        int health_point;
        int coin_count;
        int immortality_count;
        String _speed;
        String time_label_text;
        int Game_id;
        public int starting_coin_count = 0;
        static List<String> Player_ips = new List<String>();
        static int player_count = 0;

        //cocos2d.CCPoint center = new cocos2d.CCPoint(384 / 2, 240 / 2);

        //public CCLabel Time_label { get => time_label; set => time_label = value; }

        public GoldRushGame(String level, int hp, String speed, int id, List<String> players)
        {
            _level = level;
            //_mainLayer = mainLayer;
            starting_health_point = hp;
            _speed = speed;
            Game_id = id;
            Player_ips = players;
            player_count = Player_ips.Count;
            for (int i = 0; i < player_count; i++)
            {
                char_pos.Add(new int[2]);
            }
            //Timer t = new Timer(ShowTimer, null, 0, 100);

            /*Schedule(CollisionCheck, interval: 0.1f); //0.042f);
            Schedule(ShowTimer, interval: 1.0f);*/
            Console.WriteLine("Game Created");
            for (int i = 0; i < Player_ips.Count; i++)
            {
                TcpClient tcpClient = new TcpClient(Player_ips[i], 8889);
                NetworkStream clientStream = tcpClient.GetStream();
                byte[] outStream = System.Text.Encoding.ASCII.GetBytes("GoldRushStart;" + i);
                clientStream.Write(outStream, 0, outStream.Length);
                clientStream.Flush();
            }

            //CCSimpleAudioEngine.SharedEngine.PlayBackgroundMusic("sounds/backgroundmusic", true);
            //CCSimpleAudioEngine.SharedEngine.BackgroundMusicVolume = 0.3f;
            /*}
            protected override void AddedToScene()
            {
                base.AddedToScene();*/

            //tileMap = new cocos2d.CCTMXMapInfo("Assets/Content/tilemaps/dungeon_" + _level + ".tmx");

            var map = new TmxMap("Assets/Content/tilemaps/dungeon_" + _level + ".tmx");

            //tileMap.Antialiased = false;

            //this.AddChild(tileMap);

            HandleCustomTileProperties(map);

            foreach (var shooter in shooter_List)
            {
                int actual_c = shooter.column;
                int actual_r = shooter.row;
                bool go = true;

                while (go)
                {
                    if ((actual_c >= 0) && (actual_r >= 0) && (actual_c < numberOfColumns) && (actual_r < numberOfRows))
                    {
                        if (walls[actual_c, actual_r] == true)
                        {
                            wall = new Wall("shooter_wall");
                            wall.PositionX = 16 * actual_c + 8;
                            wall.PositionY = 16 * actual_r + 8;
                            shootingWall_List.Add(wall);
                            go = false;
                        }
                        if (shooter._rotation == 0)
                        {
                            actual_r++;
                        }
                        else if (shooter._rotation == 90)
                        {
                            actual_c++;
                        }
                        else if (shooter._rotation == 180)
                        {
                            actual_r--;
                        }
                        else if (shooter._rotation == 270)
                        {
                            actual_c--;
                        }
                    }
                    else
                    {
                        go = false;
                    }
                }
            }
            /*System.Drawing.Point a = new System.Drawing.Point(50, 50);
            System.Drawing.Point b = new System.Drawing.Point(40, 40);
            b = new System.Drawing.Point(a.X - b.X, a.Y - b.Y); ENNEK A MINTÁJÁRA TOLNI */
            // TODO Összes elem helyére tolása
            //Point center = new Point(384 / 2, 240 / 2);
            //CCPoint center = new CCPoint(384 / 2, 240 / 2);
            //CCPoint center2 = new CCPoint(center-center);
            /* char_diff = new Point(10, 10);
             //tileMap.TileLayersContainer.Position -= new CocosSharp.CCPoint(char_diff.X,char_diff.Y);

             foreach (var ce in charmodel_enemy_List)
             {
                 ce.Position -= new CCPoint(char_diff.X, char_diff.Y);
             }

             foreach (var treasure in treasure_List)
             {
                 treasure.Position -= new CCPoint(char_diff.X, char_diff.Y);
             }

             foreach (var shooter in shooter_List)
             {
                 shooter.Position -= new CCPoint(char_diff.X, char_diff.Y);
             }

             foreach (var wall in shootingWall_List)
             {
                 wall.Position -= new CCPoint(char_diff.X, char_diff.Y);
             }

             foreach (var door in door_List)
             {
                 door.Position -= new CCPoint(char_diff.X, char_diff.Y);
             }

             foreach (var doorkey in doorkey_List)
             {
                 doorkey.Position -= new CCPoint(char_diff.X, char_diff.Y);
             }

             foreach (var treasurekey in treasurekey_List)
             {
                 treasurekey.Position -= new CCPoint(char_diff.X, char_diff.Y);
             }

             foreach (var freezedrink in freezedrink_List)
             {
                 freezedrink.Position -= new CCPoint(char_diff.X, char_diff.Y);
             }

             foreach (var immortalitydrink in immortalitydrink_List)
             {
                 immortalitydrink.Position -= new CCPoint(char_diff.X, char_diff.Y);
             }

             foreach (var coin in coin_List)
             {
                 coin.Position -= new CCPoint(char_diff.X, char_diff.Y);
             }

             foreach (var finish in finish_List)
             {
                 finish.Position -= new CCPoint(char_diff.X, char_diff.Y);
             }*/

            // Tolás vége

            // Register for touch events
            //touchListener = new CCEventListenerTouchAllAtOnce();
            //touchListener.OnTouchesBegan = HandleInput;
            //AddEventListener(touchListener, this);

            /*Schedule(
               (dt) =>
               {
                   if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                   {
                       //_mainLayer.BackToMenu();
                       // TODO BACK TO MENU
                   }
               }

           );*/

            //Schedule();

            bullet_List = new List<Bullet>();
            BulletFactory.Self.BulletCreated += HandleBulletCreated;


            date_start = DateTime.Now;
            refresh_time = DateTime.Now;

            immortality_count = 0;
        }

        public void ShowTimer(Object o)
        {
            DateTime now = DateTime.Now;
            TimeSpan interval = now - date_start;
            /*TimeSpan slowed_time = now - date_start;// refresh_time;
            Console.WriteLine(interval==slowed_time);
            if (temp == null)
            {
                temp = interval;
            }

            if (interval == slowed_time)
            {
                temp = slowed_time;
            }
            else if ((interval > slowed_time) && (refreshed == false))
            {
                temp = interval;
                refreshed = true;
            }
            else if (slowed_time > temp)
            {
                date_start = refresh_time;
                temp = slowed_time;
            }*/

            //time_label.Text = temp.Minutes.ToString() + ":" + temp.Seconds.ToString();
            //time_label_text = temp.Minutes.ToString() + ":" + temp.Seconds.ToString();
            time_label_text = interval.Minutes.ToString() + ":" + interval.Seconds.ToString();
            Console.WriteLine(time_label_text);

        }/*
        void CollisionCheck(float unusedValue)
        {
            Console.WriteLine("sajt");
            if (health_point <= 0)
            {
                //_mainLayer.Died();
                // TODO DIE
            }
            List<Coin> tempcoin = new List<Coin>();
            foreach (var coin in coin_List)
            {
                tempcoin.Add(coin);
            }
            foreach (var coin in tempcoin)
            {
                foreach (var characterModel in charmodel_List)
                {
                    if (coin.BoundingBoxTransformedToWorld.IntersectsRect(characterModel.BoundingBoxTransformedToParent))
                    {
                        //CCSimpleAudioEngine.SharedEngine.PlayEffect("sounds/coineffect");
                        RemoveChild(coin);
                        coin_List.Remove(coin);
                        coin_count++;
                        coin_count_label.Text = "score: " + (coin_count).ToString();
                    }
                }
            }
            List<Bullet> temp_List = new List<Bullet>();
            foreach (var bullet in bullet_List)
            {
                temp_List.Add(bullet);
            }
            foreach (var bullet in temp_List)
            {
                foreach (var characterModel in charmodel_List)
                {
                    if (bullet.sprite.BoundingBoxTransformedToWorld.IntersectsRect(characterModel.BoundingBoxTransformedToParent))
                    {
                        RemoveChild(bullet);
                        bullet_List.Remove(bullet);
                        if (immortality_count == 0)
                        {
                            health_point--;
                            health_point_label.Text = "lives: " + health_point.ToString();
                        }
                        else if (immortality_count == 1)
                        {
                            foreach (var charmodel in charmodel_List)
                            {
                                charmodel.UnShield();
                                charmodel.ChangeSprite();
                            }
                            immortality_count--;
                        }
                        else
                        {
                            immortality_count--;
                        }
                    }
                }
                foreach (var wall in shootingWall_List)
                {
                    if (bullet.sprite.BoundingBoxTransformedToWorld.IntersectsRect(wall.BoundingBoxTransformedToWorld))
                    {
                        RemoveChild(bullet);
                        bullet_List.Remove(bullet);
                    }
                }
            }
            List<TreasureKey> temptreasure = new List<TreasureKey>();
            foreach (var treasurekey in treasurekey_List)
            {
                temptreasure.Add(treasurekey);
            }
            foreach (var treasurekey in temptreasure)
            {
                foreach (var characterModel in charmodel_List)
                {
                    if (treasurekey.BoundingBoxTransformedToWorld.IntersectsRect(characterModel.BoundingBoxTransformedToParent))
                    {
                        RemoveChild(treasurekey);
                        treasurekey_List.Remove(treasurekey);
                        treasure_key_count++;
                        treasure_key_label.Text = treasure_key_count.ToString();
                    }
                }
            }
            List<DoorKey> tempdoor = new List<DoorKey>();
            foreach (var doorkey in doorkey_List)
            {
                tempdoor.Add(doorkey);
            }
            foreach (var doorkey in tempdoor)
            {
                foreach (var characterModel in charmodel_List)
                {
                    if (doorkey.BoundingBoxTransformedToWorld.IntersectsRect(characterModel.BoundingBoxTransformedToParent))
                    {
                        RemoveChild(doorkey);
                        doorkey_List.Remove(doorkey);
                        door_key_count++;
                        door_key_label.Text = door_key_count.ToString();
                    }
                }
            }

            List<FreezeDrink> tempfreezedrink = new List<FreezeDrink>();
            foreach (var freezedrink in freezedrink_List)
            {
                tempfreezedrink.Add(freezedrink);
            }
            foreach (var freezedrink in tempfreezedrink)
            {
                foreach (var characterModel in charmodel_List)
                {
                    if (freezedrink.BoundingBoxTransformedToWorld.IntersectsRect(characterModel.BoundingBoxTransformedToParent))
                    {
                        RemoveChild(freezedrink);
                        freezedrink_List.Remove(freezedrink);
                        // Stop time
                        refresh_time = date_start.AddSeconds(10.0);
                        refreshed = false;
                    }
                }
            }

            List<ImmortalityDrink> tempimmortalitydrink = new List<ImmortalityDrink>();
            foreach (var immortalitydrink in immortalitydrink_List)
            {
                tempimmortalitydrink.Add(immortalitydrink);
            }
            foreach (var immortalitydrink in tempimmortalitydrink)
            {
                foreach (var characterModel in charmodel_List)
                {
                    if (immortalitydrink.BoundingBoxTransformedToWorld.IntersectsRect(characterModel.BoundingBoxTransformedToParent))
                    {
                        RemoveChild(immortalitydrink);
                        immortalitydrink_List.Remove(immortalitydrink);
                        // No damage for X collision
                        foreach (var charmodel in charmodel_List)
                        {
                            charmodel.Shield();
                            charmodel.ChangeSprite();
                        }
                        immortality_count += 5;
                    }
                }
            }
        }*/

        void HandleCustomTileProperties(TmxMap tileMap)
        {
            // Width and Height are equal so we can use either
            int tileDimension = tileMap.TileHeight;

            // Find out how many rows and columns are in our tile map
            numberOfColumns = tileMap.Width;
            numberOfRows = tileMap.Height;

            walls = new bool[numberOfColumns, numberOfRows];
            doors = new bool[numberOfColumns, numberOfRows];
            treasures = new String[numberOfColumns, numberOfRows];
            finishes = new bool[numberOfColumns, numberOfRows];
            for (int i = 0; i < numberOfColumns; i++)
            {
                for (int j = 0; j < numberOfRows; j++)
                {
                    walls[i, j] = false;
                    doors[i, j] = false;
                    finishes[i, j] = false;
                }
            }

            /*wall_id_List = new List<int>();
            charmodel_id1_List = new List<int>();
            charmodel_id2_List = new List<int>();
            charmodel_id3_List = new List<int>();
            charmodel_id4_List = new List<int>();
            charmodelenemy_id1_List = new List<int>();
            charmodelenemy_id2_List = new List<int>();
            charmodelenemy_id3_List = new List<int>();
            charmodelenemy_id4_List = new List<int>();*/


            foreach (var item in tileMap.Tilesets[0].Tiles)
            {
                int id = item.Value.Id;
                String temp = "";
                foreach (var properties in item.Value.Properties)
                {
                    if (properties.Key == "isWall" && properties.Value == "true")
                    {
                        wall_id_List.Add(id + 1);
                    }
                    if (properties.Key == "isDoor" && properties.Value == "true")
                    {
                        temp += "Door";
                    }
                    if (properties.Key == "isOpen" && properties.Value == "true")
                    {
                        temp += "opened";
                    }
                    if (properties.Key == "isOpen" && properties.Value == "false")
                    {
                        temp += "closed";
                    }
                    if (properties.Key == "IsTreasure" && properties.Value == "true")
                    {
                        treasure_id_List.Add(id + 1);
                    }

                    if (properties.Key == "isFinish" && properties.Value == "true")
                    {
                        finish_id_List.Add(id + 1);
                    }
                    if (properties.Key == "isCoin" && properties.Value == "true")
                    {
                        coin_id_List.Add(id + 1);
                    }
                    if (properties.Key == "isDrink" && properties.Value == "true")
                    {
                        temp += "Drink";
                    }
                    if (properties.Key == "type" && properties.Value == "freeze")
                    {
                        temp += "freeze";
                    }
                    if (properties.Key == "type" && properties.Value == "immortality")
                    {
                        temp += "immortality";
                    }
                    if (properties.Key == "isKey" && properties.Value == "true")
                    {
                        temp += "Key";
                    }
                    if (properties.Key == "type" && properties.Value == "door")
                    {
                        temp += "door";
                    }
                    if (properties.Key == "type" && properties.Value == "treasure")
                    {
                        temp += "treasure";
                    }
                    if (properties.Key == "isShooting" && properties.Value == "true")
                    {
                        temp += "Shooter";
                    }
                    if (properties.Key == "dir" && properties.Value == "up")
                    {
                        temp += "up";
                    }
                    if (properties.Key == "dir" && properties.Value == "down")
                    {
                        temp += "down";
                    }
                    if (properties.Key == "dir" && properties.Value == "left")
                    {
                        temp += "left";
                    }
                    if (properties.Key == "dir" && properties.Value == "right")
                    {
                        temp += "right";
                    }

                    if (properties.Key == "isCharacter" && properties.Value == "true")
                    {
                        temp += "Character";
                    }
                    if (properties.Key == "isEnemy" && properties.Value == "true")
                    {
                        temp += "EnemyTrue";
                    }
                    if (properties.Key == "isEnemy" && properties.Value == "false")
                    {
                        temp += "EnemyFalse";
                    }
                    if (properties.Key == "part" && properties.Value == "1")
                    {
                        temp += "part1";
                    }
                    if (properties.Key == "part" && properties.Value == "2")
                    {
                        temp += "part2";
                    }
                    if (properties.Key == "part" && properties.Value == "3")
                    {
                        temp += "part3";
                    }
                    if (properties.Key == "part" && properties.Value == "4")
                    {
                        temp += "part4";
                    }
                }
                if (temp.Equals("Keydoor"))
                {
                    doorkey_id_List.Add(id + 1);
                }
                else if (temp.Equals("Keytreasure"))
                {
                    treasurekey_id_List.Add(id + 1);
                }
                else if (temp.Equals("Drinkfreeze"))
                {
                    freezedrink_id_List.Add(id + 1);
                }
                else if (temp.Equals("Drinkimmortality"))
                {
                    immortalitydrink_id_List.Add(id + 1);
                }
                else if (temp.Equals("upShooter"))
                {
                    shooter_up_List.Add(id + 1);
                }
                else if (temp.Equals("downShooter"))
                {
                    shooter_down_List.Add(id + 1);
                }
                else if (temp.Equals("leftShooter"))
                {
                    shooter_left_List.Add(id + 1);
                }
                else if (temp.Equals("rightShooter"))
                {
                    shooter_right_List.Add(id + 1);
                }
                else if (temp.Equals("CharacterEnemyTruepart1"))
                {
                    charmodelenemy_id1_List.Add(id + 1);
                }
                else if (temp.Equals("CharacterEnemyTruepart2"))
                {
                    charmodelenemy_id2_List.Add(id + 1);
                }
                else if (temp.Equals("CharacterEnemyTruepart3"))
                {
                    charmodelenemy_id3_List.Add(id + 1);
                }
                else if (temp.Equals("CharacterEnemyTruepart4"))
                {
                    charmodelenemy_id4_List.Add(id + 1);
                }
                else if (temp.Equals("CharacterEnemyFalsepart1"))
                {
                    charmodel_id1_List.Add(id + 1);
                }
                else if (temp.Equals("CharacterEnemyFalsepart2"))
                {
                    charmodel_id2_List.Add(id + 1);
                }
                else if (temp.Equals("CharacterEnemyFalsepart3"))
                {
                    charmodel_id3_List.Add(id + 1);
                }
                else if (temp.Equals("CharacterEnemyFalsepart4"))
                {
                    charmodel_id4_List.Add(id + 1);
                }
                else if (temp.Equals("upDooropenedpart1"))
                {
                    door_upDooropenedpart1_id_List.Add(id + 1);
                }
                else if (temp.Equals("upDooropenedpart2"))
                {
                    door_upDooropenedpart2_id_List.Add(id + 1);
                }
                else if (temp.Equals("upDoorclosedpart1"))
                {
                    door_upDoorclosedpart1_id_List.Add(id + 1);
                }
                else if (temp.Equals("upDoorclosedpart2"))
                {
                    door_upDoorclosedpart2_id_List.Add(id + 1);
                }
                else if (temp.Equals("rightDooropenedpart1"))
                {
                    door_rightDooropenedpart1_id_List.Add(id + 1);
                }
                else if (temp.Equals("rightDooropenedpart2"))
                {
                    door_rightDooropenedpart2_id_List.Add(id + 1);
                }
                else if (temp.Equals("rightDoorclosedpart1"))
                {
                    door_rightDoorclosedpart1_id_List.Add(id + 1);
                }
                else if (temp.Equals("rightDoorclosedpart2"))
                {
                    door_rightDoorclosedpart2_id_List.Add(id + 1);
                }
                else if (temp.Equals("leftDooropenedpart1"))
                {
                    door_leftDooropenedpart1_id_List.Add(id + 1);
                }
                else if (temp.Equals("leftDooropenedpart2"))
                {
                    door_leftDooropenedpart2_id_List.Add(id + 1);
                }
                else if (temp.Equals("leftDoorclosedpart1"))
                {
                    door_leftDoorclosedpart1_id_List.Add(id + 1);
                }
                else if (temp.Equals("leftDoorclosedpart2"))
                {
                    door_leftDoorclosedpart2_id_List.Add(id + 1);
                }
                else if (temp.Equals("downDooropenedpart1"))
                {
                    door_downDooropenedpart1_id_List.Add(id + 1);
                }
                else if (temp.Equals("downDooropenedpart2"))
                {
                    door_downDooropenedpart2_id_List.Add(id + 1);
                }
                else if (temp.Equals("downDooropenedpart3"))
                {
                    door_downDooropenedpart3_id_List.Add(id + 1);
                }
                else if (temp.Equals("downDooropenedpart4"))
                {
                    door_downDooropenedpart4_id_List.Add(id + 1);
                }
                else if (temp.Equals("downDoorclosedpart1"))
                {
                    door_downDoorclosedpart1_id_List.Add(id + 1);
                }
                else if (temp.Equals("downDoorclosedpart2"))
                {
                    door_downDoorclosedpart2_id_List.Add(id + 1);
                }
                else if (temp.Equals("downDoorclosedpart3"))
                {
                    door_downDoorclosedpart3_id_List.Add(id + 1);
                }
                else if (temp.Equals("downDoorclosedpart4"))
                {
                    door_downDoorclosedpart4_id_List.Add(id + 1);
                }
            }

            var layeralso = tileMap.Layers[0];
            foreach (var tile in layeralso.Tiles)
            {
                if (wall_id_List.Contains(tile.Gid))
                {
                    // Create Wall Entity
                    walls[tile.X, tile.Y] = true;

                    // Create Wall Entity

                    wall = new Wall
                    {
                        PositionX = tile.X * 16,
                        PositionY = tile.Y * 16
                    };
                    wall_List.Add(wall);

                    //Console.WriteLine(tile.Gid + " : " + tile.X + " ; " + tile.Y);
                }

                // Create Door Entities
                else if (door_upDoorclosedpart1_id_List.Contains(tile.Gid))
                {
                    // Create Door
                    doors[tile.X, tile.Y] = true;
                    String s = "door_up";
                    // If closed
                    walls[tile.X, tile.Y] = true;

                    door = new Door(s, "false", "1");
                    door.PositionX = tile.X * 16;
                    door.PositionY = tile.Y * 16;
                    door_List.Add(door);
                }
                else if (door_upDoorclosedpart2_id_List.Contains(tile.Gid))
                {
                    // Create Door
                    doors[tile.X, tile.Y] = true;
                    String s = "door_up";
                    // If closed
                    walls[tile.X, tile.Y] = true;

                    door = new Door(s, "false", "2");
                    door.PositionX = tile.X * 16;
                    door.PositionY = tile.Y * 16;
                    door_List.Add(door);
                }
                else if (door_upDooropenedpart1_id_List.Contains(tile.Gid))
                {
                    // Create Door
                    doors[tile.X, tile.Y] = true;
                    String s = "door_up";

                    door = new Door(s, "true", "1");
                    door.PositionX = tile.X * 16;
                    door.PositionY = tile.Y * 16;
                    door_List.Add(door);
                }
                else if (door_upDooropenedpart2_id_List.Contains(tile.Gid))
                {
                    // Create Door
                    doors[tile.X, tile.Y] = true;
                    String s = "door_up";

                    door = new Door(s, "true", "2");
                    door.PositionX = tile.X * 16;
                    door.PositionY = tile.Y * 16;
                    door_List.Add(door);
                }
                else if (door_rightDoorclosedpart1_id_List.Contains(tile.Gid))
                {
                    // Create Door
                    doors[tile.X, tile.Y] = true;
                    String s = "door_right";
                    // If closed
                    walls[tile.X, tile.Y] = true;

                    door = new Door(s, "false", "1");
                    door.PositionX = tile.X * 16;
                    door.PositionY = tile.Y * 16;
                    door_List.Add(door);
                }
                else if (door_rightDoorclosedpart2_id_List.Contains(tile.Gid))
                {
                    // Create Door
                    doors[tile.X, tile.Y] = true;
                    String s = "door_right";
                    // If closed
                    walls[tile.X, tile.Y] = true;

                    door = new Door(s, "false", "2");
                    door.PositionX = tile.X * 16;
                    door.PositionY = tile.Y * 16;
                    door_List.Add(door);
                }
                else if (door_rightDooropenedpart1_id_List.Contains(tile.Gid))
                {
                    // Create Door
                    doors[tile.X, tile.Y] = true;
                    String s = "door_right";

                    door = new Door(s, "true", "1");
                    door.PositionX = tile.X * 16;
                    door.PositionY = tile.Y * 16;
                    door_List.Add(door);
                }
                else if (door_rightDooropenedpart2_id_List.Contains(tile.Gid))
                {
                    // Create Door
                    doors[tile.X, tile.Y] = true;
                    String s = "door_right";

                    door = new Door(s, "true", "2");
                    door.PositionX = tile.X * 16;
                    door.PositionY = tile.Y * 16;
                    door_List.Add(door);
                }
                else if (door_leftDoorclosedpart1_id_List.Contains(tile.Gid))
                {
                    // Create Door
                    doors[tile.X, tile.Y] = true;
                    String s = "door_left";
                    // If closed
                    walls[tile.X, tile.Y] = true;

                    door = new Door(s, "false", "1");
                    door.PositionX = tile.X * 16;
                    door.PositionY = tile.Y * 16;
                    door_List.Add(door);
                }
                else if (door_leftDoorclosedpart2_id_List.Contains(tile.Gid))
                {
                    // Create Door
                    doors[tile.X, tile.Y] = true;
                    String s = "door_left";
                    // If closed
                    walls[tile.X, tile.Y] = true;

                    door = new Door(s, "false", "2");
                    door.PositionX = tile.X * 16;
                    door.PositionY = tile.Y * 16;
                    door_List.Add(door);
                }
                else if (door_leftDooropenedpart1_id_List.Contains(tile.Gid))
                {
                    // Create Door
                    doors[tile.X, tile.Y] = true;
                    String s = "door_left";

                    door = new Door(s, "true", "1");
                    door.PositionX = tile.X * 16;
                    door.PositionY = tile.Y * 16;
                    door_List.Add(door);
                }
                else if (door_leftDooropenedpart2_id_List.Contains(tile.Gid))
                {
                    // Create Door
                    doors[tile.X, tile.Y] = true;
                    String s = "door_left";

                    door = new Door(s, "true", "2");
                    door.PositionX = tile.X * 16;
                    door.PositionY = tile.Y * 16;
                    door_List.Add(door);
                }
                else if (door_downDoorclosedpart1_id_List.Contains(tile.Gid))
                {
                    // Create Door
                    doors[tile.X, tile.Y] = true;
                    String s = "door_down";
                    // If closed
                    walls[tile.X, tile.Y] = true;

                    door = new Door(s, "false", "1");
                    door.PositionX = tile.X * 16;
                    door.PositionY = tile.Y * 16;
                    door_List.Add(door);
                }
                else if (door_downDoorclosedpart2_id_List.Contains(tile.Gid))
                {
                    // Create Door
                    doors[tile.X, tile.Y] = true;
                    String s = "door_down";
                    // If closed
                    walls[tile.X, tile.Y] = true;

                    door = new Door(s, "false", "2");
                    door.PositionX = tile.X * 16;
                    door.PositionY = tile.Y * 16;
                    door_List.Add(door);
                }
                else if (door_downDooropenedpart1_id_List.Contains(tile.Gid))
                {
                    // Create Door
                    doors[tile.X, tile.Y] = true;
                    String s = "door_down";

                    door = new Door(s, "true", "1");
                    door.PositionX = tile.X * 16;
                    door.PositionY = tile.Y * 16;
                    door_List.Add(door);
                }
                else if (door_downDooropenedpart2_id_List.Contains(tile.Gid))
                {
                    // Create Door
                    doors[tile.X, tile.Y] = true;
                    String s = "door_down";

                    door = new Door(s, "true", "2");
                    door.PositionX = tile.X * 16;
                    door.PositionY = tile.Y * 16;
                    door_List.Add(door);
                }
                else if (door_downDoorclosedpart3_id_List.Contains(tile.Gid))
                {
                    // Create Door
                    doors[tile.X, tile.Y] = true;
                    String s = "door_down";
                    // If closed
                    walls[tile.X, tile.Y] = true;

                    door = new Door(s, "false", "1");
                    door.PositionX = tile.X * 16;
                    door.PositionY = tile.Y * 16;
                    door_List.Add(door);
                }
                else if (door_downDoorclosedpart4_id_List.Contains(tile.Gid))
                {
                    // Create Door
                    doors[tile.X, tile.Y] = true;
                    String s = "door_down";
                    // If closed
                    walls[tile.X, tile.Y] = true;

                    door = new Door(s, "false", "2");
                    door.PositionX = tile.X * 16;
                    door.PositionY = tile.Y * 16;
                    door_List.Add(door);
                }
                else if (door_downDooropenedpart3_id_List.Contains(tile.Gid))
                {
                    // Create Door
                    doors[tile.X, tile.Y] = true;
                    String s = "door_down";

                    door = new Door(s, "true", "1");
                    door.PositionX = tile.X * 16;
                    door.PositionY = tile.Y * 16;
                    door_List.Add(door);
                }
                else if (door_downDooropenedpart4_id_List.Contains(tile.Gid))
                {
                    // Create Door
                    doors[tile.X, tile.Y] = true;
                    String s = "door_down";

                    door = new Door(s, "true", "2");
                    door.PositionX = tile.X * 16;
                    door.PositionY = tile.Y * 16;
                    door_List.Add(door);
                }
            }

            var layerfelso = tileMap.Layers[1];
            foreach (var tile in layerfelso.Tiles)
            {
                if (treasure_id_List.Contains(tile.Gid))
                {
                    // Create a Treasure Chest Entity
                    walls[tile.X, tile.Y] = true;
                    treasures[tile.X, tile.Y] = "closed";
                    treasure = new Treasure("treasure");
                    treasure.PositionX = tile.X * 16;
                    treasure.PositionY = tile.Y * 16;
                    treasure_List.Add(treasure);
                }
                if (finish_id_List.Contains(tile.Gid))
                {
                    // Create Finish Entity

                    finishes[tile.X, tile.Y] = true;

                    finish = new Finish();
                    finish.PositionX = tile.X * 16;
                    finish.PositionY = tile.Y * 16;
                    finish_List.Add(finish);
                }
                if (coin_id_List.Contains(tile.Gid))
                {
                    // Create Coin Entity

                    coin = new Coin();
                    coin.PositionX = tile.X * 16;
                    coin.PositionY = tile.Y * 16;
                    coin_List.Add(coin);
                    starting_coin_count++;
                }
                if (doorkey_id_List.Contains(tile.Gid))
                {
                    // Create Door Key Entity
                    doorkey = new DoorKey();
                    doorkey.PositionX = tile.X * 16;
                    doorkey.PositionY = tile.Y * 16;
                    doorkey_List.Add(doorkey);
                }
                if (treasurekey_id_List.Contains(tile.Gid))
                {
                    // Create Treasure Key Entity
                    treasurekey = new TreasureKey();
                    treasurekey.PositionX = tile.X * 16;
                    treasurekey.PositionY = tile.Y * 16;
                    treasurekey_List.Add(treasurekey);
                }
                if (shooter_down_List.Contains(tile.Gid))
                {
                    // Create Shooting Down Entity

                    int x = 0;
                    int y = -20;

                    cannon = new Cannon(x, y, "down", _speed);

                    cannon.PositionX = tile.X * 16;
                    cannon.PositionY = tile.Y * 16;

                    cannon.row = tile.X;
                    cannon.column = tile.Y;

                    shooter_List.Add(cannon);
                }
                if (shooter_up_List.Contains(tile.Gid))
                {
                    // Create Shooting Up Entity

                    int x = 0;
                    int y = 20;

                    cannon = new Cannon(x, y, "up", _speed);

                    cannon.PositionX = tile.X * 16;
                    cannon.PositionY = tile.Y * 16;

                    cannon.row = tile.X;
                    cannon.column = tile.Y;

                    shooter_List.Add(cannon);
                }
                if (shooter_left_List.Contains(tile.Gid))
                {
                    // Create Shooting Entity

                    int x = -20;
                    int y = 0;

                    cannon = new Cannon(x, y, "left", _speed);

                    cannon.PositionX = tile.X * 16;
                    cannon.PositionY = tile.Y * 16;

                    cannon.row = tile.X;
                    cannon.column = tile.Y;

                    shooter_List.Add(cannon);
                }
                if (shooter_right_List.Contains(tile.Gid))
                {
                    // Create Shooting Entity

                    int x = 20;
                    int y = 0;

                    cannon = new Cannon(x, y, "right", _speed);

                    cannon.PositionX = tile.X * 16;
                    cannon.PositionY = tile.Y * 16;

                    cannon.row = tile.X;
                    cannon.column = tile.Y;

                    shooter_List.Add(cannon);
                }
                // Create Freeze Drink Entity
                if (freezedrink_id_List.Contains(tile.Gid))
                {
                    freezedrink = new FreezeDrink();
                    freezedrink.PositionX = tile.X * 16;
                    freezedrink.PositionY = tile.Y * 16;
                    freezedrink_List.Add(freezedrink);
                }
                // Create Immortality Drink Entity
                if (immortalitydrink_id_List.Contains(tile.Gid))
                {
                    immortalitydrink = new ImmortalityDrink();
                    immortalitydrink.PositionX = tile.X * 16;
                    immortalitydrink.PositionY = tile.Y * 16;
                    immortalitydrink_List.Add(immortalitydrink);
                }
                if (charmodel_id1_List.Contains(tile.Gid))
                {
                    // Create a Character Entity
                    character = new CharacterModel("pplayer_1_", "1");

                    character.PositionX = 184;
                    character.PositionY = 128;

                    charmodel_List.Add(character);
                    character_place = new System.Drawing.Point(tile.X * 16, tile.Y * 16);
                    char_pos[0][0] = tile.X;
                    char_pos[0][1] = tile.Y + 1;

                }
                else if (charmodel_id2_List.Contains(tile.Gid))
                {
                    // Create a Character Entity
                    character = new CharacterModel("pplayer_1_", "2");

                    character.PositionX = 200;
                    character.PositionY = 128;

                    charmodel_List.Add(character);
                }
                else if (charmodel_id3_List.Contains(tile.Gid))
                {
                    // Create a Character Entity
                    character = new CharacterModel("pplayer_1_", "3");

                    character.PositionX = 184;
                    character.PositionY = 112;

                    charmodel_List.Add(character);
                }
                else if (charmodel_id4_List.Contains(tile.Gid))
                {
                    // Create a Character Entity
                    character = new CharacterModel("pplayer_1_", "4");

                    character.PositionX = 200;
                    character.PositionY = 112;

                    charmodel_List.Add(character);
                }

                else if (charmodelenemy_id1_List.Contains(tile.Gid))
                {
                    // Create a Character Entity
                    character = new CharacterModel("pplayer_2_", "1");

                    character.PositionX = 184;
                    character.PositionY = 128;


                    if (player_count > 1)
                    {
                        charmodel_enemy_List.Add(character);
                        character_place = new System.Drawing.Point(tile.X * 16, tile.Y * 16);
                        char_pos[1][0] = tile.X;
                        char_pos[1][1] = tile.Y + 1;
                    }
                }

                else if (charmodelenemy_id2_List.Contains(tile.Gid))
                {
                    // Create a Character Entity
                    character = new CharacterModel("pplayer_2_", "2");

                    character.PositionX = 200;
                    character.PositionY = 128;
                    if (player_count > 1)
                    {
                        charmodel_enemy_List.Add(character);
                    }
                }

                else if (charmodelenemy_id3_List.Contains(tile.Gid))
                {
                    // Create a Character Entity
                    character = new CharacterModel("pplayer_2_", "3");

                    character.PositionX = 184;
                    character.PositionY = 112;
                    if (player_count > 1)
                    {
                        charmodel_enemy_List.Add(character);
                    }
                }

                else if (charmodelenemy_id4_List.Contains(tile.Gid))
                {
                    // Create a Character Entity
                    character = new CharacterModel("pplayer_2_", "4");

                    character.PositionX = 200;
                    character.PositionY = 112;
                    if (player_count > 1)
                    {
                        charmodel_enemy_List.Add(character);
                    }
                }
            }
        }

        public void Step_Right(int num)
        {
            if (walls[char_pos[num][0] + 2, char_pos[num][1]] == false)
            {
                if (finishes[char_pos[num][0] + 2, char_pos[num][1]] == true)
                {
                    // _mainLayer.Victory(time_label.Text, _level, starting_coin_count - coin_count, starting_health_point - health_point);
                    // TODO VICTORY
                    Console.WriteLine("Game Won");
                    for (int i = 0; i < Player_ips.Count; i++)
                    {
                        TcpClient tcpClient = new TcpClient(Player_ips[i], 8891);
                        NetworkStream clientStream = tcpClient.GetStream();
                        byte[] outStream = System.Text.Encoding.ASCII.GetBytes("Victory;" + i);
                        clientStream.Write(outStream, 0, outStream.Length);
                        clientStream.Flush();
                    }
                }
                int move_unit;
                if (doors[char_pos[num][0] + 2, char_pos[num][1]] == true)
                {
                    move_unit = -64;
                    char_pos[num][0] = char_pos[num][0] + 4;
                }
                else
                {
                    move_unit = -16;
                    char_pos[num][0] = char_pos[num][0] + 1;
                }
                //tileMap.TileLayersContainer.Position += new CCPoint(move_unit, 0);

                foreach (var cme in charmodel_enemy_List)
                {
                    cme.MoveX(move_unit);
                }

                foreach (var treasure in treasure_List)
                {
                    treasure.MoveX(move_unit);
                }

                foreach (var bullet in bullet_List)
                {
                    bullet.MoveX(move_unit);
                }
                foreach (var shooter in shooter_List)
                {
                    shooter.MoveX(move_unit);
                }
                foreach (var wall in shootingWall_List)
                {
                    wall.MoveX(move_unit);
                }
                foreach (var door in door_List)
                {
                    door.MoveX(move_unit);
                }
                foreach (var doorkey in doorkey_List)
                {
                    doorkey.MoveX(move_unit);
                }

                foreach (var treasurekey in treasurekey_List)
                {
                    treasurekey.MoveX(move_unit);
                }

                foreach (var freezedrink in freezedrink_List)
                {
                    freezedrink.MoveX(move_unit);
                }

                foreach (var immortalitydrink in immortalitydrink_List)
                {
                    immortalitydrink.MoveX(move_unit);
                }

                foreach (var coin in coin_List)
                {
                    coin.MoveX(move_unit);
                }
                foreach (var finish in finish_List)
                {
                    finish.MoveX(move_unit);
                }
            }
        }

        public void Step_Left(int num)
        {
            if (walls[char_pos[num][0] - 1, char_pos[num][1]] == false)
            {
                if (finishes[char_pos[num][0] - 1, char_pos[num][1]] == true)
                {
                    // _mainLayer.Victory(time_label.Text, _level, starting_coin_count - coin_count, starting_health_point - health_point);
                    // TODO VICTORY
                    Console.WriteLine("Game Won");
                    for (int i = 0; i < Player_ips.Count; i++)
                    {
                        TcpClient tcpClient = new TcpClient(Player_ips[i], 8891);
                        NetworkStream clientStream = tcpClient.GetStream();
                        byte[] outStream = System.Text.Encoding.ASCII.GetBytes("Victory;" + i);
                        clientStream.Write(outStream, 0, outStream.Length);
                        clientStream.Flush();
                    }
                }
                int move_unit;
                if (doors[char_pos[num][0] - 1, char_pos[num][1]] == true)
                {
                    move_unit = 64;
                    char_pos[num][0] = char_pos[num][0] - 4;
                }
                else
                {
                    move_unit = 16;
                    char_pos[num][0] = char_pos[num][0] - 1;
                }
                //tileMap.TileLayersContainer.Position += new CCPoint(move_unit, 0);

                foreach (var cme in charmodel_enemy_List)
                {
                    cme.MoveX(move_unit);
                }

                foreach (var treasure in treasure_List)
                {
                    treasure.MoveX(move_unit);
                }

                foreach (var bullet in bullet_List)
                {
                    bullet.MoveX(move_unit);
                }

                foreach (var shooter in shooter_List)
                {
                    shooter.MoveX(move_unit);
                }
                foreach (var wall in shootingWall_List)
                {
                    wall.MoveX(move_unit);
                }
                foreach (var door in door_List)
                {
                    door.MoveX(move_unit);
                }
                foreach (var doorkey in doorkey_List)
                {
                    doorkey.MoveX(move_unit);
                }

                foreach (var treasurekey in treasurekey_List)
                {
                    treasurekey.MoveX(move_unit);
                }

                foreach (var freezedrink in freezedrink_List)
                {
                    freezedrink.MoveX(move_unit);
                }

                foreach (var immortalitydrink in immortalitydrink_List)
                {
                    immortalitydrink.MoveX(move_unit);
                }

                foreach (var coin in coin_List)
                {
                    coin.MoveX(move_unit);
                }
                foreach (var finish in finish_List)
                {
                    finish.MoveX(move_unit);
                }
            }
        }

        public void Step_Down(int num)
        {
            if ((walls[char_pos[num][0], char_pos[num][1] + 1] == false) && (walls[char_pos[num][0] + 1, char_pos[num][1] + 1] == false))
            {
                if (finishes[char_pos[num][0], char_pos[num][1] + 1] == true)
                {
                    // _mainLayer.Victory(time_label.Text, _level, starting_coin_count - coin_count, starting_health_point - health_point);
                    // TODO VICTORY
                    Console.WriteLine("Game Won");
                    for (int i = 0; i < Player_ips.Count; i++)
                    {
                        TcpClient tcpClient = new TcpClient(Player_ips[i], 8891);
                        NetworkStream clientStream = tcpClient.GetStream();
                        byte[] outStream = System.Text.Encoding.ASCII.GetBytes("Victory;" + i);
                        clientStream.Write(outStream, 0, outStream.Length);
                        clientStream.Flush();
                    }
                }
                int move_unit;
                if ((doors[char_pos[num][0], char_pos[num][1] + 1] == true) && (doors[char_pos[num][0] + 1, char_pos[num][1] + 1] == true))
                {
                    move_unit = -64;
                    char_pos[num][1] = char_pos[num][1] + 4;
                }
                else
                {
                    move_unit = -16;
                    char_pos[num][1] = char_pos[num][1] + 1;
                }
                //tileMap.TileLayersContainer.Position += new CCPoint(0, move_unit);

                foreach (var cme in charmodel_enemy_List)
                {
                    cme.MoveY(move_unit);
                }

                foreach (var treasure in treasure_List)
                {
                    treasure.MoveY(move_unit);
                }

                foreach (var bullet in bullet_List)
                {
                    bullet.MoveY(move_unit);
                }

                foreach (var shooter in shooter_List)
                {
                    shooter.MoveY(move_unit);
                }
                foreach (var wall in shootingWall_List)
                {
                    wall.MoveY(move_unit);
                }
                foreach (var door in door_List)
                {
                    door.MoveY(move_unit);
                }
                foreach (var doorkey in doorkey_List)
                {
                    doorkey.MoveY(move_unit);
                }

                foreach (var treasurekey in treasurekey_List)
                {
                    treasurekey.MoveY(move_unit);
                }
                foreach (var freezedrink in freezedrink_List)
                {
                    freezedrink.MoveY(move_unit);
                }

                foreach (var immortalitydrink in immortalitydrink_List)
                {
                    immortalitydrink.MoveY(move_unit);
                }

                foreach (var coin in coin_List)
                {
                    coin.MoveY(move_unit);
                }
                foreach (var finish in finish_List)
                {
                    finish.MoveY(move_unit);
                }
            }
        }

        public void Step_Up(int num)
        {
            if (walls[char_pos[num][0], char_pos[num][1] - 1] == false)
            {
                if (finishes[char_pos[num][0], char_pos[num][1] - 1] == true)
                {
                    //_mainLayer.Victory(time_label.Text, _level, starting_coin_count - coin_count, starting_health_point - health_point);
                    // TODO VICTORY
                    for (int i = 0; i < Player_ips.Count; i++)
                    {
                        TcpClient tcpClient = new TcpClient(Player_ips[i], 8891);
                        NetworkStream clientStream = tcpClient.GetStream();
                        byte[] outStream = System.Text.Encoding.ASCII.GetBytes("Victory;" + i);
                        clientStream.Write(outStream, 0, outStream.Length);
                        clientStream.Flush();
                    }
                    Console.WriteLine("Game Won");

                }
                int move_unit;
                if (doors[char_pos[num][0], char_pos[num][1] - 1] == true)
                {
                    move_unit = 64;
                    char_pos[num][1] = char_pos[num][1] - 4;
                }
                else
                {
                    move_unit = 16;
                    char_pos[num][1] = char_pos[num][1] - 1;
                }
                //tileMap.TileLayersContainer.Position += new CCPoint(0, move_unit);

                foreach (var cme in charmodel_enemy_List)
                {
                    cme.MoveY(move_unit);
                }

                foreach (var treasure in treasure_List)
                {
                    treasure.MoveY(move_unit);
                }
                foreach (var bullet in bullet_List)
                {
                    bullet.MoveY(move_unit);
                }

                foreach (var shooter in shooter_List)
                {
                    shooter.MoveY(move_unit);
                }
                foreach (var wall in shootingWall_List)
                {
                    wall.MoveY(move_unit);
                }
                foreach (var door in door_List)
                {
                    door.MoveY(move_unit);
                }
                foreach (var doorkey in doorkey_List)
                {
                    doorkey.MoveY(move_unit);
                }

                foreach (var treasurekey in treasurekey_List)
                {
                    treasurekey.MoveY(move_unit);
                }
                foreach (var freezedrink in freezedrink_List)
                {
                    freezedrink.MoveY(move_unit);
                }

                foreach (var immortalitydrink in immortalitydrink_List)
                {
                    immortalitydrink.MoveY(move_unit);
                }

                foreach (var coin in coin_List)
                {
                    coin.MoveY(move_unit);
                }
                foreach (var finish in finish_List)
                {
                    finish.MoveY(move_unit);
                }
            }
        }

        public void Use_Special(String ip)
        {/*
            //bal alsó negyed
            bool opened = false;
            CCPoint location = new CCPoint(charmodel_List[0].PositionX - 16, charmodel_List[0].PositionY);
            foreach (var treasure in treasure_List)
            {
                if (treasure.BoundingBoxTransformedToWorld.ContainsPoint(location))
                {
                    if (treasure_key_count > 0)
                    {
                        treasure_key_count--;
                        treasure_key_label.Text = treasure_key_count.ToString();
                        treasure.Interact();
                    }
                }
            }
            foreach (var door in door_List)
            {
                if (door.BoundingBoxTransformedToWorld.ContainsPoint(location))
                {
                    if (door_key_count > 0)
                    {
                        door.Open();
                        opened = true;
                        walls[char_pos[0] - 1, char_pos[1]] = false;
                    }
                }
            }
            location = new CCPoint(charmodel_List[0].PositionX, charmodel_List[0].PositionY - 16);
            foreach (var treasure in treasure_List)
            {
                if (treasure.BoundingBoxTransformedToWorld.ContainsPoint(location))
                {
                    if (treasure_key_count > 0)
                    {
                        treasure_key_count--;
                        treasure_key_label.Text = treasure_key_count.ToString();
                        treasure.Interact();
                    }
                }
            }
            foreach (var door in door_List)
            {
                if (door.BoundingBoxTransformedToWorld.ContainsPoint(location))
                {
                    if (door_key_count > 0)
                    {
                        door.Open();
                        opened = true;
                        walls[char_pos[0], char_pos[1] - 1] = false;
                    }
                }
            }

            //bal felső negyed
            location = new CCPoint(charmodel_List[1].PositionX - 16, charmodel_List[1].PositionY);
            foreach (var treasure in treasure_List)
            {
                if (treasure.BoundingBoxTransformedToWorld.ContainsPoint(location))
                {
                    if (treasure_key_count > 0)
                    {
                        treasure_key_count--;
                        treasure_key_label.Text = treasure_key_count.ToString();
                        treasure.Interact();
                    }
                }
            }
            foreach (var door in door_List)
            {
                if (door.BoundingBoxTransformedToWorld.ContainsPoint(location))
                {
                    if (door_key_count > 0)
                    {
                        door.Open();
                        opened = true;
                        walls[char_pos[0] - 1, char_pos[1]] = false;
                    }
                }
            }
            location = new CCPoint(charmodel_List[1].PositionX, charmodel_List[1].PositionY);
            foreach (var treasure in treasure_List)
            {
                if (treasure.BoundingBoxTransformedToWorld.ContainsPoint(location))
                {
                    if (treasure_key_count > 0)
                    {
                        treasure_key_count--;
                        treasure_key_label.Text = treasure_key_count.ToString();
                        treasure.Interact();
                    }
                }
            }
            foreach (var door in door_List)
            {
                if (door.BoundingBoxTransformedToWorld.ContainsPoint(location))
                {
                    if (door_key_count > 0)
                    {
                        door.Open();
                        opened = true;
                        walls[char_pos[0], char_pos[1] + 1] = false;
                    }
                }
            }
            location = new CCPoint(charmodel_List[1].PositionX, charmodel_List[1].PositionY + 16);
            foreach (var door in door_List)
            {
                if (door.BoundingBoxTransformedToWorld.ContainsPoint(location))
                {
                    if (door_key_count > 0)
                    {
                        door.Open();
                        opened = true;
                        walls[char_pos[0], char_pos[1] + 2] = false;
                    }
                }
            }

            //jobb alsó negyed
            location = new CCPoint(charmodel_List[2].PositionX, charmodel_List[2].PositionY - 16);
            foreach (var treasure in treasure_List)
            {
                if (treasure.BoundingBoxTransformedToWorld.ContainsPoint(location))
                {
                    if (treasure_key_count > 0)
                    {
                        treasure_key_count--;
                        treasure_key_label.Text = treasure_key_count.ToString();
                        treasure.Interact();
                    }
                }
            }
            foreach (var door in door_List)
            {
                if (door.BoundingBoxTransformedToWorld.ContainsPoint(location))
                {
                    if (door_key_count > 0)
                    {
                        door.Open();
                        opened = true;
                        walls[char_pos[0], char_pos[1] - 1] = false;
                    }
                }
            }
            location = new CCPoint(charmodel_List[2].PositionX + 16, charmodel_List[2].PositionY);
            foreach (var treasure in treasure_List)
            {
                if (treasure.BoundingBoxTransformedToWorld.ContainsPoint(location))
                {
                    if (treasure_key_count > 0)
                    {
                        treasure_key_count--;
                        treasure_key_label.Text = treasure_key_count.ToString();
                        treasure.Interact();
                    }
                }
            }
            foreach (var door in door_List)
            {
                if (door.BoundingBoxTransformedToWorld.ContainsPoint(location))
                {
                    if (door_key_count > 0)
                    {
                        door.Open();
                        opened = true;
                        walls[char_pos[0] + 2, char_pos[1]] = false;
                    }
                }
            }

            //jobb felső negyed
            location = new CCPoint(charmodel_List[3].PositionX + 16, charmodel_List[3].PositionY);
            foreach (var treasure in treasure_List)
            {
                if (treasure.BoundingBoxTransformedToWorld.ContainsPoint(location))
                {
                    if (treasure_key_count > 0)
                    {
                        treasure_key_count--;
                        treasure_key_label.Text = treasure_key_count.ToString();
                        treasure.Interact();
                    }
                }
            }
            foreach (var door in door_List)
            {
                if (door.BoundingBoxTransformedToWorld.ContainsPoint(location))
                {
                    if (door_key_count > 0)
                    {
                        door.Open();
                        opened = true;
                        walls[char_pos[0] + 2, char_pos[1]] = false;
                    }
                }
            }
            location = new CCPoint(charmodel_List[3].PositionX, charmodel_List[3].PositionY);
            foreach (var treasure in treasure_List)
            {
                if (treasure.BoundingBoxTransformedToWorld.ContainsPoint(location))
                {
                    if (treasure_key_count > 0)
                    {
                        treasure_key_count--;
                        treasure_key_label.Text = treasure_key_count.ToString();
                        treasure.Interact();
                    }
                }
            }
            foreach (var door in door_List)
            {
                if (door.BoundingBoxTransformedToWorld.ContainsPoint(location))
                {
                    if (door_key_count > 0)
                    {
                        door.Open();
                        opened = true;
                        walls[char_pos[0] + 1, char_pos[1] + 1] = false;
                    }
                }
            }
            location = new CCPoint(charmodel_List[3].PositionX, charmodel_List[3].PositionY + 16);
            foreach (var door in door_List)
            {
                if (door.BoundingBoxTransformedToWorld.ContainsPoint(location))
                {
                    if (door_key_count > 0)
                    {
                        door.Open();
                        opened = true;
                        walls[char_pos[0] + 1, char_pos[1] + 2] = false;
                    }
                }
            }
            if (opened)
            {
                door_key_count--;
                door_key_label.Text = door_key_count.ToString();
            }*/
        }

        public String GetCharacterPosition(int num)
        {
            String temp;
            temp = char_pos[num][0] + ";" + char_pos[num][1];
            return temp;
        }

        public void Open(int x, int y)
        {
            walls[x, y] = false;
        }

        public void Treasure(int x, int y)
        {
            if (treasures[x, y].Equals("closed"))
            {
                treasures[x, y] = "opened";
            }
            else
            {
                treasures[x, y] = "closed";
            }
        }

        public int GetPlayerNumber()
        {
            return player_count;
        }

        public String GetIps(int id)
        {
            return Player_ips[id];
        }

        public void RemoveFromGame(int id)
        {
            Player_ips.RemoveAt(id);
            player_count--;
        }

        void HandleBulletCreated(Bullet newBullet)
        {
            //AddChild(newBullet);
            bullet_List.Add(newBullet);
        }
    }
}