using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platformer_v1
{
    class messagebox : I_WorldObject
    {

        Texture2D blockTexture;
        Vector2 blockTextureOrigin;
        Vector2 blockPosition;
        float blockRotation;
        Vector2 blockVelocity;
        BoundingBox blockBoundingBox;
        Color blockColor;
        bool physics;
        Vector2 blockDirection;
        Vector2 blockSpeed;
        String platformName;
        bool rigidness;
        bool aliveness;
        bool collidable;

        float frame;
        int frameWidth;
        Vector2 animCenter;
        float scale;

        QuadTreeNode mNode;

        public messagebox(String platformName, Vector2 iniPos)
        {

            this.platformName = platformName;
            this.blockPosition = iniPos;
            this.blockTextureOrigin = Vector2.Zero;
            this.blockVelocity = Vector2.Zero;
            this.blockRotation = 0;
            this.blockColor = Color.White;
            this.physics = false;
            this.blockDirection = Vector2.Zero;
            this.blockSpeed = Vector2.Zero;
            rigidness = true;
            aliveness = true;
            collidable = false;

            frame = 0;
            frameWidth = 0;
            animCenter = Vector2.Zero;
            scale = 0;
        }

        public void LoadContent(ContentManager content)
        {
            blockTexture = content.Load<Texture2D>("spriteArt/" + platformName);

            UpdateBoundingBox();
        }

        public bool shouldIcheckCollisions()
        {
            return false;
        }

        public void Update(GameTime gameTime)
        {
            this.blockColor = Color.White;
            UpdateBoundingBox();
        }

        public Texture2D getTexture()
        {
            return blockTexture;
        }


        public Vector2 getTextureOrigin()
        {
            return blockTextureOrigin;
        }

        public Vector2 getPosition()
        {
            return blockPosition;
        }

        public void setPosition(Vector2 newPosition)
        {
            blockPosition += newPosition;
        }

        public float getRotation()
        {
            return blockRotation;
        }

        public Vector2 getVelocity()
        {
            return blockVelocity;
        }

        public void setVelocity(Vector2 newVelocity)
        {
            blockVelocity = newVelocity;
        }

        public Vector2 getDirection()
        {
            return blockDirection;
        }

        public void setDirection(Vector2 newDirection)
        {
            blockDirection = newDirection;
        }

        public Vector2 getSpeed()
        {
            return blockSpeed;
        }

        public void setSpeed(Vector2 newSpeed)
        {
            blockSpeed = newSpeed;
        }

        public BoundingBox getBoundingBox()
        {
            return blockBoundingBox;
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

            if (collidedObject.getName() == "usf" || collidedObject.getName() == "Spikes")
            {
                //update boundingBox size
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
            return platformName;
        }

        public Color getColor()
        {
            return this.blockColor;
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
            return frame;
        }

        public int getFrameWidth()
        {
            return frameWidth;
        }

        public Vector2 getAnimCenter()
        {
            return animCenter;
        }

        public float getScale()
        {
            return scale;
        }


        protected void UpdateBoundingBox()
        {
            this.blockBoundingBox.Min.X = this.getPosition().X;
            this.blockBoundingBox.Min.Y = this.getPosition().Y;
            this.blockBoundingBox.Max.X = this.getPosition().X + this.getTexture().Width;
            this.blockBoundingBox.Max.Y = this.getPosition().Y + this.getTexture().Height;
        }
    }
}
