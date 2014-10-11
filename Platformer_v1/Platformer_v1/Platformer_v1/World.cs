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

            
            // For now hardcoding in some stuff for testing reasons
            Player p = new Player("Pacheco", new Vector2(0,0));

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
            // update worldObject's logic
            foreach (I_WorldObject x in worldObjects)
            {
                x.Update(gameTime);
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
