using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MyUtility;

public class MarketPurchaseAct : AbstractSecondTownAct<ItemBase>
{
    //アイテムのリスト(現状最大５)

    public MarketPurchaseAct(string name, UIPrefabsSet pSet, Exec exec) : base(name, pSet, exec) { }

    protected override void loadObjects()
    {
        objects = new List<ItemBase>();

        var itemArray = new int[] { 0, 1, 2 };

        //展示するアイテムを取得
        foreach (var n in itemArray)
        {
            objects.Add(DataBaseManager.Instance.GetItemBase(n));
        }
    }

    public override void StartUp() { base.StartUp(); }

    protected override void layoutObjects()
    {
        base.layoutObjects();

        for (int i = 0; i < objects.Count + 1; i++)
        {
            prefabsSet.Buttons[i].SetActive(true);
            if (i < objects.Count)
            {
                prefabsSet.Buttons[i].transform.GetChild(0).GetComponent<Text>().text = objects[i].Name;
                prefabsSet.Buttons[i].transform.GetChild(1).GetComponent<Text>().text = objects[i].Price.ToStringMoney() + " M";
            }
            else if (i == objects.Count)
            {
                prefabsSet.Buttons[i].transform.GetChild(0).GetComponent<Text>().text = "キャンセル";
                prefabsSet.Buttons[i].transform.GetChild(1).GetComponent<Text>().text = "";
            }
        }
    }

    public override void Close() { base.Close(); }

    protected override void selectObject(int count)
    {
        AdvUIManager.Instance.UpdateMessageText("", "何を買おうか？");

        base.selectObject(count);

        //アイテムの説明更新
        if (singleIterator.Count < objects.Count)
        {
            if (prefabsSet.Window.activeSelf == false)
            {
                prefabsSet.Window.SetActive(true);
            }
            prefabsSet.Window.transform.GetChild(0).GetComponent<Text>().text = objects[count].Description;
        }

        if (singleIterator.Count == objects.Count)
        {
            prefabsSet.Window.SetActive(false);
            prefabsSet.Window.transform.GetChild(0).GetComponent<Text>().text = "";
        }
    }

    //決定ボタンをInstantiateしたい

    public override void Update()
    {
        //選択
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            singleIterator.Count++;
            selectObject(singleIterator.Count);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            singleIterator.Count--;
            selectObject(singleIterator.Count);
        }

        //決定
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (singleIterator.Count == objects.Count)
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
