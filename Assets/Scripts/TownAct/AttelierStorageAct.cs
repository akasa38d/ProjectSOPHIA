using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttelierStorageAct : AbstractSecondTownAct<AStorageAct>
{
    TownBaseAct townBaseAct;

    public AttelierStorageAct(string name, UIPrefabsSet pSet, Exec exec, TownBaseAct tbAct) : base(name, pSet, exec) { townBaseAct = tbAct; }

    protected override void loadObjects()
    {
        objects = new List<AStorageAct>();
        objects.Add(new PutItem());
        objects.Add(new PullItem());
    }

    public override void StartUp() { base.StartUp(); }

    protected override void layoutObjects()
    {
        base.layoutObjects();

        prefabsSet.Window.SetActive(false);
        prefabsSet.Window.transform.GetChild(0).GetComponent<Text>().text = "";

        townBaseAct.OpenImage();

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

    public override void CloseImage()
    {
        townBaseAct.CloseImage();
    }

    protected override void selectObject(int count)
    {
        AdvUIManager.Instance.UpdateMessageText("", "どうする？");

        base.selectObject(count);
    }

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
            if(singleIterator.Count < objects.Count)
            {
                objects[singleIterator.Count].Action();
            }
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

public abstract class AStorageAct
{
    public string Name;
    public virtual void Action() { Debug.Log("未実装です"); }
}

public class PutItem : AStorageAct
{
    public PutItem()
    {
        Name = "PutItemAct";
    }
    public override void Action()
    {
        base.Action();
    }
}

public class PullItem : AStorageAct
{
    public PullItem()
    {
        Name = "PullItemAct";
    }
    public override void Action()
    {
        base.Action();
    }
}
