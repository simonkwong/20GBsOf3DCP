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

        public Player(String playerName, Vector2 iniPos)
        {
            this.playerName = playerName;
            this.playerPosition = iniPos;
            this.playerTextureOrigin = Vector2.Zero;
            this.playerVelocity = Vector2.Zero;
            this.playerRotation = 0;
            playerColor = Color.White;
            playerGravity = new Vector2(0, 0.09f);

        }


        public void LoadContent(ContentManager content)
        {
            playerTexture = content.Load<Texture2D>("spriteArt/Pacheco");
            UpdateBoundingBox();
        }

        public void Update(GameTime gameTime)
        {
            // player controls go in here
            
            // and eventually animation stuff



            if (this.playerPosition.Y > 500)
            {
                this.playerVelocity = Vector2.Zero;
            }
            else
            {
                // if falling
                this.playerVelocity += this.playerGravity;
            }

            this.playerPosition += this.playerVelocity;


            this.playerColor = Color.White;

            UpdateBoundingBox();

            getControlInput(gameTime);
        }


        private void getControlInput(GameTime gameTime)
        {

            KeyboardState keyboard = Keyboard.GetState();

            if (keyboard.IsKeyDown(Keys.Up))
            {
                this.playerPosition -= new Vector2(0, 4);
            }

            if (keyboard.IsKeyDown(Keys.Down))
            {
                this.playerPosition += new Vector2(0, 2);
            }


            if (keyboard.IsKeyDown(Keys.Left))
            {
                this.playerPosition += new Vector2(-2, 0);
            }

            if (keyboard.IsKeyDown(Keys.Right))
            {
                this.playerPosition += new Vector2(2, 0);
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

        public float getRotation()
        {
            return playerRotation;
        }

        public Vector2 getVelocity()
        {
            return playerVelocity;
        }

        public BoundingBox getBoundingBox()
        {
            return playerBoundingBox;
        }


        public bool hasPhysics()
        {
            return true;
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
