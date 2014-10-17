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
    class Enemy : I_WorldObject
    {

        Texture2D enemyTexture;
        Vector2 enemyTextureOrigin;
        Vector2 enemyPosition;
        float enemyRotation;
        Vector2 enemyVelocity;
        BoundingBox enemyBoundingBox;
        Color enemyColor;
        bool physics;
        Vector2 enemyDirection;
        Vector2 enemySpeed;
        String enemyName;
        bool rigidness;
        bool aliveness;
        static Random rndGen = new Random();
        float movementUpdate = 0.5f;
        float currentElapsedTime = 0f;

        public Enemy(String enemyName, Vector2 iniPos)
        {

            this.enemyName = enemyName;
            this.enemyPosition = iniPos;
            this.enemyTextureOrigin = Vector2.Zero;
            this.enemyVelocity = Vector2.Zero;
            this.enemyRotation = 0;
            this.enemyColor = Color.White;
            this.physics = false;
            this.enemyDirection = Vector2.Zero;
            this.enemySpeed = Vector2.Zero;
            rigidness = true;
            aliveness = true;
        }

        public void LoadContent(ContentManager content)
        {
            enemyTexture = content.Load<Texture2D>("spriteArt/" + enemyName);

            UpdateBoundingBox();
        }

        public void Update(GameTime gameTime)
        {
            currentElapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

            this.enemyColor = Color.White;

            adjustPosition();

            if (currentElapsedTime >= movementUpdate)
            {
                RandomizeMovement();
                currentElapsedTime = 0f;
            }

            UpdateBoundingBox();
        }

        private void RandomizeMovement()
        {
            int emm = WorldData.GetInstance().enemyMaxMovement;

            float iX = rndGen.Next(-emm, emm);
            float iY = rndGen.Next(-emm, emm);

            int direction = rndGen.Next(1, 8);
            int vmagnitude = rndGen.Next(1, 3);
            int hmagnitude = rndGen.Next(1, 3);

            if (direction == 1)
                setDirection(new Vector2(1 * hmagnitude, 0));
            if (direction == 2)
                setDirection(new Vector2(-1 * hmagnitude, 0));
            if (direction == 3)
                setDirection(new Vector2(0, 1 * vmagnitude));
            if (direction == 4)
                setDirection(new Vector2(0, -1 * vmagnitude));
            if (direction == 5)
                setDirection(new Vector2(1 * hmagnitude, 1 * vmagnitude));
            if (direction == 6)
                setDirection(new Vector2(-1 * hmagnitude, 1 * vmagnitude));
            if (direction == 7)
                setDirection(new Vector2(1 * hmagnitude, -1 * vmagnitude));
            if (direction == 8)
                setDirection(new Vector2(-1 * hmagnitude, -1 * vmagnitude));




            enemySpeed = new Vector2(iX * WorldData.GetInstance().enemySpeed, iY * WorldData.GetInstance().enemySpeed);
        }

        private void adjustPosition()
        {
            if (this.enemyBoundingBox.Min.X < 0)
            {
                this.enemyPosition = new Vector2(0, enemyPosition.Y);
                this.enemyDirection *= -1;
            }

            if (this.enemyBoundingBox.Min.Y < 0)
            {
                this.enemyPosition = new Vector2(enemyPosition.X, 0);
                this.enemyDirection *= -1;
            }


            if (this.enemyBoundingBox.Max.Y > WorldData.GetInstance().ScreenHeight)
            {
                this.enemyPosition = new Vector2(enemyPosition.X, WorldData.GetInstance().ScreenHeight - enemyTexture.Height);
                this.enemyDirection *= -1;
            }



            UpdateBoundingBox();
        }

        public Texture2D getTexture()
        {
            return enemyTexture;
        }


        public Vector2 getTextureOrigin()
        {
            return enemyTextureOrigin;
        }

        public Vector2 getPosition()
        {
            return enemyPosition;
        }

        public void setPosition(Vector2 newPosition)
        {
            enemyPosition += newPosition;
        }

        public float getRotation()
        {
            return enemyRotation;
        }

        public Vector2 getVelocity()
        {
            return enemyVelocity;
        }

        public void setVelocity(Vector2 newVelocity)
        {
            enemyVelocity = newVelocity;
        }

        public Vector2 getDirection()
        {
            return enemyDirection;
        }

        public void setDirection(Vector2 newDirection)
        {
            enemyDirection = newDirection;
        }

        public Vector2 getSpeed()
        {
            return enemySpeed;
        }

        public void setSpeed(Vector2 newSpeed)
        {
            enemySpeed = newSpeed;
        }

        public BoundingBox getBoundingBox()
        {
            return enemyBoundingBox;
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

            if (collidedObject.isRigid())
            {
                //this.playerColor = Color.Red;

                BoundingBox myAABB = this.enemyBoundingBox;
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
                        this.enemyPosition.X -= xMovement;
                        this.enemyDirection *= -1;
                    }
                    else
                    {
                        this.enemyPosition.X += xMovement;
                        this.enemyDirection *= -1;
                    }
                }
                if (yMovement <= xMovement)
                {
                    if (myAABB.Min.Y < other.Min.Y)
                    {
                        this.enemyPosition.Y -= yMovement;
                        this.enemyDirection *= -1;
                    }
                    else
                    {
                        this.enemyPosition.Y += yMovement;
                        this.enemyDirection *= -1;
                    }
                }

            }
            //this.blockColor = Color.Red;
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
            return enemyName;
        }

        public Color getColor()
        {
            return this.enemyColor;
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
            this.enemyBoundingBox.Min.X = this.getPosition().X + 15;
            this.enemyBoundingBox.Min.Y = this.getPosition().Y + 15;
            this.enemyBoundingBox.Max.X = this.getPosition().X + this.getTexture().Width - 15;
            this.enemyBoundingBox.Max.Y = this.getPosition().Y + this.getTexture().Height - 15;


        }
    }
}
