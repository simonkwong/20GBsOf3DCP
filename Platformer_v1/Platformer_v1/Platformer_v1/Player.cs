using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platformer_v1
{
    class Player : WorldObject
    {

        // use this to override
        private Player(Texture2D texture, Vector2 textureOrigin, Vector2 position, float rotation, Vector2 velocity)
            : base(texture, textureOrigin, position, rotation, velocity)
        {

        }


    }
}
