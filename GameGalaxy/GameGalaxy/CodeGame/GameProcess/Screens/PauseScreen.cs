using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameGalaxy.CodeGame
{ 
    static class PauseScreen
    {
        private static Vector2 _textPosition = new Vector2();
        public static Texture2D BackgroundPause { get; set; }

        public static void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(BackgroundPause, new Rectangle(0, 0, 1100, 800), Color.White);
            _textPosition.X = 415;
            _textPosition.Y = 70;
            spriteBatch.DrawString(Screensaver.FontGalaxy, "GALAXY", _textPosition, Color.White);
            _textPosition.X = 437;
            _textPosition.Y = 200;
            spriteBatch.DrawString(Screensaver.FontGalaxy, "PAUSE", _textPosition, Color.White);
            _textPosition.X = 333;
            _textPosition.Y = 338;
            spriteBatch.DrawString(Screensaver.Font, "PRESS ENTER TO CONTINUE", _textPosition, Color.White);
            _textPosition.X = 391;
            _textPosition.Y = 418;
            spriteBatch.DrawString(Screensaver.Font, "MENU - RIGHT SHIFT", _textPosition, Color.White);
            _textPosition.X = 245;
            _textPosition.Y = 498;
            spriteBatch.DrawString(Screensaver.Font, "PRESS ESCAPE TO LEAVE YOUR GAME", _textPosition, Color.White);
            _textPosition.X = 580;
            _textPosition.Y = 760;
            spriteBatch.DrawString(Screensaver.FontMadeBy, "COURSE PROJECT MADE BY POLINA ZORKO", _textPosition, Color.White);
            spriteBatch.Draw(Screensaver.C, new Rectangle(40, 767, 20, 20), Color.White);
            _textPosition.X = 65;
            _textPosition.Y = 760;
            spriteBatch.DrawString(Screensaver.FontMadeBy, "2020", _textPosition, Color.White);
        }
    }
}
