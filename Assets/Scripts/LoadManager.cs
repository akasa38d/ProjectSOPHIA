using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LoadManager
{
    public static void LoadPlayerDataAwake()
    {
        //        InitPlayerData();
    }

    public static void LoadPlayerDataStart()
    {
        LoadPlayerData();
    }

    public static void InitPlayerData()
    {
        PlayerDataManager.Instance.Parameter = new PlayerParameter();
        SaveData.SetClass<PlayerSaveData>("p1", new PlayerSaveData());
        SaveData.Save();
    }

    public static void SavePlayerData()
    {
        var playerDataManager = PlayerDataManager.Instance;

        var playerSaveData = new PlayerSaveData();
        playerSaveData.Day = playerDataManager.Day;
        playerSaveData.Stamina = playerDataManager.Stamina;
        playerSaveData.Money = playerDataManager.Money;
        playerSaveData.MoneySaving = playerDataManager.MoneySaving;
        playerSaveData.SetItemIDs();
        playerSaveData.SetStorageItemIDs();
        SaveData.SetClass<PlayerSaveData>("p1", playerSaveData);
        SaveData.Save();
    }

    public static void LoadPlayerData()
    {
        var playerDataManager = PlayerDataManager.Instance;
        var dataBaseManager = DataBaseManager.Instance;

        var saveData = SaveData.GetClass<PlayerSaveData>("p1", new PlayerSaveData());
        playerDataManager.Day = saveData.Day;
        playerDataManager.Stamina = saveData.Stamina;
        playerDataManager.Money = saveData.Money;
        playerDataManager.MoneySaving = saveData.MoneySaving;
        foreach (var n in saveData.ItemIDs)
        {
            playerDataManager.Items.Add(dataBaseManager.GetItemBase(n));
        }
        foreach (var n in saveData.StorageItemIDs)
        {
            playerDataManager.ItemStorage.Add(dataBaseManager.GetItemBase(n));
        }
    }

    [SerializeField]
    public class PlayerSaveData
    {
        [SerializeField]
        public int Day;

        [SerializeField]
        public int Stamina;

        [SerializeField]
        public int Money;

        [SerializeField]
        public int MoneySaving;

        [SerializeField]
        public List<int> ItemIDs;

        [SerializeField]
        public List<int> StorageItemIDs;

        public PlayerSaveData()
        {
            InitSaveData();
        }

        void InitSaveData()
        {
            Day = 1;
            Stamina = 70;
            Money = 1200;
            MoneySaving = 2000;

            ItemIDs = new List<int>();
            ItemIDs.Add(0);
            ItemIDs.Add(0);
            ItemIDs.Add(0);
            ItemIDs.Add(1);
            ItemIDs.Add(1);
            ItemIDs.Add(2);
            ItemIDs.Add(3);

            StorageItemIDs = new List<int>();
            StorageItemIDs.Add(4);
            StorageItemIDs.Add(4);
            StorageItemIDs.Add(4);
            StorageItemIDs.Add(4);
            StorageItemIDs.Add(4);
        }

        public void SetItemIDs()
        {
            ItemIDs = new List<int>();
            foreach (var n in PlayerDataManager.Instance.Items)
            {
                ItemIDs.Add(n.ID);
            }
        }

        public void SetStorageItemIDs()
        {
            StorageItemIDs = new List<int>();
            foreach (var n in PlayerDataManager.Instance.ItemStorage)
            {
                StorageItemIDs.Add(n.ID);
            }
        }


    }

}
