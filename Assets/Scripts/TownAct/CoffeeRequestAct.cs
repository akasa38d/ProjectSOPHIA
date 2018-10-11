using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MyUtility;

public class CoffeeRequestAct : AbstractSecondTownAct<RunnerData>
{
    GameObject description { get { return AdvPartManager.Instance.GetDescription; } }

    public CoffeeRequestAct(VerticalSelect _select, Exec exec) : base(_select, exec) { }

    protected override void loadObjects()
    {
        objects = new List<RunnerData>();
        objects.Add(new RunnerData(0, 0));
        objects.Add(new RunnerData(1, 0));
        objects.Add(new RunnerData(2, 0));
        objects.Add(new RunnerData(3, 0));
        objects.Add(new RunnerData(4, 0));
    }

    public override void StartUp() { base.StartUp(); }

    protected override void layoutObjects()
    {
        base.layoutObjects();

        for (int i = 0; i < objects.Count + 1; i++)
        {
            select.Buttons[i].SetActive(true);
            if (i < objects.Count)
            {
                select.Buttons[i].transform.GetChild(0).GetComponent<Text>().text = objects[i].Name;
                select.Buttons[i].transform.GetChild(1).GetComponent<Text>().text = objects[i].Price.ToStringMoney() + " M";
            }
            else if (i == objects.Count)
            {
                select.Buttons[i].transform.GetChild(0).GetComponent<Text>().text = "キャンセル";
                select.Buttons[i].transform.GetChild(1).GetComponent<Text>().text = "";
            }
        }
    }

    public override void Close() { base.Close(); }

    protected override void selectObject(int count)
    {
        FrameUIManager.Instance.UpdateMessageText("", "誰を雇う？");

        base.selectObject(count);

        //アイテムの説明更新
        if (singleIterator.Num < objects.Count)
        {
            description.SetActive(true);

            description.transform.GetChild(0).GetComponent<Text>().text
                = "LV:" + objects[count].LV + "\n"
                + "評判:" + objects[count].Power + "\n"
                + objects[count].Description;
        }

        if (singleIterator.Num == objects.Count)
        {
            description.SetActive(false);
            description.transform.GetChild(0).GetComponent<Text>().text = "";
        }
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
