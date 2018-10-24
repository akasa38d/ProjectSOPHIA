using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyUtility;

public partial class MazeManager : SingletonMonoBehaviour<MazeManager>
{
    public abstract class AbstractCharacter
    {
        protected AbstractFloor Floor;
        public IntVector2 Position { get; }
        public AbstractSquare CurrentSquare;

        public GameObject SelfObject;

        public AbstractCharacter(GameObject gameObject)
        {
            SelfObject = gameObject;
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

        public void Move(AbstractSquare square)
        {
            CurrentSquare.RemoveCharacter();
            CurrentSquare = square;
            SelfObject.transform.position = CurrentSquare.ObjectPosition;
            CurrentSquare.AcceptCharacter(this);
            TurnEnd();
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
        public PlayerCharacter(GameObject characterObj) : base(characterObj) { }
    }

    public class EnemyCharacter : AbstractCharacter
    {
        public EnemyCharacter(GameObject characterObj) : base(characterObj) { }
    }


}
