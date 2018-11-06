using System;
using System.Collections.Generic;
using UnityEngine;

public class SingleTextAct : AbstractTownAct
{
    List<TextBox> textChain;
    int textIterator = 0;

    //リストに残るもの
    public SingleTextAct(string fileName, Exec exec)
    {
        var textFile = Resources.Load(fileName).ToString();
        textChain = new TextData(textFile).TextBoxChain;
        returnAct = exec;
    }

    //一時的なもの
    public SingleTextAct(List<TextBox> chain, Exec exec)
    {
        textChain = chain;
        returnAct = exec;        
    }

    public override void StartUp()
    {
        AdvPartManager.Instance.CurrentAct = this;

        textIterator = 0;

        if (textChain.Count == 0) { Close(); }
        else { openTextBox(textIterator); }
    }

    void openTextBox(int iterator)
    {
        FrameUIManager.Instance.UpdateMessageText(textChain[iterator].Person, textChain[iterator].Message);
        //画像変更や音声系の処理もここに入れる
        textIterator++;
    }

    public override void Close()
    {
        returnAct();
    }

    public override void Update()
    {
        //決定
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (textIterator > textChain.Count - 1) { Close(); }
            else { openTextBox(textIterator); }
        }
    }
}
