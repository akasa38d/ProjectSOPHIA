using UnityEngine;
using UnityEngine.UI;

public class MultiTextAct : AbstractTownAct
{
    protected TextData textData;
    protected TextData.Type textType { get { return textData.TextType; } }
    int textIterator = 0;

    SingleIterator personIterator;

    SelectLayout select;

    int groupCount { get { return textData.GroupCount; } }    //(現状最大３)

    public MultiTextAct(string fileName, Exec exec, SelectLayout _select)
    {
        var textFile = Resources.Load(fileName).ToString();
        textData = new TextData(textFile);
        returnAct = exec;
        select = _select;
    }

    public override void StartUp()
    {
        //イテレータ―の初期化
        textIterator = 0;
        personIterator = new SingleIterator(groupCount + 1);

        //UIの初期化
        layoutPeople();

        //UI初期位置
        selectPeople(personIterator.Num);
    }

    public override void Close()
    {
        //メッセージを消す
        FrameUIManager.Instance.UpdateMessageText("", "");

        //UIを消す
        select.Close();

        //返還
        returnAct();
    }

    void openTextBox(int iterator, int person)
    {
        if (textIterator == 0) { select.Close(); }

        FrameUIManager.Instance.UpdateMessageText(textData.GroupChains[person].TextBoxChain[iterator].Person, textData.GroupChains[person].TextBoxChain[iterator].Message);
        //画像変更や音声系の処理もここに入れる
        textIterator++;
    }

    void layoutPeople()
    {
        select.Close();

        for (int i = 0; i < groupCount + 1; i++)
        {
            select.Buttons[i].SetActive(true);
            if (i < groupCount)
            {
                select.Buttons[i].transform.GetChild(0).GetComponent<Text>().text = textData.GroupName[i];
            }
            else if (i == groupCount)
            {
                select.Buttons[i].transform.GetChild(0).GetComponent<Text>().text = "キャンセル";
            }
        }
    }

    void selectPeople(int number)
    {
        textIterator = 0;
        FrameUIManager.Instance.UpdateMessageText("", "誰の話を聞く？");

        for (int i = 0; i < groupCount + 1; i++)
        {
            if (i == number)
            {
                select.Buttons[i].GetComponent<Animator>().SetTrigger("IsSelect");
            }
            else if (select.Buttons[i].activeSelf == true)
            {
                select.Buttons[i].GetComponent<Animator>().SetBool("IsSelect", false);
            }
        }
    }

    public override void Update()
    {
        //複数人から選択
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            personIterator.Num++;
            selectPeople(personIterator.Num);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            personIterator.Num--;
            selectPeople(personIterator.Num);
        }

        //決定
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (personIterator.Num == groupCount)
            {
                Close();
            }
            else
            {
                if (textIterator > textData.GroupChains[personIterator.Num].TextBoxChain.Count - 1)
                {
                    layoutPeople();
                    selectPeople(personIterator.Num);
                }
                else
                {
                    openTextBox(textIterator, personIterator.Num);
                }
            }
        }

        //キャンセル
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (textIterator == 0) { Close(); }
        }

        //マウス操作・バックボタン作りたい
        if (Input.GetMouseButtonDown(0))
        {
            if (textIterator > 0)
            {
                if (textIterator > textData.GroupChains[personIterator.Num].TextBoxChain.Count - 1)
                {
                    layoutPeople();
                    selectPeople(personIterator.Num);
                }
                else
                {
                    openTextBox(textIterator, personIterator.Num);
                }
            }
        }

        for (int i = 0; i < groupCount; i++)
        {
            if (Controller.Instance.CatchButtonsEnter[i] == true)
            {
                personIterator.Num = i;
                selectPeople(i);
            }

            if (Controller.Instance.CatchButtonsDown[i] == true)
            {
                personIterator.Num = i;
                selectPeople(i);
                openTextBox(textIterator, personIterator.Num);
            }
        }
    }
}
