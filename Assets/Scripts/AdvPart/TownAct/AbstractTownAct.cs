using UnityEngine;

public abstract class AbstractTownAct
{
    public delegate void Exec();
    protected Exec returnAct;

    //起動
    public abstract void StartUp();

    //非表示
    public abstract void Close();

    //ReturnActを伴わない非表示
    public virtual void SimpleClose() { }

    //制御
    public abstract void Update();

    //1日終了時のリフレッシュ処理
    public virtual void Refresh() { }
}
