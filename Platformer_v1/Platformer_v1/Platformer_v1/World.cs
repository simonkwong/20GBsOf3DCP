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
    class World
    {
        protected Game1 game;
        public int WorldWidth { get; set; }
        public int WorldHeight { get; set; }
        Camera camera;

        const int MAX_SPIKEFIELDS = 10;

        public List<I_WorldObject> worldObjects;

        Texture2D backgroundImage;
        private ScrollingBackground scrollingBackground;

        Vector2 currentPosition = Vector2.Zero;
        Song song;


        public World(Game1 containingGame, int w, int h)
        {
            game = containingGame;
            WorldWidth = w;
            WorldHeight = h;



            worldObjects = new List<I_WorldObject>();

            Player p = new Player("Jordan", WorldData.GetInstance().playerInitialPosition, worldObjects);

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
            song = content.Load<Song>("chant1");
            // MediaPlayer.Play(song);

            backgroundImage = content.Load<Texture2D>("spriteArt/background");
            scrollingBackground = new ScrollingBackground(content, "spriteArt/background");

            foreach (I_WorldObject x in worldObjects)
            {
                x.LoadContent(content);
            }
        }

        public void Update(GameTime gameTime)
        {
            camera.Update(gameTime);

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
            scrollingBackground.Draw(sb);
            // camera draws every worldObject
            foreach (I_WorldObject x in worldObjects)
            {
                camera.Draw(x);
            }
        }
    }
}
