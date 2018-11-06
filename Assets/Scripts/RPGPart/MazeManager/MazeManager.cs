using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MazeManager : SingletonMonoBehaviour<MazeManager>
{
    Dungeon dungeon;

    public int CurrentFloor = 0;

    public void Start()
    {
        dungeon = new Dungeon();
        CreateFloor(CurrentFloor);
        instantiateStairs();
        instantiatePlayer();
        putPlayer();
    }

    public void CreateFloor(int floorNum)
    {
        dungeon.CreateFloor(floorNum);
    }

    void instantiateStairs()
    {
        dungeon.PutStairs();
    }

    void instantiatePlayer()
    {
        var gameObj = Instantiate(PrefabsManager.Instance.PlayerPrefab);
        dungeon.Player = new PlayerCharacter(gameObj);
    }
    void putPlayer()
    {
        dungeon.PutPlayer();
    }
}
