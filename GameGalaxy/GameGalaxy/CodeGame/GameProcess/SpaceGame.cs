using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
using System.IO;

namespace GameGalaxy
{
    class SpaceGame
    {
        private static Star[] _stars;
        private static List<Shot> _shots = new List<Shot>();
        private static List<ShotEnemy> _shotsEnemy = new List<ShotEnemy>();
        private static Enemy[] _enemies;
        private static int _checker = 0;
        private static int _score = 0;
        public static Random Random = new Random();
        public static Texture2D Texture2DGameScreen { get; set; }
        public static Texture2D Texture2DHeart { get; set; }
        public static SpriteBatch SpriteBatch { get; set; }
        public static Spaceship Spaceship { get; set; }                
        public static SoundEffect BoomSound;
        public static SoundEffectInstance SoundEffectInstanceBoom;
        public static SoundEffect BoomShipSound;
        public static SoundEffectInstance SoundEffectInstanceBoomShip;
        public static int Width, Height;
        public static int HeartNumber = 3;
        public static bool IsGameOver = false;

        public static int GetIntRandom(int min, int max)
        {
            return Random.Next(min, max);
        }

        public static void ShipShot()
        {
            _shots.Add(new Shot(Spaceship.GetPositionForShot));
        }

        public static void Initialization(SpriteBatch spriteBatch, int width, int height)
        {
            SpaceGame.Width = width;
            SpaceGame.Height = height;
            SpaceGame.SpriteBatch = spriteBatch;
            _stars = new Star[50];
            _enemies = new Enemy[6];
            _checker = 0;

            for (int i = 0; i < _stars.Length; i++)
            {
                _stars[i] = new Star(new Vector2(0, Random.Next(1, 10)));
            }

            Enemies enemy = Enemies.Enemy1;
            for (int i = 0; i < _enemies.Length; i++)
            {
                if (i == 0 || i == 3 || i == 5) enemy = Enemies.Enemy1;
                if (i == 1 || i == 4) enemy = Enemies.Enemy2;
                if (i == 2) enemy = Enemies.Enemy3;
                _enemies[i] = new Enemy(enemy);
            }

            for (int j = 0; j < _enemies.Length - 1; j++)
            {
                for (int i = 0; i < _enemies.Length - 1 - j; i++)
                {
                    if (_enemies[i].GetPosition.X <= _enemies[i + 1].GetPosition.X + 200 && _enemies[i].GetPosition.X >= _enemies[i + 1].GetPosition.X - 200)
                    {
                        if (_enemies[i].GetPosition.X - 250 < 0)
                        {
                            _enemies[i].SetX(250);
                        }
                        else
                        {
                            _enemies[i].SetX(-250);
                        }
                    }

                    if (_enemies[i].GetPosition.Y <= _enemies[i + 1].GetPosition.Y + 200 && _enemies[i].GetPosition.Y >= _enemies[i + 1].GetPosition.Y - 200)
                    {
                        _enemies[i].SetY(-250);
                    }
                }
            }
            Spaceship = new Spaceship(new Vector2(Width / 2 - 40, Height - 91));
        }

        public static void Draw()
        {
            SpriteBatch.Draw(Texture2DGameScreen, new Rectangle(0, 0, 1100, 800), Color.White);
            SpriteBatch.DrawString(Screensaver.FontMadeBy, "Score: " + _score, new Vector2(5, 45), Color.White);
            for (int i = 0; i < HeartNumber; i++)
            {
                SpriteBatch.Draw(Texture2DHeart, new Rectangle(5 + i * 35, 5, 30, 30), Color.White);
            }
            foreach (Star star in _stars)
            {
                star.DrawStar();
            }

            Spaceship.DrawSpaceship();
            foreach (Enemy enemy in _enemies)
            {
                enemy.DrawEnemy();
            }
            foreach (Shot shot in _shots)
            {
                shot.DrawShot();
            }
            foreach (ShotEnemy shotEnemy in _shotsEnemy)
            {
                shotEnemy.DrawShot();
            }
        }

        private static bool CollideShipAndEnemy(Enemy enemy)
        {
            int width = 1, height = 1;
            Rectangle ship = new Rectangle((int)Spaceship.GetPosition.X, (int)Spaceship.GetPosition.Y, 76, 88);
            switch (enemy.GetEnemy())
            {
                case Enemies.Enemy1:
                    width = 76;
                    height = 65;
                    break;
                case Enemies.Enemy2:
                    width = 64;
                    height = 86;
                    break;
                case Enemies.Enemy3:
                    width = 86;
                    height = 75;
                    break;
            }
            Rectangle enemies = new Rectangle((int)enemy.GetPosition.X, (int)enemy.GetPosition.Y + 15, width, height);
            return ship.Intersects(enemies);
        }

        private static bool CollideEnemyAndShipShot(Enemy enemy, Shot shots)
        {
            int width = 1, height = 1;
            Rectangle shot = new Rectangle((int)shots.GetPosition.X, (int)shots.GetPosition.Y, 60, 35);
            switch (enemy.GetEnemy())
            {
                case Enemies.Enemy1:
                    width = 76;
                    height = 65;
                    break;
                case Enemies.Enemy2:
                    width = 64;
                    height = 86;
                    break;
                case Enemies.Enemy3:
                    width = 86;
                    height = 75;
                    break;
            }
            Rectangle enemies = new Rectangle((int)enemy.GetPosition.X, (int)enemy.GetPosition.Y, width, height);
            return enemies.Intersects(shot);
        }

        private static bool CollideEnemyShotAndShip(ShotEnemy shotEnemy)
        {
            Rectangle ship = new Rectangle((int)Spaceship.GetPosition.X, (int)Spaceship.GetPosition.Y, 72, 88);
            Rectangle shot = new Rectangle((int)shotEnemy.GetPosition.X, (int)shotEnemy.GetPosition.Y, 20, 20);
            return ship.Intersects(shot);
        }

        public static void Update()
        {
            foreach (Star star in _stars)
            {
                star.UpdateStar();
            }
            foreach (Enemy enemy in _enemies)
            {
                enemy.Move();
                switch (enemy.GetEnemy())
                {
                    case Enemies.Enemy1:
                        if(_checker == 0 || _checker == 100)
                        {
                            _shotsEnemy.Add(new ShotEnemy(enemy.GetPositionForShot()));
                        }
                        if (_checker == 200)
                        {
                            _checker = 0;
                        }
                        break;
                    case Enemies.Enemy2:
                        if (_checker == 0 || _checker == 50 || _checker == 100 || _checker == 150)
                        {
                            _shotsEnemy.Add(new ShotEnemy(enemy.GetPositionForShot()));
                        }
                        if(_checker == 200)
                        {
                            _checker = 0;
                        }
                        break;
                    case Enemies.Enemy3:
                        if (_checker == 0 || _checker == 30 || _checker == 60 || _checker == 90 || _checker == 120 || _checker == 150 || _checker == 180)
                        {
                            _shotsEnemy.Add(new ShotEnemy(enemy.GetPositionForShot()));
                        }
                        if (_checker == 200)
                        {
                            _checker = 0;
                        }
                        break;
                }
            }
            _checker++;

            for (int i = 0; i < _shots.Count; i++)
            {
                _shots[i].UpdateShot();
                if(_shots[i].NotOnScreen)
                {
                    _shots.RemoveAt(i);
                    i--;                  
                }
            }

            for (int i = 0; i < _shotsEnemy.Count; i++)
            {
                _shotsEnemy[i].UpdateShot();
                if (_shotsEnemy[i].NotOnScreen)
                {
                    _shotsEnemy.RemoveAt(i);
                    i--;
                }
            }

            for (int i = 0; i < _shotsEnemy.Count; i++)
            {
                if (CollideEnemyShotAndShip(_shotsEnemy[i]))
                {
                    SoundEffectInstanceBoomShip.Play();
                    _shotsEnemy.RemoveAt(i);
                    i--;
                    HeartNumber--;
                    if(HeartNumber <= 0)
                    {
                        AddToTxt();
                    }
                }
            }

            for (int i = 0; i < _enemies.Length; i++)
            {
                for (int j = 0; j < _shots.Count; j++)
                {
                    if (CollideEnemyAndShipShot(_enemies[i], _shots[j]))
                    {
                        SoundEffectInstanceBoom.Play();
                        _shots.RemoveAt(j);
                        j--;
                        switch(_enemies[i].GetEnemy())
                        {
                            case Enemies.Enemy1:
                                _score += 10;
                                break;
                            case Enemies.Enemy2:
                                _score += 20;
                                break;
                            case Enemies.Enemy3:
                                _score += 30;
                                break;
                        }
                        _enemies[i].RandomSet();
                    }
                }
            }

            for (int i = 0; i < _enemies.Length; i++)
            {
                if (CollideShipAndEnemy(_enemies[i]))
                {
                    SoundEffectInstanceBoom.Play();
                    SoundEffectInstanceBoomShip.Play();
                    switch (_enemies[i].GetEnemy())
                    {
                        case Enemies.Enemy1:
                            _score += 10;
                            break;
                        case Enemies.Enemy2:
                            _score += 20;
                            break;
                        case Enemies.Enemy3:
                            _score += 30;
                            break;
                    }
                    _enemies[i].RandomSet();
                    HeartNumber--;
                    if (HeartNumber <= 0)
                    {
                        AddToTxt();
                    }
                }
            }
        }

        public static void GameOver()
        {
            if (IsGameOver)
            {
                while (_shotsEnemy.Count != 0)
                {
                    _shotsEnemy.RemoveAt(0);                 
                }

                while (_shots.Count != 0)
                {
                    _shots.RemoveAt(0);
                }

                _score = 0;
            }
        }

        private static void AddToTxt()
        {
            StreamReader read = new StreamReader(@"HighScore.txt", true);
            string str = read.ReadLine();
            read.Close();
            int score = 0;
            if (str != "")
            {
                score = Convert.ToInt32(str);
            }

            if (score < _score)
            {
                File.Delete(@"HighScore.txt");
                StreamWriter output = new StreamWriter(@"HighScore.txt", true);
                output.Write(_score.ToString());
                output.Close();
            }
        }
    }
}
