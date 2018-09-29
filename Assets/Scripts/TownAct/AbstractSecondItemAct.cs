using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class AbstractSecondItemAct<T> : AbstractTownAct
{
    protected ItemIterator<T> itemIterator;

    protected ItemSet prefabsSet;

    protected List<ItemBase> objects;

    public AbstractSecondItemAct(string name, ItemSet iSet, Exec exec)
    {
        //名前の設定
        Name = name;

        //オブジェクトの設定
        prefabsSet = iSet;

        //デリゲートの設定
        ReturnAct = exec;

        //リストのロード
        loadObjects();
    }

    protected virtual void loadObjects() { }

    public override void StartUp()
    {
        base.StartUp();

        //イテレータ―の初期化
        itemIterator = new ItemIterator<T>(objects.Count + 1, prefabsSet.Buttons.Count);

        //UIの表示
        LayoutObjects();

        //UI初期位置
        selectObject(itemIterator.UINow);
    }

    public virtual void LayoutObjects()
    {
        if (prefabsSet.Window != null)
        {
            prefabsSet.Window.SetActive(true);
        }

        foreach (var n in prefabsSet.Buttons)
        {
            n.SetActive(false);
        }
    }

    public override void Close()
    {
        //メッセージを消す
        AdvUIManager.Instance.UpdateMessageText("", "");

        //UIを消す
        if (prefabsSet.Window != null)
        {
            prefabsSet.Window.SetActive(false);
        }
        foreach (var n in prefabsSet.Buttons)
        {
            n.SetActive(false);
            n.transform.localScale = new Vector3(1, 1, 1);
        }

        //返還
        ReturnAct();
    }

    protected virtual void selectObject(int count)
    {
        //アニメーションさせる
        for (int i = 0; i < prefabsSet.Buttons.Count; i++)
        {
            if (i == count)
            {
                prefabsSet.Buttons[i].GetComponent<Animator>().SetTrigger("IsSelect");
            }

            else if (prefabsSet.Buttons[i].activeSelf == true)
            {
                prefabsSet.Buttons[i].GetComponent<Animator>().SetBool("IsSelect", false);
            }
        }
    }

    public override void Update() { }
}
