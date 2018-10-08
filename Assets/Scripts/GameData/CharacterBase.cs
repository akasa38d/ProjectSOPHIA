using MyUtility;

public abstract class CharacterBase
{
    public string Name;
    public int MaxHP;
    public int MaxSP;
    public int Atk;
    public int Def;
    public int Spd;
    public int Exp;
    public int NextExp;
}

//CharacterBaseの補足
public class PlayerBase : CharacterBase
{
    public int Level;
}

public class EnemyBase : CharacterBase
{
    public int ID;
    public int NormalDrop;
    public int NormalItemID;
    public int RareDrop;
    public int RareItemID;
    public int MoneyDrop;
    public int Money;

    public enum ActionType { Normal };
    public ActionType Act;

    public enum RankType { Common, FloorMaster, Boss, Runner };
    public RankType Rank;
}

public abstract class AbstractCharacterParameter
{
    protected CharacterBase characterBase;

    public string Name { get { return characterBase.Name; } }

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
}

public class PlayerParameter : AbstractCharacterParameter
{
    public int LV;

    public PlayerParameter()
    {
        loadPlayerData();
    }

    void loadPlayerData()
    {
        characterBase = new PlayerBase();
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
