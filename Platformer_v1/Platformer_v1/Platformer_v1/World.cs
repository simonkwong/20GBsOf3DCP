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
using System.Timers;

namespace Platformer_v1
{
    class World
    {
        protected Game1 game;
        public int WorldWidth { get; set; }
        public int WorldHeight { get; set; }
        Camera camera;
        int flipsong = 0;
       
        private int phraseint;
        //Dictionary<String, String> phrases;

        const int MAX_SPIKEFIELDS = 10;
        bool storyflip = false;
        bool gamemenu = true;

        public List<I_WorldObject> worldObjects;
        public List<I_WorldObject> worldtextObjects;
        public List<I_WorldObject> worldObjectspre;

        Texture2D backgroundImage;
        Texture2D initialimage;
        private ScrollingBackground scrollingBackground;
        Texture2D firstImage;
        private int time;

        Vector2 currentPosition = Vector2.Zero;
        Song song;
        Song prelude;
        private background firstBackground;
        private background initialBackground;


        //private void DrawText(String st, SpriteBatch sb, SpriteFont font)
        //{
        //    sb.DrawString(font, st, new Vector2(200, 50), Color.Black);
        //}

        public World(Game1 containingGame, int w, int h)
        {
            game = containingGame;
            WorldWidth = w;
            WorldHeight = h;
            phraseint = 0;
            time = 6;

            
            worldObjectspre = new List<I_WorldObject>();
            worldtextObjects = new List<I_WorldObject>();
            faces f = new faces("Adam", WorldData.GetInstance().playerInitialPosition, worldObjects);
            //faces fp = new faces("pachecofacebig", WorldData.GetInstance().playerInitialPosition, worldObjects);
            camera = new Camera(containingGame.spriteBatch, f);
            //worldObjects.Add(f);
            //worldObjects.Add(fp);
            //fp.setPosition(new Vector2(1000, 0));
            f.setPosition(new Vector2(300, 100));
            //phrases = new Dictionary<string,string>();
                
              
            //for (int i = 0; i < WorldData.GetInstance().fieldvaluepairs.Count; i += 2)
            //{
            //    phrases.Add(WorldData.GetInstance().fieldvaluepairs.ElementAt<string>(i), WorldData.GetInstance().fieldvaluepairs.ElementAt<string>(i + 1));
            //}

            foreach (Vector2 platPos in WorldData.GetInstance().textPositions)
            {
                messagebox box = new messagebox("poke_text_messagenew", platPos);
                box.setPosition(new Vector2(-25, 100));
                worldObjectspre.Add(box);
            }

            foreach (String s in WorldData.GetInstance().fieldvaluepairs)
            {
                messagetexts text = new messagetexts(s, new Vector2(0, 0));
                text.setPosition(new Vector2(200, 530));
                worldtextObjects.Add(text);
            }

            worldObjects = new List<I_WorldObject>();

            Player p = new Player("Adam", WorldData.GetInstance().playerInitialPosition, worldObjects);

            camera = new Camera(containingGame.spriteBatch, p);

            // Read in the xml
            // and add everything to the worldObjects list

            // Changing PlayerName to Adam, Jordan, Pacheco, or Simon draws that texture


            foreach (Vector2 spikePos in WorldData.GetInstance().spikePositions)
            {
                TestBlock s = new TestBlock("Spikes", spikePos + new Vector2(0, 2));
                s.setDirection(new Vector2(0, -1));
                s.setRigid(false);
                worldObjects.Add(s);
            }

            foreach (Vector2 enemPos in WorldData.GetInstance().enemyPositions)
            {
                Enemy e = new Enemy("Demon", enemPos);
                e.setRigid(false);
                worldObjects.Add(e);
            }

            foreach (Vector2 platPos in WorldData.GetInstance().platformPositions)
            {
                TestBlock b = new TestBlock("usf", platPos);
                worldObjects.Add(b);
            }

            worldObjects.Add(p);
            
        }

        public void LoadContent(ContentManager content)
        {
          
            foreach (I_WorldObject x in worldObjects)
            {
                x.LoadContent(content);
            }
            foreach (I_WorldObject word in worldtextObjects)
            {
                word.LoadContent(content);
            }
            foreach (I_WorldObject textbox in worldObjectspre)
            {
                textbox.LoadContent(content);
            }
            song = content.Load<Song>("chant1");
            prelude = content.Load<Song>("bar");
            camera.LoadContent(content);
            initialimage = content.Load<Texture2D>("spriteArt/church");
            initialBackground = new background(content, "spriteArt/church");
            firstImage = content.Load<Texture2D>("spriteArt/bar");
            backgroundImage = content.Load<Texture2D>("spriteArt/background");
            scrollingBackground = new ScrollingBackground(content, "spriteArt/background");
            firstBackground = new background(content, "spriteArt/bar");
            
            

        }

        public void musichandler()
        {
            if (gamemenu == true && flipsong == 0)
            {
                MediaPlayer.Play(song);
                flipsong = 1;
            }
            else if (gamemenu != true && storyflip == false && flipsong == 0)
            {
                MediaPlayer.Stop();
                MediaPlayer.Play(prelude);
                flipsong = 1;
            }
            else if (gamemenu != true && storyflip == true && flipsong == 0)
            {
                MediaPlayer.Stop();
                MediaPlayer.Play(song);
                flipsong = 1;
            }
        }


        public void Update(GameTime gameTime)
        {
            
            camera.Update(gameTime);
            
            musichandler();

            checkforenter();

            if (time == 0)
            {
                updatetext();
                time = 6;
            }
            else
            {
                time--;
            }

           
            if (storyflip == false && gamemenu == false)
            {
                // update worldObject's logic
                foreach (I_WorldObject x in worldtextObjects)
                {

                    x.Update(gameTime);

                    //checkCollisions(x);

                    //checkForAliveness(x, toDelete);

                }

                
            }

            if (storyflip != false)
            {

                List<I_WorldObject> toDelete = new List<I_WorldObject>();

                scrollBackground();

                // update worldObject's logic
                
                
                
                foreach (I_WorldObject x in worldObjects)
                {

                    x.Update(gameTime);

                    checkCollisions(x);

                    checkForAliveness(x, toDelete);

                }

                 // delete dead objects from the list populated by 
                // checkForAliveness
                foreach (I_WorldObject x in toDelete)
                {
                    worldObjects.Remove(x);
                }
            }
        }

        private void scrollBackground()
        {
            GamePadState controller = GamePad.GetState(PlayerIndex.One);


            if (camera.tracking)
            {

                if (Keyboard.GetState().IsKeyDown(Keys.Left) || controller.IsButtonDown(Buttons.DPadLeft))
                {
                    scrollingBackground.BackgroundOffset -= 0.5f;
                    scrollingBackground.ParallaxOffset -= 200;
                }

                if (Keyboard.GetState().IsKeyDown(Keys.Right) || controller.IsButtonDown(Buttons.DPadRight))
                {
                    scrollingBackground.BackgroundOffset += 0.5f;
                    scrollingBackground.ParallaxOffset += 200;
                }
            }
        }

        private void updatetext()
        {
            GamePadState controller = GamePad.GetState(PlayerIndex.One);

            if (Keyboard.GetState().IsKeyDown(Keys.Space) || controller.IsButtonDown(Buttons.B))
            {
                phraseint++;
            }

            
        }

        private void checkforenter()
        {
            GamePadState controller = GamePad.GetState(PlayerIndex.One);

            if (Keyboard.GetState().IsKeyDown(Keys.Enter) || controller.IsButtonDown(Buttons.A))
            {
                gamemenu = false;
                flipsong = 0;
            }


        }

        public void checkCollisions(I_WorldObject x)
        {
            // collision check loop against every other I_worldObject
            // this is probally not the most efficent way but yolo


            // if a collision occurs it alerts the object that it has
            // and what it collided with

            // its up to the object to decide what it will do about that

            if (!x.shouldIcheckCollisions())
            {
                return;
            }

            foreach (I_WorldObject y in worldObjects)
            {
                if (Object.ReferenceEquals(x, y))
                {
                    // do not compare to itself!
                    continue;
                }

                if (x.getBoundingBox().Intersects(y.getBoundingBox()))
                {
                    x.alertCollision(y);
                }
            }
        }

        


        public void checkForAliveness(I_WorldObject x, List<I_WorldObject> toDelete)
        {
            // checks if the worldObject should be removed from the
            // worldObject list

            if (!x.isAlive())
            {
                toDelete.Add(x);
            }
        }

        public void Draw(SpriteBatch sb)
        {
            if (gamemenu != false)
            {
                initialBackground.Draw(sb);
            }

            if (storyflip != false && gamemenu == false)
            {

                scrollingBackground.Draw(sb);

                // camera draws every worldObject
                foreach (I_WorldObject x in worldObjects)
                {
                    camera.Draw(x);
                }
            }


            else if (storyflip == false && gamemenu == false)
            {
                firstBackground.Draw(sb);

                // camera draws every worldObject
                foreach (I_WorldObject x in worldObjectspre)
                {
                    camera.Draw(x);
                }
                
                if (phraseint < worldtextObjects.Count)
                {
                    camera.DrawText(worldtextObjects[phraseint]);
                }
                else
                {
                    storyflip = true;
                    flipsong = 0;
                }
            }
        }
    }
}
