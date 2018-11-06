using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyUtility;

public partial class MazeManager : SingletonMonoBehaviour<MazeManager>
{
    public class PathFactory
    {
        RoomFloorFactory floor;
        IntVector2 position;

        protected const int PATH = 2;

        public enum Direction { North = 0, East = 1, South = 2, West = 3, Null = 4 };

        AbstractRoomFactory northWestRoom { get { return floor.Rooms[position.X, position.Y]; } }
        AbstractRoomFactory northEastRoom { get { return floor.Rooms[position.X + 1, position.Y]; } }
        AbstractRoomFactory southEastRoom { get { return floor.Rooms[position.X + 1, position.Y + 1]; } }
        AbstractRoomFactory southWestRoom { get { return floor.Rooms[position.X, position.Y + 1]; } }

        bool haveNorthPath { get { return northWestRoom.HaveEastRoom; } }
        bool haveEastPath { get { return northEastRoom.HaveSouthRoom; } }
        bool haveSouthPath { get { return southEastRoom.HaveWestRoom; } }
        bool haveWestPath { get { return southWestRoom.HaveNorthRoom; } }

        public PathFactory(int posX, int posY, RoomFloorFactory _floor)
        {
            floor = _floor;
            position = new IntVector2(posX, posY);
        }

        public void CutRandomPath()
        {
            var dir = Util.RandomEnumValue<Direction>();

            if (dir == Direction.North)
            {
                northWestRoom.HaveEastRoom = false;
                northEastRoom.HaveWestRoom = false;
            }
            if (dir == Direction.East)
            {
                northEastRoom.HaveSouthRoom = false;
                southEastRoom.HaveNorthRoom = false;
            }
            if (dir == Direction.South)
            {
                southEastRoom.HaveWestRoom = false;
                southWestRoom.HaveEastRoom = false;
            }
            if (dir == Direction.West)
            {
                southWestRoom.HaveNorthRoom = false;
                northWestRoom.HaveSouthRoom = false;
            }
        }

        public void CreatePath()
        {
            if (haveNorthPath)
            {
                var startPoint = new IntVector2(northWestRoom.EndPoint.X, Random.Range(northWestRoom.StartPoint.Y, northWestRoom.EndPoint.Y))
                    + new IntVector2(northWestRoom.Position.X * floor.MaxRoomSize.X, northWestRoom.Position.Y * floor.MaxRoomSize.Y);
                var endPoint = new IntVector2(northEastRoom.StartPoint.X - 1, Random.Range(northEastRoom.StartPoint.Y, northEastRoom.EndPoint.Y))
                    + new IntVector2(northEastRoom.Position.X * floor.MaxRoomSize.X, northEastRoom.Position.Y * floor.MaxRoomSize.Y);
                connectPathVertically(startPoint, endPoint);
            }

            if (haveWestPath)
            {
                var startPoint = new IntVector2(Random.Range(northWestRoom.StartPoint.X, northWestRoom.EndPoint.X), northWestRoom.EndPoint.Y)
                    + new IntVector2(northWestRoom.Position.X * floor.MaxRoomSize.X, northWestRoom.Position.Y * floor.MaxRoomSize.Y);
                var endPoint = new IntVector2(Random.Range(southWestRoom.StartPoint.X, southWestRoom.EndPoint.X), southWestRoom.StartPoint.Y - 1)
                    + new IntVector2(southWestRoom.Position.X * floor.MaxRoomSize.X, southWestRoom.Position.Y * floor.MaxRoomSize.Y);
                connectPathHorizontally(startPoint, endPoint);
            }

            if (position.Y == floor.MaxRoomCount.Y - 2)
            {
                if (haveSouthPath)
                {
                    var startPoint = new IntVector2(southWestRoom.EndPoint.X, Random.Range(southWestRoom.StartPoint.Y, southWestRoom.EndPoint.Y))
                    + new IntVector2(southWestRoom.Position.X * floor.MaxRoomSize.X, southWestRoom.Position.Y * floor.MaxRoomSize.Y);
                    var endPoint = new IntVector2(southEastRoom.StartPoint.X - 1, Random.Range(southEastRoom.StartPoint.Y, southEastRoom.EndPoint.Y))
                        + new IntVector2(southEastRoom.Position.X * floor.MaxRoomSize.X, southEastRoom.Position.Y * floor.MaxRoomSize.Y);
                    connectPathVertically(startPoint, endPoint);
                }
            }

            if (position.X == floor.MaxRoomCount.X - 2)
            {
                if (haveEastPath)
                {
                    var startPoint = new IntVector2(Random.Range(northEastRoom.StartPoint.X, northEastRoom.EndPoint.X), northEastRoom.EndPoint.Y)
                    + new IntVector2(northEastRoom.Position.X * floor.MaxRoomSize.X, northEastRoom.Position.Y * floor.MaxRoomSize.Y);
                    var endPoint = new IntVector2(Random.Range(southEastRoom.StartPoint.X, southEastRoom.EndPoint.X), southEastRoom.StartPoint.Y - 1)
                        + new IntVector2(southEastRoom.Position.X * floor.MaxRoomSize.X, southEastRoom.Position.Y * floor.MaxRoomSize.Y);
                    connectPathHorizontally(startPoint, endPoint);
                }
            }
        }

        void connectPathHorizontally(IntVector2 startPoint, IntVector2 endPoint)
        {
            floor.SetCell(startPoint.X, startPoint.Y, PATH);
            floor.SetCell(endPoint.X, endPoint.Y, PATH);

            extendVertically(startPoint, endPoint);
            extendtHorizontally(startPoint, endPoint);            
        }

        void connectPathVertically(IntVector2 startPoint, IntVector2 endPoint)
        {
            floor.SetCell(startPoint.X, startPoint.Y, PATH);
            floor.SetCell(endPoint.X, endPoint.Y, PATH);

            extendtHorizontally(startPoint, endPoint);
            extendVertically(startPoint, endPoint);                   
        }

        void extendtHorizontally(IntVector2 startPoint, IntVector2 endPoint)
        {    
            var distance = endPoint - startPoint;

            if (distance.X > 0)
            {
                for (int i = 0; i < distance.X; i++)
                {
                    if (i % 2 == 0)
                    {
                        startPoint.X += 1;
                        floor.SetCell(startPoint.X, startPoint.Y, PATH);
                    }
                    if (i % 2 == 1)
                    {
                        endPoint.X -= 1;
                        floor.SetCell(endPoint.X, endPoint.Y, PATH);
                    }
                }
            }

            if (distance.X < 0)
            {
                for (int i = 0; i < -distance.X; i++)
                {
                    if (i % 2 == 0)
                    {
                        startPoint.X -= 1;
                        floor.SetCell(startPoint.X, startPoint.Y, PATH);
                    }
                    if (i % 2 == 1)
                    {
                        endPoint.X += 1;
                        floor.SetCell(endPoint.X, endPoint.Y, PATH);
                    }
                }
            }
        }

        void extendVertically(IntVector2 startPoint, IntVector2 endPoint)
        {
            var distance = endPoint - startPoint;

            if (distance.Y > 0)
            {
                for (int i = 0; i < distance.Y; i++)
                {
                    if (i % 2 == 0)
                    {
                        startPoint.Y += 1;
                        floor.SetCell(startPoint.X, startPoint.Y, PATH);
                    }
                    if (i % 2 == 1)
                    {
                        endPoint.Y -= 1;
                        floor.SetCell(endPoint.X, endPoint.Y, PATH);
                    }
                }
            }

            if (distance.Y < 0)
            {
                for (int i = 0; i < -distance.Y; i++)
                {
                    if (i % 2 == 0)
                    {
                        startPoint.Y -= 1;
                        floor.SetCell(startPoint.X, startPoint.Y, PATH);
                    }
                    if (i % 2 == 1)
                    {
                        endPoint.Y += 1;
                        floor.SetCell(endPoint.X, endPoint.Y, PATH);
                    }
                }
            }
        }
    }
}
