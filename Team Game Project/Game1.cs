using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;

namespace Team_Game_Project
{
    public class Game1 : Game
    {
        enum GameState
        {
            startScreen,
            overworld,
            battle
        }
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont _text;
        private Texture2D _icons;
        private GameState _state;
        private Rectangle _screen;
        private Texture2D _player;
        private Rectangle[] _playerSrc;
        private Rectangle[] _batSrc;
        private double _activeBat;
        private int _activeMap;
        private int _activePlayer;
        private bool _isLeft;
        private Texture2D _bat;
        private bool _selector;
        private Rectangle _pos;
        private bool _sprint;
        private KeyboardState _oldKB;
        private List<Entity> _enemies;
        private Entity _activeEnemy;
        private Random _rng;
        //Screen Dimentions Code
        private int _screenWidth;
        private int _screenHeight;
        private int _screenWidthPortion;
        private int _screenHeightPortion;

        //Overworld Test Code
        private Rectangle[,] _testOverworldTiles = new Rectangle[10, 6];
        private Texture2D[,] _testOverworldTileTextures = new Texture2D[10, 6];
        private string[,] _testOverworldTileProperties = new string[10, 6];
        private Texture2D _blankTexture;

        private Player dude;
        private int _hp;
        private string _health;
        private Vector2 _textPos;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.ApplyChanges();
            _state = GameState.startScreen;
            _activeMap = 0;
            _playerSrc = new Rectangle[3];
            _batSrc = new Rectangle[6];
            _activePlayer = 1;
            _activeBat = 0;
            _sprint = false;
            _isLeft = false;
            
            _oldKB = Keyboard.GetState();
            _enemies = new List<Entity>();
            _rng = new Random();
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _textPos = new Vector2(2,2);
            
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
            _blankTexture = Content.Load<Texture2D>("White Square");
            _pos = new Rectangle(400, 200, 48, 48);
            _playerSrc[0] = new Rectangle(0, 0, 64, 128);
            _playerSrc[1] = new Rectangle(0, 129, 48, 48);
            _playerSrc[2] = new Rectangle(0, 256, 48, 48);
            _text = Content.Load<SpriteFont>("Text");
            for (int i = 0; i < 6; i++)
            {
                _batSrc[i] = new Rectangle(i * 48, 0, 48, 48);
            }

            //LoadingScreenDimentionInts
            _screenWidth = GraphicsDevice.Viewport.Width;
            _screenHeight = GraphicsDevice.Viewport.Height;

            _screenWidthPortion = _screenWidth / 10;
            _screenHeightPortion = _screenHeight / 6;

            //Loading Overworld 2D Arrays
            for (int i = 0; i < 10; i++)
            {
                int _updateTileDimensionsHeight = i * _screenHeightPortion;
                for (int j = 0; j < 6; j++)
                {
                    int _updateTileDimensionsWidth = j * _screenWidthPortion;
                    //Field
                    //Swap I and J in code to switch collumns + rows
                    if (i <= 3)
                    {
                        _testOverworldTiles[i, j] = new Rectangle (_updateTileDimensionsHeight, _updateTileDimensionsWidth, _screenWidthPortion, _screenHeightPortion);

                        _testOverworldTileProperties[i, j] = "Grass";
                        _testOverworldTileTextures[i, j] = Content.Load<Texture2D>("Grass Texture");
                    }
                    //Water
                    if (i > 3 && i < 6)
                    {
                        _testOverworldTiles[i, j] = new Rectangle(_updateTileDimensionsHeight, _updateTileDimensionsWidth, _screenWidthPortion, _screenHeightPortion);

                        _testOverworldTileProperties[i, j] = "Water";
                        _testOverworldTileTextures[i, j] = Content.Load<Texture2D>("Water Texture");
                    }
                    //Sand
                    if (i >= 6)
                    {
                        _testOverworldTiles[i, j] = new Rectangle(_updateTileDimensionsHeight, _updateTileDimensionsWidth, _screenWidthPortion, _screenHeightPortion);

                        _testOverworldTileProperties[i, j] = "Sand";
                        _testOverworldTileTextures[i, j] = Content.Load<Texture2D>("Sand Texture");
                    }
                }
            }
            dude = new Player("name", _player);
            _hp = dude.getCurrHP();
            _health = "HP: " + _hp.ToString();
            //Temporary Enemy for demo battle, axe this
            _activeEnemy = new Entity(50, 15, 5, 2, 5, "amogus", Content.Load<Texture2D>("Necromancer_creativekind-Sheet"));

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            // TODO: Add your update logic here
            KeyboardState kb = Keyboard.GetState();
            if (_state == GameState.startScreen)
            {
                if (kb.IsKeyDown(Keys.Space))
                {
                    _state = GameState.overworld;
                }
            }
            else if (_state == GameState.overworld)
            {
                if (kb.IsKeyDown(Keys.LeftAlt))
                    _state = GameState.battle;
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
                        _pos.Y -= 8;
                    }
                    else if (kb.IsKeyDown(Keys.Up) && _pos.Y <= 0 && _activeMap == 0)
                    {
                        _activeMap = 1;
                        _pos.Y = _screen.Height - 48;
                    }
                    if (kb.IsKeyDown(Keys.Down) && _pos.Y < _screen.Height - 48)
                    {
                        _pos.Y += 8;
                    }
                    else if (kb.IsKeyDown(Keys.Down) && _pos.Y >= _screen.Height - 48 && _activeMap == 1)
                    {
                        _activeMap = 0;
                        _pos.Y = 0;
                    }
                    if (kb.IsKeyDown(Keys.Left) && _pos.X >= 0)
                    {
                        _isLeft = true;
                        _pos.X -= 8;
                    }
                    if (kb.IsKeyDown(Keys.Right) && _pos.X <= _screen.Width - 48)
                    {
                        _isLeft = false;
                        _pos.X += 8;
                    }
                    _activeBat += .25;
                    if (_activeBat >= 6)
                    {
                        _activeBat = 0;
                    }
                }
            }
            else if (_state == GameState.battle)
            {
                if ((kb.IsKeyDown(Keys.Right) || kb.IsKeyDown(Keys.Left)) && !(_oldKB.IsKeyDown(Keys.Left) || _oldKB.IsKeyDown(Keys.Right)))
                {
                    _selector = !_selector;
                }
                if (kb.IsKeyDown(Keys.Z))
                {
                    if (_selector)
                    {
                        dude.attack(_activeEnemy);
                    }
                    else
                    {
                        //open skill list
                    }
                }
            }
            _oldKB = kb;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            if (_state == GameState.startScreen)
            {
                GraphicsDevice.Clear(Color.CornflowerBlue);
            }
            else
            {
                if (_activeMap == 0)
                    GraphicsDevice.Clear(Color.White);
                else if (_activeMap == 1)
                    GraphicsDevice.Clear(Color.Black);
                else
                    GraphicsDevice.Clear(Color.CornflowerBlue);
            }
            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            if (_state == GameState.overworld)
            {
                if (!_sprint)
                    _spriteBatch.Draw(_player, _pos, _playerSrc[_activePlayer], Color.White);
                else if (_isLeft)
                    _spriteBatch.Draw(_bat, _pos, _batSrc[(int)_activeBat], Color.White, 0, new Vector2(), SpriteEffects.FlipHorizontally, 0);
                else
                    _spriteBatch.Draw(_bat, _pos, _batSrc[(int)_activeBat], Color.White);
            for (int i = 0; i < 10; i++)
            {
                int _updateTileDimensionsHeight = i * _screenHeightPortion;
                for (int j = 0; j < 6; j++)
                {
                    int _updateTileDimensionsWidth = j * _screenWidthPortion;
                    _spriteBatch.Draw(_testOverworldTileTextures[i, j], _testOverworldTiles[i, j], Color.White);
                }
            }
                if (!_sprint)
                    _spriteBatch.Draw(_player, _pos, _playerSrc[_activePlayer], Color.White);
                else if (_isLeft)
                    _spriteBatch.Draw(_bat, _pos, _batSrc[(int) _activeBat], Color.White, 0, new Vector2(), SpriteEffects.FlipHorizontally, 0);
                else
                    _spriteBatch.Draw(_bat, _pos, _batSrc[(int) _activeBat], Color.White);
            }
            else if (_state == GameState.battle)
            {
                dude.Draw(_spriteBatch, new Vector2(100, 200), _playerSrc[0]);
                _spriteBatch.Draw(_icons, new Vector2(100, 350), Color.White);
                if (_activeEnemy.getCurrHP() > 0)
                    _activeEnemy.Draw(_spriteBatch, new Vector2(500, 200), new Rectangle(175, 180, 145, 175));
                if (_selector)
                    _spriteBatch.Draw(_blankTexture, new Vector2(100, 350), Color.White);
                else
                    _spriteBatch.Draw(_blankTexture, new Vector2(200, 350), Color.White);

            }
            //Drawing Overworld
            _spriteBatch.DrawString(_text, _health, _textPos, Color.DarkRed);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}