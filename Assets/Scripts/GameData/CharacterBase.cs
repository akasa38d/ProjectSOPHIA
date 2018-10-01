using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyUtility;

public class CharacterBase
{
    public string Name;
    public int Level;
    public int MaxHP;
    public int MaxSP;
    public int Atk;
    public int Def;
    public int Spd;
    public int Exp;
    public int Money;
}

//CharacterBaseの補足
public class PlayerBase
{
    //装備とか？
}

public class EnemyBase
{
    public enum ActionType { Normal };
    public ActionType Act;
}

public abstract class AbstractCharacterParameter
{
    protected CharacterBase characterBase;

    public string Name { get { return characterBase.Name; } }

    public int Level { get { return characterBase.Level; } }

    public int MaxHP { get { return characterBase.MaxHP; } }
    int hp;
    public int HP
    {
        get { return hp; }
        set
        {
            hp = value;
            hp = Util.Range(hp, 0, MaxHP);
        }
    }

    public int MaxSP
    {
        get { return characterBase.MaxSP; }
        set { characterBase.MaxSP = value; }
    }
    int sp;
    public int SP
    {
        get { return sp; }
        set
        {
            sp = value;
            sp = Util.Range(sp, 0, MaxSP);
        }
    }

    public int Atk { get { return characterBase.Atk; } }
    public int Def { get { return characterBase.Def; } }
    public int Spd { get { return characterBase.Spd; } }

    public int Exp { get { return characterBase.Exp; } }
    public int Money { get { return characterBase.Money; } }
}

public class PlayerParameter : AbstractCharacterParameter
{
    public PlayerParameter()
    {
        loadPlayerData();
    }

    void loadPlayerData()
    {
        characterBase = new CharacterBase();
        characterBase.MaxSP = 50;
    }

    //CharacterBaseの能力値＋装備で能力を決定
}

public class EnemyParameter : AbstractCharacterParameter
{
    public EnemyParameter()
    {
        loadEnemyData();
    }

    void loadEnemyData() { }
}
