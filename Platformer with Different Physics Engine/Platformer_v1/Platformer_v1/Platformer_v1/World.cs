using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Platformer_v1
{
    class World
    {
        protected Game1 game;
        public int WorldWidth { get; set; }
        public int WorldHeight { get; set; }
        Camera camera;

        public List<I_WorldObject> worldObjects;

        public World(Game1 containingGame, int w, int h)
        {
            game = containingGame;
            WorldWidth = w;
            WorldHeight = h;
            camera = new Camera(containingGame.spriteBatch);
            worldObjects = new List<I_WorldObject>();   
     
            // Read in the xml
            // and add everything to the worldObjects list

            
            
            // Changing PlayerName to Adam, Jordan, Pacheco, or Simon draws that texture

            Player p = new Player("Jordan", WorldData.GetInstance().playerInitialPosition);

            foreach (Vector2 platPos in WorldData.GetInstance().platformPositions)
            {
                TestBlock b = new TestBlock(platPos);
                worldObjects.Add(b);
            }
            
            worldObjects.Add(p);
        }

        public void LoadContent(ContentManager content)
        {
          foreach(I_WorldObject x in worldObjects)
          {
              x.LoadContent(content);
          }
        }

        public void Update(GameTime gameTime)
        {
            List<I_WorldObject> toDelete = new List<I_WorldObject>();

            // update worldObject's logic
            foreach (I_WorldObject x in worldObjects)
            {
                x.Update(gameTime);

                x.setPosition(x.getDirection() * x.getSpeed() * (float)gameTime.ElapsedGameTime.TotalSeconds);

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


        public void checkCollisions(I_WorldObject x)
        {
            // collision check loop against every other I_worldObject
            // this is probally not the most efficent way but yolo


            // if a collision occurs it alerts the object that it has
            // and what it collided with

            // its up to the object to decide what it will do about that

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
            // camera draws every worldObject
            foreach (I_WorldObject x in worldObjects)
            {
                camera.Draw(x);
            }
        }
    }
}
