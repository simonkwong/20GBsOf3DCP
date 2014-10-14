﻿using System;
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
        public List<Vector2> platformPositions { get; private set; }

        private static WorldData wData;

        private WorldData()
        {
            Gravity = new Vector2(0, 0);
            ScreenWidth = 0;
            ScreenHeight = 0;
            playerInitialPosition = new Vector2(0, 0);
            platformPositions = new List<Vector2>();
        }

        public static WorldData GetInstance()
        {
            if (wData == null)
            {
                wData = new WorldData();
                wData.LoadData("Content/WorldData.xml");
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
