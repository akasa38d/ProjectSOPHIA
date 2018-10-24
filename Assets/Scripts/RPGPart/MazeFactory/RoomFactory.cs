using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using MyUtility;

public partial class MazeManager : SingletonMonoBehaviour<MazeManager>
{
    public abstract class AbstractRoomFactory
    {
        public IntVector2 Position;
        protected RoomFloorFactory floor;

        public IntVector2 StartPoint;
        public IntVector2 EndPoint;

        protected IntVector2 maxSize { get { return floor.MaxRoomSize; } }
        protected IntVector2 minSize { get { return floor.MinRoomSize; } }

        protected const int SPACE = 1;

        public bool HaveNorthRoom = true;
        public bool HaveWestRoom = true;
        public bool HaveSouthRoom = true;
        public bool HaveEastRoom = true;

        public int PathCount
        {
            get
            {
                int value = 0;
                if (HaveNorthRoom) { value++; }
                if (HaveWestRoom) { value++; }
                if (HaveSouthRoom) { value++; }
                if (HaveEastRoom) { value++; }
                return value;
            }
        }

        public int[,] IntSquares;

        public AbstractRoomFactory() { }

        public AbstractRoomFactory(int posX, int posY, RoomFloorFactory _floor)
        {
            Position = new IntVector2(posX, posY);
            floor = _floor;

            createRoom();
        }

        protected virtual void createRoom()
        {
            cutEndOfFloor();
            setStartToEnd();
            createSquare();
        }

        protected void cutEndOfFloor()
        {
            if (Position.X == 0) { HaveWestRoom = false; }
            if (Position.Y == 0) { HaveNorthRoom = false; }
            if (Position.X == floor.MaxRoomCount.X - 1) { HaveEastRoom = false; }
            if (Position.Y == floor.MaxRoomCount.Y - 1) { HaveSouthRoom = false; }
        }

        protected virtual void setStartToEnd()
        {
            StartPoint = new IntVector2(Random.Range(SPACE, maxSize.X - minSize.X - SPACE), Random.Range(SPACE, maxSize.Y - minSize.Y - SPACE));
            EndPoint = new IntVector2(Random.Range(StartPoint.X + minSize.X, maxSize.X - SPACE), Random.Range(StartPoint.Y + minSize.Y, maxSize.Y - SPACE));
        }

        protected virtual void createSquare()
        {
            IntSquares = new int[maxSize.X, maxSize.Y];
            for (int y = 0; y < IntSquares.GetLength(1); y++)
            {
                for (int x = 0; x < IntSquares.GetLength(0); x++)
                {
                    if (inRoom(x, y)) { IntSquares[x, y] = 1; }
                }
            }
        }

        protected bool inRoom(int x, int y)
        {
            if (x >= StartPoint.X && x < EndPoint.X)
            {
                if (y >= StartPoint.Y && y < EndPoint.Y)
                {
                    return true;
                }
            }
            return false;
        }
    }

    public class PlainRoomFactory : AbstractRoomFactory
    {
        public PlainRoomFactory(int PosX, int PosY, RoomFloorFactory _floor) : base(PosX, PosY, _floor) { }
    }

    public class DonutRoomFactory : AbstractRoomFactory
    {
        IntVector2 roomSize { get { return EndPoint - StartPoint; } }

        public DonutRoomFactory(int PosX, int PosY, RoomFloorFactory _floor) : base(PosX, PosY, _floor) { }

        protected override void createRoom()
        {
            cutEndOfFloor();
            setStartToEnd();
            createSquare();
            createHole();
        }

        void createHole()
        {
            int width = Random.Range(2, 3);

            var start = StartPoint + new IntVector2(width, width);
            var end = EndPoint - new IntVector2(width, width);

            if (end.X > start.X && end.Y > start.Y)
            {
                for (int y = 0; y < IntSquares.GetLength(1); y++)
                {
                    for (int x = 0; x < IntSquares.GetLength(0); x++)
                    {
                        if (x >= start.X && x < end.X)
                        {
                            if (y >= start.Y && y < end.Y)
                            {
                                IntSquares[x, y] = 0;
                            }
                        }
                    }
                }
            }
        }
    }

    public class PathOnlyRoomFactory : AbstractRoomFactory
    {
        public PathOnlyRoomFactory(int PosX, int PosY, RoomFloorFactory _floor) : base(PosX, PosY, _floor) { }

        protected override void setStartToEnd()
        {
            StartPoint = new IntVector2(Random.Range(SPACE, maxSize.X - minSize.X - SPACE), Random.Range(SPACE, maxSize.Y - minSize.Y - SPACE));
            EndPoint = new IntVector2(StartPoint.X + 1, StartPoint.Y + 1);
        }
    }

    public class NullRoomFactory : AbstractRoomFactory
    {
        public new bool HaveNorthRoom = false;
        public new bool HaveWestRoom = false;
        public new bool HaveSouthRoom = false;
        public new bool HaveEastRoom = false;

        public NullRoomFactory(int PosX, int PosY, RoomFloorFactory _floor) : base(PosX, PosY, _floor) { }

        protected override void createSquare()
        {
            IntSquares = new int[maxSize.X, maxSize.Y];
        }
    }
}
