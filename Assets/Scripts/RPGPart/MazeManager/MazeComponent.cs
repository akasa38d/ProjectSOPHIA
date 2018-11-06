using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyUtility;

public partial class MazeManager : SingletonMonoBehaviour<MazeManager>
{
    public abstract class MazeComponent
    {
        public GameObject SelfObject { set; get; }

        public IntVector2 Position;
        public Vector3 ObjectPosition
        {
            get
            {
                if (Position == null) { return new Vector3(-10, -10, -10); }
                return new Vector3(Position.X * 10, 0, Position.Y * 10);
            }
        }

        public void Activate(GameObject prefab)
        {
            SelfObject = Instantiate(prefab);
            SelfObject.transform.position = ObjectPosition;
        }
    }
}