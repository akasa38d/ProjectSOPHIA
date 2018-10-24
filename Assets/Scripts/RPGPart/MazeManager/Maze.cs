using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyUtility;

public partial class MazeManager : SingletonMonoBehaviour<MazeManager>
{
    public class Maze
    {
        public List<AbstractFloorFactory> FloorFactories;
        AbstractFloor currentFloor;
                
        //読み込んだデータ

        public Maze()
        {
            FloorFactories = new List<AbstractFloorFactory>();

            //test
            LoadMazeData();
        }

        public void LoadMazeData()
        {
            var maxRoomSize = new IntVector2(16, 16);
            var minRoomSize = new IntVector2(6, 6);
            var roomCount = new IntVector2(3, 2);
            FloorFactories.Add(new RoomFloorFactory(maxRoomSize, minRoomSize, roomCount));
        }

        public void CreateFloor(int num)
        {
            currentFloor = new RoomFloor();
            currentFloor.CreateSquareArray(FloorFactories[num].CreateIntSquareArray());
        }
        
    }
}
