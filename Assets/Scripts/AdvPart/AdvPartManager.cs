﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AdvPartManager : SingletonMonoBehaviour<AdvPartManager>, ISceneManager
{
    PrefabsManager prefabsManager { get { return PrefabsManager.Instance; } }

    #region 実体化するゲームオブジェクト

    GameObject basicSelect;
    public SelectLayout GetBasicSelect
    {
        get
        {
            if (basicSelect == null)
            {
                basicSelect = Instantiate(prefabsManager.BasicSelectButton);
                basicSelect.transform.SetParent(AdvCanvas.transform, false);
            }
            return basicSelect.GetComponent<SelectLayout>();
        }
    }

    GameObject basicSelectRight;
    SelectLayout GetBasicSelectRight
    {
        get
        {
            if (basicSelectRight == null)
            {
                basicSelectRight = Instantiate(prefabsManager.BasicSelectRight);
                basicSelectRight.transform.SetParent(AdvCanvas.transform, false);
            }
            return basicSelectRight.GetComponent<SelectLayout>();
        }
    }

    GameObject twoTopicSelect;
    SelectLayout GetTwoTopicSelect
    {
        get
        {
            if (twoTopicSelect == null)
            {
                twoTopicSelect = Instantiate(prefabsManager.TwoTopicSelect);
                twoTopicSelect.transform.SetParent(AdvCanvas.transform, false);
            }
            return twoTopicSelect.GetComponent<SelectLayout>();
        }
    }

    GameObject facilityImage;
    public GameObject FacilityImage
    {
        get
        {
            if (facilityImage == null)
            {
                facilityImage = Instantiate(prefabsManager.FacilityImage);
                facilityImage.transform.SetParent(AdvCanvas.transform, false);
            }
            return facilityImage;
        }
    }
    public void ActivateFacilityImage(bool isOpen)
    {
        FacilityImage.SetActive(isOpen);
    }

    GameObject itemSet;
    public GameObject GetItemSet
    {
        get
        {
            if (itemSet == null)
            {
                itemSet = Instantiate(prefabsManager.ItemSetPrefabs);
                itemSet.transform.SetParent(AdvCanvas.transform, false);
            }
            return itemSet;
        }
    }

    GameObject description;
    public GameObject GetDescription
    {
        get
        {
            if (description == null)
            {
                description = Instantiate(prefabsManager.ItemDescription);
                description.transform.SetParent(AdvCanvas.transform, false);
            }
            return description;
        }
    }

    #endregion

    //キャンバス
    [SerializeField]
    GameObject AdvCanvas;

    [SerializeField]
    GameObject MenuCanvas;

    #region 各Actを呼び出すメソッド

    //タウン各地での操作・行動
    public AbstractTownAct CurrentAct;
    TownBaseAct TownBaseAct;
    Dictionary<string, AbstractTownAct> actDict = new Dictionary<string, AbstractTownAct>();
    AbstractTownAct getAct(string key)
    {
        if (!actDict.ContainsKey(key)) { return null; }
        return actDict[key];
    }

    //タウンのベース
    public void StartUpTownBase()
    {
        if (TownBaseAct == null) { TownBaseAct = new TownBaseAct(); }
        CurrentAct = TownBaseAct;
        TownBaseAct.StartUp();
    }

    //その場での会話
    public void StartUpSingleText(string fileName)
    {
        if (getAct(fileName) == null) { actDict.Add(fileName, new SingleTextAct(fileName, TownBaseAct.StartUp)); }
        TownBaseAct.Close();
        getAct(fileName).StartUp();        
    }

    //複数人から選んで会話
    public void StartUpMultiText(string fileName)
    {
        if (getAct(fileName) == null) { actDict.Add(fileName, new MultiTextAct(fileName, TownBaseAct.StartUp, GetBasicSelectRight)); }
        TownBaseAct.Close();
        getAct(fileName).StartUp();        
    }

    public void StartUpAtelierStorage()
    {
        var name = "AtelierStorage";
        if (getAct(name) == null) { actDict.Add(name, new AttelierStorageAct(GetBasicSelect, TownBaseAct.StartUp)); }
        TownBaseAct.Close();
        getAct(name).StartUp();            
    }

    public void StartUpAtelierStoragePut(AbstractTownAct.Exec exec)
    {
        var name = "AtelierStoragePut";
        if (getAct(name) == null) { actDict.Add(name, new AStoragePutAct(exec)); }
        CurrentAct.SimpleClose();
        getAct(name).StartUp();
    }

    public void StartUpAtelierStoragePull(AbstractTownAct.Exec exec)
    {
        var name = "AtelierStoragePull";
        if (getAct(name) == null) { actDict.Add(name, new AStoragePullAct(exec)); }
        CurrentAct.SimpleClose();
        getAct(name).StartUp();
    }

    public void StartUpCoffeeRequest()
    {
        var name = "CoffeeRequest";
        if (getAct(name) == null) { actDict.Add(name, new CoffeeRequestAct(GetTwoTopicSelect, TownBaseAct.StartUp)); }
        TownBaseAct.Close();
        getAct(name).StartUp();        
        ActivateFacilityImage(false);
    }

    public void StartUpMarketPurchase()
    {
        var name = "MarketPurchase";
        if (getAct(name) == null) { actDict.Add(name, new MarketPurchaseAct(TownBaseAct.StartUp)); }
        TownBaseAct.Close();
        getAct(name).StartUp();        
        ActivateFacilityImage(false);
    }

    public void StartUpMarketSell()
    {
        var name = "MarketSell";
        if (getAct(name) == null) { actDict.Add(name, new MarketSellAct(TownBaseAct.StartUp)); }
        TownBaseAct.Close();
        getAct(name).StartUp();        
        ActivateFacilityImage(false);
    }

    #endregion

    public void OpenMenuAct()
    {

    }

    public void CloseMenuAct()
    {

    }

    // Use this for initialization
    void Start()
    {
        StartUpTownBase();
        Controller.Instance.CurrentManager = this;
    }

    public void RefreshActs()
    {
        TownBaseAct.Refresh();
        foreach (var n in actDict.Values) { n.Refresh(); }
    }

    public void Control()
    {
        CurrentAct.Update();
    }
}
