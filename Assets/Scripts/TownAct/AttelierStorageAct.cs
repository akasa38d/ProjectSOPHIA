using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttelierStorageAct : AbstractSecondTownAct<AttelierStorageAct.AStorageAct>
{
    TownBaseAct townBaseAct;

    public abstract class AStorageAct
    {
        public string Name;
        public delegate void Exec();
        public Exec ReturnAct;
        public AStorageAct(Exec exec) { ReturnAct = exec; }
        public virtual void Action() { Debug.Log("未実装です"); }
    }

    public class PutItem : AStorageAct
    {
        public PutItem(Exec exec) : base(exec)
        {
            Name = "PutItemAct";
        }
        public override void Action()
        {
            AdvPartManager.Instance.StartUpAtelierStoragePut(() => { ReturnAct(); });
        }
    }

    public class PullItem : AStorageAct
    {
        public PullItem(Exec exec) : base(exec)
        {
            Name = "PullItemAct";
        }
        public override void Action()
        {
            AdvPartManager.Instance.StartUpAtelierStoragePull(() => { ReturnAct(); });
        }
    }


    public AttelierStorageAct(string name, UIPrefabsSet pSet, Exec exec, TownBaseAct tbAct) : base(name, pSet, exec)
    {
        townBaseAct = tbAct;
    }

    protected override void loadObjects()
    {
        objects = new List<AStorageAct>();
        objects.Add(new PutItem(StartUp));
        objects.Add(new PullItem(StartUp));
    }

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

    public override void SimpleClose()
    {
        townBaseAct.CloseImage();
        base.SimpleClose();
    }

    protected override void selectObject(int count)
    {
        FrameUIManager.Instance.UpdateMessageText("", "どうする？");
        base.selectObject(count);
    }

    public override void Update()
    {
        //選択
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            singleIterator.Num++;
            selectObject(singleIterator.Num);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            singleIterator.Num--;
            selectObject(singleIterator.Num);
        }

        //決定
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (singleIterator.Num < objects.Count)
            {
                objects[singleIterator.Num].Action();
                SimpleClose();
            }
            if (singleIterator.Num == objects.Count)
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

public class AStoragePutAct : AbstractSecondItemAct<AStoragePutAct>
{
    GameObject itemDescription { get { return AdvPartManager.Instance.GetItemDescription; } }

    public AStoragePutAct(string name, Exec exec) : base(name, exec) { }

    protected override void loadObjects()
    {
        objects = PlayerDataManager.Instance.Items;
    }

    public override void Close()
    {
        itemDescription.SetActive(false);
        base.Close();
    }

    protected override void selectObject(int uiCount)
    {
        FrameUIManager.Instance.UpdateMessageText("", "何をしまう？");

        base.selectObject(uiCount);

        //アイテムの説明更新
        if (uiCount + slideNow * columnCount < innerMax - 1)
        {
            itemDescription.SetActive(true);

            var description
                = objects[uiCount + slideNow * columnCount].Name + "\n"
                + objects[uiCount + slideNow * columnCount].Description;
            itemDescription.transform.GetChild(0).GetComponent<Text>().text = description;
        }

        if (uiCount + slideNow * columnCount == innerMax - 1)
        {
            itemDescription.SetActive(false);
            itemDescription.transform.GetChild(0).GetComponent<Text>().text = "";
        }
    }

    public override void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            objects.Sort((a, b) => a.ID - b.ID);
            simpleStartUp();
        }
        base.Update();
    }

    public override void Action()
    {
        PlayerDataManager.Instance.ItemStorage.Add(objects[innerNow]);
        objects.RemoveAt(innerNow);
        simpleStartUp();
    }
}

public class AStoragePullAct : AbstractSecondItemAct<AStoragePullAct>
{
    GameObject itemDescription { get { return AdvPartManager.Instance.GetItemDescription; } }

    public AStoragePullAct(string name, Exec exec) : base(name, exec) { }

    protected override void loadObjects()
    {
        objects = PlayerDataManager.Instance.ItemStorage;
    }

    public override void Close()
    {



        itemDescription.SetActive(false);
        base.Close();
    }

    protected override void selectObject(int uiCount)
    {
        FrameUIManager.Instance.UpdateMessageText("", "何を持っていく？");

        base.selectObject(uiCount);

        //アイテムの説明更新
        if (uiCount + slideNow * columnCount < innerMax - 1)
        {
            itemDescription.SetActive(true);

            var description
                = objects[uiCount + slideNow * columnCount].Name + "\n"
                + objects[uiCount + slideNow * columnCount].Description;
            itemDescription.transform.GetChild(0).GetComponent<Text>().text = description;
        }

        if (uiCount + slideNow * columnCount == innerMax - 1)
        {
            itemDescription.SetActive(false);
            itemDescription.transform.GetChild(0).GetComponent<Text>().text = "";
        }
    }

    public override void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            objects.Sort((a, b) => a.ID - b.ID);
            simpleStartUp();
        }
        base.Update();
    }

    public override void Action()
    {
        PlayerDataManager.Instance.Items.Add(objects[innerNow]);
        objects.RemoveAt(innerNow);
        simpleStartUp();
    }
}
