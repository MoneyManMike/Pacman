﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PacMan
{
    public class Clyde : BaseGameSprite
    {
        Graph Map;

        public Clyde(Texture2D texture, Vector2 position, Color color, Vector2 scale, Graph map) 
            : base(texture, position, color, scale)
        {
            UpdateTime = TimeSpan.FromMilliseconds(250);
            Map = map;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if(!HasElapsedTimePassed)
            {
                return;
            }

            //if you're more than 8 tiles away
            if(Math.Abs(GameScreen.pac.Position.X - Position.X) / 40 >= 9 || Math.Abs(GameScreen.pac.Position.Y - Position.Y) / 40 >= 9)
            {
                if (Position != GameScreen.pac.Position)
                {
                    var startVertex = Map.FindVertex(Position);
                    var targetVertex = Map.FindVertex(GameScreen.pac.Position);
                    var path = Map.AStar(startVertex, targetVertex);
                    //We pop right here because the first vertex in the stack
                    //is the start position, which is were we are already
                    path.Pop();
                    var goalPos = path.Pop().Value;
                    if (goalPos != GameScreen.pinky.Position && goalPos != GameScreen.blinky.Position && goalPos != GameScreen.Inky.Position)
                    {
                        Position = goalPos;
                    }
                }
            }
            else
            {
                //Everything is the same size so the pacman is also the grid cell size
                var gridHeight = GameScreen.pac.HitBox.Width;
                var gridWidth = GameScreen.pac.HitBox.Height;
                switch ((Directions)Game1.Rand.Next(0, 4))
                {
                    case Directions.Up:
                        if (Map.FindVertex(new Vector2(Position.X, Position.Y - gridHeight)) != null)
                        {
                            Position.Y -= gridHeight;
                        }
                        break;

                    case Directions.Down:
                        if (Map.FindVertex(new Vector2(Position.X, Position.Y + gridHeight)) != null)
                        {
                            Position.Y += gridHeight;
                        }
                        break;

                    case Directions.Right:
                        if (Map.FindVertex(new Vector2(Position.X + gridWidth, Position.Y)) != null)
                        {
                            Position.X += gridWidth;
                        }
                        break;

                    case Directions.Left:
                        if (Map.FindVertex(new Vector2(Position.X - gridWidth, Position.Y)) != null)
                        {
                            Position.X -= gridWidth;
                        }
                        break;
                }
            }
        }
    }
}
