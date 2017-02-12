using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Threading;
using System.Collections.Generic;

namespace TestGame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        TCPServer tcpServer;
        UDPServer udpServer;
        private SpriteFont font;
        Dictionary<string, Player> players;
        Vector2 PlayerCounterPosition;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            tcpServer = new TCPServer(1234, this);
            udpServer = new UDPServer(1234, this);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            new Thread(new ThreadStart(tcpServer.LoopClients)).Start();
            new Thread(new ThreadStart(udpServer.LoopClients)).Start();
            // TODO: Add your initialization logic here
            base.Initialize();
            players = new Dictionary<string, Player>();
            PlayerCounterPosition = new Vector2(10,10);

        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            base.LoadContent();
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("Graphics/Testfont");
            // TODO: use this.Content to load your game content here
        }

        internal void DestroyPlayer(string id)
        {
            players.Remove(id);
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            base.Update(gameTime);

            
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            
            base.Draw(gameTime);

            spriteBatch.Begin();
            drawPlayerCounter();
            spriteBatch.End();
        }


        void drawPlayerCounter()
        {
            string playerCounterText = "Players connected: " + players.Count;
            spriteBatch.DrawString(font, playerCounterText, PlayerCounterPosition, Color.Black);
        }

        public void CreatePlayer(string id)
        {
			Player player = new Player(id);
            players.Add(id, player);
        }

        public void UpdatePlayer(PlayerInput input)
        {
            if (input == null)
                return;
            Player p;
            if (!players.TryGetValue(input.id, out p))
                return;
			p.Input.ReceiveInputs(input);

			/*
		switch (action) {
			case PlayerAction.move:
			{                    
				float x_tmp = p.Position.X + x * playerMoveSpeed;
				float y_tmp = p.Position.Y += y * playerMoveSpeed;
				p.Position.X = MathHelper.Clamp(x_tmp, 0, GraphicsDevice.Viewport.Width - p.Width);
				p.Position.Y = MathHelper.Clamp(y_tmp, 0, GraphicsDevice.Viewport.Height - p.Height);
				break;
			}
			case PlayerAction.pressA:
			{
					//do smth
					p.buttonInput = "A";
				break;
			}
			case PlayerAction.pressB:
			{
					//do smth
					p.buttonInput = "B";
					break;
			}

		}
		*/
		}


	}
}
