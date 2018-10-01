using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractTownAct
{
    public string Name;

    //元のActへ戻すためのデリゲート
    public delegate void Exec();
    public Exec ReturnAct;

    //起動
    public virtual void StartUp()
    {
        AdvPartManager.Instance.CurrentAct = this;
        Debug.Log(AdvPartManager.Instance.CurrentAct.Name);
    }

    //非表示
    public virtual void Close() { }
    public virtual void CloseImage() { }
    public virtual void SimpleClose() { }

    //制御
    public virtual void Update() { }
}
