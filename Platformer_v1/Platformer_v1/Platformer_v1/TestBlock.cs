using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platformer_v1
{
    class TestBlock : I_WorldObject
    {

        Texture2D blockTexture;
        Vector2 blockTextureOrigin;
        Vector2 blockPosition;
        float blockRotation;
        Vector2 blockVelocity;
        BoundingBox blockBoundingBox;
        Color blockColor;


        public TestBlock(Vector2 iniPos)
        {
            this.blockPosition = iniPos;
            this.blockTextureOrigin = Vector2.Zero;
            this.blockVelocity = Vector2.Zero;
            this.blockRotation = 0;
            this.blockColor = Color.White;
        }



        public void LoadContent(ContentManager content)
        {
            blockTexture = content.Load<Texture2D>("spriteArt/yellowblock");

            UpdateBoundingBox();
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


        public float getRotation()
        {
            return blockRotation;
        }

        public Vector2 getVelocity()
        {
            return blockVelocity;
        }

        public BoundingBox getBoundingBox()
        {
            return blockBoundingBox;
        }


        public bool hasPhysics()
        {
            return false;
        }


        public void alertCollision(I_WorldObject collidedObject)
        {
            // do not allow collidedObject to enter it
            if (this.isRigid())
            { 
                
                // 4 simple cases of collision

                if (this.getBoundingBox().Max.X > collidedObject.getBoundingBox().Min.X)
                {

                }
                

            }


            this.blockColor = Color.Red;
        }

        public bool isAlive()
        {
            return true;
        }

        public String getName()
        {
            return "yellowBox";
        }

        public Color getColor()
        {
            return this.blockColor;
        }

        public bool isRigid()
        {
            return true;
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
