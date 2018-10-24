using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyUtility;

public partial class MazeManager : SingletonMonoBehaviour<MazeManager>
{
    public class AbstractObject : MazeComponent
    {
        public AbstractSquare Square;

        public bool CanEnter { get; }
        public bool CanBreak { get; }

        public void SettingSquare(AbstractSquare square) { Square = square; }

        public void EnterAction() { }
        public void BreakAction() { }
        public virtual void Break() { }
    }

    public class WallObject : AbstractObject
    {
        public new bool CanEnter { get { return false; } }
        public new bool CanBreak { get { return true; } }

        public WallObject(GameObject gameObject, IntVector2 position)
        {
            Position = position;
            SetSelfObject(gameObject);
        }
    }

    public class ItemBoxObject : AbstractObject
    {
        public new bool CanEnter { get { return false; } }
        public new bool CanBreak { get { return true; } }

        GameObject gameObject;
        public ItemBoxObject(GameObject _gameObject)
        {
            gameObject = _gameObject;
        }
    }

    public class StairsObject : AbstractObject
    {
        public new bool CanEnter { get { return true; } }
        public new bool CanBreak { get { return false; } }

        GameObject gameObject;
        public StairsObject(GameObject _gameObject)
        {
            gameObject = _gameObject;
        }
    }

    public class GoalObject : AbstractObject
    {
        public new bool CanEnter { get { return true; } }
        public new bool CanBreak { get { return false; } }

        GameObject gameObject;
        public GoalObject(GameObject _gameObject)
        {
            gameObject = _gameObject;
        }
    }
}
