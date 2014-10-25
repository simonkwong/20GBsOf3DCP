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
        Texture2D playerTexture;
        Texture2D playerTextureLeft;
        Texture2D playerTextureRight;
        Vector2 playerTextureOrigin;
        Vector2 playerPosition;
        float playerRotation;
        Vector2 playerVelocity;
        String playerName;
        Color playerColor;
        private Texture2D mytexture;
 

        List<I_WorldObject> worldObjects;
        private Vector2 currentPosition;

        public faces(String playerName, Vector2 iniPos, List<I_WorldObject> objectsList)
        {
            this.playerName = playerName;
            this.playerPosition = iniPos;
            this.playerTextureOrigin = Vector2.Zero;
            this.playerColor = Color.White;
            currentPosition = Vector2.Zero;
            this.worldObjects = objectsList;
            //mytexture = content.Load<Texture2D>("spriteArt/" + playerName);
        }

        public void LoadContent(ContentManager content)
        {
            playerTexture = content.Load<Texture2D>("spriteArt/" + playerName);
            //playerTextureLeft = content.Load<Texture2D>("spriteArt/" + playerName + "L");
            //playerTextureRight = content.Load<Texture2D>("spriteArt/" + playerName + "R");
        }

        public void Draw(SpriteBatch batch)
        {

            batch.Draw(playerTexture, new Rectangle(100, 100, 100, 100), Color.White);

            //if (backgroundOffset > backgroundWidth - screenWidth)
            //{
            //    batch.Draw(mytexture, new Rectangle((-1 * (int)backgroundOffset) + backgroundWidth, 0, backgroundWidth, screenHeight), Color.White);
            //}

            //if (drawParallax)
            //{

            //    batch.Draw(background, new Rectangle(-1 * (int)scrollOffset, 0, scrollWidth, screenHeight), Color.SlateGray);

            //    if (scrollOffset > scrollWidth - screenWidth)
            //    {
            //        batch.Draw(background, new Rectangle((-1 * (int)scrollOffset) + scrollWidth, 0, scrollWidth, screenHeight), Color.SlateGray);
            //    }
            //}
        }
         
        public void Update(GameTime gameTime)
        {
            // player controls go in here

            this.playerColor = Color.White;

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

        public String getName()
        {
            return this.playerName;
        }

        public bool shouldIcheckCollisions()
        {
            return false;
        }

        public Color getColor()
        {
            return this.playerColor;
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
