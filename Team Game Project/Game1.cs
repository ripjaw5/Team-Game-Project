using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics.Tracing;

namespace Team_Game_Project
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D _icons;
        private Rectangle _screen;
        private Texture2D _player;
        private Rectangle[] _playerSrc;
        private Rectangle[] _batSrc;
        private double _activeBat;
        private int _activeMap;
        private int _activePlayer;
        private bool _isLeft;
        private Texture2D _bat;
        //private Rectangle[,] _map;
        private Rectangle _pos;
        private bool _sprint;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.ApplyChanges();
            _activeMap = 0;
            _playerSrc = new Rectangle[3];
            _batSrc = new Rectangle[6];
            _activePlayer = 1;
            _activeBat = 0;
            _sprint = false;
            _isLeft = false;
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
            _screen = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            _player = Content.Load<Texture2D>("spr_Player");
            _bat = Content.Load<Texture2D>("spr_Bat");
            _pos = new Rectangle(400, 200, 48, 48);
            _playerSrc[0] = new Rectangle(0, 0, 64, 128);
            _playerSrc[1] = new Rectangle(0, 129, 48, 48);
            _playerSrc[2] = new Rectangle(0, 256, 48, 48);
            for (int i = 0; i < 6; i++)
            {
                _batSrc[i] = new Rectangle(i * 48, 0, 48, 48);
            }
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            KeyboardState kb = Keyboard.GetState();
            if (kb.IsKeyDown(Keys.LeftShift))
                _sprint = true;
            else
                _sprint = false;
            if (!_sprint)
            {
                if (kb.IsKeyDown(Keys.Up) && _pos.Y > 0)
                {
                    _activePlayer = 2;
                    _pos.Y -= 2;
                }
                else if (kb.IsKeyDown(Keys.Up) && _pos.Y <= 0 && _activeMap == 0)
                {
                    _activeMap = 1;
                    _pos.Y = _screen.Height - 48;
                }
                if (kb.IsKeyDown(Keys.Down) && _pos.Y < _screen.Height - 48)
                {
                    _activePlayer = 1;
                    _pos.Y += 2;
                }
                else if (kb.IsKeyDown(Keys.Down) && _pos.Y >= _screen.Height - 48 && _activeMap == 1)
                {
                    _activeMap = 0;
                    _pos.Y = 0;
                }
                if (kb.IsKeyDown(Keys.Left) && _pos.X >= 0)
                {
                    _pos.X -= 2;
                }
                if (kb.IsKeyDown(Keys.Right) && _pos.X <= _screen.Width - 48)
                {
                    _pos.X += 2;
                }
            }
            else
            {
                if (kb.IsKeyDown(Keys.Up) && _pos.Y > 0)
                {
                    _activeBat += .25;
                    _pos.Y -= 8;
                }
                else if (kb.IsKeyDown(Keys.Up) && _pos.Y <= 0 && _activeMap == 0)
                {
                    _activeMap = 1;
                    _pos.Y = _screen.Height - 48;
                }
                if (kb.IsKeyDown(Keys.Down) && _pos.Y < _screen.Height - 48)
                {
                    _activeBat += .25;
                    _pos.Y += 8;
                }
                else if (kb.IsKeyDown(Keys.Down) && _pos.Y >= _screen.Height - 48 && _activeMap == 1)
                {
                    _activeMap = 0;
                    _pos.Y = 0;
                }
                if (kb.IsKeyDown(Keys.Left) && _pos.X >= 0)
                {
                    _activeBat += .25;
                    _isLeft = true;
                    _pos.X -= 8;
                }
                if (kb.IsKeyDown(Keys.Right) && _pos.X <= _screen.Width - 48)
                {
                    _isLeft = false;
                    _activeBat += .25;
                    _pos.X += 8;
                }
                if (_activeBat >= 6)
                {
                    _activeBat = 0;
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            if (_activeMap == 0)
                GraphicsDevice.Clear(Color.White);
            else if (_activeMap == 1)
                GraphicsDevice.Clear(Color.Black);
            else
                GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            //_spriteBatch.Draw(_player, new Vector2(0, 0), null, Color.White, 0, new Vector2(0, 0), 5, SpriteEffects.None, 0);
            if (!_sprint)
                _spriteBatch.Draw(_player, _pos, _playerSrc[_activePlayer], Color.White);
            else if (_isLeft)
                _spriteBatch.Draw(_bat, _pos, _batSrc[(int) _activeBat], Color.White, 0, new Vector2(), SpriteEffects.FlipHorizontally, 0);
            else
                _spriteBatch.Draw(_bat, _pos, _batSrc[(int) _activeBat], Color.White);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}