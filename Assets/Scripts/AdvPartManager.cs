using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class AdvPartManager : SingletonMonoBehaviour<AdvPartManager>
{
    //呼び出すプレハブ
    [SerializeField]
    GameObject TownBaseSet;
    [SerializeField]
    GameObject TownTalkSet;
    [SerializeField]
    GameObject MarketPurchaseSet;
    [SerializeField]
    GameObject CoffeeRequestSet;

    [SerializeField]
    GameObject ItemSetPrefabs;
    GameObject itemSet;

    //キャンバス
    [SerializeField]
    GameObject AdvCanvas;

    //タウン各地での操作・行動
    public AbstractTownAct CurrentAct;
    AbstractTownAct townBaseAct;

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
            var prefabsSet = Instantiate(TownTalkSet);
            prefabsSet.transform.SetParent(AdvCanvas.transform, false);
            textActList.Add(new MultiTextAct(textFile, prefabsSet.GetComponent<UIPrefabsSet>(), townBaseAct.StartUp) { Name = fileName });
        }
        return acts.Last();
    }

    //タウンのベース
    public void StartUpTownBase()
    {
        var name = "TownBase";
        if (getAct(name) == null)
        {
            var prefabsSet = Instantiate(TownBaseSet);
            prefabsSet.transform.SetParent(AdvCanvas.transform, false);
            actList.Add(new TownBaseAct(name, prefabsSet.GetComponent<TownBaseSet>()));
        }
        townBaseAct = getAct(name);
        townBaseAct.StartUp();
        AdvUIManager.Instance.UpdateText();
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

        AdvUIManager.Instance.UpdateText();

        townBaseAct.Close();
    }


    public void startUpAtelierStorage()
    {
        var name = "AtelierStorage";
        if(getAct(name) == null)
        {
            var prefabsSet = Instantiate(CoffeeRequestSet);
            prefabsSet.transform.SetParent(AdvCanvas.transform, false);
            actList.Add(new AttelierStorageAct(name, prefabsSet.GetComponent<UIPrefabsSet>(), townBaseAct.StartUp, (TownBaseAct)townBaseAct));
        }
        getAct(name).StartUp();
        AdvUIManager.Instance.UpdateText();
        townBaseAct.Close();
    }


    public void StartUpCoffeeRequest()
    {
        var name = "CoffeeRequest";
        if (getAct(name) == null)
        {
            var gameObject = Instantiate(CoffeeRequestSet);
            gameObject.transform.SetParent(AdvCanvas.transform, false);
            actList.Add(new CoffeeRequestAct(name, gameObject.GetComponent<UIPrefabsSet>(), townBaseAct.StartUp));
        }
        getAct(name).StartUp();
        AdvUIManager.Instance.UpdateText();
        townBaseAct.Close();
        townBaseAct.CloseImage();
    }

    public void StartUpMarketPurchase()
    {
        var name = "MarketPurchase";
        if (getAct(name) == null)
        {
            if (itemSet == null)
            {
                itemSet = Instantiate(ItemSetPrefabs);
                itemSet.transform.SetParent(AdvCanvas.transform, false);
            }
            actList.Add(new MarketPurchaseAct(name, itemSet.GetComponent<ItemSet>(), townBaseAct.StartUp));
        }
        getAct(name).StartUp();
        AdvUIManager.Instance.UpdateText();
        townBaseAct.Close();
        townBaseAct.CloseImage();
    }

    public void StartUpMarketSell()
    {
        var name = "MarketSell";
        if (getAct(name) == null)
        {
            if (itemSet == null)
            {
                itemSet = Instantiate(ItemSetPrefabs);
                itemSet.transform.SetParent(AdvCanvas.transform, false);
            }  
            actList.Add(new MarketSellAct(name, itemSet.GetComponent<ItemSet>(), townBaseAct.StartUp));
        }
        getAct(name).StartUp();
        AdvUIManager.Instance.UpdateText();
        townBaseAct.Close();
        townBaseAct.CloseImage();
    }

    // Use this for initialization
    void Start()
    {
        StartUpTownBase();
    }
}
