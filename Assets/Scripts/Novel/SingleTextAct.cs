using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleTextAct : AbstractTextAct
{
    List<TextBox> textChain;

    //リストに残るもの
    public SingleTextAct(string textFile, Exec exec)
    {
        var text = new TextData(textFile);
        textChain = text.TextBoxChain;
        ReturnAct = exec;
    }
    //一時的なもの
    public SingleTextAct(List<TextBox> chain, Exec exec)
    {
        Name = "ProxyText";
        textChain = chain;
        ReturnAct = exec;
    }

    public override void StartUp()
    {
        base.StartUp();

        //イテレータ―の初期化
        textIterator = 0;

        Controller.Instance.SetState(Name, Update);

        if (textChain.Count == 0)
        {
            Close();
        }
        else
        {
            openTextBox(textIterator);
        }
    }

    void openTextBox(int iterator)
    {
        AdvUIManager.Instance.UpdateMessageText(textChain[iterator].Person, textChain[iterator].Message);
        //画像変更や音声系の処理もここに入れる
        textIterator++;
    }

    public override void Close()
    {
        ReturnAct();
    }

    public override void Update()
    {
        //決定
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (textIterator > textChain.Count - 1)
            {
                Close();
            }
            else
            {
                openTextBox(textIterator);
            }
        }
    }
}
