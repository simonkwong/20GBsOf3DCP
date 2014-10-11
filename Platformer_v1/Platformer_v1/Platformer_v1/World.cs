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

        public List<WorldObject> worldObjects;

        public World(Game1 containingGame, int w, int h)
        {
            game = containingGame;
            WorldWidth = w;
            WorldHeight = h;
            camera = new Camera(containingGame.spriteBatch);
            worldObjects = new List<WorldObject>();        
        }

        public void LoadContent(ContentManager content)
        {
            // load in XML file and add everything
            // into the worldObject list

            Player p = new Player();
            worldObjects.Add(p);

        }

        public void Update(GameTime gameTime)
        {
            // update worldObject's logic
            foreach (WorldObject x in worldObjects)
            {

            }
        }

        public void Draw(SpriteBatch sb)
        {
            // camera draws every worldObject
            foreach (WorldObject x in worldObjects)
            {
                camera.Draw(x);
            }
        }
    }
}
