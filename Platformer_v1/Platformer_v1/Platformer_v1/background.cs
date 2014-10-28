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

        int backgroundWidth = 1080;
        int backgroundHeight = 720;


        public background(ContentManager content, string sBackground)
        {  
            mytexture = content.Load<Texture2D>(sBackground);

            backgroundim = mytexture;
        }

        public void Draw(SpriteBatch batch)
        {

            batch.Draw(mytexture, new Rectangle(0, 0, backgroundWidth, backgroundHeight), Color.White);
          
        }
    }
}
