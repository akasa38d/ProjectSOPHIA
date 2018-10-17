using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyUtility;

public class MazeBuilder : SingletonMonoBehaviour<MazeBuilder>
{
    public void Start()
    {
        Maze maze = new Maze();
    }

    void createMaze()
    {

    }


    public class Maze
    {
        Floor floor;

        public Maze()
        {
            var maxRoomSize = new IntVector2(6, 6);
            var minRoomSize = new IntVector2(2, 2);
            var roomCount = new IntVector2(3, 2);
            floor = new Floor(maxRoomSize, minRoomSize, roomCount);
        }

        #region Floor

        public class Floor
        {
            public IntVector2 MaxRoomSize { private set; get; }
            public IntVector2 MinRoomSize { private set; get; }
            IntVector2 maxRoomCount;
            IntVector2 maxSquareCount { get { return new IntVector2(MaxRoomSize.X * maxRoomCount.X, MaxRoomSize.Y * maxRoomCount.Y); } }

            public AbstractRoom[,] Rooms;

            int getSquare(int x, int y)
            {
                int posX = x / MaxRoomSize.X;
                int posY = y / MaxRoomSize.Y;
                return Rooms[posX, posY].Squares[x % MaxRoomSize.X, y % MaxRoomSize.Y];
            }

            public Floor(IntVector2 roomMax, IntVector2 roomMin, IntVector2 roomCount)
            {
                MaxRoomSize = new IntVector2(roomMax.X, roomMax.Y);
                MinRoomSize = new IntVector2(roomMin.X, roomMin.Y);
                maxRoomCount = new IntVector2(roomCount.X, roomCount.Y);

                createRooms();

                createTest();
            }

            void createRooms()
            {
                Rooms = new AbstractRoom[maxRoomCount.X, maxRoomCount.Y];
                for (int y = 0; y < maxRoomCount.Y; y++)
                {
                    for (int x = 0; x < maxRoomCount.X; x++)
                    {
                        Rooms[x, y] = new PlainRoom(x, y, this);
                    }
                }
 //               Rooms[1, 1] = new PathRoom(1, 1, this);
            }

            void createTest()
            {
                for (int y = 0; y < maxSquareCount.Y; y++)
                {
                    for (int x = 0; x < maxSquareCount.X; x++)
                    {
                        if(getSquare(x,y) == 1)
                        {
                            var obj = Instantiate(PrefabsManager.Instance.PlaneSquare);
                            obj.transform.position = new Vector3(x * 10, 0, y * 10);
                        }
                    }
                }
            }
        }

        #endregion

        #region Room

        public abstract class AbstractRoom
        {
            public IntVector2 Position;
            protected Floor floor;

            protected IntVector2 maxSize { get { return floor.MaxRoomSize; } }
            protected IntVector2 minSize { get { return floor.MinRoomSize; } }

            protected IntVector2 startPoint;
            protected IntVector2 endPoint;

            public int[,] Squares;
            protected bool inRoom(int x, int y)
            {
                if (x >= startPoint.X && x < endPoint.X)
                {
                    if (y >= startPoint.Y && y < endPoint.Y)
                    {
                        return true;
                    }
                }
                return false;
            }

            public AbstractRoom(int posX, int posY, Floor _floor)
            {
                Position = new IntVector2(posX, posY);
                floor = _floor;

                setStartToEnd();
                createSquare();
            }

            protected virtual void setStartToEnd()
            {
                startPoint = new IntVector2(Random.Range(0, maxSize.X - minSize.X), Random.Range(0, maxSize.Y - minSize.Y));
                endPoint = new IntVector2(Random.Range(startPoint.X + minSize.X, maxSize.X), Random.Range(startPoint.Y + minSize.Y, maxSize.Y));
            }

            protected virtual void createSquare()
            {
                Squares = new int[maxSize.X, maxSize.Y];
                for (int y = 0; y < Squares.GetLength(1); y++)
                {
                    for (int x = 0; x < Squares.GetLength(0); x++)
                    {
                        Squares[x, y] = 0;
                        if (inRoom(x, y))
                        {
                            Squares[x, y] = 1;
                        }
                    }
                }
            }
        }

        public class PlainRoom : AbstractRoom
        {
            public PlainRoom(int PosX, int PosY, Floor _floor) : base(PosX, PosY, _floor) { }
        }

        public class PathRoom : AbstractRoom
        {
            public PathRoom(int PosX, int PosY, Floor _floor) : base(PosX, PosY, _floor) { }

            protected override void setStartToEnd()
            {
                startPoint = new IntVector2(Random.Range(0, maxSize.X - minSize.X), Random.Range(0, maxSize.Y - minSize.Y));
                endPoint = new IntVector2(startPoint.X + 1, startPoint.Y + 1);
            }
        }

        #endregion

        #region Square

        public abstract class AbstractSquare
        {
            public bool CanEnter;

            AbstractRoom room;

            public AbstractSquare()
            {

            }
        }

        public class WallSquare : AbstractSquare
        {
            public new bool CanEnter = false;
        }

        public class PlaneSquare : AbstractSquare
        {
            public new bool CanEnter = true;
        }

        #endregion
    }
}
