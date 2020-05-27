using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameGalaxy
{
    class Enemy
    {
        private Vector2 _position;
        private Color _color = Color.White;
        private Enemies _enemy;
        private const int _speed = 3;
        public static Texture2D Texture2DEnemy1{ get; set; }
        public static Texture2D Texture2DEnemy2{ get; set; }
        public static Texture2D Texture2DEnemy3{ get; set; }
        public static Texture2D Texture2DEnemy { get; set; }

        public Enemy(Enemies enemy)
        {
            RandomSet();
            this._enemy = enemy;
        }

        public void Move()
        {
            _position.Y += _speed;
            if (_position.Y > SpaceGame.Height)
            {
                RandomSet();
            }
        }

        public void DrawEnemy()
        {
            switch(_enemy)
            {
                case Enemies.Enemy1:
                    Texture2DEnemy = Texture2DEnemy1;
                    break;
                case Enemies.Enemy2:
                    Texture2DEnemy = Texture2DEnemy2;
                    break;
                case Enemies.Enemy3:
                    Texture2DEnemy = Texture2DEnemy3;
                    break;
            }
            SpaceGame.SpriteBatch.Draw(Texture2DEnemy, _position, _color);
        }

        public Vector2 GetPosition => new Vector2(_position.X, _position.Y);
        public void SetX(int x)
        {
            _position.X += x;
        }

        public void SetY(int y)
        {
            _position.Y += y;
        }

        public Vector2 GetPositionForShot()
        {
            int width = 0, height = 0;
            switch (_enemy)
            {
                case Enemies.Enemy1:
                    width = 76;
                    height = 74;
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
            return new Vector2(_position.X + width / 2 - 12, _position.Y + height - 2 - 12);
        }

        public void RandomSet()
        {
            _position = new Vector2(SpaceGame.GetIntRandom(100, SpaceGame.Width - 100), SpaceGame.GetIntRandom(-500, -100));
        }

        public Enemies GetEnemy()
        {
            return _enemy;
        }
    }
}
