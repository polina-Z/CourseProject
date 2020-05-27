using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace GameGalaxy
{
    class Spaceship
    {
        private Vector2 _position;
        private Color _color = Color.White;
        private const int _speed = 4;
        public static Texture2D Texture2DSpaceship { get; set; }

        public Spaceship(Vector2 pos)
        {
            this._position = pos;
        }

        public void Up()
        {
            if (this._position.Y > 0)
            {
                this._position.Y -= _speed;
            }
        }

        public void Down()
        {
            if (this._position.Y < SpaceGame.Height - 91)
            {
                this._position.Y += _speed;
            }
        }

        public void Left()
        {
            if (this._position.X > 0)
            {
                this._position.X -= _speed;
            }
        }

        public void Right()
        {
            if (this._position.X < SpaceGame.Width - 80)
            {
                this._position.X += _speed;
            }
        }

        public void DrawSpaceship()
        {
            SpaceGame.SpriteBatch.Draw(Texture2DSpaceship, _position, _color);
        }

        public Vector2 GetPositionForShot => new Vector2(_position.X + 6, _position.Y);

        public Vector2 GetPosition => new Vector2(_position.X, _position.Y);
    }
}
