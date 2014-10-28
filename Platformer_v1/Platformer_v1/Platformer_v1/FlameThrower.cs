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
    class FlameThrower : I_WorldObject
    {

        ContentManager content;
        Texture2D currentTexture;
        Texture2D textureLeft;
        Texture2D textureRight;

        QuadTreeNode mNode;

        World gameWorld;

        public FlameThrower(World mWorld)
        {
            gameWorld = mWorld;
        }

        public void LoadContent(ContentManager content)
        {
            this.content = content;
            textureLeft = content.Load<Texture2D>("spriteArt/flameThrowerL");
            textureRight = content.Load<Texture2D>("spriteArt/flameThrowerR");
            currentTexture = textureRight;
        }

        public void Update(GameTime gameTime)
        {
            if (gameWorld.p.leftRight)
            {
                currentTexture = textureRight;
            }
            else
            {
                currentTexture = textureLeft;
            }

            KeyboardState kbCurrentState = Keyboard.GetState();
            GamePadState gpCurrentState = GamePad.GetState(PlayerIndex.One);


            if (kbCurrentState.IsKeyDown(Keys.Space) || gpCurrentState.IsButtonDown(Buttons.RightTrigger))
            {
                    FireParticle p = new FireParticle(gameWorld, gameWorld.p.leftRight);
                    p.LoadContent(content);

                    gameWorld.objectsToAdd.Add(p);






            }




        }


        public Texture2D getTexture()
        {
            return currentTexture;
        }

        public Vector2 getTextureOrigin()
        {
            return new Vector2(0,0);
        }

        public Vector2 getPosition()
        {

            if (gameWorld.p.leftRight)
            {
                return gameWorld.p.getPosition() + new Vector2(0, 20);
            }
            else
            {
                return gameWorld.p.getPosition() + new Vector2(-10, 20);
            }

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
            return new BoundingBox();
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
            return true;
        }

        public void setAlive(bool a)
        {

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
            return false;
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
            return "flamethrower";
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
            return 0.0f;
        }

        public int getFrameWidth()
        {
            return 0;
        }


        public Vector2 getAnimCenter()
        {
            return new Vector2();
        }

        public float getScale()
        {
            return 0.0f;
        }


    }
}
