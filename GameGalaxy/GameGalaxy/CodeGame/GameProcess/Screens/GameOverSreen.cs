using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;

namespace GameGalaxy
{
    static class GameOverSreen
    {
        private static Vector2 _textPosition = new Vector2();
        public static Texture2D BackgroundGameOver { get; set; }        

        public static void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(BackgroundGameOver, new Rectangle(0, 0, 1100, 800), Color.White);
            _textPosition.X = 415;
            _textPosition.Y = 70;
            spriteBatch.DrawString(Screensaver.FontGalaxy, "GALAXY", _textPosition, Color.White);
            _textPosition.X = 370;
            _textPosition.Y = 200;
            spriteBatch.DrawString(Screensaver.FontGalaxy, "GAME OVER", _textPosition, Color.White);
            _textPosition.X = 370;
            _textPosition.Y = 338;
            spriteBatch.DrawString(Screensaver.Font, "YOUR SCORE: " + GetScore(), _textPosition, Color.White);
            _textPosition.X = 361;
            _textPosition.Y = 418;           
            spriteBatch.DrawString(Screensaver.Font, "MENU - RIGHT SHIFT", _textPosition, Color.White);
            _textPosition.X = 200;
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

        private static string GetScore()
        {
            StreamReader read = new StreamReader(@"YourScore.txt", true);
            string str = read.ReadLine();
            read.Close();
            return str;
        }
    }
}
