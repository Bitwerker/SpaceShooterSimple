using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
        public Texture2D SpaceShip { get; set; }
        public Texture2D Background { get; set; }
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
        }
        #endregion Methods

        #region Update
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }
        #endregion Update

        #region Draw
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            _spriteBatch.Draw(Background, Vector2.Zero, Color.White);
            _spriteBatch.Draw(SpaceShip, Vector2.Zero, Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
        #endregion Draw
    }
}
