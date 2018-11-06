using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using MyUtility;

public partial class MazeManager : SingletonMonoBehaviour<MazeManager>
{
    public class Dungeon
    {
        DungeonData dungeonData;
        AbstractFloor currentFloor;

        public PlayerCharacter Player;
        //        public List<EnemyCharacter> Enemies;

        public Dungeon()
        {
            LoadMazeData();
        }

        public void LoadMazeData()
        {
            dungeonData = new DungeonData();
            dungeonData.ID = 1;
            dungeonData.Name = "test";
            dungeonData.Type = DungeonData.DungeonType.Field;

            dungeonData.FloorDataList = DataBaseManager.Instance.DungeonFloorDataTable;
        }


        public void CreateFloor(int num)
        {
            var maxRoomSize = dungeonData.FloorDataList[num].MaxRoomCell;
            var minRoomSize = dungeonData.FloorDataList[num].MinRoomCell;
            var roomCount = dungeonData.FloorDataList[num].RoomCount;
            var floorFactory = new RoomFloorFactory(maxRoomSize, minRoomSize, roomCount);
            currentFloor = new RoomFloor();
            currentFloor.CreateCells(floorFactory.CreateIntCells());
        }

        public void PutStairs()
        {
            if (dungeonData.Type != DungeonData.DungeonType.Field)
            {
                var cell = currentFloor.GetRandomCell();
                cell.AcceptObject(new StairsObject(cell));
            }

            if (dungeonData.Type == DungeonData.DungeonType.Field)
            {
                var direction = Random.Range(0, 4);
                var cellList = new List<AbstractCell>();
                foreach (var n in currentFloor.Cells) { if (n.IsFree && n.InRoom) cellList.Add(n); }

                if (direction == 0)
                {
                    var value = cellList.Select(r => r.Position.Y).Min();
                    var cells = cellList.Where(n => n.Position.Y == value).ToList();
                    var cell = cells[Random.Range(0, cells.Count)];
                    var position = new IntVector2(cell.Position.X, cell.Position.Y - 1);
                    currentFloor.Cells[position.X, position.Y].Activate(PrefabsManager.Instance.PlainCell);
                    currentFloor.Cells[position.X, position.Y].AcceptObject(new StairsObject(currentFloor.Cells[position.X, position.Y]));
                }
                if (direction == 1)
                {
                    var value = cellList.Select(r => r.Position.X).Max();
                    var cells = cellList.Where(n => n.Position.X == value).ToList();
                    var cell = cells[Random.Range(0, cells.Count)];
                    var position = new IntVector2(cell.Position.X + 1, cell.Position.Y);
                    currentFloor.Cells[position.X, position.Y].Activate(PrefabsManager.Instance.PlainCell);
                    currentFloor.Cells[position.X, position.Y].AcceptObject(new StairsObject(currentFloor.Cells[position.X, position.Y]));
                }
                if (direction == 2)
                {
                    var value = cellList.Select(r => r.Position.Y).Max();
                    var cells = cellList.Where(n => n.Position.Y == value).ToList();
                    var cell = cells[Random.Range(0, cells.Count)];
                    var position = new IntVector2(cell.Position.X, cell.Position.Y + 1);
                    currentFloor.Cells[position.X, position.Y].Activate(PrefabsManager.Instance.PlainCell);
                    currentFloor.Cells[position.X, position.Y].AcceptObject(new StairsObject(currentFloor.Cells[position.X, position.Y]));
                }
                if (direction == 3)
                {
                    var value = cellList.Select(r => r.Position.X).Min();
                    var cells = cellList.Where(n => n.Position.X == value).ToList();
                    var cell = cells[Random.Range(0, cells.Count)];
                    var position = new IntVector2(cell.Position.X - 1, cell.Position.Y);
                    currentFloor.Cells[position.X, position.Y].Activate(PrefabsManager.Instance.PlainCell);
                    currentFloor.Cells[position.X, position.Y].AcceptObject(new StairsObject(currentFloor.Cells[position.X, position.Y]));
                }
            }
        }

        public void PutPlayer()
        {
            Player.Move(currentFloor.GetRandomCell());
        }
    }
}
