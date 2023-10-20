using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace sprite_collisions;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private SpriteBatch _debugSpriteBatch;
    private SpriteFont _debugSpriteFont;

    private List<Sprite> _sprites;

    public string debugString
    {
         get; set;
    }

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        debugString = "DEBUG STRING";
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _debugSpriteBatch = new SpriteBatch(GraphicsDevice);
        _debugSpriteFont = Content.Load<SpriteFont>("DebugFont");

        var playerTexture = Content.Load<Texture2D>("square_32");

        // Instantiate Sprite Objects for the scene
        _sprites = new List<Sprite>()
        {
            new Player(playerTexture)
            {
                input = new Input()
                {
                    Left = Keys.A,
                    Right = Keys.D,
                    Up = Keys.W,
                    Down = Keys.S,
                },
                Position = new Vector2(100,100),
                Color = Color.Blue,
                speed = 5f,
                Tag = "Player1 (Blue)",
            },
            new Player(playerTexture)
            {
                input = new Input()
                {
                    Left = Keys.Left,
                    Right = Keys.Right,
                    Up = Keys.Up,
                    Down = Keys.Down,
                },
                Position = new Vector2(300,100),
                Color = Color.Red,
                speed = 5f,
                Tag = "Player3 (Red)",
            },
            new Player(playerTexture)
            {
                input = new Input()
                {
                    Left = Keys.J,
                    Right = Keys.L,
                    Up = Keys.I,
                    Down = Keys.K,
                },
                Position = new Vector2(332,100),
                Color = Color.Black,
                speed = 5f,
                Tag = "Player4 (Black)",
            },

            // Spawn Ground Objects
            new(playerTexture)
            {
                Position = new Vector2(100,400),
                Color = Color.SandyBrown,
                speed = 5f,
            },
            new(playerTexture)
            {
                Position = new Vector2(132,400),
                Color = Color.SandyBrown,
                speed = 5f,
            },
            new(playerTexture)
            {
                Position = new Vector2(164,400),
                Color = Color.SandyBrown,
                speed = 5f,
            },
            new(playerTexture)
            {
                Position = new Vector2(196,400),
                Color = Color.SandyBrown,
                speed = 5f,
            },
            new(playerTexture)
            {
                Position = new Vector2(228,400),
                Color = Color.SandyBrown,
                speed = 5f,
            },
            new(playerTexture)
            {
                Position = new Vector2(260,400),
                Color = Color.SandyBrown,
                speed = 5f,
            }

                

            
        };

        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        foreach (var sprite in _sprites)
        {
            sprite.Update(gameTime, _sprites);

            // Get debug string from player
            if (sprite.Tag == "Player1 (Blue)")
            {
                debugString = sprite.debugString;
            }
        }
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();
        foreach (var sprite in _sprites)
            sprite.Draw(_spriteBatch);

        _spriteBatch.End();

        DebugRender();

        base.Draw(gameTime);
    }

    private void DebugRender()
    {
        _debugSpriteBatch.Begin();
        _debugSpriteBatch.DrawString(_debugSpriteFont,debugString,new Vector2(10f,10f),Color.Black);
        _debugSpriteBatch.End();
    }
}
