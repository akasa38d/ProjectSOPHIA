using UnityEngine;

public class AbstractTextAct : AbstractTownAct
{
    protected TextData textData;
    protected TextData.Type textType { get { return textData.TextType; } }
    protected int textIterator = 0;

    public AbstractTextAct(Exec exec)
    {
        ReturnAct = exec;
    }
    public AbstractTextAct(string fileName, Exec exec)
    {
        var textFile = Resources.Load(fileName).ToString();
        textData = new TextData(textFile);
        ReturnAct = exec;
    }
}
