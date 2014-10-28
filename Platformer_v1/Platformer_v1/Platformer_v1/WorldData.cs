using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Platformer_v1
{
    class WorldData
    {
        public Vector2 Gravity { get; private set; }
        public int ScreenWidth { get; private set; }
        public int ScreenHeight { get; private set; }
        public Vector2 playerInitialPosition { get; private set; }
        public int MaxJumpHeight { get; private set; }
        public List<Vector2> movingPlatformPositions { get; private set; }
        public List<Vector2> platformPositions { get; private set; }
        public List<Vector2> spikePositions { get; private set; }
        public List<Vector2> enemyPositions { get; private set; }
        public List<Vector2> coinPositions { get; private set; }
        public List<Vector2> scrollPositions { get; private set; }
        public Vector2 playerDirection { get; private set; }
        public int playerSpeed { get; private set; }
        public int enemySpeed { get; private set; }
        public int enemyMaxMovement { get; private set; }

        public List<String> fieldvaluepairs { get; private set; }
        public List<Vector2> textPositions { get; private set; }


        private static WorldData wData;

        public static int level = 1;

        public static bool newLevelEvent;


        private WorldData()
        {
            fieldvaluepairs = new List<String>();
            textPositions = new List<Vector2>();

            Gravity = Vector2.Zero; ;
            ScreenWidth = 0;
            ScreenHeight = 0;
            playerInitialPosition = Vector2.Zero;
            MaxJumpHeight = 0;
            movingPlatformPositions = new List<Vector2>();
            platformPositions = new List<Vector2>();
            spikePositions = new List<Vector2>();
            enemyPositions = new List<Vector2>();
            coinPositions = new List<Vector2>();
            playerDirection = Vector2.Zero;
            playerSpeed = 0;
            enemySpeed = 0;
            enemyMaxMovement = 0;
            scrollPositions = new List<Vector2>();
            newLevelEvent = false;
        }

        public static WorldData GetInstance()
        {
            if (wData == null || newLevelEvent)
            {
                wData = new WorldData();
                newLevelEvent = false;

                switch (level)
                {
                    case 1:
                        Console.WriteLine("LOADING FROM LEVEL 1");
                        wData.LoadData("Content/Level1.xml");
                        break;
                    case 2:
                        Console.WriteLine("LOADING FROM LEVEL 2");
                        wData.LoadData("Content/Level2.xml");
                        break;
                    default:
                        Console.WriteLine("NO LEVEL FOUND!");
                        break;
                }

            }

            return wData;
        }



        protected void AddData(XElement elem)
        {
            XMLParse.AddValueToClassInstance(elem, WorldData.GetInstance());
        }

        private void LoadData(String dataFile)
        {

            using (XmlReader reader = XmlReader.Create(new StreamReader(dataFile)))
            {
                XDocument xml = XDocument.Load(reader);
                XElement root = xml.Root;
                foreach (XElement elem in root.Elements())
                {
                    AddData(elem);
                }
            }

        }
    }
}
