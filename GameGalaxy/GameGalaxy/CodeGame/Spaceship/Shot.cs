using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameGalaxy
{
    class Shot
    {
        private Vector2 _position;
        private Vector2 _direction;
        private const int _speed = 6;
        private Color _color = Color.White;
        public static Texture2D Texture2DShot { get; set; }
        public bool NotOnScreen
        {
            get
            {
                return _position.Y < 0; 
            }
        }


        public Shot(Vector2 pos)
        {
            this._position = pos;
            this._direction = new Vector2(0, -_speed);
        }
        
        public void UpdateShot()
        {
            if (_position.Y >= 0)
            {
                _position += _direction;
            }
        }

        public void DrawShot()
        {
            SpaceGame.SpriteBatch.Draw(Texture2DShot, _position, _color);
        }

        public Vector2 GetPosition => new Vector2(_position.X, _position.Y);
    }
}
