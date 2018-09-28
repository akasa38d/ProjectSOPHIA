using System.Collections.Generic;

public class PlayerDataManager : SingletonMonoBehaviour<PlayerDataManager>
{
    //現在の日付・場所
    public DateData currentDate;
    public string CurrentPlace;

    //パラメーター（遅延取得OK?、LVごとの数値をデータベースで保存）
    public PlayerParameter Parameter;    

    //スタミナ限界
    public int MaxStamina = 100;
    //スタミナ（探索時はMaxSPに変換される）
    public int Stamina;

    //手持ちの金
    public int Money;
    //貯金
    public int MoneySaving;

    //手持アイテム限界（成長有り）
    public int ItemsSize = 30;
    //手持ちのアイテム（仮）
    public List<ItemBase> Items = new List<ItemBase>();
    //装備（武器、防具、アクセサリー）

    //保管庫アイテム限界（成長有り）
    public int ItemStorageSize = 120;
    //保管庫のアイテム（仮）
    public ItemBase[] ItemStorage = new ItemBase[180];

    new void Awake()
    {
        LoadManager.LoadPlayerDataAwake();
    }

    void Start()
    {
        LoadManager.LoadPlayerDataStart();
    }
}
