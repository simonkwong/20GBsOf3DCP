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

        private SpriteFont font;

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

            if (obj.getName() != "coin")
            {
                mSpriteBatch.Draw(obj.getTexture(), objPosInScreenSpace, null, obj.getColor(), obj.getRotation(), obj.getTextureOrigin(), 1.0f, SpriteEffects.None, 0);
            }
            else if (obj.getName() == "coin")
            {
                Rectangle srcRect = new Rectangle((int)obj.getFrame() * obj.getFrameWidth(), 0, obj.getFrameWidth(), obj.getTexture().Height);

                mSpriteBatch.Draw(obj.getTexture(), objPosInScreenSpace, srcRect, Color.White, obj.getRotation(), Vector2.Zero, obj.getScale(), SpriteEffects.None, 0);
            }
        }


        public void LoadContent(ContentManager content)
        {
            font = content.Load<SpriteFont>("messagefont");
        }

        public void DrawText(I_WorldObject text)
        {
            Vector2 objPosInCameraSpace = text.getPosition();
            objPosInScreenSpace = objPosInCameraSpace;
            string texttowrite = parseText(text.getName());
            mSpriteBatch.DrawString(font, texttowrite, objPosInScreenSpace, Color.Black, text.getRotation(), text.getTextureOrigin(), 1.0f, SpriteEffects.None, 0);
        }

        private String parseText(String text)
        {
            String line = String.Empty;
            String returnString = String.Empty;
            String[] wordArray = text.Split(' ');

            foreach (String word in wordArray)
            {
                if (font.MeasureString(line + word).Length() > 650)
                {
                    returnString = returnString + line + '\n';
                    line = String.Empty;
                }

                line = line + word + ' ';
            }

            return returnString + line;
        }

    }
}
