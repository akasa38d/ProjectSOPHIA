using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarketSellAct : AbstractSecondTownAct<ItemBase>
{
    //アイテムのリスト(現状最大３０)

    public ItemSet itemSet;

    public MarketSellAct(string name, UIPrefabsSet pSet, Exec exec) : base(name, pSet, exec) { }

    protected override void loadObjects()
    {
        objects = PlayerDataManager.Instance.Items;
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
            }
            else if (i == objects.Count)
            {
                prefabsSet.Buttons[i].transform.GetChild(0).GetComponent<Text>().text = "キャンセル";
            }
        }
    }

    public override void Close() { base.Close(); }

    protected override void selectObject(int count)
    {
        AdvUIManager.Instance.UpdateMessageText("", "何を売ろうか？");

        base.selectObject(count);

        //アイテムの説明更新
        if (singleIterator.Count < objects.Count)
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

        if (singleIterator.Count == objects.Count)
        {
            prefabsSet.Window.SetActive(false);
            prefabsSet.Window.transform.GetChild(0).GetComponent<Text>().text = "";
        }
    }

    public override void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            singleIterator.Count++;
            selectObject(singleIterator.Count);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            singleIterator.Count--;
            selectObject(singleIterator.Count);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (singleIterator.Count - 7 >= 0)
            {
                singleIterator.Count -= 7;
                selectObject(singleIterator.Count);
            }
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (singleIterator.Count + 7 < singleIterator.MaxNumber)
            {
                singleIterator.Count += 7;
                selectObject(singleIterator.Count);
            }
        }

        //決定
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (singleIterator.Count == PlayerDataManager.Instance.Items.Count)
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
