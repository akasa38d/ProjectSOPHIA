using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyUtility;

public class DungeonData
{
    public int ID;
    public string Name;
    public enum DungeonType { Field, UpBuild, DownBuild }
    public DungeonType Type { set; get; }

    public List<FloorData> FloorDataList;

    public class FloorData
    {
        public int FloorNum;
        public enum FloorType { RoomFloor, ReverseRoomFloor, DesignFloor }
        public FloorType Type;

        public IntVector2 RoomCount;
        public IntVector2 MaxRoomCell;
        public IntVector2 MinRoomCell;

        public int MaxEnemyNum;
        public int MinEnemyNum;
        /*
        public FloorEnemyData[] EnemyData = new FloorEnemyData[5];

        public class FloorEnemyData
        {
            public int ID;
            public int Probability;
        }
        */
    }
}
