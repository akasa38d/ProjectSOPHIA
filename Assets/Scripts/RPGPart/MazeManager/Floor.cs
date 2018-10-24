using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyUtility;

public partial class MazeManager : SingletonMonoBehaviour<MazeManager>
{
    public abstract class AbstractFloor
    {
        public IntVector2 SquareCount { private set; get; }
        public AbstractSquare[,] SquareArray;

        public PlayerCharacter player;
        public List<EnemyCharacter> Enemies;

        public AbstractFloor() { }

        public void StartUpFloor() { }

        public virtual void CreateSquareArray(int[,] intSquareArray) { }
        public virtual void SetPlayer() { }
        public virtual void SetEnemies() { }
    }

    public class RoomFloor : AbstractFloor
    {
        public RoomFloor() { }

        public override void CreateSquareArray(int[,] intSquareArray)
        {
            SquareArray = new AbstractSquare[intSquareArray.GetLength(0), intSquareArray.GetLength(1)];
            for (int y = 0; y < SquareArray.GetLength(1); y++)
            {
                for (int x = 0; x < SquareArray.GetLength(0); x++)
                {
                    if (intSquareArray[x, y] == 1)
                    {
                        SquareArray[x, y] = new PlainSquare(this, new IntVector2(x, y));
                        Instance.InstantiateSquare(SquareArray[x, y], PrefabsManager.Instance.PlaneSquare);
                    }
                }
            }
        }
    }
}
