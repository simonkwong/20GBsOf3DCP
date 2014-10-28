using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Platformer_v1
{
    class Coin : I_WorldObject
    {


        public bool Looping { get; set; }
        public float SecondsPerFrame { get; set; }
        public float Scale { get; set; }

        public static int cointCount = 0;

        Texture2D coinTexture;
        Vector2 coinTextureOrigin;
        Vector2 coinPosition;
        float coinRotation;
        Vector2 coinVelocity;
        BoundingBox coinBoundingBox;
        Color coinColor;
        bool physics;
        Vector2 coinDirection;
        Vector2 coinSpeed;
        String coinName;
        bool rigidness;
        bool aliveness;
        bool collidable;
        QuadTreeNode mNode;

        float mTimeSinceLastFrame;

        int mCurrentFrame;
        Vector2 mCenter;
        int mNumFrames;
        int mWidth;

        SoundEffect coinSound;
        SoundEffectInstance coinSound_instance;
        TextBox scoreDisplay;
        
        public void LoadContent(ContentManager content)
        {
            coinTexture = content.Load<Texture2D>("spriteArt/coin");
            coinSound = content.Load<SoundEffect>("sounds/coin_collect");

            coinSound_instance = coinSound.CreateInstance();
            coinSound_instance.Volume = 0.1f;

            mNumFrames = coinTexture.Width / mWidth;

        }


        public Coin(String name, int width, Vector2 center, bool looping, float secondsPerFrame, Vector2 position, TextBox scoreDisplay)
        {
            this.coinTextureOrigin = Vector2.Zero;
            this.coinVelocity = Vector2.Zero;
            this.coinColor = Color.White;
            this.physics = false;
            this.coinDirection = Vector2.Zero;
            this.coinSpeed = Vector2.Zero;
            rigidness = true;
            aliveness = true;
            collidable = true;

            mWidth = width;
            SecondsPerFrame = secondsPerFrame;
            coinPosition = position;
            mCenter = new Vector2(mWidth / 2, 20 / 2);
            mCurrentFrame = 0;
            coinRotation = 0;
            mTimeSinceLastFrame = 0;
            Scale = 1;
            Looping = looping;
            coinName = name;

            this.scoreDisplay = scoreDisplay;
        }

        public void Update(GameTime gameTime)
        {
            UpdateBoundingBox();

            mTimeSinceLastFrame += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (mTimeSinceLastFrame > SecondsPerFrame)
            {
                if (mCurrentFrame == mNumFrames - 1)
                {
                    if (Looping)
                    {
                        mCurrentFrame = 0;
                    }
                }
                else
                {
                    mCurrentFrame++;
                }
                mTimeSinceLastFrame = 0;
            }
        }

        public Texture2D getTexture()
        {
            return coinTexture;
        }


        public Vector2 getTextureOrigin()
        {
            return coinTextureOrigin;
        }

        public Vector2 getPosition()
        {
            return coinPosition;
        }

        public void setPosition(Vector2 newPosition)
        {
            this.coinPosition += newPosition;
        }

        public float getRotation()
        {
            return coinRotation;
        }

        public Vector2 getVelocity()
        {
            return coinVelocity;
        }

        public void setVelocity(Vector2 newVelocity)
        {
            coinVelocity = newVelocity;
        }

        public Vector2 getDirection()
        {
            return coinDirection;
        }

        public void setDirection(Vector2 newDirection)
        {
            coinDirection = newDirection;
        }

        public Vector2 getSpeed()
        {
            return coinSpeed;
        }

        public void setSpeed(Vector2 newSpeed)
        {
            coinSpeed = newSpeed;
        }

        public BoundingBox getBoundingBox()
        {
            return coinBoundingBox;
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
            if (collidedObject.getName() != "fire particle")
            {
                coinSound_instance.Play();

                scoreDisplay.text = "coins: " + ++cointCount;
                this.setAlive(false);
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
            return coinName;
;
        }

        public Color getColor()
        {
            return coinColor;
        }

        public bool isRigid()
        {
            return rigidness;
        }

        public void setRigid(bool r)
        {
            rigidness = r;
        }

        public bool isCollidable()
        {
            return collidable;
        }

        public void setCollidability(bool c)
        {
            collidable = c;
        }

        public QuadTreeNode getNode()
        {
            return mNode;
        }

        public void setNode(QuadTreeNode n)
        {
            mNode = n;
        }

        public float getFrame()
        {
            return mCurrentFrame;
        }

        public int getFrameWidth()
        {
            return mWidth;
        }

        public Vector2 getAnimCenter()
        {
            return mCenter;
        }

        public float getScale()
        {
            return Scale;
        }

        protected void UpdateBoundingBox()
        {
            this.coinBoundingBox.Min.X = this.getPosition().X;
            this.coinBoundingBox.Min.Y = this.getPosition().Y;
            this.coinBoundingBox.Max.X = this.getPosition().X + 1;
            this.coinBoundingBox.Max.Y = this.getPosition().Y + 1;
        }
    }
}
