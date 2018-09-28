using UnityEngine;

public class AbstractTextAct : AbstractTownAct
{
    protected TextData textData;
    protected TextData.Type textType { get { return textData.TextType; } }

    protected int textIterator = 0;

    protected UIPrefabsSet prefabsSet;    

    public AbstractTextAct() { }
    public AbstractTextAct(string textFile) { }
    public AbstractTextAct(string textFile, UIPrefabsSet buttonSet) { }
}
