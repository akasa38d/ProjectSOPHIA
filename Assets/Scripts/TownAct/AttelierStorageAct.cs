using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttelierStorageAct : AbstractSecondTownAct<AttelierStorageAct.AStorageAct>
{
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
            Name = "収納する";
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
            Name = "持ち出す";
        }
        public override void Action()
        {
            AdvPartManager.Instance.StartUpAtelierStoragePull(() => { ReturnAct(); });
        }
    }

    GameObject description { get { return AdvPartManager.Instance.GetDescription; } }

    public AttelierStorageAct(VerticalSelect _select, Exec exec) : base(_select, exec) { }

    protected override void loadObjects()
    {
        objects = new List<AStorageAct>();
        objects.Add(new PutItem(StartUp));
        objects.Add(new PullItem(StartUp));
    }

    protected override void layoutObjects()
    {
        base.layoutObjects();

        description.SetActive(false);
        description.transform.GetChild(0).GetComponent<Text>().text = "";

        AdvPartManager.Instance.ActivateFacilityImage(true);

        for (int i = 0; i < objects.Count + 1; i++)
        {
            select.Buttons[i].SetActive(true);

            if (i < objects.Count)
            {
                select.Buttons[i].transform.GetChild(0).GetComponent<Text>().text = objects[i].Name;
            }
            else if (i == objects.Count)
            {
                select.Buttons[i].transform.GetChild(0).GetComponent<Text>().text = "キャンセル";
            }
        }
    }

    public override void SimpleClose()
    {
        AdvPartManager.Instance.ActivateFacilityImage(false);
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
    GameObject description { get { return AdvPartManager.Instance.GetDescription; } }

    public AStoragePutAct(Exec exec) : base(exec) { }

    protected override void loadObjects()
    {
        objects = PlayerDataManager.Instance.Items;
    }

    public override void Close()
    {
        description.SetActive(false);
        base.Close();
    }

    protected override void selectObject(int uiCount)
    {
        FrameUIManager.Instance.UpdateMessageText("", "何をしまう？");

        base.selectObject(uiCount);

        //アイテムの説明更新
        if (uiCount + slideNow * columnCount < innerMax - 1)
        {
            this.description.SetActive(true);

            var description
                = objects[uiCount + slideNow * columnCount].Name + "\n"
                + objects[uiCount + slideNow * columnCount].Description;
            this.description.transform.GetChild(0).GetComponent<Text>().text = description;
        }

        if (uiCount + slideNow * columnCount == innerMax - 1)
        {
            description.SetActive(false);
            description.transform.GetChild(0).GetComponent<Text>().text = "";
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
    GameObject description { get { return AdvPartManager.Instance.GetDescription; } }

    public AStoragePullAct(Exec exec) : base(exec) { }

    protected override void loadObjects()
    {
        objects = PlayerDataManager.Instance.ItemStorage;
    }

    public override void Close()
    {
        description.SetActive(false);
        base.Close();
    }

    protected override void selectObject(int uiCount)
    {
        FrameUIManager.Instance.UpdateMessageText("", "何を持っていく？");

        base.selectObject(uiCount);

        //アイテムの説明更新
        if (uiCount + slideNow * columnCount < innerMax - 1)
        {
            this.description.SetActive(true);

            var description
                = objects[uiCount + slideNow * columnCount].Name + "\n"
                + objects[uiCount + slideNow * columnCount].Description;
            this.description.transform.GetChild(0).GetComponent<Text>().text = description;
        }

        if (uiCount + slideNow * columnCount == innerMax - 1)
        {
            description.SetActive(false);
            description.transform.GetChild(0).GetComponent<Text>().text = "";
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
