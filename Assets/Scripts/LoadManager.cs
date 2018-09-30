using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LoadManager
{
    //test
    public static void LoadPlayerDataAwake()
    {
        var playerDataManager = PlayerDataManager.Instance;
        playerDataManager.currentDate = new DateData { Year = 1, Month = 1, Week = 2, Day = 3 };
        playerDataManager.CurrentPlace = "こんなところ";
        playerDataManager.Money = 1200;
        playerDataManager.Stamina = 50;
 //       playerDataManager.Parameter = new PlayerParameter();
    }

    public static void LoadPlayerDataStart()
    {
        var playerDataManager = PlayerDataManager.Instance;
        var dataBaseManager = DataBaseManager.Instance;
        playerDataManager.Items.Add(dataBaseManager.GetItemBase(0));
        playerDataManager.Items.Add(dataBaseManager.GetItemBase(0));
        playerDataManager.Items.Add(dataBaseManager.GetItemBase(0));
        playerDataManager.Items.Add(dataBaseManager.GetItemBase(0));
        playerDataManager.Items.Add(dataBaseManager.GetItemBase(1));
        playerDataManager.Items.Add(dataBaseManager.GetItemBase(1));
        playerDataManager.Items.Add(dataBaseManager.GetItemBase(2));
        playerDataManager.Items.Add(dataBaseManager.GetItemBase(3));
        
        for(int i = 0; i < 6; i++)
        {
            playerDataManager.ItemStorage.Add(dataBaseManager.GetItemBase(4));
        }
    }
}
