using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platformer_v1
{
    interface I_WorldObject
    {
        void LoadContent(ContentManager content);

        void Update(GameTime gameTime);

        Texture2D getTexture();

        Vector2 getTextureOrigin();

        Vector2 getPosition();

        float getRotation();

        Vector2 getVelocity();

    }

}
