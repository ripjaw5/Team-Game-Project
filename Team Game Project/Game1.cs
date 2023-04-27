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
            titleScreen,
            startScreen,
            overworld,
            battle
        }
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont _text;
        private Texture2D _icons;
        private Texture2D _skills;
        private GameState _state;
        private Rectangle _screen;
        private Texture2D _player;
        private Rectangle[] _playerSrc;
        private Rectangle[] _batSrc;
        private Rectangle[] _moronaSrc;
        private double _activeBat;
        private bool _menu;
        private double _activePlayer;
        private int _menuPos;
        private bool _isLeft;
        private bool _isRight;
        private bool _isUp;
        private bool _isDown;
        private Texture2D _bat;
        private bool _selector;
        private Rectangle _pos;
        private bool _sprint;
        private KeyboardState _oldKB;
        private List<Entity> _enemies;
        private List<Entity> _bossEnemies;
        private Entity _activeEnemy;
        private Random _rng;
        //Screen Dimentions Code
        private int _screenWidth;
        private int _screenHeight;
        private int _screenWidthPortion;
        private int _screenHeightPortion;
        private int _turnTimer;

        //Overworld Test Code
        //I = Columns J = Rows
        private Rectangle[,] _testOverworldTiles = new Rectangle[10, 6];
        private Texture2D[,] _testOverworldTileTextures = new Texture2D[10, 6];
        private string[,] _testOverworldTileProperties = new string[10, 6];
        private Texture2D _blankTexture;

        //Overworld Int 2D Array
        //Default is [1,1]
        private int[,] _testOverworldScreens = new int[3, 3];
        private int[,] _screenDifficultyValues = new int[3, 3];
        private bool _leftTransition = false;
        private bool _rightTransition = false;
        private bool _upTransition = false;
        private bool _downTransition = false;

        //OverworldTextures
        private int _TextureTracker = 1;
        private Texture2D _Grass1;
        private Texture2D _Grass2;
        private Texture2D _Grass3;
        private Texture2D _Grass4;

        private int _currentScreenValue1 = 1;
        private int _currentScreenValue2 = 1;
        private bool _yourTurn;
        private int _transitionRng;

        private Player dude;
        private int _hp;
        private string _health;
        private Vector2 _textPos;
        private Texture2D _white;
        private Vector2 position;

        private bool _firstUse = false;

        private int VariableChecker = 0;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.ApplyChanges();
            _state = GameState.titleScreen;
            //_activeMap = 0;
            _playerSrc = new Rectangle[13];
            _batSrc = new Rectangle[6];
            _activePlayer = 1;
            _activeBat = 0;
            _sprint = false;
            _isLeft = false;
            _isRight = false;
            _isUp = false;
            _isDown = false;
            _yourTurn = true;
            _oldKB = Keyboard.GetState();
            _enemies = new List<Entity>();
            _bossEnemies = new List<Entity>();
            _rng = new Random();
            _turnTimer = 0;
            _bossEnemies = new List<Entity>();
            _menu = false;
            _menuPos = 0;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _textPos = new Vector2(2,2);
            position = new Vector2(450, 200);
            
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


            //Screen Difficulty
            //Left Column
            _screenDifficultyValues[0, 0] = 3;
            _screenDifficultyValues[1, 0] = 2;
            _screenDifficultyValues[2, 0] = 3;
            //Middle Column
            _screenDifficultyValues[0, 1] = 2;
            _screenDifficultyValues[1, 1] = 1;
            _screenDifficultyValues[2, 1] = 2;
            //Right Column
            _screenDifficultyValues[0, 2] = 3;
            _screenDifficultyValues[1, 2] = 2;
            _screenDifficultyValues[2, 2] = 3;

            //OverworldSpriteLoading
            _Grass1 = Content.Load<Texture2D>("Grass1");
            _Grass2 = Content.Load<Texture2D>("Grass2");
            _Grass3 = Content.Load<Texture2D>("Grass3");
            _Grass4 = Content.Load<Texture2D>("Grass4");

            // TODO: use this.Content to load your game content here
            _icons = Content.Load<Texture2D>("AttackMenu");
            _skills = Content.Load<Texture2D>("SkillsMenu");
            _screen = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            _player = Content.Load<Texture2D>("Updated Player Spritesheet");
            _bat = Content.Load<Texture2D>("spr_Bat");
            _blankTexture = Content.Load<Texture2D>("White Square");
            _pos = new Rectangle(400, 200, 50, 100);
            _playerSrc[0] = new Rectangle(0, 0, 64, 128);
            //FaceUp
            _playerSrc[1] = new Rectangle(0, 160, 80, 160);
            _playerSrc[2] = new Rectangle(80, 160, 80, 160);
            _playerSrc[3] = new Rectangle(160, 160, 80, 160);
            _playerSrc[4] = new Rectangle(240, 160, 80, 160);
            //FaceDown
            _playerSrc[5] = new Rectangle(0, 320, 80, 160);
            _playerSrc[6] = new Rectangle(80, 320, 80, 160);
            _playerSrc[7] = new Rectangle(160, 320, 80, 160);
            _playerSrc[8] = new Rectangle(240, 320, 80, 160);
            //FaceRight
            _playerSrc[9] = new Rectangle(0, 0, 80, 160);
            _playerSrc[10] = new Rectangle(80, 0, 80, 160);
            _playerSrc[11] = new Rectangle(160, 0, 80, 160);
            _playerSrc[12] = new Rectangle(240, 0, 80, 160);

            for (int i = 0; i < 7; i++)
            {
                [insert spritesheet rectangle listname here][i] = new Rectangle(i * 65, 0, 65, 65);
            }

            _text = Content.Load<SpriteFont>("Text");
            _white = Content.Load<Texture2D>("white");
            for (int i = 0; i < 6; i++)
            {
                _batSrc[i] = new Rectangle(i * 48, 0, 48, 48);
            }

            //LoadingScreenDimentionInts
            _screenWidth = GraphicsDevice.Viewport.Width;
            _screenHeight = GraphicsDevice.Viewport.Height;

            _screenWidthPortion = _screenWidth / 10;
            _screenHeightPortion = _screenHeight / 6;


            dude = new Player("name", _player);
            dude.makeSkillList();
            _hp = dude.getCurrHP();
            _health = "HP: " + _hp.ToString();
            
            
            _enemies.Clear();
            _activeEnemy = new Entity(50, 8, 3, 1, 3, "amogus", Content.Load<Texture2D>("Necromancer_creativekind-Sheet"), 100).clone(dude);
            // EASY ENEMIES
            // Slimes are a very easy enemy, should be all over the place at the start
            _enemies.Add(new Entity(10, 1, 2, 1, 2, "Slime", Content.Load<Texture2D>("Slime"), 50));
            // The Necomancer is a basic enemy, should be common at the start
            _enemies.Add(new Entity(25, 12 , 7, 1, 15, "Necromancer", Content.Load<Texture2D>("Necromancer_creativekind-Sheet"), 75));
            // The Soldier enemy should be one of the more common enemies found, not too challenging, but can take you out if you are not careful
            _enemies.Add(new Entity(30, 20 , 10, 3, 10, "Soldier", Content.Load<Texture2D>("SoldierIcon"), 150));
            //The Wizard is a magical attacking version of the soldier, with weaker physical defense
            _enemies.Add(new Entity(30, 3, 5, 25, 35, "Wizard", Content.Load<Texture2D>("WizardIcon"), 100));


            //MEDIUM ENEMIES
            // The Tank enemy should not be too diffucult, it merely exists to annoy the player
            _enemies.Add(new Entity(5, 1, 100, 1, 150, "Tank", Content.Load<Texture2D>("TankIcon"), 200));
            ////The captain is a stonger version of the soldier be aware when fighting them
            _enemies.Add(new Entity(90, 60, 35, 12, 70, "Captain", Content.Load<Texture2D>("CaptainIcon"), 300));
            //// Destructo is a rare glass cannon type enemy 
            _enemies.Add(new Entity(30, 90, 5, 1, 5, "Destructo", Content.Load<Texture2D>("DestructoIcon"), 250));
           

            ////PAIN ENEMIES
            //// The Knight is a late game enemy
            _enemies.Add(new Entity(300, 200, 150, 10, 150, "Knight", Content.Load<Texture2D>("KnightIcon"), 400));
            //// The Hunter is an early game boss that later becomes a normal enemy
            _enemies.Add(new Entity(60, 55, 40, 25, 40, "Hunter", Content.Load<Texture2D>("HunterIcon"), 250));
            
            ////A SPECIAL KIND OF PAIN
            //// The Vampire Knight is a tougher version of the Knight
            _enemies.Add(new Entity(500, 250, 150, 30, 200, "Vampire Knight", Content.Load<Texture2D>("VampireKnightIcon"), 500));
            //// The Blood Knight is a magical attacking version of the Knight
            _enemies.Add(new Entity(450, 10, 100, 200, 175, "Blood Knight", Content.Load<Texture2D>("BloodKnightIcon"), 400));
           


            // BOSS ENCOUNTERS
            // The Hunter is an early game boss that later becomes a normal enemy ENCOUNTER AT LEVEL 5
            _bossEnemies.Add(new Entity(60, 55, 40, 25, 40, "Hunter", Content.Load<Texture2D>("hunter"), 1000));
            //Lady Morona is an early game boss, meant to be a bottleneck for the player ENCOUNTER AT LEVEL 10!!!
            _bossEnemies.Add(new Entity(300,30,90,120,90, "Lady Morona", Content.Load<Texture2D>("Morona"), 1500)); 
            // Captain Odric is a mid game boss ENCOUNTER AT LEVEL 15
            _bossEnemies.Add(new Entity(500,300,180,100,180,"Captain Odric", Content.Load<Texture2D>("Odric"), 2000));
            // Vampire Knight Arvad is a late game boss ENCOUNTER AT LEVEL 20
            _bossEnemies.Add(new Entity(1500,500,300,120,300, "Vampire Knight Arvad", Content.Load<Texture2D>("Arvad"), 3000));
            //Vampire Lord CringeFail is the Final Boss of the game
            _bossEnemies.Add(new Entity(2500,1200,400,500,400, "Vampire Lord Cringefail",Content.Load<Texture2D>("Slime"),50000));
          
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            bool move = false;
            // TODO: Add your update logic here
            KeyboardState kb = Keyboard.GetState();
            if (_state == GameState.titleScreen)
            {
                if (kb.IsKeyDown(Keys.Space))
                {
                    _state = GameState.startScreen;
                }
            }
            else if (_state == GameState.startScreen)
            {
                if (kb.IsKeyDown(Keys.Space) && !_oldKB.IsKeyDown(Keys.Space))
                {
                    _state = GameState.overworld;
                    LoadContent();
                }
            }
            else if (_state == GameState.overworld)
            {
                if (kb.IsKeyDown(Keys.LeftShift))
                    _sprint = true;
                else
                {
                    _sprint = false;
                    _pos.Width = 50;
                    _pos.Height = 100;
                }
                if (!_sprint)
                {
                    if (kb.IsKeyDown(Keys.Up) && _pos.Y > 0)
                    {
                        _isLeft = false;
                        _isDown = false;
                        _isRight = false;
                        _isUp = true;
                        if (VariableChecker == 0)
                        {
                            _activePlayer = 5;

                            VariableChecker = 1;
                        }
                        _activePlayer += .25;
                        if (_activePlayer >= 9)
                        {
                            _activePlayer = 5;
                        }
                        
                        _pos.Y -= 2;
                        move = true;
                    }
                    else if (kb.IsKeyDown(Keys.Up) && _pos.Y <= 0)
                    {
                        _pos.Y = _screen.Height - 48;
                        _upTransition = true;
                        if (VariableChecker == 0)
                        {
                            _activePlayer = 5;

                            VariableChecker = 1;
                        }
                        _activePlayer += .25;
                        if (_activePlayer >= 9)
                        {
                            _activePlayer = 5;
                        }
                    }
                    if (kb.IsKeyDown(Keys.Down) && _pos.Y < _screen.Height - 48)
                    {
                        _isLeft = false;
                        _isDown = true;
                        _isRight = false;
                        _isUp = false;
                        if (VariableChecker == 0)
                        {
                            _activePlayer = 1;

                            VariableChecker = 1;
                        }

                        _activePlayer += .25;
                        if (_activePlayer >= 5)
                        {
                            _activePlayer = 1;
                        }
                        _pos.Y += 2;
                        move = true;
                    }
                    else if (kb.IsKeyDown(Keys.Down) && _pos.Y >= _screen.Height - 48)
                    {
                        _pos.Y = 0;
                        _downTransition = true;
                        if (VariableChecker == 0)
                        {
                            _activePlayer = 1;

                            VariableChecker = 1;
                        }

                        _activePlayer += .25;
                        if (_activePlayer >= 5)
                        {
                            _activePlayer = 1;
                        }
                    }
                    if (kb.IsKeyDown(Keys.Left) && _pos.X > 0)
                    {
                        _isLeft = true;
                        _isDown = false;
                        _isRight = false;
                        _isUp = false;
                        if (VariableChecker == 0)
                        {
                            _activePlayer = 9;

                            VariableChecker = 1;
                        }

                        _activePlayer += .25;
                        if (_activePlayer >= 13)
                        {
                            _activePlayer = 9;
                        }
                        _pos.X -= 2;
                        move = true;
                    }
                    else if (kb.IsKeyDown(Keys.Left) && _pos.X <= 0)
                    {
                        _pos.X = _screen.Width - 48;
                        _leftTransition = true;
                        if (VariableChecker == 0)
                        {
                            _activePlayer = 9;

                            VariableChecker = 1;
                        }

                        _activePlayer += .25;
                        if (_activePlayer >= 13)
                        {
                            _activePlayer = 9;
                        }
                    }
                    if (kb.IsKeyDown(Keys.Right) && _pos.X < _screen.Width - 48)
                    {
                        _isLeft = false;
                        _isDown = false;
                        _isRight = true;
                        _isUp = false;
                        if (VariableChecker == 0)
                        {
                            _activePlayer = 9;

                            VariableChecker = 1;
                        }
                        _activePlayer += .25;
                        if (_activePlayer >= 13)
                        {
                            _activePlayer = 9;
                        }
                        _pos.X += 2;
                        move = true;
                    }
                    else if (kb.IsKeyDown(Keys.Right) && _pos.X <= _screen.Width - 48)
                    {
                        if (VariableChecker == 0)
                        {
                            _activePlayer = 9;

                            VariableChecker = 1;
                        }
                        _activePlayer += .25;
                        if (_activePlayer >= 13)
                        {
                            _activePlayer = 9;
                        }
                        _pos.X = 0;
                        _rightTransition = true;
                    }
                    if (move)
                    {
                        if (_rng.Next(1000) < 3)
                        {
                            int max = 4;
                            if (_screenDifficultyValues[_currentScreenValue1, _currentScreenValue2] == 2)
                                max = 7;
                            else if (_screenDifficultyValues[_currentScreenValue1, _currentScreenValue2] == 3)
                                max = _enemies.Count;
                            _activeEnemy = _enemies[_rng.Next(max)].clone(dude);
                            _state = GameState.battle;
                            _yourTurn = true;
                        }
                    }
                }
                else
                {
                    if (kb.IsKeyDown(Keys.Up) && _pos.Y > 0)
                    {
                        _pos.Y -= 8;
                        move = true;
                    }
                    else if (kb.IsKeyDown(Keys.Up) && _pos.Y <= 0)
                    {
                        _pos.Y = _screen.Height - 48;
                        _upTransition = true;
                    }
                    if (kb.IsKeyDown(Keys.Down) && _pos.Y < _screen.Height - 48)
                    {
                        _pos.Y += 8;
                        move = true;
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
                        move = true;
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
                        move = true;
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
                    if (move)
                    {
                        if (_rng.Next(1000) < 10)
                        {
                            int max = 4;
                            if (_screenDifficultyValues[_currentScreenValue1, _currentScreenValue2] == 2)
                                max = 7;
                            else if (_screenDifficultyValues[_currentScreenValue1, _currentScreenValue2] == 3)
                                max = _enemies.Count;
                            _activeEnemy = _enemies[_rng.Next(max)].clone(dude); ;
                            _state = GameState.battle;
                            _yourTurn = true;
                        }
                    }
                }
            }
            else if (_state == GameState.battle)
            {
                if (_yourTurn && _turnTimer <= 0)
                {
                    if (!_menu)
                    {
                        if ((kb.IsKeyDown(Keys.Right) || kb.IsKeyDown(Keys.Left)) && !(_oldKB.IsKeyDown(Keys.Left) || _oldKB.IsKeyDown(Keys.Right)))
                        {
                            _selector = !_selector;
                        }
                        if (_yourTurn && _turnTimer <= 0)
                        {
                            if (kb.IsKeyDown(Keys.Z))
                            {
                                if (_selector)
                                {
                                    _turnTimer = 30;
                                    dude.attack(_activeEnemy);
                                    _yourTurn = false;
                                }
                                else
                                {
                                    _menu = true;
                                    _turnTimer = 30;
                                }
                            }
                        }
                    }
                    else if (_menu)
                    {
                        if (kb.IsKeyDown(Keys.X))
                        {
                            _menu = false;
                            _turnTimer = 30;
                        }
                        else if (kb.IsKeyDown(Keys.Z))
                        {
                            dude.useSkill(_activeEnemy, Player._skillList[_menuPos]);
                            _turnTimer = 30;
                            _yourTurn = false;
                            _menu = false;
                        }
                        else if (kb.IsKeyDown(Keys.Down) && _menuPos < dude.getLevel() - 1 && !_oldKB.IsKeyDown(Keys.Down))
                        {
                            _menuPos++;
                        }
                        else if (kb.IsKeyDown(Keys.Up) && _menuPos > 0 && !_oldKB.IsKeyDown(Keys.Up))
                            _menuPos--;
                    }
                }
                if (!_yourTurn && _turnTimer <= 0)
                {
                    _activeEnemy.attack(dude);
                    _yourTurn = true;
                    _turnTimer = 30;
                }
                _turnTimer--;
                if (_activeEnemy.getCurrHP() <= 0)
                    _state = GameState.overworld;
                if (dude.getCurrHP() <= 0)
                    _state = GameState.startScreen;
            }
            _oldKB = kb;

            //Transition UPDATING
            if (_upTransition && _currentScreenValue2 != -1)
            {
                _testOverworldScreens[_currentScreenValue1, _currentScreenValue2] = 0;
                _currentScreenValue2 -= 1;
                _testOverworldScreens[_currentScreenValue1, _currentScreenValue2] = 1;

                _upTransition = false;
            }
            if (_downTransition && _currentScreenValue2 != 3)
            {
                _testOverworldScreens[_currentScreenValue1, _currentScreenValue2] = 0;
                _currentScreenValue2 += 1;
                _testOverworldScreens[_currentScreenValue1, _currentScreenValue2] = 1;

                _downTransition = false;
            }
            if (_leftTransition)
            {
                _testOverworldScreens[_currentScreenValue1, _currentScreenValue2] = 0;
                _currentScreenValue1 -= 1;
                _testOverworldScreens[_currentScreenValue1, _currentScreenValue2] = 1;

                _leftTransition = false;
            }
            if (_rightTransition)
            {
                _testOverworldScreens[_currentScreenValue1, _currentScreenValue2] = 0;
                _currentScreenValue1 += 1;
                _testOverworldScreens[_currentScreenValue1, _currentScreenValue2] = 1;

                _rightTransition = false;
            }
            if (_leftTransition && _currentScreenValue2 == -1)
            {
                _testOverworldScreens[_currentScreenValue1, _currentScreenValue2] = 0;
                _currentScreenValue1 = _rng.Next(0, 3);
                _currentScreenValue2 = _rng.Next(0, 3);
                _testOverworldScreens[_currentScreenValue1, _currentScreenValue2] = 1;

                _leftTransition = false;
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

                        //TextureOverrider
                        
                        if (_TextureTracker == 1)
                        {
                            _testOverworldTileTextures[i, j] = Content.Load<Texture2D>("Grass1");
                        }
                        if (_TextureTracker == 2)
                        {
                            _testOverworldTileTextures[i, j] = Content.Load<Texture2D>("Grass2");
                        }
                        if (_TextureTracker == 3)
                        {
                            _testOverworldTileTextures[i, j] = Content.Load<Texture2D>("Grass3");
                        }
                        if (_TextureTracker == 4)
                        {
                            _testOverworldTileTextures[i, j] = Content.Load<Texture2D>("Grass4");
                        }

                        if (_TextureTracker != 5)
                        {
                            _TextureTracker++;
                        }
                        if (_TextureTracker == 5)
                        {
                            _TextureTracker = 1;
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

                        
                        //Field
                        if (j % 2 == 0)
                        {
                            _testOverworldTiles[i, j] = new Rectangle(_updateTileDimensionsHeight, _updateTileDimensionsWidth, _screenWidthPortion, _screenHeightPortion);

                            _testOverworldTileProperties[i, j] = "Grass";
                            _testOverworldTileTextures[i, j] = Content.Load<Texture2D>("Grass Texture");

                            if (_TextureTracker == 1)
                            {
                                _testOverworldTileTextures[i, j] = Content.Load<Texture2D>("Grass1");
                            }
                            if (_TextureTracker == 2)
                            {
                                _testOverworldTileTextures[i, j] = Content.Load<Texture2D>("Grass2");
                            }
                            if (_TextureTracker == 3)
                            {
                                _testOverworldTileTextures[i, j] = Content.Load<Texture2D>("Grass3");
                            }
                            if (_TextureTracker == 4)
                            {
                                _testOverworldTileTextures[i, j] = Content.Load<Texture2D>("Grass4");
                            }
                        }
                        //Water
                        if (j % 2 != 0)
                        {
                            _testOverworldTiles[i, j] = new Rectangle(_updateTileDimensionsHeight, _updateTileDimensionsWidth, _screenWidthPortion, _screenHeightPortion);

                            _testOverworldTileProperties[i, j] = "Water";
                            _testOverworldTileTextures[i, j] = Content.Load<Texture2D>("Water Texture");

                            if (_TextureTracker == 1)
                            {
                                _testOverworldTileTextures[i, j] = Content.Load<Texture2D>("Water1");
                            }
                            if (_TextureTracker == 2)
                            {
                                _testOverworldTileTextures[i, j] = Content.Load<Texture2D>("Water2");
                            }
                            if (_TextureTracker == 3)
                            {
                                _testOverworldTileTextures[i, j] = Content.Load<Texture2D>("Water3");
                            }
                            if (_TextureTracker == 4)
                            {
                                _testOverworldTileTextures[i, j] = Content.Load<Texture2D>("Water4");
                            }
                        }

                        if (_TextureTracker != 5)
                        {
                            _TextureTracker++;
                        }
                        if (_TextureTracker == 5)
                        {
                            _TextureTracker = 1;
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

                            if (_TextureTracker == 1)
                            {
                                _testOverworldTileTextures[i, j] = Content.Load<Texture2D>("Grass1");
                            }
                            if (_TextureTracker == 2)
                            {
                                _testOverworldTileTextures[i, j] = Content.Load<Texture2D>("Grass2");
                            }
                            if (_TextureTracker == 3)
                            {
                                _testOverworldTileTextures[i, j] = Content.Load<Texture2D>("Grass3");
                            }
                            if (_TextureTracker == 4)
                            {
                                _testOverworldTileTextures[i, j] = Content.Load<Texture2D>("Grass4");
                            }
                        }
                        //Pavement
                        else
                        {
                            _testOverworldTiles[i, j] = new Rectangle(_updateTileDimensionsHeight, _updateTileDimensionsWidth, _screenWidthPortion, _screenHeightPortion);

                            _testOverworldTileProperties[i, j] = "Sand";
                            _testOverworldTileTextures[i, j] = Content.Load<Texture2D>("Sand Texture");

                            //TextureOverrider

                            if (_TextureTracker == 1)
                            {
                                _testOverworldTileTextures[i, j] = Content.Load<Texture2D>("Pavement1");
                            }
                            if (_TextureTracker == 2)
                            {
                                _testOverworldTileTextures[i, j] = Content.Load<Texture2D>("Pavement2");
                            }
                            if (_TextureTracker == 3)
                            {
                                _testOverworldTileTextures[i, j] = Content.Load<Texture2D>("Pavement3");
                            }
                            if (_TextureTracker == 4)
                            {
                                _testOverworldTileTextures[i, j] = Content.Load<Texture2D>("Pavement4");
                            }

                            

                        }
                        if (_TextureTracker != 5)
                        {
                            _TextureTracker++;
                        }
                        if (_TextureTracker == 5)
                        {
                            _TextureTracker = 1;
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
                        
                        //Field
                        if (j == 2 || j == 3 || j == 5 || j == 7)
                        {
                            _testOverworldTiles[i, j] = new Rectangle(_updateTileDimensionsHeight, _updateTileDimensionsWidth, _screenWidthPortion, _screenHeightPortion);

                            _testOverworldTileProperties[i, j] = "Grass";
                            _testOverworldTileTextures[i, j] = Content.Load<Texture2D>("Grass Texture");

                            if (_TextureTracker == 1)
                            {
                                _testOverworldTileTextures[i, j] = Content.Load<Texture2D>("Grass1");
                            }
                            if (_TextureTracker == 2)
                            {
                                _testOverworldTileTextures[i, j] = Content.Load<Texture2D>("Grass2");
                            }
                            if (_TextureTracker == 3)
                            {
                                _testOverworldTileTextures[i, j] = Content.Load<Texture2D>("Grass3");
                            }
                            if (_TextureTracker == 4)
                            {
                                _testOverworldTileTextures[i, j] = Content.Load<Texture2D>("Grass4");
                            }

                            
                        }
                        //Water
                        else
                        {
                            _testOverworldTiles[i, j] = new Rectangle(_updateTileDimensionsHeight, _updateTileDimensionsWidth, _screenWidthPortion, _screenHeightPortion);

                            
                            _testOverworldTileTextures[i, j] = Content.Load<Texture2D>("Water Texture");

                            if (_TextureTracker == 1)
                            {
                                _testOverworldTileProperties[i, j] = "Water";
                                _testOverworldTileTextures[i, j] = Content.Load<Texture2D>("Water1");
                            }
                            
                            if (_TextureTracker == 2)
                            {
                                _testOverworldTileProperties[i, j] = "Water";
                                _testOverworldTileTextures[i, j] = Content.Load<Texture2D>("Water2");
                            }
                            
                            if (_TextureTracker == 3)
                            {
                                _testOverworldTileProperties[i, j] = "Water";
                                _testOverworldTileTextures[i, j] = Content.Load<Texture2D>("Water3");
                            }
                            if (_TextureTracker == 3 && (j == 0 || j == 4 || j == 1))
                            {
                                _testOverworldTileProperties[i, j] = "Bridge";
                                _testOverworldTileTextures[i, j] = Content.Load<Texture2D>("BridgeUp1");
                            }
                            if (_TextureTracker == 4)
                            {
                                _testOverworldTileProperties[i, j] = "Water";
                                _testOverworldTileTextures[i, j] = Content.Load<Texture2D>("Water4");
                            }
                            if (_TextureTracker == 4 && j == 1)
                            {
                                _testOverworldTileProperties[i, j] = "Bridge";
                                _testOverworldTileTextures[i, j] = Content.Load<Texture2D>("BridgeUp1");
                            }


                            
                        }
                        if (_TextureTracker != 5)
                        {
                            _TextureTracker++;
                        }
                        if (_TextureTracker == 5)
                        {
                            _TextureTracker = 1;
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
                        
                        //Sand
                        if (j == 0 || j == 2 || j == 3 || j == 5 || j == 7)
                        {
                            _testOverworldTiles[i, j] = new Rectangle(_updateTileDimensionsHeight, _updateTileDimensionsWidth, _screenWidthPortion, _screenHeightPortion);

                            _testOverworldTileProperties[i, j] = "Sand";
                            _testOverworldTileTextures[i, j] = Content.Load<Texture2D>("Sand Texture");

                            if (_TextureTracker == 1)
                            {
                                _testOverworldTileTextures[i, j] = Content.Load<Texture2D>("Pavement1");
                            }
                            if (_TextureTracker == 2)
                            {
                                _testOverworldTileTextures[i, j] = Content.Load<Texture2D>("Pavement2");
                            }
                            if (_TextureTracker == 3)
                            {
                                _testOverworldTileTextures[i, j] = Content.Load<Texture2D>("Pavement3");
                            }
                            if (_TextureTracker == 4)
                            {
                                _testOverworldTileTextures[i, j] = Content.Load<Texture2D>("Pavement4");
                            }
                        }
                        //Water
                        else
                        {
                            _testOverworldTiles[i, j] = new Rectangle(_updateTileDimensionsHeight, _updateTileDimensionsWidth, _screenWidthPortion, _screenHeightPortion);

                            _testOverworldTileProperties[i, j] = "Water";
                            _testOverworldTileTextures[i, j] = Content.Load<Texture2D>("Water Texture");

                            if (_TextureTracker == 1)
                            {
                                _testOverworldTileTextures[i, j] = Content.Load<Texture2D>("Water1");
                            }
                            if (_TextureTracker == 2)
                            {
                                _testOverworldTileTextures[i, j] = Content.Load<Texture2D>("Water2");
                            }
                            if (_TextureTracker == 3)
                            {
                                _testOverworldTileTextures[i, j] = Content.Load<Texture2D>("BridgeUp1");
                            }
                            if (_TextureTracker == 4)
                            {
                                _testOverworldTileTextures[i, j] = Content.Load<Texture2D>("BridgeUp1");
                            }
                        }

                        if (_TextureTracker != 5)
                        {
                            _TextureTracker++;
                        }
                        if (_TextureTracker == 5)
                        {
                            _TextureTracker = 1;
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


                        if (_TextureTracker == 1)
                        {
                            _testOverworldTileTextures[i, j] = Content.Load<Texture2D>("Water1");
                        }
                        if (_TextureTracker == 2)
                        {
                            _testOverworldTileTextures[i, j] = Content.Load<Texture2D>("Water2");
                        }
                        if (_TextureTracker == 3)
                        {
                            _testOverworldTileTextures[i, j] = Content.Load<Texture2D>("Water3");
                        }
                        if (_TextureTracker == 4)
                        {
                            _testOverworldTileTextures[i, j] = Content.Load<Texture2D>("Water4");
                        }
                        if (_TextureTracker != 5)
                        {
                            _TextureTracker++;
                        }
                        if (_TextureTracker == 5)
                        {
                            _TextureTracker = 1;
                        }
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

                        if (_TextureTracker == 1)
                        {
                            _testOverworldTileTextures[i, j] = Content.Load<Texture2D>("Grass1");
                        }
                        if (_TextureTracker == 2)
                        {
                            _testOverworldTileTextures[i, j] = Content.Load<Texture2D>("Grass2");
                        }
                        if (_TextureTracker == 3)
                        {
                            _testOverworldTileTextures[i, j] = Content.Load<Texture2D>("Grass3");
                        }
                        if (_TextureTracker == 4)
                        {
                            _testOverworldTileTextures[i, j] = Content.Load<Texture2D>("Grass4");
                        }

                    }
                    if (_TextureTracker != 5)
                    {
                        _TextureTracker++;
                    }
                    if (_TextureTracker == 5)
                    {
                        _TextureTracker = 1;
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

                            if (_TextureTracker == 1)
                            {
                                _testOverworldTileTextures[i, j] = Content.Load<Texture2D>("Water1");
                            }
                            if (_TextureTracker == 2)
                            {
                                _testOverworldTileTextures[i, j] = Content.Load<Texture2D>("Water2");
                            }
                            if (_TextureTracker == 3)
                            {
                                _testOverworldTileTextures[i, j] = Content.Load<Texture2D>("Water3");
                            }
                            if (_TextureTracker == 4)
                            {
                                _testOverworldTileTextures[i, j] = Content.Load<Texture2D>("Water4");
                            }

                        }
                        //sand
                        else
                        {
                            _testOverworldTiles[i, j] = new Rectangle(_updateTileDimensionsHeight, _updateTileDimensionsWidth, _screenWidthPortion, _screenHeightPortion);

                            _testOverworldTileProperties[i, j] = "Sand";
                            _testOverworldTileTextures[i, j] = Content.Load<Texture2D>("Sand Texture");

                            if (_TextureTracker == 1)
                            {
                                _testOverworldTileTextures[i, j] = Content.Load<Texture2D>("Pavement1");
                            }
                            if (_TextureTracker == 2)
                            {
                                _testOverworldTileTextures[i, j] = Content.Load<Texture2D>("Pavement2");
                            }
                            if (_TextureTracker == 3)
                            {
                                _testOverworldTileTextures[i, j] = Content.Load<Texture2D>("Pavement3");
                            }
                            if (_TextureTracker == 4)
                            {
                                _testOverworldTileTextures[i, j] = Content.Load<Texture2D>("Pavement4");
                            }
                        }
                        if (_TextureTracker != 5)
                        {
                            _TextureTracker++;
                        }
                        if (_TextureTracker == 5)
                        {
                            _TextureTracker = 1;
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

                        if (_TextureTracker == 1)
                        {
                            _testOverworldTileTextures[i, j] = Content.Load<Texture2D>("Pavement1");
                        }
                        if (_TextureTracker == 2)
                        {
                            _testOverworldTileTextures[i, j] = Content.Load<Texture2D>("Pavement2");
                        }
                        if (_TextureTracker == 3)
                        {
                            _testOverworldTileTextures[i, j] = Content.Load<Texture2D>("Pavement3");
                        }
                        if (_TextureTracker == 4)
                        {
                            _testOverworldTileTextures[i, j] = Content.Load<Texture2D>("Pavement4");
                        }

                        if (_TextureTracker != 5)
                        {
                            _TextureTracker++;
                        }
                        if (_TextureTracker == 5)
                        {
                            _TextureTracker = 1;
                        }
                    }
                    
                }
                
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            if (_state == GameState.titleScreen)
            {
                GraphicsDevice.Clear(Color.Black);
                //DRAW TITLECARD HERE
                _spriteBatch.DrawString(_text, "Once upon a time, a man was minding his own business at home, when a Vampire Lord \n rudely came and attacked him. Now, he must have his vengeance, eliminating all who \n stand in his way. That man is known as the", new Vector2(10, 0), Color.White);
                _spriteBatch.DrawString(_text, "Press space to continue", new Vector2(300, 450), Color.White);
            }
            else if (_state == GameState.startScreen)
            {
                GraphicsDevice.Clear(Color.Black);
                _spriteBatch.DrawString(_text, "Controls: \n Overworld: \n Arrow Keys to move \n Shift to move faster \n \n Combat: \n Arrow keys to change selection \n Z to confirm \n X to Cancel", new Vector2(), Color.White);
                _spriteBatch.DrawString(_text, "Press space to continue", new Vector2(300, 450), Color.White);
            }
            else
            {
                GraphicsDevice.Clear(Color.White);
            }
            if (_state == GameState.overworld)
            {
                for (int i = 0; i < 10; i++)
                {
                    int _updateTileDimensionsHeight = i * _screenHeightPortion;
                    for (int j = 0; j < 6; j++)
                    {
                        int _updateTileDimensionsWidth = j * _screenWidthPortion;
                        _spriteBatch.Draw(_testOverworldTileTextures[i, j], _testOverworldTiles[i, j], Color.White);
                    }
                }
                if (!_sprint && !_isLeft)
                {
                    _spriteBatch.Draw(_player, _pos, _playerSrc[(int)_activePlayer], Color.White);
                }
                if (!_sprint && _isLeft)
                {
                    _spriteBatch.Draw(_player, _pos, _playerSrc[(int)_activePlayer], Color.White, 0, new Vector2(), SpriteEffects.FlipHorizontally, 0);
                }
                else if (!_sprint && _isUp)
                {
                    
                    _spriteBatch.Draw(_player, _pos, _playerSrc[(int)_activePlayer], Color.White);
                }
                else if (!_sprint && _isDown)
                {
                    _spriteBatch.Draw(_player, _pos, _playerSrc[(int)_activePlayer], Color.White);
                }
                else if (_sprint && _isLeft)
                {
                    _pos.Width = 50;
                    _pos.Height = 50;
                    _spriteBatch.Draw(_bat, _pos, _batSrc[(int)_activeBat], Color.White, 0, new Vector2(), SpriteEffects.FlipHorizontally, 0);
                }
                else if (_sprint)
                {
                    _pos.Width = 50;
                    _pos.Height = 50;
                    _spriteBatch.Draw(_bat, _pos, _batSrc[(int)_activeBat], Color.White);
                }
                else
                    _spriteBatch.Draw(_player, _pos, _playerSrc[(int)_activePlayer], Color.White);
            }
            else if (_state == GameState.battle)
            {
                dude.Draw(_spriteBatch, new Vector2(100, 180), _playerSrc[9]);
                if (_yourTurn && _turnTimer <= 0)
                {
                    if (!_menu)
                    {
                        _spriteBatch.Draw(_icons, new Vector2(100, 350), Color.White);
                        _spriteBatch.Draw(_skills, new Vector2(200, 350), Color.White);
                        if (_selector)
                            _spriteBatch.Draw(_blankTexture, new Vector2(100, 350), Color.White);
                        else
                            _spriteBatch.Draw(_blankTexture, new Vector2(200, 350), Color.White);
                    }
                    else
                    {
                        _spriteBatch.Draw(_white, new Vector2(195, 20 * _menuPos), Color.White);
                        for (int i = 0; i < dude.getLevel() && i < Player._skillList.Count; i++)
                        {
                            Player._skillList[i].Draw(_spriteBatch, new Vector2(200, i * 20), _text, dude.getCurrHP());
                        }
                    }
                }
                _spriteBatch.DrawString(_text, "HP: " + dude.getCurrHP() + "\n Lv: " + dude.getLevel(), _textPos, Color.DarkRed);
                if (_activeEnemy.getCurrHP() > 0)
                    _activeEnemy.Draw(_spriteBatch, new Vector2(500, 200), null);
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
