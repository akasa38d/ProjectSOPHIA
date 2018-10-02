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
    }

    //非表示
    public virtual void Close() { }
    //ReturnActを伴わない非表示
    public virtual void SimpleClose() { }

    //制御
    public virtual void Update() { }

    //1日終了時のリフレッシュ処理
    public virtual void Refresh() { }
}
