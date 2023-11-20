using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;


namespace TuesdayListsInMonoGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Texture2D _hero;
        private Texture2D _stone;
        private Texture2D _cloud;

        private List<Vector2> _floorPosition;
        private List<Vector2> _cloudPosition;
        private List<float> _cloudSpeed;
        private List<float> _cloudTransparency;

        private Random _rng;

        private int _count = 0;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _floorPosition = new List<Vector2>();
            _cloudPosition = new List<Vector2>();
            _cloudTransparency = new List<float>();
            _cloudSpeed = new List<float>();

            _rng = new Random();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _hero = Content.Load<Texture2D>("hero");
            _stone = Content.Load<Texture2D>("stone");
            _cloud = Content.Load<Texture2D>("cloud");

            for (int i = 0; i < 7; i++)
            {
                _floorPosition.Add(new Vector2(i * _stone.Width * 0.25f, 425));  
            }

            for (int i = 0; i < 20; i++)
            {
                _cloudPosition.Add(new Vector2(_rng.Next(0, 1600), _rng.Next(0,250)));
                _cloudTransparency.Add(_rng.Next(45, 101) / 100f);
                _cloudSpeed.Add(_rng.Next(0, 101) / 100f);
            }

            



            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            /*
            _count++;
            if(_count % 60 == 0 && _floorPosition.Count > 0)
            {
                _floorPosition.RemoveAt(0);
            }
            */

            for (int i = 0; i < _cloudSpeed.Count; i++)
            {
                _cloudPosition[i] += new Vector2(-_cloudSpeed[i], 0);
            }


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();


            //draw the floor
            for (int i = 0; i < _floorPosition.Count; i++)
            {
                _spriteBatch.Draw(_stone,    //this is our texture
                    _floorPosition[i],    //this is our position
                    null,                   //ignore this
                    Color.White,            //this is the colour overlay
                    0f,                      //this is the rotation
                    new Vector2(_stone.Width / 2, _stone.Height / 2),  //this is the origin 
                    new Vector2(0.25f, 0.25f),       //this is our scale
                    SpriteEffects.None,     //this is a flip (none)
                    0);                     //this is the layer (ignore)
            }

            //draw the clouds
            for (int i = 0; i < _cloudPosition.Count; i++)
            {
                _spriteBatch.Draw(_cloud,    //this is our texture
                    _cloudPosition[i],    //this is our position
                    null,                   //ignore this
                    Color.White * _cloudTransparency[i],            //this is the colour overlay
                    0f,                      //this is the rotation
                    new Vector2(_cloud.Width * 0.25f / 2, _cloud.Height * 0.25f / 2), //this is the origin
                    new Vector2(0.25f, 0.25f),       //this is our scale
                    SpriteEffects.None,     //this is a flip (none)
                    0);                     //this is the layer (ignore)
            }

            _spriteBatch.Draw(_hero, new Vector2(325, 150), null, Color.White, 0, new Vector2(0,0), new Vector2(0.5f, 0.5f), SpriteEffects.None, 0);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
