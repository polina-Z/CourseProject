using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameGalaxy
{
    class Star
    {
        private Vector2 _position;
        private Vector2 _direction;
        private Color _color;
        public static Texture2D Texture2DStar { get; set; }

        public Star(Vector2 pos, Vector2 dir)
        {
            this._position = pos;
            this._direction = dir;
        }

        public Star(Vector2 dir)
        {
            this._direction = dir;
            RandomSet();
        }

        public void UpdateStar()
        {
            _position += _direction;
            if (_position.Y > SpaceGame.Height)
            {
                RandomSet();
            }
        }

        private void RandomSet()
        {
            _position = new Vector2(SpaceGame.GetIntRandom(0, SpaceGame.Width), SpaceGame.GetIntRandom(-300, SpaceGame.Height));
            _color = Color.FromNonPremultiplied(SpaceGame.GetIntRandom(0, 256), SpaceGame.GetIntRandom(0, 256),
                SpaceGame.GetIntRandom(0, 256), SpaceGame.GetIntRandom(0, 256));
        }

        public void DrawStar()
        {
            SpaceGame.SpriteBatch.Draw(Texture2DStar, _position, _color);
        }
    }
}
