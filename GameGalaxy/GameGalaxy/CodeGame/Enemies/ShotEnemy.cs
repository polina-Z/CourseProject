using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameGalaxy
{
    class ShotEnemy
    {
        private Vector2 _position;
        private Vector2 _direction;
        private const int _speed = 5;
        private Color _color = Color.White;
        public static Texture2D Texture2DShotEnemy { get; set; }
        public bool NotOnScreen
        {
            get
            {
                return (_position.Y > SpaceGame.Height) || (_position.Y < 0);
            }
        }

        public ShotEnemy(Vector2 pos)
        {
            this._position = pos;
            this._direction = new Vector2(0, _speed);
        }

        public void UpdateShot()
        {
            if (_position.Y >= 0 && _position.Y <= SpaceGame.Height)
            {
                _position += _direction;
            }
        }

        public void DrawShot()
        {
            SpaceGame.SpriteBatch.Draw(Texture2DShotEnemy, _position, _color);
        }

        public Vector2 GetPosition => new Vector2(_position.X, _position.Y);
    }
}

