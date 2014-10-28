using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
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

        private double startTime;
        private float duration;

        private Song victorySong;

        World gameWorld;

        private SoundEffectInstance currentSound;
        int index;


        public ScaryImage()
        {
            scarySounds = new List<SoundEffect>();
            scaryImages = new List<Texture2D>();
            scareEnabled = false;
            startTime = -1;
            duration = -1;
            index = -1;
        }

        public void LoadContent(ContentManager content)
        {
            scaryImages.Add(content.Load<Texture2D>("spriteArt/the-ring"));
            scaryImages.Add(content.Load<Texture2D>("spriteArt/youre_winner"));
            scarySounds.Add(content.Load<SoundEffect>("sounds/scaryscream1"));


            victorySong = content.Load<Song>("sounds/win");
      
    
            currentImg = scaryImages.ElementAt(0);
            currentSound = scarySounds.ElementAt(0).CreateInstance();
            currentSound.Volume = 0.1f;
        }

        public void update(GameTime gametime)
        {
            if (scareEnabled && startTime == -1)
            {
                // record startTime
                startTime = gametime.TotalGameTime.TotalMilliseconds;

                if (index == 1)
                {
         
                }
                else
                {
                    // play sound
                    currentSound.Play();
                }

                
            }

            if (scareEnabled && gametime.TotalGameTime.TotalMilliseconds - startTime >= 1000)
            {
                // turn off the scare
                startTime = -1;
                scareEnabled = false;
            }
        }

        public void scare(int index, float scareTime)
        {
            if (index == 1)
            {
                Console.Write("HERE");
                this.index = index;
                currentImg = scaryImages.ElementAt(index);
                MediaPlayer.Play(victorySong);
                this.duration = scareTime;
                scareEnabled = true;
            }
            else
            {
                this.index = index;
                currentImg = scaryImages.ElementAt(index);
                currentSound = scarySounds.ElementAt(index).CreateInstance();
                currentSound.Volume = 0.1f;
                scareEnabled = true;
                this.duration = scareTime;
            }

        }

        public void Draw(SpriteBatch sb)
        {
            if (scareEnabled)
            {
                if (index == 1)
                {
                    sb.Draw(currentImg, new Vector2(400,200), Color.White);
                }
                else
                {
                    sb.Draw(currentImg, new Vector2(0,0), Color.White);
                }

                
            }
        }
    }
}
