using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AdvPartManager : SingletonMonoBehaviour<AdvPartManager>
{
    PrefabsManager prefabsManager { get { return PrefabsManager.Instance; } }

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

    GameObject itemDescription;
    public GameObject GetItemDescription
    {
        get
        {
            if (itemDescription == null)
            {
                itemDescription = Instantiate(prefabsManager.ItemDescription);
                itemDescription.transform.SetParent(AdvCanvas.transform, false);
            }
            return itemDescription;
        }
    }

    //キャンバス
    [SerializeField]
    GameObject AdvCanvas;
    [SerializeField]
    GameObject MenuCanvas;

    //タウン各地での操作・行動
    public AbstractTownAct CurrentAct;
    TownBaseAct townBaseAct;    //(TownbaseAct)にキャストすべき？actListと別にすべき？

    List<AbstractTownAct> actList = new List<AbstractTownAct>();
    AbstractTownAct getAct(string name)
    {
        var acts = actList.Where(n => n.Name == name);
        if (acts.Count() == 0)
        {
            return null;
        }
        return acts.First();
    }

    //テキスト関係
    List<AbstractTextAct> textActList = new List<AbstractTextAct>();
    AbstractTextAct getTextAct(string fileName)
    {
        //一度しかInstantiateしないならactListに統合できる？
        var acts = textActList.Where(n => n.Name == fileName);
        if (acts.Count() == 0)
        {
            var textFile = Resources.Load(fileName).ToString();
            var prefabsSet = Instantiate(prefabsManager.TownTalkSet);
            prefabsSet.transform.SetParent(AdvCanvas.transform, false);
            textActList.Add(new MultiTextAct(textFile, prefabsSet.GetComponent<UIPrefabsSet>(), townBaseAct.StartUp) { Name = fileName });
        }
        return acts.Last();
    }

    //タウンのベース
    public void StartUpTownBase()
    {
        var name = "TownBase";
        if (townBaseAct == null)
        {
            var prefabsSet = Instantiate(prefabsManager.TownBaseSet);
            prefabsSet.transform.SetParent(AdvCanvas.transform, false);
            townBaseAct = new TownBaseAct(name, prefabsSet.GetComponent<TownBaseSet>());
        }
        townBaseAct.StartUp();
    }

    //その場での会話
    public void StartUpSingleText(string fileName)
    {
        var textFile = Resources.Load(fileName).ToString();

        var acts = textActList.Where(n => n.Name == fileName);

        if (acts.Count() == 0)
        {
            textActList.Add(new SingleTextAct(textFile, townBaseAct.StartUp) { Name = fileName });
        }

        textActList.Where(n => n.Name == fileName).First().StartUp();

        townBaseAct.Close();
    }

    //複数人から選んで会話
    public void StartUpMultiText(string fileName)
    {
        getTextAct(fileName).StartUp();
        townBaseAct.Close();
    }

    public void StartUpAtelierStorage()
    {
        //CoffeeRequestSetは仮
        var name = "AtelierStorage";
        if (getAct(name) == null)
        {
            var prefabsSet = Instantiate(prefabsManager.CoffeeRequestSet);
            prefabsSet.transform.SetParent(AdvCanvas.transform, false);
            actList.Add(new AttelierStorageAct(name, prefabsSet.GetComponent<UIPrefabsSet>(), townBaseAct.StartUp, townBaseAct));
        }
        getAct(name).StartUp();
        townBaseAct.Close();
    }

    public void StartUpAtelierStoragePut(AbstractTownAct.Exec exec)
    {
        var name = "AtelierStoragePut";
        if (getAct(name) == null)
        {
            actList.Add(new AStoragePutAct(name, exec));
        }
        CurrentAct.SimpleClose();
        getAct(name).StartUp();
    }

    public void StartUpAtelierStoragePull(AbstractTownAct.Exec exec)
    {
        var name = "AtelierStoragePull";
        if (getAct(name) == null)
        {
            actList.Add(new AStoragePullAct(name, exec));
        }
        CurrentAct.SimpleClose();
        getAct(name).StartUp();
    }

    public void StartUpCoffeeRequest()
    {
        var name = "CoffeeRequest";
        if (getAct(name) == null)
        {
            var gameObject = Instantiate(prefabsManager.CoffeeRequestSet);
            gameObject.transform.SetParent(AdvCanvas.transform, false);
            actList.Add(new CoffeeRequestAct(name, gameObject.GetComponent<UIPrefabsSet>(), townBaseAct.StartUp));
        }
        getAct(name).StartUp();
        townBaseAct.Close();
        townBaseAct.CloseImage();
    }

    public void StartUpMarketPurchase()
    {
        var name = "MarketPurchase";
        if (getAct(name) == null)
        {
            actList.Add(new MarketPurchaseAct(name, townBaseAct.StartUp));
        }
        getAct(name).StartUp();
        townBaseAct.Close();
        townBaseAct.CloseImage();
    }

    public void StartUpMarketSell()
    {
        var name = "MarketSell";
        if (getAct(name) == null)
        {
            actList.Add(new MarketSellAct(name, townBaseAct.StartUp));
        }
        getAct(name).StartUp();
        townBaseAct.Close();
        townBaseAct.CloseImage();
    }

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
        SetControl();
    }

    void SetControl()
    {
        Controller.Instance.SetControlUpdate = () => CurrentAct.Update();
    }

    public void RefreshActs()
    {
        townBaseAct.Refresh();
        foreach (var n in actList) { n.Refresh(); }
    }
}
