using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platformer_v1
{
    class Player : I_WorldObject
    {
        Vector2 playerGravity;
        Texture2D playerTexture;
        Vector2 playerTextureOrigin;
        Vector2 playerPosition;
        float playerRotation;
        Vector2 playerVelocity;
        String playerName;
        Color playerColor;
        BoundingBox playerBoundingBox;
        bool physics;
        int PLAYER_SPEED;
        Vector2 GRAVITY;

        enum State
        {
            Walking,
            Jumping
        }

        State playerState;
        KeyboardState kbCurrentState;
        KeyboardState kbPreviousState;
        Vector2 currentPosition;
        Vector2 playerDirection;
        Vector2 playerSpeed;

        GamePadState gpCurrentState;
        GamePadState gpPreviousState;

        bool rigidness;
        bool aliveness;

        public Player(String playerName, Vector2 iniPos)
        {
            this.playerName = playerName;
            this.playerPosition = iniPos;
            this.playerTextureOrigin = Vector2.Zero;
            this.playerVelocity = Vector2.Zero;
            this.playerRotation = 0;
            this.playerColor = Color.White;
            this.playerGravity = new Vector2(0, 0.048f);
            this.playerSpeed = Vector2.Zero;
            this.playerDirection = WorldData.GetInstance().playerDirection;
            this.playerState = State.Walking;

            physics = true;
            rigidness = false;
            aliveness = true;
            currentPosition = Vector2.Zero;
            PLAYER_SPEED = WorldData.GetInstance().playerSpeed;
            GRAVITY = WorldData.GetInstance().Gravity;
        }
        
        public void LoadContent(ContentManager content)
        {
            playerTexture = content.Load<Texture2D>("spriteArt/" + playerName);
            UpdateBoundingBox();
        }

        public void Update(GameTime gameTime)
        {
            // player controls go in here

            this.playerColor = Color.White;

            kbCurrentState = Keyboard.GetState();
            gpCurrentState = GamePad.GetState(PlayerIndex.One);

            UpdateBoundingBox();

            if (playerVelocity == Vector2.Zero && playerSpeed == Vector2.Zero)
            {
                UpdateMovement(kbCurrentState, gpCurrentState);
                UpdateJump(kbCurrentState, gpCurrentState);
            }
            
            if (this.playerBoundingBox.Min.X < 0)
            {
                this.playerPosition = new Vector2(0, playerPosition.Y);
            }

            if (this.playerBoundingBox.Max.Y >= WorldData.GetInstance().ScreenHeight)
            {
                this.setVelocity(Vector2.Zero);
                this.setPosition(playerVelocity);
                this.setDirection(new Vector2(1, 0));
                this.setSpeed(Vector2.Zero);
                this.setPhysics(false);
                this.playerState = State.Walking;
            }
            else
            {
                if (hasPhysics())
                    // is falling
                    this.playerVelocity += this.playerGravity;

                else
                    setPhysics(true);
            }

            this.playerPosition += this.playerVelocity;

            kbPreviousState = kbCurrentState;
            gpPreviousState = gpCurrentState;
        }


        private void UpdateMovement(KeyboardState kbCurrentState, GamePadState gpCurrentState)
        {

            if (playerState == State.Walking)
            {

                playerSpeed = Vector2.Zero;
                playerDirection = Vector2.Zero;

                if (kbCurrentState.IsKeyDown(Keys.Left) || gpCurrentState.IsButtonDown(Buttons.DPadLeft))
                {
                    playerSpeed.X = PLAYER_SPEED;
                    playerDirection.X = -1;
                }
                if (kbCurrentState.IsKeyDown(Keys.Right) || gpCurrentState.IsButtonDown(Buttons.DPadRight))
                {
                    playerSpeed.X = PLAYER_SPEED;
                    playerDirection.X = 1;
                }
            }
        }

        private void UpdateJump(KeyboardState kbCurrentState, GamePadState gpCurrentState)
        {

            if (playerState == State.Walking)
            {
                if (kbCurrentState.IsKeyDown(Keys.Up) || gpCurrentState.IsButtonDown(Buttons.DPadUp))
                {
                    if (playerState != State.Jumping)
                    {
                        playerState = State.Jumping;
                        currentPosition = playerPosition;
                        playerDirection.Y = -1;
                        playerSpeed = new Vector2(PLAYER_SPEED / 2, PLAYER_SPEED);
                    }
                }
            }
            if (playerState == State.Jumping)
            {
                UpdateMovement(kbCurrentState, gpCurrentState);
                if (currentPosition.Y - playerPosition.Y > WorldData.GetInstance().MaxJumpHeight)
                {
                    dropPlayer();
                }
                if (playerPosition.Y > currentPosition.Y)
                {
                    playerPosition.Y = currentPosition.Y;
                    playerState = State.Walking;
                    playerDirection = Vector2.Zero;
                }
            }
        }
   
        public Texture2D getTexture()
        {
            return playerTexture;
        }

        public Vector2 getTextureOrigin()
        {
            return playerTextureOrigin;
        }

        public Vector2 getPosition()
        {
            return playerPosition;
        }

        public void setPosition(Vector2 newPosition)
        {
            playerPosition += newPosition;
        }

        public float getRotation()
        {
            return playerRotation;
        }

        public Vector2 getVelocity()
        {
            return playerVelocity;
        }

        public void setVelocity(Vector2 newVelocity)
        {
            playerVelocity = newVelocity;
        }

        public Vector2 getDirection()
        {
            return playerDirection;
        }

        public void setDirection(Vector2 newDirection)
        {
            playerDirection = newDirection;
        }

        public Vector2 getSpeed()
        {
            return playerSpeed;
        }

        public void setSpeed(Vector2 newSpeed)
        {
            playerSpeed = newSpeed;
        }

        public BoundingBox getBoundingBox()
        {
            return playerBoundingBox;
        }

        public bool hasPhysics()
        {
            return physics;
        }

        public void setPhysics(bool p)
        {
            physics = p;
        }

        public void alertCollision(I_WorldObject collidedObject)
        {

            if (!collidedObject.isRigid())
            {
                if (collidedObject.getName() == "Spikes")
                {
                    setAlive(false);
                    
                    this.setVelocity(Vector2.Zero);
                    this.setDirection(new Vector2(1, 0));
                    this.setSpeed(Vector2.Zero);
                    this.setPhysics(false);
                    this.playerState = State.Walking;

                    playerPosition = WorldData.GetInstance().playerInitialPosition;

                    setAlive(true);
                }
            }

            if (collidedObject.isRigid())
            {
                //this.playerColor = Color.Red;

                BoundingBox myAABB = this.playerBoundingBox;
                BoundingBox other = collidedObject.getBoundingBox();

                int leftBound = (int)Math.Max(myAABB.Min.X, other.Min.X);
                int rightBound = (int)Math.Min(myAABB.Max.X, other.Max.X);
                int upperBound = (int)Math.Max(myAABB.Min.Y, other.Min.Y);
                int lowerBound = (int)Math.Min(myAABB.Max.Y, other.Max.Y);

                int xMovement = rightBound - leftBound;
                int yMovement = lowerBound - upperBound;

                if (xMovement < yMovement)
                {
                    if (myAABB.Min.X < other.Min.X)
                    {
                        this.playerPosition.X -= xMovement;
                        dropPlayer();
                    }
                    else
                    {
                        this.playerPosition.X += xMovement;
                        dropPlayer();
                    }
                }
                if (yMovement <= xMovement)
                {
                    if (myAABB.Min.Y < other.Min.Y)
                    {
                        this.playerPosition.Y -= yMovement;
                        this.setVelocity(Vector2.Zero);
                        this.setPosition(playerVelocity);
                        this.setDirection(new Vector2(1, 0));
                        this.setSpeed(Vector2.Zero);
                        this.setPhysics(false);
                        this.playerState = State.Walking;
                    }
                    else
                    {
                        this.playerPosition.Y += yMovement;
                        dropPlayer();
                    }
                }
            }           
        }

        private void dropPlayer()
        {
            if (playerState == State.Jumping)
            {
                playerDirection.Y = 1;
                playerSpeed = WorldData.GetInstance().Gravity;
            }
        }

        public bool isAlive()
        {
            return aliveness;
        }

        public void setAlive(bool a)
        {
            aliveness = a;
        }

        public String getName()
        {
            return this.playerName;
        }

        public Color getColor()
        {
            return this.playerColor;
        }

        public bool isRigid()
        {
            return rigidness;
        }

        public void setRigid(bool r)
        {
            rigidness = r;
        }


        protected void UpdateBoundingBox()
        {
            this.playerBoundingBox.Min.X = this.getPosition().X;
            this.playerBoundingBox.Min.Y = this.getPosition().Y;
            this.playerBoundingBox.Max.X = this.getPosition().X + this.getTexture().Width;
            this.playerBoundingBox.Max.Y = this.getPosition().Y + this.getTexture().Height;

        }
    }
}
