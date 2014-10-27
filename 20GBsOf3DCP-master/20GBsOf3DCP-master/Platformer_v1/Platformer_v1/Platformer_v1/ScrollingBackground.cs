using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Platformer_v1
{
    public class ScrollingBackground
    {
        private Texture2D mytexture, background;

        int screenWidth = 1080;
        int screenHeight = 720;

        int backgroundWidth = 1080;
        int backgroundHeight = 720;

        int scrollWidth = 1080;
        int scrollHeight = 720;

        float backgroundOffset;
        float scrollOffset;

        public ScrollingBackground(ContentManager content, string sBackground)
        {
            
            mytexture = content.Load<Texture2D>(sBackground);
            backgroundWidth = mytexture.Width;
            backgroundHeight = mytexture.Height;
            background = mytexture;
            scrollWidth = background.Width;
            scrollHeight = background.Height;
            drawParallax = false;
        }
        
        public float BackgroundOffset
        {
            get { return backgroundOffset; }
            set
            {
                backgroundOffset = value;
                if (backgroundOffset < 0)
                {
                    backgroundOffset += backgroundWidth;
                }
                if (backgroundOffset > backgroundWidth)
                {
                    backgroundOffset -= backgroundWidth;
                }
            }
        }

        public float ParallaxOffset
        {
            get { return scrollOffset; }
            set
            {
                scrollOffset = value;
                if (scrollOffset < 0)
                {
                    scrollOffset += scrollWidth;
                }
                if (scrollOffset > scrollWidth)
                {
                    scrollOffset -= scrollWidth;
                }
            }
        }

        // Determines if we will draw the Parallax overlay.
        bool drawParallax = true;

        public bool DrawParallax
        {
            get { return drawParallax; }
            set { drawParallax = value; }
        }

        public void Draw(SpriteBatch batch)
        {

            batch.Draw(mytexture, new Rectangle(-1 * (int) backgroundOffset, 0, backgroundWidth, screenHeight), Color.White);

            if (backgroundOffset > backgroundWidth - screenWidth)
            {
                batch.Draw(mytexture, new Rectangle((-1 * (int) backgroundOffset) + backgroundWidth, 0, backgroundWidth, screenHeight), Color.White);
            }

            if (drawParallax)
            {

                batch.Draw(background, new Rectangle(-1 * (int) scrollOffset, 0, scrollWidth, screenHeight), Color.SlateGray);

                if (scrollOffset > scrollWidth - screenWidth)
                {
                    batch.Draw(background, new Rectangle((-1 * (int) scrollOffset) + scrollWidth, 0, scrollWidth, screenHeight), Color.SlateGray);
                }
            }
        }
    }
}
