using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyUtility;

public partial class MazeManager : SingletonMonoBehaviour<MazeManager>
{
    public abstract class AbstractCharacter : MazeComponent
    {
        protected AbstractFloor Floor;

        public AbstractCell CurrentCell;
        public new IntVector2 Position { get { return CurrentCell.Position; } }

        public AbstractCharacter(GameObject gameObject)
        {
            SelfObject = gameObject;
        }

        public void RegisterTurnManager()
        {
            TurnManager.Instance.AddCharacter(this);
        }

        public void EntryFloor(AbstractFloor floor) { Floor = floor; }

        public void ExitFloor() { Floor = null; }

        public void TurnStart() { }

        public void Control() { }

        public void Attack(PlayerCharacter player)
        {
            TurnEnd();
        }

        public void Attack(EnemyCharacter enemy)
        {
            TurnEnd();
        }

        public void Move(AbstractCell square)
        {
            CurrentCell?.RemoveCharacter();
            CurrentCell = square;
            SelfObject.transform.position = CurrentCell.ObjectPosition;
            CurrentCell.AcceptCharacter(this);
            TurnEnd();
        }

        bool checkCellEnter(IntVector2 targetPosition)
        {
            var target = Position + targetPosition;
            if (target.X < 0 || target.X >= Floor.CellCount.X) { return false; }
            if (target.Y < 0 || target.Y >= Floor.CellCount.Y) { return false; }
            if (Floor.Cells[target.X, target.Y].CanEnter == false) { return false; }
            if (target.X != 0 && target.Y != 0)
            {
                if (Floor.Cells[target.X, Position.Y].CanEnter == false) { return false; }
                if (Floor.Cells[Position.X, target.Y].CanEnter == false) { return false; }
            }
            return true;
        }

        public void Wait()
        {
            TurnEnd();
        }

        public void TurnEnd() { }

        public void Break() { }
    }

    public class PlayerCharacter : AbstractCharacter
    {
        public PlayerCharacter(GameObject prefab) : base(prefab) { }
    }

    public class EnemyCharacter : AbstractCharacter
    {
        public EnemyCharacter(GameObject prefab) : base(prefab) { }
    }

}
