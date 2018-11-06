using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using MyUtility;

public class DataBaseManager : SingletonMonoBehaviour<DataBaseManager>
{
    public List<PlayerBase> PlayerBaseTable = new List<PlayerBase>();
    public List<EnemyBase> EnemyBaseTable = new List<EnemyBase>();
    public List<ItemBase> ItemBaseTable = new List<ItemBase>();
    public List<ItemProperty> ItemPropertyTable = new List<ItemProperty>();
    public List<RunnerBase> RunnerBaseTable = new List<RunnerBase>();

    public List<DungeonData.FloorData> DungeonFloorDataTable = new List<DungeonData.FloorData>();

    public new void Awake()
    {
        loadPlayerBase();
        loadEnemyBase();

        loadItemBase();
        //LoadItemProperty();

        loadRunnerBase();
        LoadDungeonFloorData();

    }

    #region Player

    void loadPlayerBase()
    {
        var tempTable = (TempPlayerBase)Resources.Load("Data/PlayerBaseTable");
        for (int i = 0; i < tempTable.sheets[0].list.Count; i++)
        {
            var n = tempTable.sheets[0].list[i];
            var playerBase = new PlayerBase();
            convertToPlayerBase(n, playerBase);
            PlayerBaseTable.Add(playerBase);
        }
    }

    void convertToPlayerBase(TempPlayerBase.Param n, PlayerBase playerBase)
    {
        playerBase.Level = n.LV;
        playerBase.Name = "ソフィア";
        playerBase.MaxHP = n.MaxHP;
        playerBase.MaxSP = 0;
        playerBase.Atk = n.Atk;
        playerBase.Def = n.Def;
        playerBase.Spd = 2;
        playerBase.Exp = 0;
        playerBase.NextExp = n.NextExp;
    }

    public PlayerBase getPlayerBase(int level)
    {
        var playerBaseTable = PlayerBaseTable.Where(n => n.Level == level);
        return playerBaseTable?.First();
    }

    #endregion

    #region Enemy

    void loadEnemyBase()
    {
        var tempTable = (TempEnemyBase)Resources.Load("Data/EnemyBaseTable");
        for (int i = 0; i < tempTable.sheets[0].list.Count; i++)
        {
            var n = tempTable.sheets[0].list[i];
            var enemyBase = new EnemyBase();
            convertToEnemyBase(n, enemyBase);
            EnemyBaseTable.Add(enemyBase);
        }
    }

    void convertToEnemyBase(TempEnemyBase.Param n, EnemyBase enemyBase)
    {
        enemyBase.ID = n.ID;
        enemyBase.Name = n.Name;
        enemyBase.MaxHP = n.MaxHP;
        enemyBase.MaxSP = n.MaxSP;
        enemyBase.Atk = n.Atk;
        enemyBase.Def = n.Def;
        enemyBase.Spd = n.Spd;
        enemyBase.MoneyDrop = n.MoneyDrop;
        enemyBase.Money = n.Money;
        enemyBase.NormalDrop = n.NormalDrop;
        enemyBase.NormalItemID = n.NormalDrop;
        enemyBase.RareDrop = n.RareDrop;
        enemyBase.RareItemID = n.RareItem;
        if (EnemyBase.ActionType.TryParse(n.ActType, out enemyBase.Act))
        {
            enemyBase.Act = EnemyBase.ActionType.Normal;
        }
        if (EnemyBase.RankType.TryParse(n.RankType, out enemyBase.Rank))
        {
            enemyBase.Rank = EnemyBase.RankType.Common;
        }

    }

    public EnemyBase getEnemyBase(int id)
    {
        var enemyBaseTable = EnemyBaseTable.Where(n => n.ID == id);
        return enemyBaseTable?.First();
    }

    #endregion

    #region ItemBase

    void loadItemBase()
    {
        var tempTable = (TempItemBase)Resources.Load("Data/ItemBaseTable");
        for (int i = 0; i < tempTable.sheets[0].list.Count; i++)
        {
            var n = tempTable.sheets[0].list[i];
            var itemBase = new ItemBase();
            convertToItemBase(n, itemBase);
            ItemBaseTable.Add(itemBase);
        }
    }

    void convertToItemBase(TempItemBase.Param n, ItemBase itemBase)
    {
        itemBase.ID = n.ID;
        itemBase.Name = n.Name;
        if (ItemBase.BaseType.TryParse(n.Type, out itemBase.Type) == false)
        {
            itemBase.Type = ItemBase.BaseType.Null;
        }
        if (ItemBase.ItemAction.TryParse(n.ActionName, out itemBase.ActionType) == false)
        {
            itemBase.ActionType = ItemBase.ItemAction.Null;
        }
        foreach (var material in n.Material.Split(new[] { ", " }, StringSplitOptions.None))
        {
            ItemBase.ItemMaterialType materialType;
            if (ItemBase.ItemMaterialType.TryParse(material, out materialType))
            {
                if (itemBase.MaterialTypes.Count < 3)
                {
                    itemBase.MaterialTypes.Add(materialType);
                }
            }
        }
        if (itemBase.MaterialTypes.Count == 0)
        {
            itemBase.MaterialTypes.Add(ItemBase.ItemMaterialType.Null);
        }
        itemBase.Value = n.Value;
        itemBase.Price = n.Price;
        itemBase.Quality = n.Quality;
        itemBase.Range = n.Range;
        itemBase.InitNum = n.InitNum;
        itemBase.Description = n.Description;
    }

    public ItemBase GetItemBase(int id)
    {
        var itemBaseTable = ItemBaseTable.Where(n => n.ID == id);
        return itemBaseTable?.First();
    }

    //void LoadItemProperty(){}

    #endregion

    #region ItemProperty

    void loadItemProperty()
    {
        var tempTable = (TempItemProperty)Resources.Load("Data/ItemPropaertyTable");
        for (int i = 0; i < tempTable.sheets[0].list.Count; i++)
        {
            var n = tempTable.sheets[0].list[i];
            var itemProperty = new ItemProperty();
            convertToItemProperty(n, itemProperty);
            ItemPropertyTable.Add(itemProperty);
        }
    }

    void convertToItemProperty(TempItemProperty.Param n, ItemProperty itemProperty)
    {
        itemProperty.ID = n.ID;
        itemProperty.Name = n.Name;
        if (ItemProperty.PropType.TryParse(n.Type, out itemProperty.Type) == false)
        {
            itemProperty.Type = ItemProperty.PropType.Null;
        }
        itemProperty.Value = n.Value;
        itemProperty.Price = n.Price;
        itemProperty.Quality = n.Quality;
        itemProperty.Range = n.Range;
        itemProperty.InitNum = n.InitNum;
    }

    public ItemProperty GetItemProp(int id)
    {
        var itemPropertyTable = ItemPropertyTable.Where(n => n.ID == id);
        if (itemPropertyTable == null) { return null; }
        return itemPropertyTable.First();
    }

    #endregion

    #region Runner

    public void loadRunnerBase()
    {
        var tempTable = (TempRunnerBase)Resources.Load("Data/RunnerBaseTable");
        for (int i = 0; i < tempTable.sheets[0].list.Count; i++)
        {
            var n = tempTable.sheets[0].list[i];
            var runnerBase = new RunnerBase();
            convertToRunnerBase(n, runnerBase);
            RunnerBaseTable.Add(runnerBase);
        }
    }

    void convertToRunnerBase(TempRunnerBase.Param n, RunnerBase runnerBase)
    {
        runnerBase.ID = n.ID;
        runnerBase.Name = n.Name;
        runnerBase.ImageFileName = n.ImageFileName;

        foreach (var nextExp in n.NextExp.Split(new[] { ", " }, StringSplitOptions.None))
        {
            int next;
            if (int.TryParse(nextExp, out next))
            {
                if (runnerBase.NextExp.Count < 2)
                {
                    runnerBase.NextExp.Add(next);
                }
            }
        }

        foreach (var price in n.Price.Split(new[] { ", " }, StringSplitOptions.None))
        {
            int p;
            if (int.TryParse(price, out p))
            {
                if (runnerBase.Price.Count < 3)
                {
                    runnerBase.Price.Add(p);
                }
            }
        }

        foreach (var power in n.Power.Split(new[] { ", " }, StringSplitOptions.None))
        {
            int pow;
            if (int.TryParse(power, out pow))
            {
                if (runnerBase.Power.Count < 3)
                {
                    runnerBase.Power.Add(pow);
                }
            }
        }

        foreach (var personality in n.Personality.Split(new[] { ". " }, StringSplitOptions.None))
        {
            RunnerBase.Personality personal;
            Enum.TryParse(personality, out personal);
        }

        runnerBase.Description = n.Description;
    }

    public RunnerBase GetRunnerBase(int id)
    {
        var runnerTabele = RunnerBaseTable.Where(n => n.ID == id);
        return runnerTabele?.First();
    }

    #endregion

    #region DungeonFloorData

    public void LoadDungeonFloorData()
    {
        var tempTable = (TempDungeonFloor)Resources.Load("Data/DungeonFloorTable");
        for (int i = 0; i < tempTable.sheets[0].list.Count; i++)
        {
            var n = tempTable.sheets[0].list[i];
            var dungeonFloor = new DungeonData.FloorData();
            convertToFloorData(n, dungeonFloor);
            DungeonFloorDataTable.Add(dungeonFloor);
        }
    }

    void convertToFloorData(TempDungeonFloor.Param n, DungeonData.FloorData floorData)
    {
        floorData.FloorNum = n.Floor;
        if (DungeonData.FloorData.FloorType.TryParse(n.Type, out floorData.Type) == false)
        {
            Debug.Log("FloorType読み込み失敗");
        }
        floorData.RoomCount = new IntVector2(n.RoomCountX, n.RoomCountY);
        floorData.MaxRoomCell = new IntVector2(n.MaxRoomCellX, n.MaxRoomCellY);
        floorData.MinRoomCell = new IntVector2(n.MinRoomCellX, n.MinRoomCellY);
        floorData.MaxEnemyNum = n.MaxEnemyNum;
        floorData.MinEnemyNum = n.MinEnemyNum;
    }

    public DungeonData.FloorData GetFloorData(int floorNum)
    {
        var floorDataTable = DungeonFloorDataTable.Where(n => n.FloorNum == floorNum);
        return floorDataTable?.First();
    }

    #endregion

}