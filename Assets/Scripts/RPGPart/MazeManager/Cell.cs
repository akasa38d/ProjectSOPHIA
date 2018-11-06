using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyUtility;

public partial class MazeManager : SingletonMonoBehaviour<MazeManager>
{
    public abstract class AbstractCell : MazeComponent
    {
        #region Field

        public AbstractFloor Floor { private set; get; }

        public bool InRoom = false;

        public bool CanEnter
        {
            get
            {
                if (ExistObject) { if (Object.CanEnter == false) return false; }
                if (ExistCharacter) { return false; }
                return true;
            }
        }

        public bool IsFree
        {
            get
            {
                if (ExistObject) { return false; }
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

        public AbstractCell(AbstractFloor floor, IntVector2 position)
        {
            Floor = floor;
            Position = position;
        }

        #endregion

        #region Method

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

    public class PlainCell : AbstractCell
    {
        public PlainCell(AbstractFloor floor, IntVector2 position) : base(floor, position) { }

        public PlainCell(AbstractFloor floor, IntVector2 position, GameObject prefab) : base(floor, position)
        {
            Activate(prefab);
        }        
    }
}