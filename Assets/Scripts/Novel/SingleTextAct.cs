using System.Collections.Generic;
using UnityEngine;

public class SingleTextAct : AbstractTextAct
{
    List<TextBox> textChain;

    //リストに残るもの
    public SingleTextAct(string fileName, Exec exec) : base(fileName, exec)
    {
        textChain = textData.TextBoxChain;
    }
    //一時的なもの
    public SingleTextAct(List<TextBox> chain, Exec exec) : base(exec)
    {
        textChain = chain;
    }

    public override void StartUp()
    {
        base.StartUp();

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
        ReturnAct();
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
