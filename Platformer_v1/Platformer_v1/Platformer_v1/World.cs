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

        public ContentManager content;

        public List<I_WorldObject> worldObjects;
        public List<I_WorldObject> intersectingObjects;
        public List<I_WorldObject> toDelete;

        Texture2D backgroundImage;
        private ScrollingBackground scrollingBackground;

        Vector2 currentPosition = Vector2.Zero;
        Song song;



        static Random rndGen = new Random();

        public QuadTree qt;

        public TextBox scoreDisplay;

        public ScaryImage scareImg;

        public Game1 containingGame;
        public int w;
        public int h;

        public Player p;

        public World(Game1 containingGame, int w, int h)
        {
            this.containingGame = containingGame;
            this.w = w;
            this.h = h;
            init_everything();
        }

        public void init_everything()
        {
            scareImg = new ScaryImage();
            scoreDisplay = new TextBox(new Vector2(20, 0), "coins: 0", "HUDfont", Color.Yellow);
            game = containingGame;
            WorldWidth = w;
            WorldHeight = h;

            int worldX = 0;
            int worldY = 0;

            toDelete = new List<I_WorldObject>();
            worldObjects = new List<I_WorldObject>();
            intersectingObjects = new List<I_WorldObject>();

            p = new Player("Simon", WorldData.GetInstance().playerInitialPosition, worldObjects);

            worldObjects.Add(p);

            camera = new Camera(containingGame.spriteBatch, p);

            foreach (Vector2 spikePos in WorldData.GetInstance().spikePositions)
            {
                if (spikePos.X > worldX)
                    worldX = (int)spikePos.X;
                if (spikePos.Y > worldY)
                    worldY = (int)spikePos.Y;

                TestBlock s = new TestBlock("Spikes", spikePos + new Vector2(0, 2), scareImg, this);
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

                TestBlock b = new TestBlock("usf", platPos, scareImg, this);
                worldObjects.Add(b);
            }

            foreach (Vector2 movingPlatPos in WorldData.GetInstance().movingPlatformPositions)
            {
                if (movingPlatPos.X > worldX)
                    worldX = (int)movingPlatPos.X;
                if (movingPlatPos.Y > worldY)
                    worldY = (int)movingPlatPos.Y;

                TestBlock mp = new TestBlock("movingPlatform", movingPlatPos, scareImg, this);
                worldObjects.Add(mp);
            }

            foreach (Vector2 coinPos in WorldData.GetInstance().coinPositions)
            {
                if (coinPos.X > worldX)
                    worldX = (int)coinPos.X;
                if (coinPos.Y > worldY)
                    worldY = (int)coinPos.Y;

                Coin c = new Coin("coin", 20, new Vector2(0, 0), true, 0.08f, coinPos, scoreDisplay);
                c.setRigid(false);
                worldObjects.Add(c);
            }


            foreach (Vector2 scrollPos in WorldData.GetInstance().scrollPositions)
            {
                if (scrollPos.X > worldX)
                    worldX = (int)scrollPos.X;
                if (scrollPos.Y > worldY)
                    worldY = (int)scrollPos.Y;

                TestBlock s = new TestBlock("scroll", scrollPos, scareImg, this);
                s.setRigid(false);
                worldObjects.Add(s);
            }

            TestBlock scareBLock = new TestBlock("ScareBlock", WorldData.GetInstance().playerInitialPosition + new Vector2(800, 300), scareImg, this);


            worldObjects.Add(scareBLock);

            qt = new QuadTree(new Rectangle(0, 0, worldX, worldY));
        }


        public void LoadContent(ContentManager content)
        {

            this.content = content;

            scareImg.LoadContent(content);
            scoreDisplay.LoadContent(content);
            song = content.Load<Song>("chant1");
            MediaPlayer.Play(song);

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
            scareImg.update(gameTime);
            camera.Update(gameTime);
            scrollBackground();

            foreach (I_WorldObject x in worldObjects)
            {
                x.Update(gameTime);

                int level_before = WorldData.level;

                if (x.getName() == "Jordan" || x.getName() == "Simon" || x.getName() == "Adam")
                {
                    x.setNode(qt.UpdateLocation(x, x.getNode()));
                    checkCollisions(x);
                }


                if (!x.isAlive())
                {
                    x.getNode().RemoveElement(x);
                }
                x.setNode(qt.UpdateLocation(x, x.getNode()));

                checkForAliveness(x, toDelete);

                int level_after = WorldData.level;

                if (level_before != level_after)
                {
                    Console.WriteLine("NEW LEVEL EVENT OCCURED");
                    WorldData.newLevelEvent = true;

                    init_everything();
                    LoadContent(content);
                }

        

            }

            foreach (I_WorldObject z in toDelete)
            {
                worldObjects.Remove(z);
            }
        }

        public void checkCollisions(I_WorldObject x)
        {
            
            qt.FindIntersects(boundingBoxToRectangle(x), ref intersectingObjects);
            
            foreach (I_WorldObject y in intersectingObjects)
            {
                if (!object.ReferenceEquals(x, y))
                {
                    x.alertCollision(y);
                    y.alertCollision(x);


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

            scoreDisplay.Draw(sb);

            scareImg.Draw(sb);

        }
    }
}
