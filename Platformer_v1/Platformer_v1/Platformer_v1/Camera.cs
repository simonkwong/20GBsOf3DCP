using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platformer_v1
{
    class Camera
    {

        public Vector2 Position { get; set; }
        public float Rotation { get; set; }
        public float Zoom { get; set; }

        private SpriteBatch mSpriteBatch;

        public Camera(SpriteBatch sb)
        {
            mSpriteBatch = sb;
            Position = new Vector2(250, 250);
            Rotation = 0.0f;
            Zoom = 1.0f;
        }

        public void Draw(WorldObject obj)
        {
            Vector2 objPosInCameraSpace = obj.Position;
            Vector2 objPosInScreenSpace = objPosInCameraSpace;

            mSpriteBatch.Draw(obj.Texture, objPosInScreenSpace, null, obj.color, obj.Rotation, obj.TextureOrigin, 1.0f, SpriteEffects.None, 0);

        }


    }
}
