using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class AbstractSecondTownAct<T> : AbstractTownAct
{
    protected SingleIterator singleIterator;

    protected SelectLayout select;

    protected List<T> objects;

    public AbstractSecondTownAct(SelectLayout _select, Exec exec)
    {
        //オブジェクトの設定
        select = _select;

        //デリゲートの設定
        returnAct = exec;

        //リストのロード
        loadObjects();
    }
    
    protected virtual void loadObjects() { }

    public override void StartUp()
    {
        AdvPartManager.Instance.CurrentAct = this;

        //イテレータ―の初期化
        singleIterator = new SingleIterator(objects.Count + 1);

        //UIの表示
        layoutObjects();

        //UI初期位置
        selectObject(singleIterator.Num);
    }

    protected virtual void layoutObjects()
    {
        foreach (var n in select.Buttons)
        {
            n.SetActive(false);
        }
    }

    public override void Close()
    {
        FrameUIManager.Instance.UpdateMessageText("", "");

        SimpleClose();

        returnAct();
    }

    public override void SimpleClose()
    {
        foreach (var n in select.Buttons)
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
        for (int i = 0; i < select.Buttons.Count; i++)
        {
            if (i == count)
            {
                select.Buttons[i].GetComponent<Animator>().SetTrigger("IsSelect");
            }

            else if (select.Buttons[i].activeSelf == true)
            {
                select.Buttons[i].GetComponent<Animator>().SetBool("IsSelect", false);
            }
        }
    }

    public override void Update() { }
}
