using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platformer_v1
{
    interface I_WorldObject
    {
        void LoadContent(ContentManager content);

        void Update(GameTime gameTime);

        Texture2D getTexture();

        Vector2 getTextureOrigin();

        Vector2 getPosition();
        void setPosition(Vector2 newPosition);

        float getRotation();

        Vector2 getVelocity();
        void setVelocity(Vector2 newVelocity);

        BoundingBox getBoundingBox();

        // determines if the phsyics engine will update it
        bool hasPhysics();
        void setPhysics(bool p);

        // if false will be deleted from the worldObject list!
        bool isAlive();


        // tests if object is rigid aka can I stand on / walk through this
        bool isRigid();


        // alert a world object that a collision has occured with the given object
        void alertCollision(I_WorldObject collidedObject);




        // also mainly for debugging
        Color getColor();

        // for debugging
        String getName();

    }

}
