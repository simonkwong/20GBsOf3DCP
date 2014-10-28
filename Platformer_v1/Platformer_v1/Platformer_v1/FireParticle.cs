using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platformer_v1
{
    class FireParticle : I_WorldObject 
    {

        Texture2D fireTexture;
        double creationTime;
        World gameWorld;
        Vector2 position;
        bool leftRight;
        BoundingBox boundingBox;

        QuadTreeNode mNode;

        bool alive;

        double duration;

        public FireParticle(World mWorld, bool leftRight)
        {
            boundingBox = new BoundingBox();
            this.leftRight = leftRight;

            if (leftRight)
            {
                position = mWorld.p.getPosition() + new Vector2(20, 20);
            }
            else
            {
                position = mWorld.p.getPosition() - new Vector2(20, -20);
            }
          
            alive = true;
            gameWorld = mWorld;
            creationTime = -1;

            duration = 100;
        }

        public void LoadContent(ContentManager content)
        {
            fireTexture = content.Load<Texture2D>("spriteArt/fire");
        }

        public void Update(GameTime gameTime)
        {
            UpdateBoundingBox();

            if (creationTime == -1)
            {
                // first time
                creationTime = gameTime.TotalGameTime.TotalMilliseconds;
            }

            if ((gameTime.TotalGameTime.TotalMilliseconds - creationTime) >= duration)
            {
                this.setAlive(false);
            }


            if (leftRight)
            {
                position += new Vector2(10, 0);
            }
            else
            {
                position -= new Vector2(10, 0);
            }



   
        }



        public Texture2D getTexture()
        {
            return fireTexture;
        }

        public Vector2 getTextureOrigin()
        {
            return new Vector2(0, 0);
        }

        public Vector2 getPosition()
        {
            return position;
        }

        public void setPosition(Vector2 newPosition)
        {

        }

        public float getRotation()
        {
            return 0;
        }

        public Vector2 getVelocity()
        {
            return new Vector2();
        }

        public void setVelocity(Vector2 newVelocity)
        {

        }

        public Vector2 getDirection()
        {
            return new Vector2();
        }

        public void setDirection(Vector2 newDirection)
        {

        }

        public Vector2 getSpeed()
        {
            return new Vector2();
        }

        public void setSpeed(Vector2 newSpeed)
        {

        }

        public BoundingBox getBoundingBox()
        {
            return boundingBox;
        }

        // determines if the phsyics engine will update it
        public bool hasPhysics()
        {
            return false;
        }

        public void setPhysics(bool p)
        {

        }

        // if false will be deleted from the worldObject list!
        public bool isAlive()
        {
            return alive;
        }

        public void setAlive(bool a)
        {
            alive = a;
        }

        // tests if object is rigid aka can I stand on / walk through this
        public bool isRigid()
        {
            return false;
        }

        public void setRigid(bool r)
        {
            
        }

        public bool isCollidable()
        {
            return true;
        }

        public void setCollidability(bool c)
        {

        }

        // alert a world object that a collision has occured with the given object
        public void alertCollision(I_WorldObject collidedObject)
        {

        }


        // also mainly for debugging
        public Color getColor()
        {
            return Color.White;
        }

        // for debugging
        public String getName()
        {
            return "fire particle";
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
            return 0;
        }

        public int getFrameWidth()
        {
            return 0;
        }

        public Vector2 getAnimCenter()
        {
            return Vector2.Zero;
        }

        public float getScale()
        {
            return 0.0f;
        }


        protected void UpdateBoundingBox()
        {
            this.boundingBox.Min.X = this.getPosition().X ;
            this.boundingBox.Min.Y = this.getPosition().Y ;
            this.boundingBox.Max.X = this.getPosition().X + this.getTexture().Width ;
            this.boundingBox.Max.Y = this.getPosition().Y + this.getTexture().Height ;


        }
    }
}
