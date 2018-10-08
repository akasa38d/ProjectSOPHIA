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
        selectPeople(personIterator.Num);
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
            if (personIterator.Num == groupNumber)
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

        for (int i = 0; i < groupNumber; i++)
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
