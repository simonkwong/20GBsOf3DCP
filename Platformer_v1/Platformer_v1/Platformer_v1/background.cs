using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Platformer_v1
{
    public class background
    {
        private Texture2D mytexture, backgroundim;

        int screenWidth = 1080;
        int screenHeight = 720;

        int backgroundWidth = 1080;
        int backgroundHeight = 720;

        int scrollWidth = 1080;
        int scrollHeight = 720;

        float backgroundOffset;
        float scrollOffset;

          public background(ContentManager content, string sBackground)
        {
            
            mytexture = content.Load<Texture2D>(sBackground);
            backgroundWidth = mytexture.Width;
            backgroundHeight = mytexture.Height;
            backgroundim = mytexture;
            
        }

        public void Draw(SpriteBatch batch)
        {

            batch.Draw(mytexture, new Rectangle(-1 * (int)backgroundOffset, 0, backgroundWidth, screenHeight), Color.White);

            if (backgroundOffset > backgroundWidth - screenWidth)
            {
                batch.Draw(mytexture, new Rectangle((-1 * (int)backgroundOffset) + backgroundWidth, 0, backgroundWidth, screenHeight), Color.White);
            }

          
        }
    }
}
