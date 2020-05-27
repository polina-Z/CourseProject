using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using GameGalaxy.CodeGame;

namespace GameGalaxy
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        State state = State.MainScreen;
        KeyboardState keyboardState, oldKeyboardState;
        MouseState mouseState;
        Song song;
        SoundEffect shotSound;
        SoundEffectInstance soundEffectInstanceShot;
        SoundEffect PauseSound;
        SoundEffectInstance soundEffectInstancePause;
        SoundEffect GameOverSound;
        SoundEffectInstance soundEffectInstanceGameOver;
        private bool _isReleased = true;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            graphics.PreferredBackBufferWidth = 1100;
            graphics.PreferredBackBufferHeight = 800;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Screensaver.BackgroundStart = Content.Load<Texture2D>("backgroundStart");
            Screensaver.C = Content.Load<Texture2D>("C");
            Screensaver.Font = Content.Load<SpriteFont>("Screensaver");
            Screensaver.FontGalaxy = Content.Load<SpriteFont>("Galaxy");
            Screensaver.FontMadeBy = Content.Load<SpriteFont>("MadeBy");

            GameOverSreen.BackgroundGameOver = Content.Load<Texture2D>("backgroundGameOver");

            PauseScreen.BackgroundPause = Content.Load<Texture2D>("Pause");            

            SpaceGame.Initialization(spriteBatch, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            SpaceGame.Texture2DGameScreen = Content.Load<Texture2D>("GameScreen");
            SpaceGame.Texture2DHeart = Content.Load<Texture2D>("Heart");

            Star.Texture2DStar = Content.Load<Texture2D>("Star");            

            Spaceship.Texture2DSpaceship = Content.Load<Texture2D>("MainSpaceship");
            Shot.Texture2DShot = Content.Load<Texture2D>("Shot");

            Enemy.Texture2DEnemy1 = Content.Load<Texture2D>("Enemy1");
            Enemy.Texture2DEnemy2 = Content.Load<Texture2D>("Enemy2");
            Enemy.Texture2DEnemy3 = Content.Load<Texture2D>("Enemy3");           
            ShotEnemy.Texture2DShotEnemy = Content.Load<Texture2D>("EnemyShot");

            song = Content.Load<Song>("song1");
            MediaPlayer.Play(song);
            MediaPlayer.IsRepeating = true;

            shotSound = Content.Load<SoundEffect>("ShotSound1");
            soundEffectInstanceShot = shotSound.CreateInstance();

            PauseSound = Content.Load<SoundEffect>("PauseSong");
            soundEffectInstancePause = PauseSound.CreateInstance();

            GameOverSound = Content.Load<SoundEffect>("GameOver");
            soundEffectInstanceGameOver = GameOverSound.CreateInstance();

            SpaceGame.BoomSound = Content.Load<SoundEffect>("Boom");
            SpaceGame.SoundEffectInstanceBoom = SpaceGame.BoomSound.CreateInstance();
            SpaceGame.BoomShipSound = Content.Load<SoundEffect>("boomShip");
            SpaceGame.SoundEffectInstanceBoomShip = SpaceGame.BoomShipSound.CreateInstance();
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>

        protected override void Update(GameTime gameTime)
        {
            if(SpaceGame.HeartNumber <= 0)
            {
                state = State.GameOver;
            }
            keyboardState = Keyboard.GetState();
            mouseState = Mouse.GetState();
            switch (state)
            {
                case State.MainScreen:
                    Screensaver.Update();
                    SpaceGame.IsGameOver = false;
                    if(keyboardState.IsKeyDown(Keys.Enter))
                    {
                        state = State.Game;
                    }
                    break;
                case State.Game:
                    SpaceGame.Update();
                    SpaceGame.IsGameOver = false;
                    if (keyboardState.IsKeyDown(Keys.RightShift))
                    {
                        state = State.MainScreen;
                        SpaceGame.IsGameOver = true;
                        SpaceGame.GameOver();
                        SpaceGame.HeartNumber = 3;
                        SpaceGame.Initialization(spriteBatch, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
                    }
                    if (keyboardState.IsKeyDown(Keys.Up))
                    {
                        SpaceGame.Spaceship.Up();
                    }
                    if (keyboardState.IsKeyDown(Keys.Down))
                    {
                        SpaceGame.Spaceship.Down();
                    }
                    if (keyboardState.IsKeyDown(Keys.Left))
                    {
                        SpaceGame.Spaceship.Left();
                    }
                    if (keyboardState.IsKeyDown(Keys.Right))
                    {
                        SpaceGame.Spaceship.Right();
                    }
                    if (keyboardState.IsKeyDown(Keys.Space) && oldKeyboardState.IsKeyUp(Keys.Space) || 
                           (mouseState.LeftButton == ButtonState.Pressed && _isReleased == true))
                    {
                        SpaceGame.ShipShot();
                        soundEffectInstanceShot.Play();
                        _isReleased = false;
                    }
                    if (keyboardState.IsKeyDown(Keys.P))
                    {
                        MediaPlayer.Pause();
                        state = State.Pause;
                    }
                    break;
                case State.GameOver:
                    MediaPlayer.Stop();
                    soundEffectInstanceGameOver.Play();
                    soundEffectInstanceGameOver.IsLooped = true;
                    SpaceGame.IsGameOver = true;
                    SpaceGame.GameOver();
                    if (keyboardState.IsKeyDown(Keys.RightShift))
                    {
                        soundEffectInstanceGameOver.Stop();
                        soundEffectInstanceGameOver.IsLooped = false;
                        state = State.MainScreen;
                        SpaceGame.HeartNumber = 3;
                        SpaceGame.Initialization(spriteBatch, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
                        MediaPlayer.Play(song);
                    }
                    break;
                case State.Pause:
                    soundEffectInstancePause.Play();
                    soundEffectInstancePause.IsLooped = true;
                    if (keyboardState.IsKeyDown(Keys.RightShift))
                    {
                        soundEffectInstancePause.Stop();
                        soundEffectInstancePause.IsLooped = false;
                        SpaceGame.IsGameOver = true;
                        SpaceGame.GameOver();
                        state = State.MainScreen;
                        SpaceGame.HeartNumber = 3;
                        SpaceGame.Initialization(spriteBatch, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
                        MediaPlayer.Play(song);
                    }
                    if(keyboardState.IsKeyDown(Keys.Enter))
                    {
                        soundEffectInstancePause.Stop();
                        soundEffectInstancePause.IsLooped = false;                        
                        MediaPlayer.Play(song);
                        state = State.Game;
                    }
                    break;
            }
            oldKeyboardState = keyboardState;
            if (mouseState.LeftButton == ButtonState.Released)
            {
                _isReleased = true;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            switch (state)
            {
                case State.MainScreen:
                    Screensaver.Draw(spriteBatch);
                    break;
                case State.Game:
                    SpaceGame.Draw();
                    break;
                case State.GameOver:
                    GameOverSreen.Draw(spriteBatch);
                    break;
                case State.Pause:
                    PauseScreen.Draw(spriteBatch);
                    break;
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
