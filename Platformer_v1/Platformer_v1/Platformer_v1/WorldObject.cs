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
        static Dictionary<Texture2D, Color[]> textureDataDictionary = null;
        private static bool ROTATE = false;

        public Texture2D Texture { get; set; }
        public Vector2 TextureOrigin { get; set; }
        public Vector2 Position;
        public float Rotation { get; set; }
        public Color color;
        public Vector2 velocity;
        // Pointer to the data for the texture for this object
        public Color[] data;
    }
}
