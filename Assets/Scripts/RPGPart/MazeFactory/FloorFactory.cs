using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using MyUtility;

public partial class MazeManager : SingletonMonoBehaviour<MazeManager>
{
    public abstract class AbstractFloorFactory
    {
        protected virtual IntVector2 maxSquareCount { get; }
        public abstract int GetSquare(int x, int y);

        public abstract int[,] CreateIntSquareArray();
    }

    public class RoomFloorFactory : AbstractFloorFactory
    {
        public IntVector2 MaxRoomSize { private set; get; }
        public IntVector2 MinRoomSize { private set; get; }
        public IntVector2 MaxRoomCount { private set; get; }

        protected override IntVector2 maxSquareCount { get { return new IntVector2(MaxRoomSize.X * MaxRoomCount.X, MaxRoomSize.Y * MaxRoomCount.Y); } }

        public AbstractRoomFactory[,] Rooms;
        List<PathFactory> pathFactories;

        public override int GetSquare(int x, int y)
        {
            int posX = x / MaxRoomSize.X;
            int posY = y / MaxRoomSize.Y;
            return Rooms[posX, posY].IntSquares[x % MaxRoomSize.X, y % MaxRoomSize.Y];
        }

        public void SetSquare(int x, int y, int value)
        {
            int posX = x / MaxRoomSize.X;
            int posY = y / MaxRoomSize.Y;
            Rooms[posX, posY].IntSquares[x % MaxRoomSize.X, y % MaxRoomSize.Y] = value;
        }

        public RoomFloorFactory(IntVector2 roomMax, IntVector2 roomMin, IntVector2 roomCount)
        {
            MaxRoomSize = new IntVector2(roomMax);
            MinRoomSize = new IntVector2(roomMin);
            MaxRoomCount = new IntVector2(roomCount);            
        }

        public override int[,] CreateIntSquareArray()
        {
            createPathMakers();
            createRooms();
            cutPath();
            createPathOnlyRoom();
            checkNullRoom();
            createPath();

            var intSquareArray = new int[maxSquareCount.X, maxSquareCount.Y];

            for (int y = 0; y < maxSquareCount.Y; y++)
            {
                for (int x = 0; x < maxSquareCount.X; x++)
                {
                    intSquareArray[x,y] = GetSquare(x, y);
                }
            }

            return intSquareArray;
        }

        void createPathMakers()
        {
            pathFactories = new List<PathFactory>();
            for (int y = 0; y < MaxRoomCount.Y - 1; y++)
            {
                for (int x = 0; x < MaxRoomCount.X - 1; x++)
                {
                    pathFactories.Add(new PathFactory(x, y, this));
                }
            }
        }

        void createRooms()
        {
            Rooms = new AbstractRoomFactory[MaxRoomCount.X, MaxRoomCount.Y];

            int randomNum = Random.Range(0, 10);

            for (int y = 0; y < MaxRoomCount.Y; y++)
            {
                for (int x = 0; x < MaxRoomCount.X; x++)
                {
                    if (randomNum < 9) { Rooms[x, y] = new PlainRoomFactory(x, y, this); }
                    else { Rooms[x, y] = new DonutRoomFactory(x, y, this); }
                }
            }
        }

        void cutPath()
        {
            foreach (var n in pathFactories) { n.CutRandomPath(); }
        }

        void createPathOnlyRoom()
        {
            var maxRoomCount = MaxRoomCount.X * MaxRoomCount.Y;
            var target = maxRoomCount / 4;

            for (int i = 0; i < target; i++)
            {
                var x = Random.Range(0, MaxRoomCount.X);
                var y = Random.Range(0, MaxRoomCount.Y);
                if (Rooms[x, y].PathCount > 1)
                {
                    Rooms[x, y] = new PathOnlyRoomFactory(x, y, this);
                }
            }
        }

        void checkNullRoom()
        {
            for (int y = 0; y < MaxRoomCount.Y; y++)
            {
                for (int x = 0; x < MaxRoomCount.X; x++)
                {
                    if (Rooms[x, y].PathCount == 0) { Rooms[x, y] = new NullRoomFactory(x, y, this); }
                }
            }
        }

        void createPath()
        {
            foreach (var n in pathFactories) { n.CreatePath(); }
        }

    }
}

