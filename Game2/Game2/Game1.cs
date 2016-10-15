using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

using Shooter;
using System;

namespace Game2
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //Fonts
        //private SpriteFont font;
        //private int score = 0;

        //Rotating Mine
        //private Texture2D arrow, arrow2;
        //private float angle = 0;


        // Represents the player 

        Player player;
        //Player player2;

        // Keyboard states used to determine key presses

        KeyboardState currentKeyboardState;

        KeyboardState previousKeyboardState;

        // Gamepad states used to determine button presses

        GamePadState currentGamePadState;

        GamePadState previousGamePadState;



        //Mouse states used to track Mouse button press

        MouseState currentMouseState;

        MouseState previousMouseState;



        // A movement speed for the player

        float playerMoveSpeed;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            // Initialize the player class

            player = new Player();

            // Set a constant player move speed

            playerMoveSpeed = 8.0f;


            //Enable the FreeDrag gesture.

            TouchPanel.EnabledGestures = GestureType.FreeDrag;



            //player2 = new Player();
            base.Initialize();



        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            // Load the player resources 


            //[OLD CODE- NO ANIMATION]
            //First Player
            //Vector2 playerPosition = new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X, GraphicsDevice.Viewport.TitleSafeArea.Y + GraphicsDevice.Viewport.TitleSafeArea.Height / 2);

            //Vector2 playerPosition = new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X, GraphicsDevice.Viewport.TitleSafeArea.Height);
            //player.Initialize(Content.Load<Texture2D>("Graphics\\player"), playerPosition);

            //Second Player
            //Vector2 playerPosition2 = new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X, GraphicsDevice.Viewport.TitleSafeArea.Y + GraphicsDevice.Viewport.TitleSafeArea.Height / 5);

            //player2.Initialize(Content.Load<Texture2D>("Graphics\\player"), playerPosition2);

            //Font
            //font = Content.Load<SpriteFont>("gameFont");


            // Load the player resources

            Animation playerAnimation = new Animation();

            Texture2D playerTexture = Content.Load<Texture2D>("Graphics\\shipAnimation");

            playerAnimation.Initialize(playerTexture, Vector2.Zero, 115, 69, 8, 30, Color.White, 1f, true);


            Vector2 playerPosition = new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X, GraphicsDevice.Viewport.TitleSafeArea.Y + GraphicsDevice.Viewport.TitleSafeArea.Height / 2);

            player.Initialize(playerAnimation, playerPosition);

            //Limit to 30FPS
            //TargetElapsedTime = TimeSpan.FromTicks(333333);

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
            // TODO: Add your update logic here
            previousMouseState = currentMouseState;

            currentMouseState = Mouse.GetState();


            // Save the previous state of the keyboard and game pad so we can determine single key/button presses

            previousGamePadState = currentGamePadState;

            previousKeyboardState = currentKeyboardState;



            // Read the current state of the keyboard and gamepad and store it

            currentKeyboardState = Keyboard.GetState();

            currentGamePadState = GamePad.GetState(PlayerIndex.One);

            //Font Score Increment
            //score++;

            //Angle increment
            //angle += 0.01f;

            //Update the player

            UpdatePlayer(gameTime);

            base.Update(gameTime);

        }

        private void UpdatePlayer(GameTime gameTime)

        {
            player.Update(gameTime);
            // Get Thumbstick Controls

            player.Position.X += currentGamePadState.ThumbSticks.Left.X * playerMoveSpeed;

            player.Position.Y -= currentGamePadState.ThumbSticks.Left.Y * playerMoveSpeed;


            // Windows 8 Touch Gestures for MonoGame

            while (TouchPanel.IsGestureAvailable)

            {

                GestureSample gesture = TouchPanel.ReadGesture();



                if (gesture.GestureType == GestureType.FreeDrag)

                {

                    player.Position += gesture.Delta;


                }

            }

            //Get Mouse State then Capture the Button type and Respond Button Press

            Vector2 mousePosition = new Vector2(currentMouseState.X, currentMouseState.Y);

            if (currentMouseState.LeftButton == ButtonState.Pressed)

            {

                Vector2 posDelta = mousePosition - player.Position;

                posDelta.Normalize();

                posDelta = posDelta * playerMoveSpeed;

                player.Position = player.Position + posDelta;

            }



            // Use the Keyboard / Dpad

            if (currentKeyboardState.IsKeyDown(Keys.Left) || currentGamePadState.DPad.Left == ButtonState.Pressed)

            {

                player.Position.X -= playerMoveSpeed;

            }



            if (currentKeyboardState.IsKeyDown(Keys.Right) || currentGamePadState.DPad.Right == ButtonState.Pressed)

            {

                player.Position.X += playerMoveSpeed;

            }



            if (currentKeyboardState.IsKeyDown(Keys.Up) || currentGamePadState.DPad.Up == ButtonState.Pressed)

            {

                player.Position.Y -= playerMoveSpeed;

            }



            if (currentKeyboardState.IsKeyDown(Keys.Down) || currentGamePadState.DPad.Down == ButtonState.Pressed)

            {

                player.Position.Y += playerMoveSpeed;

            }



            // Make sure that the player does not go out of bounds

            player.Position.X = MathHelper.Clamp(player.Position.X, 0, GraphicsDevice.Viewport.Width - player.Width);

            player.Position.Y = MathHelper.Clamp(player.Position.Y, 0, GraphicsDevice.Viewport.Height - player.Height);

        }


        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            // Start drawing

            spriteBatch.Begin();



            // Draw the Player

            player.Draw(spriteBatch);
            //player2.Draw(spriteBatch);
            //spriteBatch.DrawString(font, "Score", new Vector2(100, 100), Color.Black);
            //spriteBatch.DrawString(font, "Score: " + score, new Vector2(100, 100), Color.Black);


            //Vector2 location = new Vector2(400, 240);
            //Rectangle sourceRectangle = new Rectangle(0, 0, arrow.Width, arrow.Height);
            //Vector2 origin = new Vector2(0, 0);

            //spriteBatch.Draw(arrow, location, sourceRectangle, Color.White, angle, origin, 1.0f, SpriteEffects.None, 1);

            //Vector2 location2 = new Vector2(600, 400);
            //Rectangle sourceRectangle2 = new Rectangle(0, 0, arrow.Width, arrow.Height);
            //Vector2 origin2 = new Vector2(0, 0);

            //spriteBatch.Draw(arrow2, location2, sourceRectangle2, Color.White, angle, origin, 1.0f, SpriteEffects.None, 1);

            // Stop drawing

            spriteBatch.End();



            base.Draw(gameTime);
        }
    }
}
