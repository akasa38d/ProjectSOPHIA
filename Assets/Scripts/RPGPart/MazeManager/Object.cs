using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyUtility;

public partial class MazeManager : SingletonMonoBehaviour<MazeManager>
{
    public class AbstractObject : MazeComponent
    {
        public AbstractCell CurrentCell;
        public new IntVector2 Position { get { return CurrentCell?.Position; } }

        public bool CanEnter { get; }
        public bool CanBreak { get; }

        public AbstractObject(AbstractCell cell) { CurrentCell = cell; }

        public void EnterAction() { }
        public void BreakAction() { }
        public virtual void Break() { }
    }

    public class WallObject : AbstractObject
    {
        public new bool CanEnter { get { return false; } }
        public new bool CanBreak { get { return true; } }

        public WallObject(AbstractCell cell) : base(cell) { }
    }

    public class ItemBoxObject : AbstractObject
    {
        public new bool CanEnter { get { return false; } }
        public new bool CanBreak { get { return true; } }

        public ItemBoxObject(AbstractCell cell) : base(cell) { }
    }

    public class StairsObject : AbstractObject
    {
        public new bool CanEnter { get { return true; } }
        public new bool CanBreak { get { return false; } }

        public StairsObject(AbstractCell cell) : base(cell) { }
    }

    public class GoalObject : AbstractObject
    {
        public new bool CanEnter { get { return true; } }
        public new bool CanBreak { get { return false; } }

        public GoalObject(AbstractCell cell) : base(cell) { }

    }
}
