using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class AbstractSecondTownAct<T> : AbstractTownAct
{
    protected SingleIterator singleIterator;

    protected UIPrefabsSet prefabsSet;

    protected List<T> objects;

    public AbstractSecondTownAct(string name, UIPrefabsSet pSet, Exec exec)
    {
        //名前の設定
        Name = name;

        //オブジェクトの設定
        prefabsSet = pSet;

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
        singleIterator = new SingleIterator(objects.Count + 1);

        //UIの表示
        layoutObjects();

        //UI初期位置
        selectObject(singleIterator.Num);
    }

    protected virtual void layoutObjects()
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
        FrameUIManager.Instance.UpdateMessageText("", "");

        SimpleClose();

        //返還
        ReturnAct();
    }

    public override void SimpleClose()
    {
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

        //イテレーターをリセット
        singleIterator.ResetNum();
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
