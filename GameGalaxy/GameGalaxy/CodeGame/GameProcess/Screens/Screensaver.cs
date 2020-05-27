using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;

namespace GameGalaxy
{
    static class Screensaver
    {
        private static int _timeCounter = 0;
        private static Color _color;
        private static Vector2 _textPosition = new Vector2();
        public static Texture2D BackgroundStart { get; set; }
        public static Texture2D C { get; set; }
        public static SpriteFont Font { get; set; }
        public static SpriteFont FontGalaxy { get; set; }
        public static SpriteFont FontMadeBy { get; set; }

        public static void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(BackgroundStart, new Rectangle(0, 0, 1100, 800), Color.White);
            _textPosition.X = 415;
            _textPosition.Y = 70;
            spriteBatch.DrawString(FontGalaxy, "GALAXY", _textPosition, _color);
            _textPosition.X = 140;
            _textPosition.Y = 200;
            spriteBatch.DrawString(Font, "CONTROLS", _textPosition, _color);
            _textPosition.X = 40;
            _textPosition.Y = 262;
            spriteBatch.DrawString(Font, "MOVE LEFT - ARROW LEFT", _textPosition, _color);
            _textPosition.X = 40;
            _textPosition.Y = 304;
            spriteBatch.DrawString(Font, "MOVE RIGHT - ARROW RIGHT", _textPosition, _color);
            _textPosition.X = 40;
            _textPosition.Y = 346;
            spriteBatch.DrawString(Font, "MOVE UP - ARROW UP", _textPosition, _color);
            _textPosition.X = 40;
            _textPosition.Y = 388;
            spriteBatch.DrawString(Font, "MOVE DOWN - ARROW DOWN", _textPosition, _color);
            _textPosition.X = 40;
            _textPosition.Y = 430;
            spriteBatch.DrawString(Font, "SHOOT - SPACEBAR OR LEFT MOUSE BUTTON", _textPosition, _color);
            _textPosition.X = 620;
            _textPosition.Y = 262;
            spriteBatch.DrawString(Font, "MENU - RIGHT SHIFT", _textPosition, _color);
            _textPosition.X = 620;
            _textPosition.Y = 304;
            spriteBatch.DrawString(Font, "PAUSE - P", _textPosition, _color);
            _textPosition.X = 620;
            _textPosition.Y = 346;
            spriteBatch.DrawString(Font, "HIGH SCORE: " + GetHighScore(), _textPosition, _color);
            _textPosition.X = 280;
            _textPosition.Y = 498;
            spriteBatch.DrawString(Font, "PRESS ENTER TO BEGIN GAME", _textPosition, _color);
            _textPosition.X = 200;
            _textPosition.Y = 540;
            spriteBatch.DrawString(Font, "OR PRESS ESCAPE TO LEAVE YOUR GAME", _textPosition, _color);
            _textPosition.X = 580;
            _textPosition.Y = 760;
            spriteBatch.DrawString(FontMadeBy, "COURSE PROJECT MADE BY POLINA ZORKO", _textPosition, _color);
            spriteBatch.Draw(C, new Rectangle(40, 767, 20, 20), _color);
            _textPosition.X = 65;
            _textPosition.Y = 760;
            spriteBatch.DrawString(FontMadeBy, "2020", _textPosition, _color);
        }

        public static void Update()
        {
            _color = Color.FromNonPremultiplied(255, 255, 255, _timeCounter % 256);
            if(_timeCounter > 255)
            {
                _color = Color.FromNonPremultiplied(255, 255, 255, 255);
            }
            _timeCounter++;
        }

        private static string GetHighScore()
        {
            StreamReader read = new StreamReader(@"HighScore.txt", true);
            string str = read.ReadLine();
            read.Close();
            return str;
        }
    }
}
