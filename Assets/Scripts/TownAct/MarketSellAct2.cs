using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarketSellAct : AbstractSecondItemAct<MarketSellAct>
{
    public MarketSellAct(string name, ItemSet iSet, Exec exec) : base(name, iSet, exec) { }

    protected override void loadObjects()
    {
        objects = PlayerDataManager.Instance.Items;
    }

    public override void StartUp() { base.StartUp(); }

    public override void LayoutObjects() { base.LayoutObjects(); }

    public override void Close() { base.Close(); }

    protected override void selectObject(int uiCount)
    {
        AdvUIManager.Instance.UpdateMessageText("", "何を売ろうか？");

        base.selectObject(uiCount);

        //アイテムの説明更新
        if (uiCount + slideNow * columnCount < innerMax - 1)
        {
            itemSet.Window.SetActive(true);

            var description
                = objects[uiCount + slideNow * columnCount].Name + "\n"
                + objects[uiCount + slideNow * columnCount].Price + " M" + "\n"
                + objects[uiCount + slideNow * columnCount].Description;
            itemSet.Window.transform.GetChild(0).GetComponent<Text>().text = description;
        }

        if (uiCount + slideNow * columnCount == innerMax - 1)
        {
            itemSet.Window.SetActive(false);
            itemSet.Window.transform.GetChild(0).GetComponent<Text>().text = "";
        }
    }

    public override void Update() { base.Update(); }

    public override void Action()
    {
        base.Action();
    }
}
