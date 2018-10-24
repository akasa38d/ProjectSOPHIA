using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MazeManager : SingletonMonoBehaviour<MazeManager>
{
    Maze maze;

    public int CurrentFloor = 0;

    public void Start()
    {
        maze = new Maze();
        CreateFloor(CurrentFloor);
    }

    public void CreateFloor(int floorNum)
    {
        maze.CreateFloor(floorNum);
    }

    public void InstantiateSquare(AbstractSquare square, GameObject gameObject)
    {
        var gameObj = Instantiate(gameObject);
        square.SetSelfObject(gameObj);
    }

    public void InstantiateObject(AbstractObject _object, GameObject gameObject)
    {
        var gameObj = Instantiate(gameObject);
        _object.SetSelfObject(gameObj);
    }    
}
