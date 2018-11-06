using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : SingletonMonoBehaviour<TurnManager>
{    
    public List<MazeManager.AbstractCharacter> CharacterList { private set; get; }

    public TurnManager() { CharacterList = new List<MazeManager.AbstractCharacter>(); }

    public void AddCharacter(MazeManager.AbstractCharacter character)
    {
        CharacterList.Add(character);
    }
    public void RemoveCharacter(int num)
    {
        CharacterList.RemoveAt(num);
    }

    public void CharacterControl(int num)
    {

    }
}
