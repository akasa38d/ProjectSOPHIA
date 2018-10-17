using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class AbstractSecondItemAct<T> : AbstractTownAct
{
    protected ItemIterator<T> itemIterator;
    protected int slideNow { get { return itemIterator.SlideNow; } }
    protected int slideMax { get { return itemIterator.SlideMax; } }
    protected int columnCount { get { return itemIterator.ColumnCount; } }
    protected int uiMax { get { return itemIterator.UIMax; } }
    protected int innerNow { get { return itemIterator.InnerNow; } }
    protected int innerMax { get { return itemIterator.InnerMax; } }

    protected SelectLayoutLong itemSet;

    protected List<ItemBase> objects;

    public AbstractSecondItemAct(Exec exec)
    {
        itemSet = AdvPartManager.Instance.GetItemSet.GetComponent<SelectLayoutLong>();
        ReturnAct = exec;
        loadObjects();
    }

    protected virtual void loadObjects() { }

    public override void StartUp()
    {
        base.StartUp();

        //イテレータ―の初期化
        itemIterator = new ItemIterator<T>(objects.Count + 1, itemSet.Buttons.Count, this);

        //UIの表示
        LayoutObjects();

        //UI初期位置
        selectObject(itemIterator.UINow);
    }

    protected virtual void simpleStartUp()
    {
        itemIterator.ResetInnerMax(objects.Count + 1);

        //UIの表示
        LayoutObjects();

        //UI初期位置
        selectObject(itemIterator.UINow);
    }

    public virtual void LayoutObjects()
    {
        foreach (var n in itemSet.Buttons)
        {
            n.SetActive(false);
            n.transform.localScale = new Vector3(1, 1, 1);
        }

        for (int i = 0; i < uiMax; i++)
        {
            itemSet.Buttons[i].SetActive(true);

            if (i + slideNow * columnCount < innerMax - 1)
            {
                //画像にしたい
                itemSet.Buttons[i].transform.GetChild(0).GetComponent<Text>().text = objects[i + slideNow * columnCount].Name;
            }
            else if (i + slideNow * columnCount == innerMax - 1)
            {
                itemSet.Buttons[i].transform.GetChild(0).GetComponent<Text>().text = "キャンセル";
            }
        }

        if (slideNow == 0) { itemSet.UpIcon.SetActive(false); }
        if(slideNow > 0) { itemSet.UpIcon.SetActive(true); }
        if(slideNow < slideMax) { itemSet.DownIcon.SetActive(true); }
        if (slideNow == slideMax) { itemSet.DownIcon.SetActive(false); }
    }

    public override void Close()
    {
        FrameUIManager.Instance.UpdateMessageText("", "");

        foreach (var n in itemSet.Buttons)
        {
            n.SetActive(false);
            n.transform.localScale = new Vector3(1, 1, 1);
        }

        itemSet.UpIcon.SetActive(false);
        itemSet.DownIcon.SetActive(false);

        //返還
        ReturnAct();
    }

    protected virtual void selectObject(int uiCount)
    {
        //アニメーションさせる
        for (int i = 0; i < itemSet.Buttons.Count; i++)
        {
            if (i == uiCount)
            {
                itemSet.Buttons[i].GetComponent<Animator>().SetTrigger("IsSelect");
            }
            else if (itemSet.Buttons[i].activeSelf == true)
            {
                itemSet.Buttons[i].GetComponent<Animator>().SetBool("IsSelect", false);
            }
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
            if(innerNow < innerMax - 1)
            {
                Action();
            }
            else if (innerNow == innerMax - 1)
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

    public virtual void Action()
    {
        Debug.Log(innerNow + "番アイテムを選択");
    }
}
