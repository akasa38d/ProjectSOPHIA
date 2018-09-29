using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarketSellAct2 : AbstractSecondItemAct<MarketSellAct2>
{
    public MarketSellAct2(string name, ItemSet iSet, Exec exec) : base(name, iSet, exec) { }

    protected override void loadObjects()
    {
        objects = PlayerDataManager.Instance.Items;
    }

    public override void StartUp() { base.StartUp(); }

    public override void LayoutObjects()
    {
        base.LayoutObjects();

        for (int i = 0; i < itemIterator.UIMax; i++)
        {
            prefabsSet.Buttons[i].SetActive(true);

            //後でプロパティを設定
            if (i + itemIterator.SlideCount * itemIterator.ColumnCount < objects.Count)
            {
                prefabsSet.Buttons[i].transform.GetChild(0).GetComponent<Text>().text = objects[i + itemIterator.SlideCount * itemIterator.ColumnCount].Name;
            }
            else if (i == itemIterator.UIMax - 1 && i + itemIterator.SlideCount * itemIterator.ColumnCount == itemIterator.InnerMax - 1)
            {
                prefabsSet.Buttons[i].transform.GetChild(0).GetComponent<Text>().text = "キャンセル";
            }
        }

        if (itemIterator.SlideCount == 0) { prefabsSet.UpButton.SetActive(false); }
        if (itemIterator.SlideCount == itemIterator.SlideMax) { prefabsSet.DownButton.SetActive(false); }
    }

    public override void Close() { base.Close(); }

    protected override void selectObject(int count)
    {
        AdvUIManager.Instance.UpdateMessageText("", "何を売ろうか？");

        base.selectObject(count);

        //アイテムの説明更新
        if (itemIterator.InnerNow < objects.Count)
        {
            if (prefabsSet.Window.activeSelf == false)
            {
                prefabsSet.Window.SetActive(true);
            }

            var description
                = objects[count].Name + "\n"
                + objects[count].Price + " M" + "\n"
                + objects[count].Description;
            prefabsSet.Window.transform.GetChild(0).GetComponent<Text>().text = description;
        }

        if (itemIterator.InnerNow == objects.Count)
        {
            prefabsSet.Window.SetActive(false);
            prefabsSet.Window.transform.GetChild(0).GetComponent<Text>().text = "";
        }
    }

    public override void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            itemIterator.Right();
            selectObject(itemIterator.UINow);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            itemIterator.Left();
            selectObject(itemIterator.UINow);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            itemIterator.Up();
            selectObject(itemIterator.UINow);

        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            itemIterator.Down();
            selectObject(itemIterator.UINow);
        }

        //決定
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (itemIterator.InnerNow == objects.Count)
            {
                Close();
            }
        }

        //キャンセル
        if (Input.GetKeyDown(KeyCode.X))
        {
            Close();
        }
    }
}
