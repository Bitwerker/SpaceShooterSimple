using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.IO;

namespace SpaceShooter
{
    public class Spaceshooter : Game
    {
        #region EngineProps
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        #endregion EngineProps

        #region CustomProperties
        
        #region Sprites
        public Texture2D SpaceShip { get; set; }
        public Texture2D Background { get; set; }
        public Texture2D Bullet { get; set; }
        #endregion Sprites

        #region Fonts
        public SpriteFont MyFont { get; set; }
        #endregion Font

        #region Audio
        public SoundEffect ShotSound { get; set; }

        public Song BackgroundSound { get; set; }
        #endregion Audio

        #region Positions
        public Vector2 ShipPosition { get; set; }
        public Vector2 ShotPosition { get; set; }
        public Vector2 ShotOffset { get; set; }
        #endregion Positions

        #region Booleans
        public bool HasShot { get; set; } = false;
        #endregion Booleans

        public float VelocityShip { get; set; } = 4;

        public float VelocityBullet { get; set; } = 5;

        #endregion CustomProperties

        #region ctor
        public Spaceshooter()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        } 
        #endregion ctor

        #region Initialize
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }
        #endregion Initialize

        #region LoadContent
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            LoadCustomEntitys();
        }

        #endregion LoadContent

        #region Methods
        private void LoadCustomEntitys()
        {
            SpaceShip = Content.Load<Texture2D>("Sprites/space_ship");
            Background = Content.Load<Texture2D>("Sprites/background");
            Bullet = Content.Load<Texture2D>("Sprites/shot");

            // intialize ShotOffset relativ to the size of the ship image
            ShotOffset = new Vector2(SpaceShip.Width / 2, SpaceShip.Height / 2);
            ShotOffset += new Vector2(55, -22);//Hacking for been in the middle of the ship , but sligthly bevore
            
            MyFont = Content.Load<SpriteFont>("Fonts/defaultFont");
            ShotSound = Content.Load<SoundEffect>("Audio/flaunch");
            BackgroundSound = Content.Load<Song>("Audio/battleThemeA");
            MediaPlayer.Play(BackgroundSound);

        }
        #endregion Methods

        #region Update
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            Vector2 movement = Vector2.Zero; //never manipulate the vector2 directly of an entity -> can cause issues

            //1.)catch current state of the Keyboard
            KeyboardState keystate = Keyboard.GetState();

            if (keystate.IsKeyDown(Keys.Right))
            {
                movement.X += VelocityShip;
            }

            if (keystate.IsKeyDown(Keys.Left))
            {
                movement.X -= VelocityShip;
            }

            if (keystate.IsKeyDown(Keys.Up))
            {
                movement.Y -= VelocityShip;
            }

            if (keystate.IsKeyDown(Keys.Down))
            {
                movement.Y += VelocityShip;
            }

            if (keystate.IsKeyDown(Keys.Space) && !HasShot)
            {
                ShotPosition = ShipPosition + ShotOffset;
                HasShot = true;
                ShotSound.Play();
            }

            ShipPosition += movement;

            if (HasShot)
            {
                ShotPosition += new Vector2(VelocityBullet, 0);

                if (ShotPosition.X > GraphicsDevice.Viewport.Width)
                {
                    HasShot = false;
                }
            }

            base.Update(gameTime);
        }
        #endregion Update

        #region Draw
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            _spriteBatch.Draw(Background, Vector2.Zero, Color.White);
            _spriteBatch.Draw(SpaceShip, ShipPosition, Color.White);
            if (HasShot)
            {
                _spriteBatch.Draw(Bullet, ShotPosition, Color.White);
            }           
            _spriteBatch.DrawString(MyFont, "Space Shooter", Vector2.Zero, Color.Yellow);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
        #endregion Draw
    }
}
