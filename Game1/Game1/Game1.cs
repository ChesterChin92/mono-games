using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Shooter;
using System;
using System.Collections.Generic;

namespace Game1
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        
        //[Fonts]
        private SpriteFont font;
        private int score = 0;
        private int score2 = 0;

        //Key Lock Control
        int lock_up = 0;
        int lock_down = 0;
        int lock_left = 0;
        int lock_right = 0;

        int lock_w = 0;
        int lock_a = 0;
        int lock_s = 0;
        int lock_d = 0;

        //Rotating Mine
        //private Texture2D arrow, arrow2;
        //private float angle = 0;
                        
            
        //Player
        Player player,player2;
        

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

        // Image used to display the static background
        Texture2D mainBackground;
        Rectangle rectBackground;
        float scale = 1f;
        ParallaxingBackground bgLayer1;
        ParallaxingBackground bgLayer2;

        // Enemies
        Texture2D enemyTexture,enemyTexture2;
        List<Enemy> enemies,enemies2;

        // The rate at which the enemies appear
        TimeSpan enemySpawnTime,enemySpawnTime2;
        TimeSpan previousSpawnTime,previousSpawnTime2;

        // Random number generator class
        Random random,random2;

        // Collections of explosions
        List<Explosion> explosions;

        //Texture to hold explosion animation.
        Texture2D explosionTexture;

        //[DEFAULT VALUE- NO EDIT NEEDED]
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
            player2 = new Player();
            // Set a constant player move speed

            playerMoveSpeed = 8.0f;

            //Background
            bgLayer1 = new ParallaxingBackground();
            bgLayer2 = new ParallaxingBackground();
            mainBackground = Content.Load<Texture2D>("mainbackground");

            //Enable the FreeDrag gesture.
            TouchPanel.EnabledGestures = GestureType.FreeDrag;

            //Setup bg
            rectBackground = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);

            // Initialize the enemies list
            enemies = new List<Enemy>();
            enemies2 = new List<Enemy>();

            // Set the time keepers to zero
            previousSpawnTime = TimeSpan.Zero;
            previousSpawnTime2 = TimeSpan.Zero;
            // Used to determine how fast enemy respawns
            enemySpawnTime = TimeSpan.FromSeconds(0.7f);
            enemySpawnTime2 = TimeSpan.FromSeconds(1.0f);

            // Initialize random number generator
            random = new Random();
            random2 = new Random();
             
            // Initialize collection of explosions.
            explosions = new List<Explosion>();


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
            font = Content.Load<SpriteFont>("gameFont");


            // [SHIP 2]Load the Ship Animation resources 
            Animation playerAnimation2 = new Animation();
            Texture2D playerTexture2 = Content.Load<Texture2D>("shipAnimationflip-green");
            playerAnimation2.Initialize(playerTexture2, Vector2.Zero, 115, 69, 8, 30, Color.White, 1f, true);
            Vector2 playerPosition2 = new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X, GraphicsDevice.Viewport.TitleSafeArea.Y + GraphicsDevice.Viewport.TitleSafeArea.Height);
            player2.Initialize(playerAnimation2, playerPosition2);


            // [SHIP 1]Load the Ship Animation resources 
            Animation playerAnimation = new Animation();
            Texture2D playerTexture = Content.Load<Texture2D>("shipAnimation");
            playerAnimation.Initialize(playerTexture, Vector2.Zero, 115, 69, 8, 30, Color.White, 1f, true);
            Vector2 playerPosition = new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X *2 , GraphicsDevice.Viewport.TitleSafeArea.Y + GraphicsDevice.Viewport.TitleSafeArea.Height / 2);
            player.Initialize(playerAnimation, playerPosition);

            
            // Load the background
            bgLayer1.Initialize(Content, "bgLayer1", GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height, -1);
            bgLayer2.Initialize(Content, "bgLayer2", GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height, -2);

            // Load Enemy
            enemyTexture = Content.Load<Texture2D>("mineAnimation");
            enemyTexture2 = Content.Load<Texture2D>("mineAnimation-flip");

            // load the explosion sheet
            explosionTexture = Content.Load<Texture2D>("explosion");


            //[LIMIT TIME FRAME]
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

            // Update the parallaxing background
            bgLayer1.Update(gameTime);
            bgLayer2.Update(gameTime);

            // Update the enemies
            UpdateEnemies(gameTime);
            UpdateEnemies2(gameTime);

            // Update the collision
            UpdateCollision();
            UpdateCollision2();
            
            //Update Explosion
            UpdateExplosions(gameTime);
            

            //Font Score Increment
            //score++;

            //Angle increment
            //angle += 0.01f;

            //[NOTE: EVERY CONPONENT UPDATE IS DONE IN UPDATE PLAYER]
            //Update the player
            
            UpdatePlayer2(gameTime);
            UpdatePlayer(gameTime);

            base.Update(gameTime);

        }

        private void AddEnemy()
        {
            // Create the animation object
            Animation enemyAnimation = new Animation();
            //Animation enemyAnimation2 = new Animation();
            // Initialize the animation with the correct animation information
            enemyAnimation.Initialize(enemyTexture, Vector2.Zero, 47, 61, 8, 30, Color.White, 1f, true);
            //enemyAnimation2.Initialize(enemyTexture2, Vector2.Zero, 47, 61, 8, 30, Color.White, 1f, true);
            // Randomly generate the position of the enemy
            Vector2 position = new Vector2(GraphicsDevice.Viewport.Width + enemyTexture.Width / 2,
            random.Next(100, GraphicsDevice.Viewport.Height - 100));

            // Create an enemy
            Enemy enemy = new Enemy();
            //Enemy enemy2 = new Enemy();
            // Initialize the enemy
            enemy.Initialize(enemyAnimation, position);
            //enemy2.Initialize(enemyAnimation2, position);
            // Add the enemy to the active enemies list
            enemies.Add(enemy);
            //enemies.Add(enemy2);
    
        }

        private void AddEnemy2()
        {
            // Create the animation object
            Animation enemyAnimation2 = new Animation();

            // Initialize the animation with the correct animation information
            enemyAnimation2.Initialize(enemyTexture2, Vector2.Zero, 47, 61, 8, 30, Color.White, 1f, true);

            // Randomly generate the position of the enemy
            Vector2 position2 = new Vector2(GraphicsDevice.Viewport.Width + enemyTexture2.Width / 2,random.Next(0, GraphicsDevice.Viewport.Height - 100));

            // Create an enemy
            Enemy enemy2 = new Enemy();

            // Initialize the enemy
            enemy2.Initialize(enemyAnimation2, position2);

            // Add the enemy to the active enemies list
            enemies2.Add(enemy2);
        }

        private void UpdateCollision()
        {
            // Use the Rectangle’s built-in intersect function to
            // determine if two objects are overlapping
            Rectangle rectangle1;
            Rectangle rectangle2;


            // Only create the rectangle once for the player
            rectangle1 = new Rectangle((int)player.Position.X,(int)player.Position.Y,player.Width,player.Height);


            // Do the collision between the player and the enemies
            for (int i = 0; i < enemies.Count; i++)
            {
                rectangle2 = new Rectangle((int)enemies[i].Position.X,
                (int)enemies[i].Position.Y,

                enemies[i].Width,
                enemies[i].Height);

                // Determine if the two objects collided with each
                // other
                if (rectangle1.Intersects(rectangle2))
                {
                    // Subtract the health from the player based on
                    // the enemy damage
                    player.Health -= enemies[i].Damage;



                    // Since the enemy collided with the player
                    // destroy it
                    enemies[i].Health = 0;
                    AddExplosion(enemies[i].Position);
                    score2++;

                    // If the player health is less than zero we died
                    if (player.Health <= 0)
                        player.Active = false;
                }
            }
        }

        private void UpdateCollision2()
        {
            // Use the Rectangle’s built-in intersect function to
            // determine if two objects are overlapping
            Rectangle rectangle1;
            Rectangle rectangle2;


            // Only create the rectangle once for the player
            rectangle1 = new Rectangle((int)player2.Position.X, (int)player2.Position.Y, player2.Width, player2.Height);


            // Do the collision between the player and the enemies
            for (int i = 0; i < enemies.Count; i++)
            {
                rectangle2 = new Rectangle((int)enemies[i].Position.X,
                (int)enemies[i].Position.Y,

                enemies[i].Width,
                enemies[i].Height);

                // Determine if the two objects collided with each
                // other
                if (rectangle1.Intersects(rectangle2))
                {
                    // Subtract the health from the player based on
                    // the enemy damage
                    player2.Health -= enemies[i].Damage;



                    // Since the enemy collided with the player
                    // destroy it
                    enemies[i].Health = 0;
                    AddExplosion(enemies[i].Position);
                    score++;

                    // If the player health is less than zero we died
                    if (player2.Health <= 0)
                        player2.Active = false;
                }
            }
        }

        private void UpdateExplosions(GameTime gameTime)
        {
            for (var e = 0; e < explosions.Count; e++)
            {
                explosions[e].Update(gameTime);

                if (!explosions[e].Active)
                    explosions.Remove(explosions[e]);
            }
        }
        
        private void UpdateEnemies(GameTime gameTime)


        {
          

            // Spawn a new enemy enemy every 1.5 seconds
            if (gameTime.TotalGameTime - previousSpawnTime > enemySpawnTime)
            {
                previousSpawnTime = gameTime.TotalGameTime;
                // Add an Enemy
                AddEnemy();
            }


            // Update the Enemies
            for (int i = enemies.Count - 1; i >= 0;i--)
            {
                enemies[i].Update(gameTime);
                if (enemies[i].Active == false)
                {
                    enemies.RemoveAt(i);
                }
            }
        }

        private void UpdateEnemies2(GameTime gameTime)


        {
            // Spawn a new enemy enemy every 1.5 seconds
            if (gameTime.TotalGameTime - previousSpawnTime > enemySpawnTime)
            {
                previousSpawnTime = gameTime.TotalGameTime;
                // Add an Enemy
                AddEnemy2();
            }


            // Update the Enemies
            for (int i = enemies2.Count - 1; i >= 0; i--)
            {
                enemies2[i].Update(gameTime);
                if (enemies2[i].Active == false)
                {
                    enemies2.RemoveAt(i);
                }
            }
        }

        //[----------------------THE GAMES CORE--------------------------------]
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
            if ((currentKeyboardState.IsKeyDown(Keys.Left) || currentGamePadState.DPad.Left == ButtonState.Pressed) && lock_left == 0 )

            {

                player.Position.X -= playerMoveSpeed;
                //player.Position.X = 100;
            }

            if ((currentKeyboardState.IsKeyDown(Keys.M) || currentGamePadState.DPad.Left == ButtonState.Pressed) && lock_left == 1)
            {
                player.Position.X -= playerMoveSpeed;
            }


            if (currentKeyboardState.IsKeyDown(Keys.Right) || currentGamePadState.DPad.Right == ButtonState.Pressed)

            {
                player.Position.X += playerMoveSpeed;
                //player.Position.X = 300;
            }
            if ((currentKeyboardState.IsKeyDown(Keys.Up) || currentGamePadState.DPad.Up == ButtonState.Pressed) && lock_up == 0)

            {
                player.Position.Y -= playerMoveSpeed;
          
            }

            if ((currentKeyboardState.IsKeyDown(Keys.Q) || currentGamePadState.DPad.Up == ButtonState.Pressed) && lock_up == 1)

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

        //[----------------------THE GAMES CORE--------------------------------]
        private void UpdatePlayer2(GameTime gameTime)
        {
            player2.Update(gameTime);


      

            // Get Thumbstick Controls
            player2.Position.X += currentGamePadState.ThumbSticks.Left.X * playerMoveSpeed;
            player2.Position.Y -= currentGamePadState.ThumbSticks.Left.Y * playerMoveSpeed;


            // Windows 8 Touch Gestures for MonoGame

            while (TouchPanel.IsGestureAvailable)

            {
                GestureSample gesture = TouchPanel.ReadGesture();
                if (gesture.GestureType == GestureType.FreeDrag)
                {
                    player2.Position += gesture.Delta;
                }

            }

            //Get Mouse State then Capture the Button type and Respond Button Press
            Vector2 mousePosition = new Vector2(currentMouseState.X, currentMouseState.Y);
            if (currentMouseState.LeftButton == ButtonState.Pressed)

            {
                Vector2 posDelta = mousePosition - player.Position;
                posDelta.Normalize();
                posDelta = posDelta * playerMoveSpeed;
                player2.Position = player2.Position + posDelta;
            }



            // Use the Keyboard / Dpad
            if (currentKeyboardState.IsKeyDown(Keys.A) || currentGamePadState.DPad.Left == ButtonState.Pressed)

            {

                player2.Position.X -= playerMoveSpeed;
                //player.Position.X = 100;
            }



            if ((currentKeyboardState.IsKeyDown(Keys.D) || currentGamePadState.DPad.Right == ButtonState.Pressed) && lock_d == 0)

            {
                player2.Position.X += playerMoveSpeed;
                //player.Position.X = 300;
            }

            if ((currentKeyboardState.IsKeyDown(Keys.P) || currentGamePadState.DPad.Right == ButtonState.Pressed) && lock_d == 1)

            {
                player2.Position.X += playerMoveSpeed;
                //player.Position.X = 300;
            }


            if (currentKeyboardState.IsKeyDown(Keys.W) || currentGamePadState.DPad.Up == ButtonState.Pressed)

            {
                player2.Position.Y -= playerMoveSpeed;

            }

            if (currentKeyboardState.IsKeyDown(Keys.S) || currentGamePadState.DPad.Down == ButtonState.Pressed)

            {
                player2.Position.Y += playerMoveSpeed;
            }

            // Make sure that the player does not go out of bounds
            player2.Position.X = MathHelper.Clamp(player2.Position.X, 0, GraphicsDevice.Viewport.Width - player2.Width);
            player2.Position.Y = MathHelper.Clamp(player2.Position.Y, 0, GraphicsDevice.Viewport.Height - player2.Height);

        }

        protected void AddExplosion(Vector2 enemyPosition)
        {
            Animation explosionAnimation = new Animation();

            explosionAnimation.Initialize(explosionTexture,
                enemyPosition,
                134,
                134,
                12,
                30,
                Color.White,
                1.0f,
                true);

            Explosion explosion = new Explosion();
            explosion.Initialize(explosionAnimation, enemyPosition);
           
            explosions.Add(explosion);
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

            //Draw the Main Background Texture
            spriteBatch.Draw(mainBackground, rectBackground, Color.White);

            // Draw the moving background
            bgLayer1.Draw(spriteBatch);
            bgLayer2.Draw(spriteBatch);

            // Draw the Enemies
            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].Draw(spriteBatch);

            }


            // draw explosions
            foreach (var e in explosions)
            {
                e.Draw(spriteBatch);
            };



            // Draw the Player
            player2.Draw(spriteBatch);
            player.Draw(spriteBatch);
         

            //[TEST CODE-IGNORE]
            //player2.Draw(spriteBatch);
            //spriteBatch.DrawString(font, "Score", new Vector2(100, 100), Color.Black);
            spriteBatch.DrawString(font, "Orange Score: " + score, new Vector2(20, 20), Color.Orange);
            spriteBatch.DrawString(font, "Green Score: " + score2, new Vector2(300, 20), Color.Green);
            spriteBatch.DrawString(font, "Avoid Getting Hit!!", new Vector2(20, 550), Color.Gold);

            //[First Concept, Improved to more logical]
            //if ((score >= 5 && score <=15) || (score2 >= 5 && score2 <= 15))
            //{
            //    spriteBatch.DrawString(font, "Orange LEFT key changed to 'M'!", new Vector2(300, 550), Color.Red);
            //    lock_left = 1;
            //}

            if (score + score2 >= 10 && score + score2 <= 30)
            {
                spriteBatch.DrawString(font, "Orange LEFT key changed to 'M'!", new Vector2(300, 550), Color.Orange);
                lock_left = 1;
            }

            if (score + score2 >= 31 && score + score2 <= 60)
            {
                spriteBatch.DrawString(font, "", new Vector2(300, 550), Color.White);
                spriteBatch.DrawString(font, "Green RIGHT changed to 'P'!", new Vector2(300, 550), Color.Green);
                lock_left = 0;
                lock_d = 1;
            }


            if (score + score2 >= 61 && score + score2 <= 90)
            {
                spriteBatch.DrawString(font, "", new Vector2(300, 550), Color.White);
                spriteBatch.DrawString(font, "Orange UP changed to 'Q'!", new Vector2(300, 550), Color.Orange);
                lock_d = 0;
                lock_up = 1;
            }

            if (score + score2 >= 91 && score + score2 <= 120)
            {
                spriteBatch.DrawString(font, "", new Vector2(300, 550), Color.White);
                spriteBatch.DrawString(font, "Green UP changed to 'Q'!", new Vector2(300, 550), Color.Green);
                lock_d = 0;
                lock_up = 1;
            }

            if (score + score2 >= 121)
            {
                spriteBatch.DrawString(font, "", new Vector2(300, 550), Color.White);
                if (score > score2)
                {
                   
                    spriteBatch.DrawString(font, "Orange Win!!!", new Vector2(400, 400), Color.Black);
                   
                }

                if (score2 > score)
                {
                    
                    spriteBatch.DrawString(font, "Green Win!!!", new Vector2(400, 400), Color.Black);
                        
                    
                }
            }






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
