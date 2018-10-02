using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class DataBaseManager : SingletonMonoBehaviour<DataBaseManager>
{
    public List<ItemBase> ItemBaseTable = new List<ItemBase>();
    public List<RunnerBase> RunnerBaseTable = new List<RunnerBase>();

    public new void Awake()
    {
        LoadItemBase();
        //LoadItemProperty();

        LoadRunnerBase();
    }

    #region Player

    void LoadPlayerBase()
    {

    }

    void convertToPlayerBase()
    {

    }

    public PlayerBase getPlayerBase(int level)
    {
        return new PlayerBase();
    }

    #endregion

    #region Item

    void LoadItemBase()
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

        if (ItemBase.ItemType.TryParse(n.Type, out itemBase.Type) == false)
        {
            itemBase.Type = ItemBase.ItemType.Null;
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
        itemBase.Num = n.Num;
        itemBase.Description = n.Description;
    }

    public ItemBase GetItemBase(int id)
    {
        var itemBaseTable = ItemBaseTable.Where(n => n.ID == id);
        if (itemBaseTable == null) { return null; }
        return itemBaseTable.First();
    }

    //void LoadItemProperty(){}

    #endregion


    #region Runner

    public void LoadRunnerBase()
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
        if (runnerTabele == null) { return null; }
        return runnerTabele.First();
    }

    #endregion

}