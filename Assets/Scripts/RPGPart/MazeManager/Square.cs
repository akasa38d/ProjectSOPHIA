using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyUtility;

public partial class MazeManager : SingletonMonoBehaviour<MazeManager>
{
    public abstract class AbstractSquare : MazeComponent
    {
        #region Field

        public AbstractFloor Floor { private set; get; }

        public bool CanEnter
        {
            get
            {
                if (ExistObject) { if (Object.CanEnter == false) return false; }
                if (ExistCharacter) { return false; }
                return true;
            }
        }

        public AbstractObject Object;
        public AbstractCharacter Character;

        public bool ExistObject { get { return Object != null; } }
        public bool ExistCharacter { get { return Character != null; } }

        #endregion

        #region Constructor

        public AbstractSquare(AbstractFloor floor, IntVector2 position)
        {
            Floor = floor;
            Position = position;
        }

        #endregion

        #region Method

        public override void SetSelfObject(GameObject gameObject)
        {
            SelfObject = gameObject;
            SelfObject.transform.position = ObjectPosition;
        }

        public void AcceptObject(AbstractObject obj) { Object = obj; }

        public void RemoveObject() { Object = null; }

        public void AcceptCharacter(AbstractCharacter character)
        {
            Character = character;
            //Trapの発動、アイテムを拾うなど
        }

        public void RemoveCharacter() { Character = null; }

        #endregion
    }

    public class PlainSquare : AbstractSquare
    {
        public PlainSquare(AbstractFloor floor, IntVector2 position) : base(floor, position) { }
    }
}