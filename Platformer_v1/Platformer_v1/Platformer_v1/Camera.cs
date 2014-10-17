using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Platformer_v1
{
    class Camera
    {

        public Vector2 Position { get; set; }
        public float Rotation { get; set; }
        public float Zoom { get; set; }
        public I_WorldObject trackMe;
        Vector2 objPosInScreenSpace;
        public bool tracking;

        private SpriteBatch mSpriteBatch;

        public Camera(SpriteBatch sb, I_WorldObject trackMe)
        {
            mSpriteBatch = sb;
            Position = trackMe.getPosition();
            Rotation = 0.0f;
            Zoom = 1.0f;
            this.trackMe = trackMe;
            tracking = false;
        }

        public void Update(GameTime gameTime)
        {
            this.Position = trackMe.getPosition();            
        }

        public void Draw(I_WorldObject obj)
        {
            if (trackMe.getPosition().X >= WorldData.GetInstance().ScreenWidth / 2)
            {

                //Vector2 objPosInCameraSpace = obj.getPosition() - new Vector2((trackMe.getPosition()).X, 0);
                Vector2 objPosInCameraSpace = obj.getPosition() - new Vector2((trackMe.getPosition()).X - (WorldData.GetInstance().ScreenWidth / 2), 0);
                objPosInScreenSpace = objPosInCameraSpace;

                tracking = true;

            }
            else
            {
                tracking = false;
                Vector2 objPosInCameraSpace = obj.getPosition();
                objPosInScreenSpace = objPosInCameraSpace;
            }

            mSpriteBatch.Draw(obj.getTexture(), objPosInScreenSpace, null, obj.getColor(), obj.getRotation(), obj.getTextureOrigin(), 1.0f, SpriteEffects.None, 0);

        }


    }
}
