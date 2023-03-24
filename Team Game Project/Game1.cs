using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Team_Game_Project
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D _thief;
        private Texture2D _icons;
        private Rectangle _screen;
        private Texture2D _player;
        private Texture2D _goblin;
        
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            _icons = Content.Load<Texture2D>("free_icons1");
            _screen = new Rectangle(0, 0, 1920, 1080);
            _player = Content.Load<Texture2D>("Necromancer_creativekind-Sheet"); 
            _goblin = Content.Load<Texture2D>("Goblin Small");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            //_spriteBatch.Draw(_player, new Vector2(0, 0), null, Color.White, 0, new Vector2(0, 0), 5, SpriteEffects.None, 0);
            _spriteBatch.Draw(_player, new Vector2(0, 0), Color.White);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}