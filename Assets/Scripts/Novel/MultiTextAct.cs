using UnityEngine;
using UnityEngine.UI;

public class MultiTextAct : AbstractTextAct
{
    SingleIterator personIterator;

    int groupNumber { get { return textData.GroupNumber; } }    //(現状最大３)

    public MultiTextAct(string textFile, UIPrefabsSet pSet, Exec exec)
    {
        //オブジェクトの設定
        prefabsSet = pSet;

        //テキストのロード
        textData = new TextData(textFile);

        ReturnAct = exec;
    }

    public override void StartUp()
    {
        base.StartUp();

        //イテレータ―の初期化
        textIterator = 0;
        personIterator = new SingleIterator(groupNumber + 1);

        //UIの初期化
        layoutPeople();

        //UI初期位置
        selectPeople(personIterator.Count);
    }

    public override void Close()
    {
        //メッセージを消す
        FrameUIManager.Instance.UpdateMessageText("", "");

        //UIを消す
        closePeople();

        //返還
        ReturnAct();
    }

    void openTextBox(int iterator, int person)
    {
        if (textIterator == 0) { closePeople(); }

        FrameUIManager.Instance.UpdateMessageText(textData.GroupChains[person].TextBoxChain[iterator].Person, textData.GroupChains[person].TextBoxChain[iterator].Message);
        //画像変更や音声系の処理もここに入れる
        textIterator++;
    }

    void layoutPeople()
    {
        foreach (var n in prefabsSet.Buttons)
        {
            n.SetActive(false);
            n.transform.localScale = new Vector3(1, 1, 1);
        }

        for (int i = 0; i < groupNumber + 1; i++)
        {
            prefabsSet.Buttons[i].SetActive(true);
            if (i < groupNumber)
            {
                prefabsSet.Buttons[i].transform.GetChild(0).GetComponent<Text>().text = textData.GroupName[i];
            }
            else if (i == groupNumber)
            {
                prefabsSet.Buttons[i].transform.GetChild(0).GetComponent<Text>().text = "キャンセル";
            }
        }
    }

    void selectPeople(int number)
    {
        textIterator = 0;
        FrameUIManager.Instance.UpdateMessageText("", "誰の話を聞く？");

        for (int i = 0; i < groupNumber + 1; i++)
        {
            if (i == number)
            {
                prefabsSet.Buttons[i].GetComponent<Animator>().SetTrigger("IsSelect");
            }
            else if (prefabsSet.Buttons[i].activeSelf == true)
            {
                prefabsSet.Buttons[i].GetComponent<Animator>().SetBool("IsSelect", false);
            }
        }
    }

    void closePeople()
    {
        foreach (var a in prefabsSet.Buttons)
        {
            a.transform.localScale = new Vector3(1, 1, 1);
            a.SetActive(false);
        }
    }

    public override void Update()
    {
        //複数人から選択
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            personIterator.Count++;
            selectPeople(personIterator.Count);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            personIterator.Count--;
            selectPeople(personIterator.Count);
        }

        //決定
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (personIterator.Count == groupNumber)
            {
                Close();
            }
            else
            {
                if (textIterator > textData.GroupChains[personIterator.Count].TextBoxChain.Count - 1)
                {
                    layoutPeople();
                    selectPeople(personIterator.Count);
                }
                else
                {
                    openTextBox(textIterator, personIterator.Count);
                }
            }
        }

        //キャンセル
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (textIterator == 0)
            {
                Close();
            }
        }

        //マウス操作・バックボタン作りたい
        if (Input.GetMouseButtonDown(0))
        {
            if (textIterator > 0)
            {
                if (textIterator > textData.GroupChains[personIterator.Count].TextBoxChain.Count - 1)
                {
                    layoutPeople();
                    selectPeople(personIterator.Count);
                }
                else
                {
                    openTextBox(textIterator, personIterator.Count);
                }
            }
        }

        for (int i = 0; i < groupNumber; i++)
        {
            if (Controller.Instance.CatchButtonsEnter[i] == true)
            {
                personIterator.Count = i;
                selectPeople(i);
            }

            if (Controller.Instance.CatchButtonsDown[i] == true)
            {
                personIterator.Count = i;
                selectPeople(i);
                openTextBox(textIterator, personIterator.Count);
            }
        }
    }
}
