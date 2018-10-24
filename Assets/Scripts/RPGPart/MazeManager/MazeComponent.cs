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
        public Vector3 ObjectPosition { get { return new Vector3(Position.X * 10, 0, Position.Y * 10); } }

        public virtual void SetSelfObject(GameObject gameObject)
        {
            SelfObject = gameObject;
            SelfObject.transform.position = ObjectPosition;
        }
    }
}