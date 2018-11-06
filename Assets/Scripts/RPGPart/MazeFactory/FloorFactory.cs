using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using MyUtility;

public partial class MazeManager : SingletonMonoBehaviour<MazeManager>
{
    public abstract class AbstractFloorFactory
    {
        protected virtual IntVector2 maxCellCount { get; }

        public abstract int GetCell(int x, int y);

        public abstract int[,] CreateIntCells();
    }

    public class RoomFloorFactory : AbstractFloorFactory
    {
        Dungeon maze;
        public IntVector2 MaxRoomSize { protected set; get; }
        public IntVector2 MinRoomSize { protected set; get; }
        public IntVector2 MaxRoomCount { protected set; get; }

        protected override IntVector2 maxCellCount { get { return new IntVector2(MaxRoomSize.X * MaxRoomCount.X, MaxRoomSize.Y * MaxRoomCount.Y); } }

        public AbstractRoomFactory[,] Rooms;
        List<PathFactory> pathFactories;

        public override int GetCell(int x, int y)
        {
            int posX = x / MaxRoomSize.X;
            int posY = y / MaxRoomSize.Y;
            return Rooms[posX, posY].IntCells[x % MaxRoomSize.X, y % MaxRoomSize.Y];
        }

        public void SetCell(int x, int y, int value)
        {
            int posX = x / MaxRoomSize.X;
            int posY = y / MaxRoomSize.Y;
            Rooms[posX, posY].IntCells[x % MaxRoomSize.X, y % MaxRoomSize.Y] = value;
        }

        public RoomFloorFactory(IntVector2 roomMax, IntVector2 roomMin, IntVector2 roomCount)
        {
            MaxRoomSize = new IntVector2(roomMax);
            MinRoomSize = new IntVector2(roomMin);
            MaxRoomCount = new IntVector2(roomCount);
        }

        public override int[,] CreateIntCells()
        {
            createPathMakers();
            createRooms();
            cutPath();
            createPathOnlyRoom();
            checkNullRoom();
            createPath();

            var intCellArray = new int[maxCellCount.X, maxCellCount.Y];

            for (int y = 0; y < maxCellCount.Y; y++)
            {
                for (int x = 0; x < maxCellCount.X; x++)
                {
                    intCellArray[x, y] = GetCell(x, y);
                }
            }

            return intCellArray;
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

        protected virtual void createRooms()
        {
            Rooms = new AbstractRoomFactory[MaxRoomCount.X, MaxRoomCount.Y];            

            for (int y = 0; y < MaxRoomCount.Y; y++)
            {
                for (int x = 0; x < MaxRoomCount.X; x++)
                {
                    int randomNum = Random.Range(0, 10);
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

    public class ReverseRoomFloorFactory : RoomFloorFactory
    {
        public ReverseRoomFloorFactory(IntVector2 roomMax, IntVector2 roomMin, IntVector2 roomCount) : base(roomMax, roomMin, roomCount) { }

        public override int[,] CreateIntCells()
        {
            createRooms();

            var intCells = new int[maxCellCount.X, maxCellCount.Y];

            for (int y = 0; y < maxCellCount.Y; y++)
            {
                for (int x = 0; x < maxCellCount.X; x++)
                {
                    intCells[x, y] = GetCell(x, y);
                }
            }

            reverseIntCells(intCells);

            return intCells;
        }

        protected override void createRooms()
        {
            Rooms = new AbstractRoomFactory[MaxRoomCount.X, MaxRoomCount.Y];

            for (int y = 0; y < MaxRoomCount.Y; y++)
            {
                for (int x = 0; x < MaxRoomCount.X; x++)
                {
                    Rooms[x, y] = new PlainRoomFactory(x, y, this);
                }
            }
        }

        void reverseIntCells(int[,] cells)
        {
            for (int y = 0; y < cells.GetLength(1); y++)
            {
                for (int x = 0; x < cells.GetLength(0); x++)
                {
                    if (cells[x, y] == 0) { cells[x, y] = 1; }
                    if (cells[x, y] == 1) { cells[x, y] = 0; }
                }
            }
        }
    }

    public class DesignFloorFactory { }
}

