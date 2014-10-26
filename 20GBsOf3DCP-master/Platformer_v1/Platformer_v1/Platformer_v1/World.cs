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

        const int MAX_SPIKEFIELDS = 20;

        public List<I_WorldObject> worldObjects;
        public List<I_WorldObject> intersectingObjects;
        public List<I_WorldObject> toDelete;

        Texture2D backgroundImage;
        private ScrollingBackground scrollingBackground;

        Vector2 currentPosition = Vector2.Zero;
        Song song;

        public QuadTree qt;

        public World(Game1 containingGame, int w, int h)
        {
            game = containingGame;
            WorldWidth = w;
            WorldHeight = h;

            int worldX = 0;
            int worldY = 0;

            toDelete = new List<I_WorldObject>();
            worldObjects = new List<I_WorldObject>();
            intersectingObjects = new List<I_WorldObject>();

            Player p = new Player("Simon", WorldData.GetInstance().playerInitialPosition, worldObjects);

            worldObjects.Add(p);

            camera = new Camera(containingGame.spriteBatch, p);

            foreach (Vector2 spikePos in WorldData.GetInstance().spikePositions)
            {
                if (spikePos.X > worldX)
                    worldX = (int) spikePos.X;
                if (spikePos.Y > worldY)
                    worldY = (int)spikePos.Y;

                TestBlock s = new TestBlock("Spikes", spikePos + new Vector2(0, 2));
                s.setDirection(new Vector2(0, -1));
                s.setRigid(false);
                worldObjects.Add(s);
            }

            foreach (Vector2 enemPos in WorldData.GetInstance().enemyPositions)
            {

                if (enemPos.X > worldX)
                    worldX = (int)enemPos.X;
                if (enemPos.Y > worldY)
                    worldY = (int)enemPos.Y;

                Enemy e = new Enemy("pachecoface", enemPos);
                e.setRigid(false);
                worldObjects.Add(e);
            }

            foreach (Vector2 platPos in WorldData.GetInstance().platformPositions)
            {
                if (platPos.X > worldX)
                    worldX = (int)platPos.X;
                if (platPos.Y > worldY)
                    worldY = (int)platPos.Y;

                TestBlock b = new TestBlock("usf", platPos);
                worldObjects.Add(b);
            }

            Console.WriteLine(worldX);

            Console.WriteLine(worldY);

            qt = new QuadTree(new Rectangle(0, 0, worldX, worldY));
        }

        public void LoadContent(ContentManager content)
        {
            song = content.Load<Song>("chant1");
            //MediaPlayer.Play(song);

            backgroundImage = content.Load<Texture2D>("spriteArt/background");
            scrollingBackground = new ScrollingBackground(content, "spriteArt/background");

            foreach (I_WorldObject x in worldObjects)
            {
                x.LoadContent(content);
                x.setNode(qt.insert(x));
            }
        }

        public void Update(GameTime gameTime)
        {
            camera.Update(gameTime);

            scrollBackground();

            foreach (I_WorldObject x in worldObjects)
            {
                x.Update(gameTime);

                if (x.getName() == "Jordan" || x.getName() == "Simon" || x.getName() == "Adam")
                {
                    x.setNode(qt.UpdateLocation(x, x.getNode()));
                    checkCollisions(x);
                }
                 
                checkForAliveness(x, toDelete);
            }

            foreach (I_WorldObject z in toDelete)
            {
                worldObjects.Remove(z);
            }
        }

        public void checkCollisions(I_WorldObject x)
        {
            Rectangle region = new Rectangle((int)x.getPosition().X, (int)x.getPosition().Y,
                                             WorldData.GetInstance().ScreenWidth, WorldData.GetInstance().ScreenHeight);


            qt.FindIntersects(boundingBoxToRectangle(x), ref intersectingObjects);

            Console.WriteLine(intersectingObjects.Count);
            
            foreach (I_WorldObject y in intersectingObjects)
            {
                if (!object.ReferenceEquals(x, y))
                {
                    x.alertCollision(y);
                }
            }

        }

        private Rectangle boundingBoxToRectangle(I_WorldObject obj)
        {
            Rectangle AABB = new Rectangle((int)obj.getPosition().X, (int)obj.getPosition().Y,
                                           obj.getTexture().Width, obj.getTexture().Height);

            return AABB;
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

        public void checkForAliveness(I_WorldObject x, List<I_WorldObject> toDelete)
        {
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
