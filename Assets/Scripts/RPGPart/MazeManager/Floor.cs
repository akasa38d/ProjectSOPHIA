using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyUtility;

public partial class MazeManager : SingletonMonoBehaviour<MazeManager>
{
    public abstract class AbstractFloor
    {
        Dungeon maze;

        public IntVector2 CellCount { private set; get; }
        public AbstractCell[,] Cells;

        public AbstractFloor() { }

        public void StartUpFloor() { }

        public virtual void CreateCells(int[,] intSquareArray) { }

        public AbstractCell GetRandomCell()
        {
            var cells = new List<AbstractCell>();
            foreach (var n in Cells) { if (n.IsFree) { cells.Add(n); } }
            return cells[Random.Range(0, cells.Count)];
        }
    }

    public class RoomFloor : AbstractFloor
    {
        public new IntVector2 CellCount { get { return new IntVector2(Cells.GetLength(0), Cells.GetLength(1)); } }

        public RoomFloor() { }

        public override void CreateCells(int[,] intSquareArray)
        {
            Cells = new AbstractCell[intSquareArray.GetLength(0), intSquareArray.GetLength(1)];
            for (int y = 0; y < Cells.GetLength(1); y++)
            {
                for (int x = 0; x < Cells.GetLength(0); x++)
                {
                    if (intSquareArray[x, y] == 0)
                    {
                        Cells[x, y] = new PlainCell(this, new IntVector2(x, y));
                        Cells[x, y].AcceptObject(new WallObject(Cells[x, y]));
                    }
                    if (intSquareArray[x, y] == 1)
                    {
                        Cells[x, y] = new PlainCell(this, new IntVector2(x, y), PrefabsManager.Instance.PlainCell);
                        Cells[x, y].InRoom = true;
                    }
                    if (intSquareArray[x, y] == 2)
                    {
                        Cells[x, y] = new PlainCell(this, new IntVector2(x, y), PrefabsManager.Instance.PlainCell);
                    }
                }
            }
        }
    }
}
