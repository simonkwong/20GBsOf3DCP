using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platformer_v1
{
    class ScaryImage
    {

        private bool scareEnabled;
        private List<Texture2D> scaryImages;
        private List<SoundEffect> scarySounds;

        private Texture2D currentImg;

        private float startTime;
        private float duration;

        private SoundEffectInstance currentSound;


        public ScaryImage()
        {
            scarySounds = new List<SoundEffect>();
            scaryImages = new List<Texture2D>();
            scareEnabled = false;
            startTime = -1;
            duration = -1;
        }

        public void LoadContent(ContentManager content)
        {
            scaryImages.Add(content.Load<Texture2D>("spriteArt/the-ring"));
            scarySounds.Add(content.Load<SoundEffect>("sounds/scaryscream1"));

            currentImg = scaryImages.ElementAt(0);
            currentSound = scarySounds.ElementAt(0).CreateInstance();
            currentSound.Volume = 0.1f;
            
        }

        public void update(GameTime gametime)
        {

            if (scareEnabled && startTime == -1)
            {
                // record startTime
                startTime = gametime.TotalGameTime.Seconds;

                // play sound
                currentSound.Play();
                
            }

            if (scareEnabled && gametime.TotalGameTime.Seconds - startTime >= duration)
            {
                // turn off the scare
                startTime = -1;
                scareEnabled = false;
            }
        }

        public void scare(int index, float scareTime)
        {
            currentImg = scaryImages.ElementAt(index);
            currentSound = scarySounds.ElementAt(0).CreateInstance();
            currentSound.Volume = 0.1f;
            scareEnabled = true;
            this.duration = scareTime;
        }

        public void Draw(SpriteBatch sb)
        {
            if (scareEnabled)
            {
                sb.Draw(currentImg, new Vector2(0,0), Color.White);
            }
        }
    }
}
