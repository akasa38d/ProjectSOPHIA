using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarketPurchaseAct : AbstractSecondItemAct<MarketPurchaseAct>
{
    GameObject itemDescription { get { return AdvPartManager.Instance.GetDescription; } }

    public MarketPurchaseAct(Exec exec) : base(exec) { }

    protected override void loadObjects()
    {
        //test
        objects = new List<ItemBase>();
        var itemArray = new int[] { 0, 1, 2 };
        foreach (var n in itemArray)
        {
            objects.Add(DataBaseManager.Instance.GetItemBase(n));
        }
    }

    public override void Close()
    {
        itemDescription.SetActive(false);
        base.Close();
    }

    protected override void selectObject(int uiCount)
    {
        FrameUIManager.Instance.UpdateMessageText("", "何を買おうか？");

        base.selectObject(uiCount);

        //アイテムの説明更新
        if (uiCount + slideNow * columnCount < innerMax - 1)
        {
            itemDescription.SetActive(true);

            var description
                = objects[uiCount + slideNow * columnCount].Name + "\n"
                + objects[uiCount + slideNow * columnCount].Price + " M" + "\n"
                + objects[uiCount + slideNow * columnCount].Description;
            itemDescription.transform.GetChild(0).GetComponent<Text>().text = description;
        }

        if (uiCount + slideNow * columnCount == innerMax - 1)
        {
            itemDescription.SetActive(false);
            itemDescription.transform.GetChild(0).GetComponent<Text>().text = "";
        }
    }

    public override void Action()
    {
        var playerData = PlayerDataManager.Instance;
        if (playerData.Money < objects[innerNow].Price)
        {
            FrameUIManager.Instance.UpdateMessageText("", "お金が足りない……");
        }
        else if (playerData.Items.Count >= playerData.ItemsSize)
        {
            FrameUIManager.Instance.UpdateMessageText("", "これ以上持てない……");
        }
        else
        {
            playerData.Money -= objects[innerNow].Price;
            playerData.Items.Add(objects[innerNow]);
            objects.RemoveAt(innerNow);
            simpleStartUp();
        }
    }

    public override void Refresh()
    {
        loadObjects();
    }
}

public class MarketSellAct : AbstractSecondItemAct<MarketSellAct>
{
    GameObject itemDescription { get { return AdvPartManager.Instance.GetDescription; } }

    public MarketSellAct(Exec exec) : base(exec) { }

    protected override void loadObjects()
    {
        objects = PlayerDataManager.Instance.Items;
    }

    public override void Close()
    {
        itemDescription.SetActive(false);
        base.Close();
    }

    protected override void selectObject(int uiCount)
    {
        FrameUIManager.Instance.UpdateMessageText("", "何を売ろうか？");

        base.selectObject(uiCount);

        //アイテムの説明更新
        if (uiCount + slideNow * columnCount < innerMax - 1)
        {
            itemDescription.SetActive(true);

            var description
                = objects[uiCount + slideNow * columnCount].Name + "\n"
                + objects[uiCount + slideNow * columnCount].Price / 2 + " M" + "\n"
                + objects[uiCount + slideNow * columnCount].Description;
            itemDescription.transform.GetChild(0).GetComponent<Text>().text = description;
        }

        if (uiCount + slideNow * columnCount == innerMax - 1)
        {
            itemDescription.SetActive(false);
            itemDescription.transform.GetChild(0).GetComponent<Text>().text = "";
        }
    }

    public override void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            objects.Sort((a, b) => a.ID - b.ID);
            simpleStartUp();
        }
        base.Update();
    }

    public override void Action()
    {
        PlayerDataManager.Instance.Money += objects[innerNow].Price / 2;
        objects.RemoveAt(innerNow);
        simpleStartUp();
    }
}
