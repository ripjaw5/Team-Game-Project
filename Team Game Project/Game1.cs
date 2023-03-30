﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
        //private int _activeMap;
        private int _activePlayer;
        private bool _isLeft;
        private Texture2D _bat;
        //private Rectangle[,] _map;
        private Rectangle _pos;
        private bool _sprint;

        //Screen Dimentions Code
        private int _screenWidth;
        private int _screenHeight;
        private int _screenWidthPortion;
        private int _screenHeightPortion;

        //Overworld Test Code
        //I = Columns J = Rows
        private Rectangle[,] _testOverworldTiles = new Rectangle[10, 6];
        private Texture2D[,] _testOverworldTileTextures = new Texture2D[10, 6];
        private string[,] _testOverworldTileProperties = new string[10, 6];
        private Texture2D _blankTexture;

        //Overworld Int 2D Array
        //Default is [1,1]
        private int[,] _testOverworldScreens = new int[3, 3];
        private bool _leftTransition = false;
        private bool _rightTransition = false;
        private bool _upTransition = false;
        private bool _downTransition = false;

        private int _currentScreenValue1 = 1;
        private int _currentScreenValue2 = 1;

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
            //_activeMap = 0;
            _playerSrc = new Rectangle[3];
            _batSrc = new Rectangle[6];
            _activePlayer = 1;
            _activeBat = 0;
            _sprint = false;
            _isLeft = false;
            dude = new Player("name");
            
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _textPos = new Vector2(2,2);
            _hp = dude.getCurrHP();
            _health = "HP: " + _hp.ToString();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //OverworldScreenLoading
            _testOverworldScreens[0, 0] = 0;
            _testOverworldScreens[1, 0] = 0;
            _testOverworldScreens[2, 0] = 0;

            _testOverworldScreens[0, 1] = 0;
            //Screen Origin
            _testOverworldScreens[1, 1] = 1;
            _testOverworldScreens[2, 1] = 0;

            _testOverworldScreens[0, 2] = 0;
            _testOverworldScreens[1, 2] = 0;
            _testOverworldScreens[2, 2] = 0;

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

            //Screen Origin
            if (_testOverworldScreens[1, 1] == 1)
            {
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
                            _testOverworldTiles[i, j] = new Rectangle(_updateTileDimensionsHeight, _updateTileDimensionsWidth, _screenWidthPortion, _screenHeightPortion);

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
            }
            //Screen Up 1
            if (_testOverworldScreens[1, 0] == 1)
            {
                for (int i = 0; i < 10; i++)
                {
                    int _updateTileDimensionsHeight = i * _screenHeightPortion;
                    for (int j = 0; j < 6; j++)
                    {
                        int _updateTileDimensionsWidth = j * _screenWidthPortion;
                        //Field
                        //Swap I and J in code to switch collumns + rows
                        if (i >= 6)
                        {
                            _testOverworldTiles[i, j] = new Rectangle(_updateTileDimensionsHeight, _updateTileDimensionsWidth, _screenWidthPortion, _screenHeightPortion);

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
                        if (i <= 3)
                        {
                            _testOverworldTiles[i, j] = new Rectangle(_updateTileDimensionsHeight, _updateTileDimensionsWidth, _screenWidthPortion, _screenHeightPortion);

                            _testOverworldTileProperties[i, j] = "Sand";
                            _testOverworldTileTextures[i, j] = Content.Load<Texture2D>("Sand Texture");
                        }
                    }
                }
            }


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
                    else if (kb.IsKeyDown(Keys.Up) && _pos.Y <= 0)
                    {
                        _pos.Y = _screen.Height - 48;
                        _upTransition = true;

                        
                    }
                    if (kb.IsKeyDown(Keys.Down) && _pos.Y < _screen.Height - 48)
                    {
                        _activePlayer = 1;
                        _pos.Y += 2;
                    }
                    else if (kb.IsKeyDown(Keys.Down) && _pos.Y >= _screen.Height - 48)
                    {
                        _pos.Y = 0;
                        _downTransition = true;

                        
                    }
                    //Added Left Right Transition Detection
                    
                        if (kb.IsKeyDown(Keys.Left) && _pos.X > 0)
                    {
                        _pos.X -= 2;
                    }
                        else if (kb.IsKeyDown(Keys.Left) && _pos.X <= 0)
                    {
                        _pos.X = _screen.Width - 48;
                        _leftTransition = true;
                    }
                    if (kb.IsKeyDown(Keys.Right) && _pos.X < _screen.Width - 48)
                    {
                        _pos.X += 2;
                    }
                    else if (kb.IsKeyDown(Keys.Right) && _pos.X <= _screen.Width - 48)
                    {
                        _pos.X = 0;
                        _rightTransition = true;
                    }
                }
                else
                {
                    if (kb.IsKeyDown(Keys.Up) && _pos.Y > 0)
                    {
                        _pos.Y -= 8;
                    }
                    else if (kb.IsKeyDown(Keys.Up) && _pos.Y <= 0)
                    {
                        _pos.Y = _screen.Height - 48;
                        _upTransition = true;

                        
                    }
                    if (kb.IsKeyDown(Keys.Down) && _pos.Y < _screen.Height - 48)
                    {
                        _pos.Y += 8;
                    }
                    else if (kb.IsKeyDown(Keys.Down) && _pos.Y >= _screen.Height - 48)
                    {
                        _pos.Y = 0;
                        _downTransition = true;

                        
                    }
                    if (kb.IsKeyDown(Keys.Left) && _pos.X > 0)
                    {
                        _isLeft = true;
                        _pos.X -= 8;
                    }
                    else if (kb.IsKeyDown(Keys.Left) && _pos.X <= 0)
                    {
                        _pos.X = _screen.Width - 48;
                        _leftTransition = true;
                    }
                    if (kb.IsKeyDown(Keys.Right) && _pos.X < _screen.Width - 48)
                    {
                        _isLeft = false;
                        _pos.X += 8;
                    }
                    else if (kb.IsKeyDown(Keys.Right) && _pos.X <= _screen.Width - 48)
                    {
                        _pos.X = 0;
                        _rightTransition = true;
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

            }

            //Transition UPDATING
            if (_upTransition == true && _currentScreenValue2 != -1)
            {
                _testOverworldScreens[_currentScreenValue1, _currentScreenValue2] = 0;
                _currentScreenValue2 -= 1;
                _testOverworldScreens[_currentScreenValue1, _currentScreenValue2] = 1;

                _upTransition = false;
            }
            if (_downTransition == true && _currentScreenValue2 != 3)
            {
                _testOverworldScreens[_currentScreenValue1, _currentScreenValue2] = 0;
                _currentScreenValue2 += 1;
                _testOverworldScreens[_currentScreenValue1, _currentScreenValue2] = 1;

                _downTransition = false;
            }
            if (_leftTransition == true)
            {
                _testOverworldScreens[_currentScreenValue1, _currentScreenValue2] = 0;
                _currentScreenValue1 -= 1;
                _testOverworldScreens[_currentScreenValue1, _currentScreenValue2] = 1;

                _leftTransition = false;
            }
            if (_rightTransition == true)
            {
                _testOverworldScreens[_currentScreenValue1, _currentScreenValue2] = 0;
                _currentScreenValue1 += 1;
                _testOverworldScreens[_currentScreenValue1, _currentScreenValue2] = 1;

                _rightTransition = false;
            }

            //Screen Origin
            if (_testOverworldScreens[1, 1] == 1)
            {
                for (int i = 0; i < 10; i++)
                {
                    int _updateTileDimensionsHeight = i * _screenHeightPortion;
                    for (int j = 0; j < 6; j++)
                    {
                        int _updateTileDimensionsWidth = j * _screenWidthPortion;
                        //Field
                        
                        _testOverworldTiles[i, j] = new Rectangle(_updateTileDimensionsHeight, _updateTileDimensionsWidth, _screenWidthPortion, _screenHeightPortion);

                        _testOverworldTileProperties[i, j] = "Grass";
                        _testOverworldTileTextures[i, j] = Content.Load<Texture2D>("Grass Texture");
                    }
                }
            }
            //Screen Up 1
            if (_testOverworldScreens[1, 0] == 1)
            {
                for (int i = 0; i < 10; i++)
                {
                    int _updateTileDimensionsHeight = i * _screenHeightPortion;
                    for (int j = 0; j < 6; j++)
                    {
                        int _updateTileDimensionsWidth = j * _screenWidthPortion;
                        //Field
                        //Swap I and J in code to switch collumns + rows
                        if (i % 2 == 0)
                        {
                            _testOverworldTiles[i, j] = new Rectangle(_updateTileDimensionsHeight, _updateTileDimensionsWidth, _screenWidthPortion, _screenHeightPortion);

                            _testOverworldTileProperties[i, j] = "Grass";
                            _testOverworldTileTextures[i, j] = Content.Load<Texture2D>("Grass Texture");
                        }
                        //Water
                        if (i % 2 != 0)
                        {
                            _testOverworldTiles[i, j] = new Rectangle(_updateTileDimensionsHeight, _updateTileDimensionsWidth, _screenWidthPortion, _screenHeightPortion);

                            _testOverworldTileProperties[i, j] = "Water";
                            _testOverworldTileTextures[i, j] = Content.Load<Texture2D>("Water Texture");
                        }
                        //Field
                        if (j % 2 == 0)
                        {
                            _testOverworldTiles[i, j] = new Rectangle(_updateTileDimensionsHeight, _updateTileDimensionsWidth, _screenWidthPortion, _screenHeightPortion);

                            _testOverworldTileProperties[i, j] = "Grass";
                            _testOverworldTileTextures[i, j] = Content.Load<Texture2D>("Grass Texture");
                        }
                        //Water
                        if (j % 2 != 0)
                        {
                            _testOverworldTiles[i, j] = new Rectangle(_updateTileDimensionsHeight, _updateTileDimensionsWidth, _screenWidthPortion, _screenHeightPortion);

                            _testOverworldTileProperties[i, j] = "Water";
                            _testOverworldTileTextures[i, j] = Content.Load<Texture2D>("Water Texture");
                        }


                    }
                }
            }
            //Screen Down 1
            if (_testOverworldScreens[1, 2] == 1)
            {
                for (int i = 0; i < 10; i++)
                {
                    int _updateTileDimensionsHeight = i * _screenHeightPortion;
                    for (int j = 0; j < 6; j++)
                    {
                        int _updateTileDimensionsWidth = j * _screenWidthPortion;
                        //Field
                        //Swap I and J in code to switch collumns + rows
                        if (j % 2 == 0 && i % 2 == 0)
                        {
                            _testOverworldTiles[i, j] = new Rectangle(_updateTileDimensionsHeight, _updateTileDimensionsWidth, _screenWidthPortion, _screenHeightPortion);

                            _testOverworldTileProperties[i, j] = "Grass";
                            _testOverworldTileTextures[i, j] = Content.Load<Texture2D>("Grass Texture");
                        }
                        //sand
                        else
                        {
                            _testOverworldTiles[i, j] = new Rectangle(_updateTileDimensionsHeight, _updateTileDimensionsWidth, _screenWidthPortion, _screenHeightPortion);

                            _testOverworldTileProperties[i, j] = "Sand";
                            _testOverworldTileTextures[i, j] = Content.Load<Texture2D>("Sand Texture");
                        }
                        
                    }
                }
            }
            //Screen Left 1
            if (_testOverworldScreens[0, 1] == 1)
            {
                for (int i = 0; i < 10; i++)
                {
                    int _updateTileDimensionsHeight = i * _screenHeightPortion;
                    for (int j = 0; j < 6; j++)
                    {
                        int _updateTileDimensionsWidth = j * _screenWidthPortion;
                        //Field
                        //Swap I and J in code to switch collumns + rows
                        if (i == 2 || i == 3 || i == 5 || i == 7)
                        {
                            _testOverworldTiles[i, j] = new Rectangle(_updateTileDimensionsHeight, _updateTileDimensionsWidth, _screenWidthPortion, _screenHeightPortion);

                            _testOverworldTileProperties[i, j] = "Grass";
                            _testOverworldTileTextures[i, j] = Content.Load<Texture2D>("Grass Texture");
                        }
                        //Water
                        else
                        {
                            _testOverworldTiles[i, j] = new Rectangle(_updateTileDimensionsHeight, _updateTileDimensionsWidth, _screenWidthPortion, _screenHeightPortion);

                            _testOverworldTileProperties[i, j] = "Water";
                            _testOverworldTileTextures[i, j] = Content.Load<Texture2D>("Water Texture");
                        }
                        //Field
                        if (j == 2 || j == 3 || j == 5 || j == 7)
                        {
                            _testOverworldTiles[i, j] = new Rectangle(_updateTileDimensionsHeight, _updateTileDimensionsWidth, _screenWidthPortion, _screenHeightPortion);

                            _testOverworldTileProperties[i, j] = "Grass";
                            _testOverworldTileTextures[i, j] = Content.Load<Texture2D>("Grass Texture");
                        }
                        //Water
                        else
                        {
                            _testOverworldTiles[i, j] = new Rectangle(_updateTileDimensionsHeight, _updateTileDimensionsWidth, _screenWidthPortion, _screenHeightPortion);

                            _testOverworldTileProperties[i, j] = "Water";
                            _testOverworldTileTextures[i, j] = Content.Load<Texture2D>("Water Texture");
                        }
                    }
                }
            }
            //Screen Right 1
            if (_testOverworldScreens[2, 1] == 1)
            {
                for (int i = 0; i < 10; i++)
                {
                    int _updateTileDimensionsHeight = i * _screenHeightPortion;
                    for (int j = 0; j < 6; j++)
                    {
                        int _updateTileDimensionsWidth = j * _screenWidthPortion;
                        //Sand
                        //Swap I and J in code to switch collumns + rows
                        if (i == 2 || i == 3 || i == 5 || i == 7)
                        {
                            _testOverworldTiles[i, j] = new Rectangle(_updateTileDimensionsHeight, _updateTileDimensionsWidth, _screenWidthPortion, _screenHeightPortion);

                            _testOverworldTileProperties[i, j] = "Sand";
                            _testOverworldTileTextures[i, j] = Content.Load<Texture2D>("Sand Texture");
                        }
                        //Water
                        else
                        {
                            _testOverworldTiles[i, j] = new Rectangle(_updateTileDimensionsHeight, _updateTileDimensionsWidth, _screenWidthPortion, _screenHeightPortion);

                            _testOverworldTileProperties[i, j] = "Water";
                            _testOverworldTileTextures[i, j] = Content.Load<Texture2D>("Water Texture");
                        }
                        //Sand
                        if (j == 2 || j == 3 || j == 5 || j == 7)
                        {
                            _testOverworldTiles[i, j] = new Rectangle(_updateTileDimensionsHeight, _updateTileDimensionsWidth, _screenWidthPortion, _screenHeightPortion);

                            _testOverworldTileProperties[i, j] = "Sand";
                            _testOverworldTileTextures[i, j] = Content.Load<Texture2D>("Sand Texture");
                        }
                        //Water
                        else
                        {
                            _testOverworldTiles[i, j] = new Rectangle(_updateTileDimensionsHeight, _updateTileDimensionsWidth, _screenWidthPortion, _screenHeightPortion);

                            _testOverworldTileProperties[i, j] = "Water";
                            _testOverworldTileTextures[i, j] = Content.Load<Texture2D>("Water Texture");
                        }
                    }
                }
            }
            //Screen Top Left
            if (_testOverworldScreens[0, 0] == 1)
            {
                for (int i = 0; i < 10; i++)
                {
                    int _updateTileDimensionsHeight = i * _screenHeightPortion;
                    for (int j = 0; j < 6; j++)
                    {
                        int _updateTileDimensionsWidth = j * _screenWidthPortion;
                        
                        //Water
                        
                            _testOverworldTiles[i, j] = new Rectangle(_updateTileDimensionsHeight, _updateTileDimensionsWidth, _screenWidthPortion, _screenHeightPortion);

                            _testOverworldTileProperties[i, j] = "Water";
                            _testOverworldTileTextures[i, j] = Content.Load<Texture2D>("Water Texture");
                        
                        
                    }
                }
            }
            //Screen Top Right
            if (_testOverworldScreens[2, 0] == 1)
            {
                for (int i = 0; i < 10; i++)
                {
                    int _updateTileDimensionsHeight = i * _screenHeightPortion;
                    for (int j = 0; j < 6; j++)
                    {
                        int _updateTileDimensionsWidth = j * _screenWidthPortion;
                        //Field
                        
                        _testOverworldTiles[i, j] = new Rectangle(_updateTileDimensionsHeight, _updateTileDimensionsWidth, _screenWidthPortion, _screenHeightPortion);

                        _testOverworldTileProperties[i, j] = "Grass";
                        _testOverworldTileTextures[i, j] = Content.Load<Texture2D>("Grass Texture");

                    }
                }
            }
            //Screen Bottom Left
            if (_testOverworldScreens[0, 2] == 1)
            {
                for (int i = 0; i < 10; i++)
                {
                    int _updateTileDimensionsHeight = i * _screenHeightPortion;
                    for (int j = 0; j < 6; j++)
                    {
                        int _updateTileDimensionsWidth = j * _screenWidthPortion;
                        //Water
                        //Swap I and J in code to switch collumns + rows
                        if (j % 2 != 0 && i % 2 != 0)
                        {
                            _testOverworldTiles[i, j] = new Rectangle(_updateTileDimensionsHeight, _updateTileDimensionsWidth, _screenWidthPortion, _screenHeightPortion);

                            _testOverworldTileProperties[i, j] = "Water";
                            _testOverworldTileTextures[i, j] = Content.Load<Texture2D>("Water Texture");
                        }
                        //sand
                        else
                        {
                            _testOverworldTiles[i, j] = new Rectangle(_updateTileDimensionsHeight, _updateTileDimensionsWidth, _screenWidthPortion, _screenHeightPortion);

                            _testOverworldTileProperties[i, j] = "Sand";
                            _testOverworldTileTextures[i, j] = Content.Load<Texture2D>("Sand Texture");
                        }
                    }
                }
            }
            //Screen Bottom Right
            if (_testOverworldScreens[2, 2] == 1)
            {
                for (int i = 0; i < 10; i++)
                {
                    int _updateTileDimensionsHeight = i * _screenHeightPortion;
                    for (int j = 0; j < 6; j++)
                    {
                        int _updateTileDimensionsWidth = j * _screenWidthPortion;
                        
                        //Sand
                        
                            _testOverworldTiles[i, j] = new Rectangle(_updateTileDimensionsHeight, _updateTileDimensionsWidth, _screenWidthPortion, _screenHeightPortion);

                            _testOverworldTileProperties[i, j] = "Sand";
                            _testOverworldTileTextures[i, j] = Content.Load<Texture2D>("Sand Texture");
                        
                    }
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            if (_state == GameState.startScreen)
            {
                GraphicsDevice.Clear(Color.CornflowerBlue);
            }
            //else
            //{
            //    if (_activeMap == 0)
            //        GraphicsDevice.Clear(Color.White);
            //    else if (_activeMap == 1)
            //        GraphicsDevice.Clear(Color.Black);
            //    else
            //        GraphicsDevice.Clear(Color.CornflowerBlue);
            //}
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
            }
            else if (_state == GameState.battle)
            {
                _spriteBatch.Draw(_player, new Vector2(100, 200), _playerSrc[0], Color.White);
                _spriteBatch.Draw(_icons, new Vector2(100, 350), Color.White);
            }
            

            //if (_activeMap == 0)
            //    GraphicsDevice.Clear(Color.White);
            //else if (_activeMap == 1)
            //    GraphicsDevice.Clear(Color.Black);
            //else
            //    GraphicsDevice.Clear(Color.CornflowerBlue);

            //Drawing Overworld
            

            // TODO: Add your drawing code here
            
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
            _spriteBatch.DrawString(_text, _health, _textPos, Color.DarkRed);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}