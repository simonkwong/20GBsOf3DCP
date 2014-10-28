using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Platformer_v1
{
    class QuadTree
    {
        const int MAX_DEPTH = 4;
        private QuadTreeNode mRoot;

        private int mMinWidth;
        /// <summary>
        /// Create an empty quadtree that will store rectangles that fit entirely within 
        /// the Rectangle defined by worldsize
        /// </summary>
        /// <param name="worldsize">The size of the world this quadtree can represent</param>
        public QuadTree(Rectangle worldsize)
        {
            mRoot = new QuadTreeNode(worldsize, null);
            mMinWidth = worldsize.Width / (int)Math.Pow(4, MAX_DEPTH - 1);
        }

        /// <summary>
        /// Update the location of a world element within the quadtree
        /// </summary>
        /// <param name="worldObj">The object whose location we wish to update</param>
        /// <param name="tree">A pointer to the quadtree node where this object currently lives</param>
        /// <returns>Pointer to the node in the quadtree where the element lives after the update</returns>
        public QuadTreeNode UpdateLocation(I_WorldObject worldObj, QuadTreeNode tree)
        {
            if (tree != null)
            {
                Rectangle aabb = boundingBoxToRectangle(worldObj);
                if (tree.Area.Contains(aabb) && AddToThisNode(tree, aabb))
                    return tree;

                tree.RemoveElement(worldObj);
                while (tree.Parent != null && !tree.Area.Contains(aabb))
                {
                    tree = tree.Parent;
                }
                return insert(tree, worldObj);
            }
            return insert(worldObj);
        }

        /// <summary>
        /// Insert a world element into the quadtree
        /// </summary>
        /// <param name="elem">World element to insert</param>
        /// <returns>The quadtree node where the element was placed (used for changing the
        /// location of the quadtree element</returns>
        public QuadTreeNode insert(I_WorldObject elem)
        {
            return insert(mRoot, elem);
        }

        /// <summary>
        /// Find a list of all world elements in the quadtree that intersect with a given region
        /// </summary>
        /// <param name="region">The region to check for intersection</param>
        /// <param name="intersects">(Output parameter) the list of all objects whose
        /// AABB intersects this region</param>
        public void FindIntersects(Rectangle region, ref List<I_WorldObject> intersects)
        {
            intersects.Clear();
            FindIntersects(mRoot, ref intersects, region);
        }

        /// <summary>
        /// Helper method to find a list of all world elements in the quadtree that intersect
        /// a given region
        /// </summary>
        /// <param name="root">Root of the tree to search</param>
        /// <param name="intersects">(output parameter) list of all objects who AABB intersect this region</param>
        /// <param name="r">Region to check for intersection</param>
        private void FindIntersects(QuadTreeNode root, ref List<I_WorldObject> intersects, Rectangle r)
        {
            if (root != null)
            {
                foreach (I_WorldObject o in root.mElements)
                {
                    if (boundingBoxToRectangle(o).Intersects(r))
                    {
                        intersects.Add(o);
                    }
                }

                int midWidth = root.Area.Width / 2;
                int midHeight = root.Area.Height / 2;

                if ((r.Left < root.Area.Left + midWidth) && (r.Top < root.Area.Top + midHeight))
                {
                    FindIntersects(root.mUpperLeft, ref intersects, r);
                }
                if ((r.Right > root.Area.Left + midWidth) && (r.Top < root.Area.Top + midHeight))
                {
                    FindIntersects(root.mUpperRight, ref intersects, r);
                }
                if ((r.Left < root.Area.Left + midWidth) && (r.Bottom > root.Area.Top + midHeight))
                {
                    FindIntersects(root.mLowerLeft, ref intersects, r);
                }
                if ((r.Right > root.Area.Left + midWidth) && (r.Bottom > root.Area.Top + midHeight))
                {
                    FindIntersects(root.mLowerRight, ref intersects, r);
                }
            }
        }



        /// <summary>
        /// Helper method to insert a world element into a quadtree
        /// </summary>
        /// <param name="root">Root of the tree to insert the element into</param>
        /// <param name="elem">Element to insert</param>
        /// <param name="depth">Maximum depth to insert the element</param>
        /// <returns>A pointer to the quadtree node where the element is inserted (for moving
        /// or removing the element at a later time)</retfrns>
        private QuadTreeNode insert(QuadTreeNode root, I_WorldObject elem)
        {
            Rectangle elemAABB = boundingBoxToRectangle(elem);

            if (AddToThisNode(root, elemAABB))
            {
                root.mElements.Add(elem);
                return root;
            }
            int midWidth = root.Area.Width / 2;
            int midHeight = root.Area.Height / 2;
            int centerX = root.Area.Left + midWidth;
            int centerY = root.Area.Top + midHeight;

            if (elemAABB.Left < centerX && elemAABB.Top < centerY)
            {
                if (root.mUpperLeft == null)
                {
                    root.mUpperLeft = new QuadTreeNode(new Rectangle(root.Area.X, root.Area.Y, midWidth, midHeight), root);
                }
                return insert(root.mUpperLeft, elem);
            }
            else if (elemAABB.Left > centerX && elemAABB.Top < centerY)
            {
                if (root.mUpperRight == null)
                {
                    root.mUpperRight = new QuadTreeNode(new Rectangle(root.Area.X + midWidth, root.Area.Y, root.Area.Width - midWidth, midHeight), root);
                }
                return insert(root.mUpperRight, elem);
            }
            else if (elemAABB.Left < centerX)
            {
                if (root.mLowerLeft == null)
                {
                    root.mLowerLeft = new QuadTreeNode(new Rectangle(root.Area.X, root.Area.Y + midHeight, midWidth, root.Area.Height - midHeight), root);
                }
                return insert(root.mLowerLeft, elem);
            }
            else
            {
                if (root.mLowerRight == null)
                {
                    root.mLowerRight = new QuadTreeNode(new Rectangle(root.Area.X + midWidth, root.Area.Y + midHeight, root.Area.Width - midWidth, root.Area.Height - midHeight), root);
                }
                return insert(root.mLowerRight, elem);
            }
        }


        /// <summary>
        /// Helper method to determine if a region should be added to the root of a given
        /// tree.  Region is added to the root if it does not fit completely within one
        /// of the four quadrants of the area stored at the root.
        /// </summary>
        /// <param name="root">Root of the tree to check</param>
        /// <param name="region">Region to check</param>
        /// <returns>If the region should be added to the root of the given tree</returns>
        private bool AddToThisNode(QuadTreeNode root, Rectangle region)
        {
            int midWidth = root.Area.Width / 2;
            int midHeight = root.Area.Height / 2;
            int centerX = root.Area.Left + midWidth;
            int centerY = root.Area.Top + midHeight;

            return root.Area.Width <= mMinWidth ||
                    ((region.Left < centerX && region.Right > centerX) ||
                     (region.Top < centerY && region.Bottom > centerY));
        }

        private Rectangle boundingBoxToRectangle(I_WorldObject obj)
        {
            Rectangle AABB;

            if (obj.getName().CompareTo("coin") == 0)
            {
                AABB = new Rectangle((int)obj.getPosition().X, (int)obj.getPosition().Y,
                               20, obj.getTexture().Height);
            }
            else
            {
                AABB = new Rectangle((int)obj.getPosition().X, (int)obj.getPosition().Y,
                               obj.getTexture().Width, obj.getTexture().Height);
            }


            return AABB;
        }
    }

    public class QuadTreeNode
    {
        public QuadTreeNode(Rectangle area, QuadTreeNode parent)
        {
            Area = area;
            mElements = new List<I_WorldObject>(10);
            Parent = parent;
        }
        public Rectangle Area { get; set; }
        public QuadTreeNode mUpperLeft;
        public QuadTreeNode mUpperRight;
        public QuadTreeNode mLowerRight;
        public QuadTreeNode mLowerLeft;
        public QuadTreeNode Parent;
        public List<I_WorldObject> mElements;

        internal void RemoveElement(I_WorldObject worldObj)
        {
            mElements.Remove(worldObj);
        }
    }
}
