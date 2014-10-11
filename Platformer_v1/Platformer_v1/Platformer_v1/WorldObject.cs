using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platformer_v1
{

    class WorldObject
    {
        public Texture2D texture { get; set; }
        public Vector2 textureOrigin { get; set; }
        public Vector2 position;
        public float rotation { get; set; }
        public Vector2 velocity;



        public WorldObject(Texture2D texture, Vector2 textureOrigin, Vector2 position, float rotation, Vector2 velocity)
        {
            this.texture = texture;
            this.textureOrigin = textureOrigin;
            this.position = position;
            this.rotation = rotation;
            this.velocity = velocity;
        }


    }

}
