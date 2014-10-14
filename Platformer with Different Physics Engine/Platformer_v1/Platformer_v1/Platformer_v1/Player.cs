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

        enum State
        {
            Walking,
            Jumping
        }

        State playerState;
        KeyboardState kbPreviousState;
        Vector2 currentPosition;
        Vector2 playerDirection;
        Vector2 playerSpeed;
        
        public Player(String playerName, Vector2 iniPos)
        {
            this.playerName = playerName;
            this.playerPosition = iniPos;
            this.playerTextureOrigin = Vector2.Zero;
            this.playerVelocity = Vector2.Zero;
            this.playerRotation = 0;
            playerColor = Color.White;
            playerGravity = new Vector2(0, 0.098f);
            physics = true;
            playerState = State.Walking;
            currentPosition = Vector2.Zero;
            playerDirection = WorldData.GetInstance().playerDirection;
            playerSpeed = Vector2.Zero;
            PLAYER_SPEED = WorldData.GetInstance().playerSpeed;
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

            KeyboardState kbCurrentState = Keyboard.GetState();
            
            UpdateBoundingBox();

            UpdateMovement(kbCurrentState);
/*
            if (playerBoundingBox.Max.Y >= WorldData.GetInstance().ScreenHeight)
            {
                playerSpeed = Vector2.Zero;
            }
            else
            {
                // is falling
                if (hasPhysics())
                {
                    
                    playerDirection.Y = 1;
                    playerSpeed.Y += WorldData.GetInstance().Gravity.Y + PLAYER_SPEED;
                }

            }

            setPhysics(true);
*/
            UpdateJump(kbCurrentState);

            kbPreviousState = kbCurrentState;
        }


        private void UpdateMovement(KeyboardState kbCurrentState)
        {

            if (playerState == State.Walking)
            {

                playerSpeed = Vector2.Zero;
                playerDirection = Vector2.Zero;

                if (kbCurrentState.IsKeyDown(Keys.Left))
                {
                    playerSpeed.X = PLAYER_SPEED;
                    playerDirection.X = -1;
                }
                if (kbCurrentState.IsKeyDown(Keys.Right))
                {
                    playerSpeed.X = PLAYER_SPEED;
                    playerDirection.X = 1;
                }
            }
        }

        private void UpdateJump(KeyboardState kbCurrentState)
        {

            if (playerState == State.Walking)
            {
                if (kbCurrentState.IsKeyDown(Keys.Up) == true)
                {
                    if (playerState != State.Jumping)
                    {
                        playerState = State.Jumping;
                        currentPosition = playerPosition;
                        playerDirection.Y = -1;
                        playerSpeed = new Vector2(PLAYER_SPEED, PLAYER_SPEED);
                    }
                }
            }
            if (playerState == State.Jumping)
            {
                UpdateMovement(kbCurrentState);
                if (currentPosition.Y - playerPosition.Y > WorldData.GetInstance().MaxJumpHeight)
                {
                    playerDirection.Y = 1;
                    playerSpeed = new Vector2(PLAYER_SPEED, PLAYER_SPEED) + WorldData.GetInstance().Gravity;
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
            this.playerColor = Color.Red;
        }

        public bool isAlive()
        {
            return true;
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
            return false;
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
