using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platformer_v1
{
    class TextBox
    {

        public Vector2 position;
        public SpriteFont Font1;
        public String text;
        public String fontName;
        public Color color;

        public TextBox(Vector2 position, String text, String fontName, Color color)
        {
            this.position = position;
            this.text = text;
            this.fontName = fontName;
            this.color = color;
        }

        public void LoadContent(ContentManager content)
        {
            Font1 = content.Load<SpriteFont>(fontName);
        }

      
        public void Draw(SpriteBatch sb)
        {

            sb.DrawString(Font1, text, position, color,
            0, new Vector2(0, 0), 1.0f, SpriteEffects.None, 0.5f);
        }
    }
}
