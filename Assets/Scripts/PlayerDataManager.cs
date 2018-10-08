using System.Collections.Generic;
using UnityEngine;

public class PlayerDataManager : SingletonMonoBehaviour<PlayerDataManager>
{
    //現在の日付・場所
    public int day;
    public int Day
    {
        set
        {
            day = value;
            FrameUIManager.Instance.UpdateDay();
        }
        get { return day; }
    }

    string currentPlace;
    public string CurrentPlace
    {
        set
        {
            currentPlace = value;
            FrameUIManager.Instance.UpdatePlaceText();
        }
        get { return currentPlace; }
    }

    //パラメーター（遅延取得OK?、LVごとの数値をデータベースで保存）
    public PlayerParameter Parameter;

    //スタミナ限界
    public int MaxStamina = 100;
    //スタミナ（探索時はMaxSPに変換される）
    public int stamina;
    public int Stamina
    {
        set
        {
            stamina = value;
            FrameUIManager.Instance.UpdateStamina();
        }
        get { return stamina; }
    }

    //手持ちの金
    int money;
    public int Money
    {
        set
        {
            money = value;
            FrameUIManager.Instance.UpdateMoneyText();
        }
        get { return money; }
    }

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
    public List<ItemBase> ItemStorage = new List<ItemBase>();

    new void Awake()
    {
        LoadManager.LoadPlayerDataAwake();
    }

    void Start()
    {
        LoadManager.LoadPlayerDataStart();
    }
}
