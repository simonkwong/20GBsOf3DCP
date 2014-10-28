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
    class faces : I_WorldObject
    {
        Texture2D faceTexture;
        Texture2D faceTextureLeft;
        Texture2D faceTextureRight;
        Vector2 faceTextureOrigin;
        Vector2 facePosition;
        float faceRotation;
        Vector2 faceVelocity;
        String faceName;
        Color faceColor;
        private Texture2D mytexture;
 

        List<I_WorldObject> worldObjects;
        private Vector2 currentPosition;

        public faces(String faceName, Vector2 iniPos, List<I_WorldObject> objectsList)
        {
            this.faceName = faceName;
            this.facePosition = iniPos;
            this.faceTextureOrigin = Vector2.Zero;
            this.faceColor = Color.White;
            currentPosition = Vector2.Zero;
            this.worldObjects = objectsList;
            //mytexture = content.Load<Texture2D>("spriteArt/" + faceName);
        }

        public void LoadContent(ContentManager content)
        {
            faceTexture = content.Load<Texture2D>("spriteArt/" + faceName);
            //playerTextureLeft = content.Load<Texture2D>("spriteArt/" + faceName + "L");
            //playerTextureRight = content.Load<Texture2D>("spriteArt/" + faceName + "R");
        }

         
        public void Update(GameTime gameTime)
        {
            // player controls go in here

            this.faceColor = Color.White;

        }

        public Texture2D getTexture()
        {
            return faceTexture;
        }

        public Vector2 getTextureOrigin()
        {
            return faceTextureOrigin;
        }

        public Vector2 getPosition()
        {
            return facePosition;
        }

        public void setPosition(Vector2 newPosition)
        {
            facePosition += newPosition;
        }

        public String getName()
        {
            return this.faceName;
        }

        public bool shouldIcheckCollisions()
        {
            return false;
        }

        public Color getColor()
        {
            return this.faceColor;
        }

        public void alertCollision(I_WorldObject collidedObject)
        {
            return;
        }

        public void setRigid(bool r)
        {
            return;
        }

        public bool isRigid()
        {
            return true;
        }

        public bool isAlive()
        {
            return true;
        }


        public void setAlive(bool a)
        {
            return;
        }

        public bool hasPhysics()
        {
            return true;
        }

        public void setPhysics(bool p)
        {
            return;
        }

        public Vector2 getSpeed()
        {
            Vector2 vect = new Vector2(0, 0);
            return vect;
        }

        public void setSpeed(Vector2 newSpeed)
        {
            return;
        }

        public BoundingBox getBoundingBox()
        {
            BoundingBox bb = new BoundingBox();
            return bb;
        }

        public float getRotation()
        {
            return 1;
        }

        public Vector2 getVelocity()
        {
            Vector2 vv = new Vector2(0, 0);
            return vv;
        }

        public void setVelocity(Vector2 newVelocity)
        {
            return;
        }

        public Vector2 getDirection()
        {
            Vector2 vv = new Vector2(0, 0);
            return vv;
        }

        public void setDirection(Vector2 newDirection)
        {
            return;
        }

      
    }
}
